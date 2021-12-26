Public Class chat_Utilities
    Public Shared Function BytesToString(ByVal data() As Byte) As String
        'Dim enc As New System.Text.UTF8Encoding()
        'BytesToString = enc.GetString(data)
        Return System.Text.UTF8Encoding.UTF8.GetString(data)
    End Function

    Public Shared Function StrToByteArray(ByVal text As String) As Byte()
        'Dim encoding As New System.Text.UTF8Encoding()
        'StrToByteArray = encoding.GetBytes(text)
        Return System.Text.UTF8Encoding.UTF8.GetBytes(text)
    End Function

    Public Class LargeArrayTransferHelper
        Private client As Swebex_Server.chat_Client
        Private server As Swebex_Server.chat_Server
        Private isServer As Boolean
        Private incommingMessages As List(Of IncomingMessage)
        Private serverCallback As Swebex_Server.chat_Server.ServerCallbackDelegate
        Private clientCallback As Swebex_Server.chat_Client.ClientCallbackDelegate
        Private signatureBytes() As Byte
        Private incommingMessageLock As New Object

        Private Class IncomingMessage
            Public channel As Byte
            Public sessionId As Integer
            Public length As Integer
            Public bytes As System.IO.MemoryStream
        End Class

        Sub New(ByRef _client As Swebex_Server.chat_Client)
            client = _client
            clientCallback = client.ClientCallbackObject
            isServer = False
            Init()
        End Sub

        Sub New(ByRef _server As Swebex_Server.chat_Server)
            server = _server
            isServer = True
            serverCallback = server.ServerCallbackObject
            Init()
        End Sub

        Private Sub Init()
            incommingMessages = New List(Of IncomingMessage)
            signatureBytes = StrToByteArray("<utility=LargeArrayTransferHelperV1.0")
        End Sub

        Private Function VerifySignature(ByRef bytes() As Byte) As Boolean
            If bytes.Length < signatureBytes.Length Then Return False

            Dim verifyed As Boolean = True
            For count As Int32 = 0 To signatureBytes.Length - 1
                If bytes(count) <> signatureBytes(count) Then
                    verifyed = False
                    Exit For
                End If
            Next

            Return verifyed
        End Function

        ''' <summary>
        ''' Put this at the top of your callback, in a if statement. If it eveluates to true, then call return (the bytes were handled by this method). This method will
        ''' eveluate all incoming packets within the channelrange, and assemble any large arrays sent. When one is complete, it will call the callback itself for you,
        ''' and hand you the completed large array.
        ''' Ie:
        ''' 
        ''' If lat.HandleIncomingBytes(bytes, dataChannel) then Return
        ''' 
        ''' </summary>
        ''' <param name="bytes">The bytes supplied by your callback.</param>
        ''' <param name="channel">The channel supplied by your callback.</param>
        ''' <param name="sessionId">The sessionId supplied by your callback - obviously just for servers.</param>
        ''' <param name="channelRange">This byte array should contain two elements. The first is the lowest chanel this function should evaluate, the second is the highest. 
        ''' Leave it blank to eveluate all valid channels. However, not specifying a channelRange may slow down comunications.</param>
        ''' <returns>A boolean value indication weather or not this incoming packet was handled by this function.</returns>
        ''' <remarks></remarks>
        Public Function HandleIncomingBytes(ByVal bytes As Byte(), ByVal channel As Byte, Optional ByVal sessionId As Integer = -1, Optional ByVal channelRange() As Byte = Nothing) As Boolean

            If channelRange IsNot Nothing Then
                ' Is channelRange valid?
                If channelRange.Count <> 2 Then
                    MsgBox("When using TcpComm.LargeArrayTransferHelper.HandleIncomingBytes(), channelRange must be a byte array that has two elements. See the xml parameter information for details.", _
                           MsgBoxStyle.Critical, "TcpComm.LargeArrayTransferHelper error")
                    Return False
                End If

                ' Are the channels supplied valid?
                If channelRange(0) < 1 Or channelRange(1) > 251 Then
                    MsgBox("When using TcpComm.LargeArrayTransferHelper.HandleIncomingBytes(), channelRange must be a byte array that has two elements. See the xml parameter information for details.", _
                           MsgBoxStyle.Critical, "TcpComm.LargeArrayTransferHelper error")
                    Return False
                End If

                ' Are we eveluating data on this channel?
                If channelRange IsNot Nothing And channel < channelRange(0) Or channel > channelRange(1) Then Return False
            End If

            ' Check to see if we're receiving an incoming large array header:
            If VerifySignature(bytes) Then
                Dim msg As String = BytesToString(bytes)
                Dim msgParts() As String
                Dim length As Integer = 0
                Dim incomingChanel As Byte

                ' Get the incoming array size and channel from the lat helper header:
                msg = msg.Replace("<utility=LargeArrayTransferHelperV1.0", "")
                msg = msg.Replace(">", "")
                msgParts = msg.Split(",")

                For Each part As String In msgParts
                    If part.Contains("arraysize=") Then
                        part = part.Replace("arraysize=", "")
                        length = Convert.ToInt32(part)
                    End If

                    If part.Contains("channel=") Then
                        part = part.Replace("channel=", "")
                        incomingChanel = Convert.ToByte(part)
                    End If
                Next

                ' Add this large array job to our incommingMessages list:
                SyncLock incommingMessageLock
                    incommingMessages.Add(New IncomingMessage With { _
                                .channel = incomingChanel,
                                .bytes = New System.IO.MemoryStream(length),
                                .length = length,
                                .sessionId = sessionId
                                })
                End SyncLock

                ' Return true so we know not to
                ' process this in the callback.
                Return True
            End If

            SyncLock incommingMessageLock
                ' It's not a LAT header. Are we handling this packet?
                If incommingMessages.Count = 0 Then Return False
                Dim removeThis As IncomingMessage = Nothing
                Dim handledThis As Boolean = False

                ' Search our list of incoming large arrays to see if we should handle this message
                For Each message As IncomingMessage In incommingMessages
                    If message.channel = channel And message.sessionId = sessionId Then
                        handledThis = True
                        message.bytes.Write(bytes, 0, bytes.Length)

                        ' Are we finished?
                        If message.length = message.bytes.Length Then
                            ' Do a callback from a background thread to avoid a deadlock.
                            Dim doCallback As New Threading.Thread(AddressOf bgCallback)
                            doCallback.Start(message)

                            removeThis = message
                            Exit For
                        End If
                    End If
                Next

                If removeThis IsNot Nothing Then incommingMessages.Remove(removeThis)
                If handledThis Then Return True
                Return False
            End SyncLock

        End Function

        Private Sub bgCallback(ByVal msg As Object)
            Dim message As IncomingMessage = CType(msg, IncomingMessage)
            If isServer Then
                serverCallback(message.bytes.ToArray().Clone(), message.sessionId, message.channel)
            Else
                clientCallback(message.bytes.ToArray().Clone(), message.channel)
            End If
        End Sub

        Public Function SendArray(ByVal bytes As Byte(), ByVal channel As Byte, Optional ByRef errMsg As String = "") As Boolean
            Return SendArray(bytes, channel, -1, errMsg)
        End Function

        Public Function SendArray(ByVal bytes As Byte(), ByVal channel As Byte, ByVal sessionId As Integer, ByRef errMsg As String) As Boolean
            Dim header As String = String.Format("<utility=LargeArrayTransferHelperV1.0,arraysize={0},channel={1}>", _
                                                 bytes.Length.ToString(), channel.ToString())

            If isServer Then
                If sessionId < 0 Then
                    errMsg = sessionId.ToString & " is an invalid value for sessionId. To send a large array from the server, you must specify a sessionId."
                    Return False
                End If

                If channel < 1 Or channel > 251 Then
                    errMsg = channel.ToString & " is an invalid channel."
                    Return False
                End If

                If Not server.SendText(header, channel, sessionId, errMsg) Then Return False
                If Not server.SendBytes(bytes, channel, sessionId, errMsg) Then Return False
            Else
                If channel < 1 Or channel > 251 Then
                    errMsg = channel.ToString & " is an invalid channel."
                    Return False
                End If

                If Not client.SendText(header, channel, errMsg) Then Return False
                If Not client.SendBytes(bytes, channel, errMsg) Then Return False
            End If

            Return True
        End Function
    End Class
End Class
