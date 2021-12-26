'Imports System.Net.Sockets
'Imports System.Net
'Imports System.IO
'Imports System.Collections.Concurrent
'Imports System.Threading

Public Class chat_Server
    Public errMsg As String

    ' Define the callback delegate type
    Public Delegate Sub ServerCallbackDelegate(ByVal bytes() As Byte, ByVal sessionID As Int32, ByVal dataChannel As Byte)

    ' Create Delegate object
    Public ServerCallbackObject As ServerCallbackDelegate

    Private Listener As TcpListener
    Private continue_running As Boolean = False
    Private blockSize As UInt16
    Private Port As Integer
    Private localAddr As IPAddress
    Private Mbps As UInt32
    Private newSessionId As Int32 = 0
    Public IsRunning As Boolean = False
    Private serverState As currentState = currentState.stopped
    Private enforceUniqueMachineIds As Boolean
    Private SessionCollection As New Sessions
    Private SessionCollectionLocker As New Object

    Public Class message
        Public bytes() As Byte
        Public dataChannel As Byte
        Public sessionID As Int32
    End Class

    Private Enum currentState
        err = -1
        stopped = 0
        running = 1
        idle = 2
    End Enum

    Private Class Sessions
        Private sessionCollection As New List(Of SessionCommunications)
        Private sessionLockObject As New Object
        Private reusableSessions As New Concurrent.ConcurrentQueue(Of Int32)
        Private newSessions As New Concurrent.ConcurrentQueue(Of TcpClient)
        Private newSessionId As Int32 = 0

        Public Function VerifyAndChangeUniqueMachineId(ByRef session As SessionCommunications, ByVal NewMachineId As String) As Boolean

            Dim machineIdIsUnique As Boolean = True

            SyncLock sessionLockObject

                For count = 0 To sessionCollection.Count - 1
                    If sessionCollection.Item(count).machineId = NewMachineId And sessionCollection.Item(count).machineIdValidated Then
                        machineIdIsUnique = False
                        Exit For
                    End If
                Next

                If machineIdIsUnique = True Then
                    session.machineId = NewMachineId
                    session.machineIdValidated = True
                End If

            End SyncLock

            Return machineIdIsUnique
        End Function

        Public Sub AddSession(ByRef client As TcpClient, ByRef runThread As Thread, ByVal enforceUinqueIds As Boolean)

            SyncLock sessionLockObject
                Dim id As Int32 = GetReusableSessionID()
                Dim session As SessionCommunications = Nothing

                If id > -1 Then
                    session = sessionCollection.Item(id)
                    'session.Close()
                    session.IsRunning = False
                    session.shuttingDown = True
                    session.machineId = ""
                    session.machineIdValidated = False
                    session.enforceUniqueMachineIds = enforceUinqueIds

                    Try
                        If session.theClient.Client IsNot Nothing Then session.theClient.Client.Dispose()
                    Catch ex As Exception
                    End Try

                    session.theClient = client
                    session.disConnect = False
                    session.shuttingDown = False

                    'session = New SessionCommunications(client, id)
                    'session.enforceUniqueMachineIds = enforceUinqueIds    
                    'session.machineId = ""
                    'session.machineIdValidated = False

                    'sessionCollection.Item(id) = Nothing
                    'sessionCollection.Item(id) = session

                Else
                    id = newSessionId
                    newSessionId += 1
                    session = New SessionCommunications(client, id)
                    sessionCollection.Add(session)
                End If

                If enforceUinqueIds = False Then session.machineIdValidated = True
                runThread.Name = "Server session #" & id.ToString()
                runThread.Start(session)

            End SyncLock

        End Sub

        Public Sub CloseSession(ByRef session As SessionCommunications)
            SyncLock sessionLockObject
                session.IsRunning = False
                session.machineId = ""
                session.machineIdValidated = False
                ReuseSessionNumber(session.sessionID)
            End SyncLock
        End Sub

        Public Sub AddSession(ByVal theNewSession As SessionCommunications)
            SyncLock sessionLockObject
                If sessionCollection.Count > theNewSession.sessionID Then
                    sessionCollection.Item(theNewSession.sessionID) = Nothing
                    sessionCollection.Item(theNewSession.sessionID) = theNewSession
                Else
                    sessionCollection.Add(theNewSession)
                End If
            End SyncLock
        End Sub

        Public Function GetReusableSessionID() As Int32
            Dim sessionNumber As Int32 = -1

            If reusableSessions.TryDequeue(sessionNumber) Then
                Return sessionNumber
            End If

            Return -1
        End Function

        Public Sub ReuseSessionNumber(ByVal sessionNumber As Int32)
            reusableSessions.Enqueue(sessionNumber)
        End Sub

        Public Function GetSession(ByVal sessionID As Int32, ByRef session As SessionCommunications) As Boolean
            Try
                If sessionCollection.Item(sessionID).machineIdValidated = False Then Return False

                session = sessionCollection.Item(sessionID)
                If session.machineIdValidated = False Then Return False
                If session Is Nothing Then Return False
                If Not session.IsRunning Then Return False
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetSession(ByVal MachineID As String, ByRef session As SessionCommunications) As Boolean
            session = Nothing

            SyncLock sessionLockObject
                For Each connectedSession In sessionCollection
                    If connectedSession.IsRunning And connectedSession.machineId = MachineID And connectedSession.machineIdValidated = True Then
                        session = connectedSession
                        Exit For
                    End If
                Next
            End SyncLock

            If session Is Nothing Then Return False
            Return True
        End Function

        Public Function RemoveSession(ByRef session As SessionCommunications) As Boolean
            Dim retVal As Boolean = True

            SyncLock sessionLockObject
                Try
                    sessionCollection.Remove(session)
                Catch ex As Exception
                    retVal = False
                End Try
            End SyncLock

            Return retVal
        End Function

        Public Sub Broadcast(ByVal msg As message)
            Dim thisCopy As New List(Of SessionCommunications)

            SyncLock sessionLockObject
                For i As Int32 = 0 To sessionCollection.Count - 1
                    thisCopy.Add(sessionCollection.Item(i))
                Next
            End SyncLock

            For i As Int32 = 0 To thisCopy.Count - 1
                If thisCopy.Item(i) IsNot Nothing AndAlso thisCopy.Item(i).IsRunning Then
                    Try
                        thisCopy.Item(i).sendQueue.Enqueue(msg)
                    Catch ex As Exception
                    End Try
                End If
            Next

        End Sub

        Public Function GetSessionCollection() As List(Of SessionCommunications)
            Dim thisCopy As New List(Of SessionCommunications)

            SyncLock sessionLockObject
                For i As Int32 = 0 To sessionCollection.Count - 1
                    If sessionCollection.Item(i).machineIdValidated = True Then thisCopy.Add(sessionCollection.Item(i))
                Next
            End SyncLock

            Return thisCopy
        End Function

        Public Sub ShutDown()
            SyncLock sessionLockObject
                For Each session As SessionCommunications In sessionCollection
                    Try
                        If session IsNot Nothing Then session.Close()
                    Catch ex As Exception
                    End Try
                Next
            End SyncLock
        End Sub
    End Class

    Public Class SessionCommunications
        Public UserBytesToBeSentAvailable As Boolean = False
        Public UserBytesToBeSent As New MemoryStream
        Public UserOutputChannel As Byte
        Public SystemBytesToBeSentAvailable As Boolean = False
        Public SystemBytesToBeSent() As Byte
        Public SystemOutputChannel As Byte
        Public theClient As TcpClient
        Public IsRunning As Boolean = False
        Public remoteIpAddress As System.Net.IPAddress
        Public bytesRecieved() As Byte
        Public sessionID As Int32
        Public disConnect As Boolean = False
        Public bytesSentThisSecond As Int32 = 0
        Public bytesRecievedThisSecond As Int32 = 0
        Public fileBytesRecieved As Int64 = 0
        Public filebytesSent As Int64 = 0
        Public SendingFile As Boolean = False
        Public FileBeingSentPath As String
        Public IncomingFileSize As Int64
        Public IncomingFileName As String
        Public ReceivingFile As Boolean = False
        Public sendPacketSize As Boolean = False
        Public fileReader As FileStream
        Public fileWriter As chat_AsyncUnbuffWriter
        Public ReceivedFilesFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\ServerReceivedFiles"
        Public userName As String
        Public password As String
        Public paused As Boolean
        Public pauseSent As Boolean
        Public sendQueue As ConcurrentQueue(Of message)
        Public messageIn As MessageInQueue
        Public machineId As String
        Public shuttingDown As Boolean
        Public enforceUniqueMachineIds As Boolean
        Public machineIdValidated As Boolean

        Public Sub SendErrorMessage(ByVal message As String)
            message = "ERR: " & message

            If sendQueue Is Nothing Then sendQueue = New ConcurrentQueue(Of message)

            sendQueue.Enqueue(New message With { _
                              .bytes = chat_Utilities.StrToByteArray(message),
                              .dataChannel = 254,
                              .sessionID = sessionID
                          })

        End Sub

        Public Sub QueueSystemMessage(ByVal message As String)

            If sendQueue Is Nothing Then sendQueue = New ConcurrentQueue(Of message)

            sendQueue.Enqueue(New message With { _
                              .bytes = chat_Utilities.StrToByteArray(message),
                              .dataChannel = 254,
                              .sessionID = sessionID
                          })

        End Sub

        Public Class MessageInQueue
            Public queue As New ConcurrentQueue(Of message)
            Private bgThread As New Threading.Thread(AddressOf Pump)
            Private running As Boolean
            Private callBack As ServerCallbackDelegate

            Public Sub New(ByRef _callBack As ServerCallbackDelegate)
                callBack = _callBack
                running = True

                bgThread.IsBackground = True
                bgThread.Start()
            End Sub

            Public Sub Close()
                running = False
            End Sub

            Private Sub Pump()
                Dim msg As message = Nothing

                While running
                    If queue.Count > 0 Then
                        If queue.TryDequeue(msg) Then
                            callBack(msg.bytes, msg.sessionID, msg.dataChannel)
                        End If
                    End If

                    If queue.Count = 0 Then Thread.Sleep(2)
                End While
            End Sub

        End Class

        Public Sub New(ByVal _theClient As TcpClient, ByVal _sessionID As Int32, Optional ByVal uniqueMachineIds As Boolean = True)
            theClient = _theClient
            sessionID = _sessionID
            paused = False
            pauseSent = False
            shuttingDown = False
            enforceUniqueMachineIds = uniqueMachineIds
            machineIdValidated = False

        End Sub

        Public Sub Close(Optional ByVal secondsToWaitForSendQueueToEmpty As Int32 = 3, Optional ByVal closeInBackground As Boolean = False)

            If closeInBackground = True Then
                If messageIn IsNot Nothing Then messageIn.Close()
                Dim bgThread As New Thread(AddressOf WaitClose)
                bgThread.Start(New TimeSpan(0, 0, secondsToWaitForSendQueueToEmpty))
                Return
            End If

            If messageIn IsNot Nothing Then messageIn.Close()

            Dim emptySendQueueTimeout = Now.Add(New TimeSpan(0, 0, secondsToWaitForSendQueueToEmpty))
            shuttingDown = True

            Try
                While (sendQueue.Count > 0) Or (UserBytesToBeSentAvailable = True)
                    Thread.Sleep(5)
                    If Now > emptySendQueueTimeout Then Exit While
                End While
            Catch ex As Exception
                ' sendQueue is nothing... not interested in this error.
            End Try

            disConnect = True
        End Sub

        Private Sub WaitClose(ByVal o As Object)
            Dim emptySendQueueTimeout = Now.Add(CType(o, TimeSpan))
            shuttingDown = True

            Try
                While (sendQueue.Count > 0) Or (UserBytesToBeSentAvailable = True)
                    Thread.Sleep(5)
                    If Now > emptySendQueueTimeout Then Exit While
                End While
            Catch ex As Exception
                ' sendQueue is nothing... not interested in this error.
            End Try

            Thread.Sleep(1000)

            disConnect = True
        End Sub

    End Class

    ''' <summary>
    ''' Returns a current copy of the server's internal list of sessions as a List(Of SessionCommunications). It is possible that some sessions may be inactive, 
    ''' or disconnected. Care should be taken to check the session.isRunning before using one,
    '''  because inactive or disconnected sessions may be overwritten by new connections at any moment. 
    ''' </summary>
    ''' <returns>List(Of SessionCommunications)</returns>
    ''' <remarks></remarks>
    Public Function GetSessionCollection() As List(Of SessionCommunications)
        Dim thisCollection As List(Of SessionCommunications) = SessionCollection.GetSessionCollection()
        Return thisCollection
    End Function

    ''' <summary>
    ''' Gets the session object associated with the sessionId. Returns Nothing for sessions where session.isRunning = False.
    ''' </summary>
    ''' <param name="sessionId"></param>
    ''' <returns>A TcpComm.Server.SessionCommunications object</returns>
    ''' <remarks></remarks>
    Public Function GetSession(ByVal sessionId As Int32) As SessionCommunications
        Dim theSession As SessionCommunications = Nothing

        ' Sessions that are not running are not returned, so that they're sendqueues are not
        ' accidently inflated.
        If SessionCollection.GetSession(sessionId, theSession) Then Return theSession
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the first session object associated with the MachineID. Returns Nothing for sessions where session.isRunning = False.
    ''' </summary>
    ''' <param name="aMachineID"></param>
    ''' <returns>A TcpComm.Server.SessionCommunications object</returns>
    ''' <remarks></remarks>
    Public Function GetSession(ByVal aMachineID As String) As SessionCommunications
        GetSession = Nothing
        SessionCollection.GetSession(aMachineID, GetSession)
        Return GetSession
    End Function

    ' CallbackForm must implement an UpdateUI Sub.
    Public Sub New(ByVal callbackMethod As ServerCallbackDelegate, Optional ByVal _throttledBytesPerSecond As UInt32 = 9000000, Optional ByVal enforceUniqueMachineId As Boolean = True)

        Mbps = _throttledBytesPerSecond
        enforceUniqueMachineIds = enforceUniqueMachineId

        ' BlockSize should be 62500 or 63100, depending on requested speed. 
        ' Excellent performance, and works great with throttling.
        Dim _blockSize As UInt16

        ' Get corrected blocksize for throttling.
        If Mbps < 300000 Then
            If Mbps > 16000 Then
                blockSize = 4000
            Else
                blockSize = CUShort((Mbps / 4))
            End If
        ElseIf Mbps > 300000 And Mbps < 500000 Then
            blockSize = 16000
        ElseIf Mbps > 500000 And Mbps < 1000000 Then
            blockSize = 32000
        Else
            Dim count As UInt32 = 0
            Dim aFourth As Decimal = 0

            If Mbps > 25000000 Then
                _blockSize = 63100
            Else
                _blockSize = 62500
            End If

            aFourth = CDec(Mbps / 4)

            Do
                count += _blockSize
                If (count + _blockSize) > aFourth Then
                    Mbps = CUInt(count * 4)
                    blockSize = _blockSize
                    Exit Do
                End If
            Loop

        End If

        ' Initialize the delegate object to point to the user's callback method.
        ServerCallbackObject = callbackMethod
    End Sub

    Public Sub ThrottleNetworkBps(ByVal bytesPerSecond As UInteger)
        ' Default value is 9000000 Mbps. Ok throughput, and 
        ' good performance for the server (low CPU usage).
        Mbps = bytesPerSecond
    End Sub

    ''' <summary>
    ''' This is a convienience function that handles the work of converting the text you would like to send to a byte array. 
    ''' Passes back the return value and errMsg of SendBytes(). Returns True on success and False on falure. Check the errMsg 
    ''' string for send failure explanations.
    ''' </summary>
    ''' <param name="textMessage"></param>
    ''' <param name="channel"></param>
    ''' <param name="sessionid"></param>
    ''' <param name="errMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendText(ByVal textMessage As String, Optional ByVal channel As Byte = 1, Optional ByVal sessionid As Int32 = -1, _
                             Optional ByRef errMsg As String = "") As Boolean
        Dim selected_client As String = Nothing
        Try
            For Each itm As ListViewItem In My.Forms.main_form.lv_chat_Client_list.SelectedItems
                If itm.Selected Then selected_client = itm.SubItems(2).Text.ToString
            Next
        Catch ex As Exception
        End Try


        If textMessage = "" Then
            errMsg = "Your text message must contain some text."
            Return False
        End If

        SendText = SendBytes(chat_Utilities.StrToByteArray(My.Forms.main_form.local_user_name + ": " + textMessage), channel, sessionid, errMsg)
        If SendText = True Then
            If InStr(textMessage, "<||>") > 0 Then
                My.Forms.main_form.temp_str = Split(textMessage, ">")
                My.Forms.main_form.rtb_chat_history.AppendText("-> " + selected_client + ": " + My.Forms.main_form.temp_str(0).Substring(0, My.Forms.main_form.temp_str(0).Length - 3) + " " + fn_translate("msg_sending_file") + " :")
                My.Forms.main_form.rtb_chat_history.SelectionColor = Color.Blue
                My.Forms.main_form.rtb_chat_history.SelectionFont = New Font(My.Forms.main_form.rtb_chat_history.SelectionFont, FontStyle.Underline)
                My.Forms.main_form.rtb_chat_history.AppendText("_" + My.Forms.main_form.temp_str(1) + "_" + vbNewLine)
                My.Forms.main_form.rtb_chat_history.SelectionColor = Color.Black
                My.Forms.main_form.rtb_chat_history.SelectionFont = New Font(My.Forms.main_form.rtb_chat_history.SelectionFont, FontStyle.Regular)
            Else
                My.Forms.main_form.rtb_chat_history.AppendText(CStr("-> " + selected_client + ": " + textMessage) + vbNewLine)
            End If

            If My.Forms.main_form.chb_logging_chat_messages.Checked = True Then fn_write_to_log(My.Forms.main_form.chat_log_file, CStr(My.Forms.main_form.local_user_name + "-> " + selected_client + ": " + textMessage))
        End If

        Return SendText
    End Function


    Public Function Start(ByVal prt As Integer, Optional ByRef errorMessage As String = "") As Boolean

        If serverState = currentState.running Then
            errorMessage = fn_translate("msg_server_is_running")
            Return False
        End If

        serverState = currentState.idle

        Dim listenerThread As New Thread(AddressOf theListener)

        Try
            Port = prt
            localAddr = GetLocalIpAddress()
            'localAddr = My.Forms.main_form.localAddr
            continue_running = True
            IsRunning = True

            listenerThread.Name = "Server Listener Thread"
            listenerThread.Start()
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try

        While serverState <> currentState.running
            Thread.Sleep(10)
            If serverState = currentState.err Or serverState = currentState.stopped Then
                errorMessage = errMsg
                Return False
            End If
        End While

        Return True
    End Function



    Public Sub Close()
        continue_running = False

        Try
            Listener.Stop()
        Catch ex As Exception
        End Try

        Try
            SessionCollection.ShutDown()
        Catch ex As Exception
        End Try

        IsRunning = False

        ServerCallbackObject(chat_Utilities.StrToByteArray("Server Stopped."), -1, 255)
        serverState = currentState.stopped
    End Sub

    Private Function GetLocalIpAddress() As System.Net.IPAddress
        Dim strHostName As String
        '        Dim addresses() As System.Net.IPAddress
        Dim addresses As System.Net.IPAddress

        strHostName = System.Net.Dns.GetHostName()
        'addresses = System.Net.Dns.GetHostAddresses(strHostName)

        ' Find an IpV4 address
        'For Each address As System.Net.IPAddress In addresses
        '    ' Return the first IpV4 IP Address we find in the list.
        '    If address.AddressFamily = AddressFamily.InterNetwork Then
        '        Return address
        '    End If
        'Next

        ' No IpV4 address? Return the loopback address.
        Return System.Net.IPAddress.Any
        'Return System.Net.IPAddress.Loopback
    End Function


    Public Function GetBlocksize() As UInt16
        Return blockSize
    End Function

    ''' <summary>
    ''' Returns the size of the selected session's sendqueue. Returns -1 if the session is nothing, or session.isRunning = False. 
    ''' CAUTION: Calling this function too often will result in decreased performance, and failing to call it at all may result
    ''' in an out of memory error. You can continue to add messages to a session's send queue for as long as the session is active 
    ''' (isRunning = True), but that doesn't mean they are being sent as fast as you are adding them to the queue (or at all, for that matter). 
    ''' </summary>
    ''' <param name="sessionId"></param>
    ''' <returns>An Int32</returns>
    ''' <remarks></remarks>
    Public Function GetSendQueueSize(ByVal sessionId As Int32) As Int32
        Dim sendQueueSize As Int32 = -1
        Dim session As SessionCommunications = Nothing

        If SessionCollection.GetSession(sessionId, session) Then
            If session IsNot Nothing AndAlso session.IsRunning Then
                GetSendQueueSize = session.sendQueue.Count
            End If
        End If

        Return sendQueueSize
    End Function


    Public Function GetFile(ByVal _path As String, ByVal sessionID As Int32) As Boolean

        Dim thisSession As SessionCommunications = Nothing
        If SessionCollection.GetSession(sessionID, thisSession) Then
            If thisSession Is Nothing Then Return False
            If Not thisSession.IsRunning Then Return False
            thisSession.sendQueue.Enqueue(New message With { _
                                      .bytes = chat_Utilities.StrToByteArray("GFR:" & _path),
                                      .sessionID = sessionID,
                                      .dataChannel = 254
                                  })
        Else
            Return False
        End If

        Return True
    End Function


    Public Function SendFile(ByVal _path As String, ByVal sessionID As Int32) As Boolean
        fn_copy_file(_path, IO.Path.Combine(My.Forms.main_form.FilesFolder, GetFilenameFromPath(_path)))

        Dim thisSession As SessionCommunications = Nothing
        If SessionCollection.GetSession(sessionID, thisSession) Then
            If thisSession Is Nothing Then Return False
            If Not thisSession.IsRunning Then Return False

            If thisSession.shuttingDown Then
                errMsg = "The session is shutting down, and will not accept any more outgoing messages."
                Return False
            End If

            thisSession.sendQueue.Enqueue(New message With { _
                                      .bytes = chat_Utilities.StrToByteArray("SFR:" & _path),
                                      .sessionID = sessionID,
                                      .dataChannel = 254
                                  })
        Else
            Return False
        End If

        Return True
    End Function



    Public Function SendBytes(ByVal bytes() As Byte, Optional ByVal channel As Byte = 1, Optional ByVal sessionID As Int32 = -1, _
                              Optional ByRef errMsg As String = "") As Boolean
        Dim foundSession As Boolean = False

        If channel = 0 Or channel > 250 Then
            errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
            Return False
        End If

        Dim msg As message = New message()
        ReDim msg.bytes(bytes.Length - 1)
        msg.dataChannel = channel
        msg.sessionID = sessionID

        Buffer.BlockCopy(bytes, 0, msg.bytes, 0, bytes.Length)

        If sessionID > -1 Then
            Dim targetSession As SessionCommunications = Nothing
            If SessionCollection.GetSession(sessionID, targetSession) Then
                If targetSession.shuttingDown Then
                    errMsg = "The session is shutting down, and will not accept any more outgoing messages."
                    Return False
                End If
                targetSession.sendQueue.Enqueue(msg)
                Return True
            End If
        Else
            SessionCollection.Broadcast(msg)
            Return True
        End If

        errMsg = "The session you are trying to write to is no longer available."
        Return False
    End Function

    Public Function SendBytes(ByRef bytes() As Byte, ByVal offset As Int32, ByVal count As Int32, Optional ByVal channel As Byte = 1, Optional ByVal sessionID As Int32 = -1, _
                              Optional ByRef errMsg As String = "") As Boolean
        Dim foundSession As Boolean = False

        If channel = 0 Or channel > 250 Then
            errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
            Return False
        End If

        Dim msg As New message()
        ReDim msg.bytes(count - 1)

        Buffer.BlockCopy(bytes, offset, msg.bytes, 0, count)
        msg.dataChannel = channel
        msg.sessionID = sessionID

        If sessionID > -1 Then
            Dim targetSession As SessionCommunications = Nothing
            If SessionCollection.GetSession(sessionID, targetSession) Then
                If targetSession.shuttingDown Then
                    errMsg = "The session is shutting down, and will not accept any more outgoing messages."
                    Return False
                End If
                targetSession.sendQueue.Enqueue(msg)
                Return True
            End If
        Else
            SessionCollection.Broadcast(msg)
            Return True
        End If

        errMsg = "The session you are trying to write to is no longer available."
        Return False
    End Function

    Public Function SendBytes(ByRef streamBytes As MemoryStream, Optional ByVal channel As Byte = 1, Optional ByVal sessionID As Int32 = -1, _
                              Optional ByRef errMsg As String = "") As Boolean
        Dim foundSession As Boolean = False

        If channel = 0 Or channel > 250 Then
            errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
            Return False
        End If

        Dim msg As message = New message()
        ReDim msg.bytes(streamBytes.Length - 1)
        msg.dataChannel = channel
        msg.sessionID = sessionID

        streamBytes.Position = 0
        streamBytes.Read(msg.bytes, 0, msg.bytes.Length)

        If sessionID > -1 Then
            Dim targetSession As SessionCommunications = Nothing
            If SessionCollection.GetSession(sessionID, targetSession) Then
                If targetSession.shuttingDown Then
                    errMsg = "The session is shutting down, and will not accept any more outgoing messages."
                    Return False
                End If
                targetSession.sendQueue.Enqueue(msg)
                Return True
            End If
        Else
            SessionCollection.Broadcast(msg)
            Return True
        End If

        errMsg = "The session you are trying to write to is no longer available."
        Return False
    End Function

    Private Function RcvBytes(ByVal data() As Byte, ByVal session As SessionCommunications, Optional ByVal dataChannel As Byte = 1) As Boolean
        ' dataType: >0 = data channel, > 250 = internal messages. 0 is an invalid channel number (it's the puck)

        If dataChannel < 1 Then
            RcvBytes = False
            Exit Function
        End If

        Try
            ' Check to see if our app is closing
            If Not continue_running Then Return False

            Dim passedData(data.Length - 1) As Byte
            Array.Copy(data, passedData, data.Length)

            If session IsNot Nothing Then
                session.messageIn.queue.Enqueue(New message With { _
                                                                .bytes = passedData,
                                                                .dataChannel = dataChannel,
                                                                .sessionID = session.sessionID
                                                            })
            Else
                ' These are internal system messages. There is no session associated with them
                ServerCallbackObject(data, -1, dataChannel)
            End If

        Catch ex As Exception

            ' An unexpected error.
            Debug.WriteLine("Unexpected error in server\RcvBytes: " & ex.Message)
            Return False
        End Try

        Return True
    End Function

    Private Function SendExternalSystemMessage(ByVal message As String, ByVal session As SessionCommunications) As Boolean

        session.SystemBytesToBeSent = chat_Utilities.StrToByteArray(message)
        session.SystemOutputChannel = 254 ' Text messages / commands on channel 254
        session.SystemBytesToBeSentAvailable = True

        Return True
    End Function

    Private Function CheckSessionPermissions(ByVal session As SessionCommunications, ByVal cmd As String) As Boolean
        ' Your security code here...

        Return True
    End Function

    Private Function BeginFileSend(ByVal _path As String, ByVal session As SessionCommunications, ByVal fileLength As Long) As Boolean

        Try

            session.fileReader = New FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None, chat_AsyncUnbuffWriter.GetPageSize)
            session.SendingFile = True
            BeginFileSend = True

        Catch ex As Exception
            BeginFileSend = False
            _path = ex.Message
            session.SendingFile = False
        End Try

        Try
            If Not BeginFileSend Then session.fileReader.Close()
        Catch ex As Exception
        End Try

    End Function

    Private Sub GetMoreFileBytesIfAvailable(ByVal session As SessionCommunications)
        Dim bytesRead As Int32 = 0

        If session.SendingFile And Not session.SystemBytesToBeSentAvailable Then
            Try
                If session.SystemBytesToBeSent.Length <> blockSize Then ReDim session.SystemBytesToBeSent(blockSize - 1)
                bytesRead = session.fileReader.Read(session.SystemBytesToBeSent, 0, blockSize)
                If bytesRead <> blockSize Then ReDim Preserve session.SystemBytesToBeSent(bytesRead - 1)

                If bytesRead > 0 Then
                    session.SystemOutputChannel = 253 ' File transfer from server to client
                    session.SystemBytesToBeSentAvailable = True
                Else

                    ReDim session.SystemBytesToBeSent(blockSize - 1)
                    SendExternalSystemMessage("->Done", session) ' Send the client a completion notice.
                    session.SendingFile = False

                    ' Clean up
                    session.fileReader.Close()
                    session.fileReader = Nothing
                    GC.GetTotalMemory(True)
                End If
            Catch ex As Exception
                SendExternalSystemMessage("ERR: " & ex.Message, session)

                ' We're finished.
                ReDim session.SystemBytesToBeSent(blockSize - 1)
                session.SendingFile = False
                session.fileReader.Close()
            End Try
        End If

    End Sub

    Private Function GetFilenameFromPath(ByRef filePath As String) As String
        Dim filePathParts() As String

        If filePath.Trim = "" Then Return ""

        Try
            filePathParts = Split(filePath, "\")
            GetFilenameFromPath = filePathParts(filePathParts.Length - 1)
        Catch ex As Exception
            filePath = ex.Message
            Return ""
        End Try

    End Function

    Private Function CreateFolders(ByVal _path As String) As Boolean
        CreateFolders = True

        Dim parts() As String
        Dim path As String = ""
        Dim count As Int32
        parts = Split(_path, "\")

        path = parts(0)
        For count = 1 To parts.Length - 2
            path += "\" & parts(count)
            Try
                If Not Directory.Exists(path) Then
                    Directory.CreateDirectory(path)
                End If
            Catch ex As Exception
            End Try
        Next

    End Function

    Private Function BeginToReceiveAFile(ByVal _path As String, ByVal session As SessionCommunications) As Boolean
        Dim readBuffer As Int32 = 0
        session.ReceivingFile = True
        BeginToReceiveAFile = True
        session.fileBytesRecieved = 0

        Try

            CreateFolders(_path) ' Just a 256k write buffer for the server. Let's try to avoid memory problems...
            session.fileWriter = New chat_AsyncUnbuffWriter(_path, True, 1024 * 256, session.IncomingFileSize)

        Catch ex As Exception
            _path = ex.Message
            session.ReceivingFile = False
        End Try

        If Not session.ReceivingFile Then
            Try
                session.fileWriter.Close()
            Catch ex As Exception
            End Try
            Return False
        End If
    End Function

    Private Function HandleIncomingFileBytes(ByRef bytes() As Byte, ByVal session As SessionCommunications) As Boolean

        Try
            session.fileWriter.Write(bytes, bytes.Length)
            HandleIncomingFileBytes = True
        Catch ex As Exception
            HandleIncomingFileBytes = False
        End Try

    End Function

    Private Sub FinishReceivingTheFile(ByVal session As SessionCommunications)
        Try
            session.fileWriter.Close()
            session.fileWriter = Nothing
            session.ReceivingFile = False
        Catch ex As Exception
            session.ReceivingFile = False
        End Try
    End Sub

    Private Sub HandleIncomingSystemMessages(ByVal bytes() As Byte, ByVal channel As Byte, ByVal session As SessionCommunications)

        If channel = 254 Then ' Text commands / messages passed between server and client
            Dim message As String = chat_Utilities.BytesToString(bytes)
            Dim filePath As String
            Dim tmp As String = ""

            ' Get File Request: The client wants us to send them a file.
            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "GFR:" Then
                ' Get file path...
                filePath = message.Substring(4, message.Length - 4)

                ' Does it exist?
                If File.Exists(filePath) Then
                    ' Do they have permission to get this file?
                    If CheckSessionPermissions(session, "GFR") Then
                        ' Are we already busy sending them a file?
                        If Not session.SendingFile Then
                            Dim _theFilesInfo As New FileInfo(filePath)
                            If BeginFileSend(filePath, session, _theFilesInfo.Length) Then
                                ' Send only the file NAME. It will have a different path on the other side.
                                SendExternalSystemMessage("Sending:" & GetFilenameFromPath(filePath) & _
                                                          ":" & _theFilesInfo.Length, session)
                            Else
                                ' FilePath contains the error message.
                                SendExternalSystemMessage("ERR: " & filePath, session)
                            End If
                        Else
                            ' There's already a GFR in progress.
                            SendExternalSystemMessage("ERR: File: ''" & _
                                                      session.FileBeingSentPath & _
                                                      "'' is still in progress. Only one file " & _
                                                      "may be transfered (from server to client) at a time.", session)
                        End If
                    Else
                        ' This user doesn't have rights to "get" this file. Send an error.
                        SendExternalSystemMessage("ERR: You do not have permission to receive files. Access Denied.", session)
                    End If
                Else
                    ' File doesn't exist. Send an error.
                    SendExternalSystemMessage("ERR: The requested file can not be found by the server.", session)
                End If
            End If

            ' We're being informed that we will be receiving a file:
            If message.Length > 7 Then tmp = message.Substring(0, 8)
            If tmp = "Sending:" Then
                ' Strip away the headder...
                Dim msgParts() As String = Split(message, ":")
                session.IncomingFileSize = Convert.ToInt64(msgParts(2))
                session.IncomingFileName = msgParts(1)
                tmp = session.ReceivedFilesFolder & "\" & session.IncomingFileName
                SystemMessage("Receiving file: " & session.IncomingFileName)
                If Not BeginToReceiveAFile(tmp, session) Then
                    SystemMessage("ERR: " & tmp)
                    SendExternalSystemMessage("Abort->", session)
                End If
            End If

            If message.Length > 10 Then tmp = message.Substring(0, 10)
            If tmp = "MachineID:" Then
                message = message.Substring(10, message.Length - 10)
                message = message.Trim

                If enforceUniqueMachineIds = True Then
                    If message = "" Then
                        'Send a connection Error:
                        'session.QueueSystemMessage("CERR:This server requires a unique Machine ID.")
                        'And a connection rejection notice:
                        session.QueueSystemMessage("connection:rejected")
                        ' Marke the session as invalidated, and set the machine name to nothing:
                        session.machineIdValidated = False
                        session.machineId = ""
                    Else
                        If SessionCollection.VerifyAndChangeUniqueMachineId(session, message) = False Then
                            'Send a connection Error:
                            'session.QueueSystemMessage("CERR:This server requires a unique Machine ID.")
                            'And a connection rejection notice:
                            session.QueueSystemMessage("connection:rejected")
                            ' Marke the session as invalidated, and set the machine name to nothing:
                            session.machineIdValidated = False
                            session.machineId = ""
                        Else
                            SystemMessage("Session#" & session.sessionID & " MachineID:" & session.machineId)
                            session.QueueSystemMessage("connection:accepted")
                        End If
                    End If
                Else
                    session.machineId = message
                    SystemMessage("Session#" & session.sessionID & " MachineID:" & session.machineId)
                    session.QueueSystemMessage("connection:accepted")
                End If

            End If

            If message = "<-Done" Then
                FinishReceivingTheFile(session)
                SystemMessage("<-Done")
            End If

            ' We've been notified that no file data will be forthcoming.
            If message = "Abort<-" Then
                WrapUpIncomingFile(session)
                SystemMessage("<-Aborted.")
                SendExternalSystemMessage("<-Aborted.", session)
            End If

            ' Send File Request: The client wants to send us a file.
            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "SFR:" Then
                If CheckSessionPermissions(session, "SFR") Then
                    Dim parts() As String
                    parts = Split(message, "SFR:")
                    SendExternalSystemMessage("GFR:" & parts(1), session)
                Else
                    ' This user doesn't have rights to send us a file. Send an error.
                    SendExternalSystemMessage("ERR: You do not have permission to send files. Access Denied.", session)
                End If
            End If

            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "GDR:" Then ' Get Directory Request
                ' Send each file in the directory and all subdirectories.
                ' To be implemented in the future.
            End If

            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "ERR:" Then ' The client has sent us an error message.
                ' Pass it on up to the user.
                SystemMessage(message)
            End If

            ' New queue throttling code
            If message = "pause" Then
                session.paused = True
            End If

            If message = "resume" Then
                session.paused = False
            End If

            If message = "Abort->" Then
                Try
                    session.SendingFile = False
                    ReDim session.SystemBytesToBeSent(blockSize - 1)
                    SendExternalSystemMessage("->Aborted.", session)
                    SystemMessage("->Aborted.")
                    session.fileReader.Close()
                Catch ex As Exception
                End Try
            End If

            ' The client is disconnecting. Close the connection gracefully...
            If message = "close" Then
                ' This will be caught by the try in the run sub, and execution
                ' will drop out of the communication loop immediately and 
                ' begin the shutdown process.

                Throw New Exception("Shutting session down gracefully.")
            End If
        ElseIf channel = 253 Then ' File transfer from server to client

        ElseIf channel = 252 Then ' File transfer from client to server
            Try
                If session.ReceivingFile Then
                    HandleIncomingFileBytes(bytes, session)
                    session.fileBytesRecieved += bytes.Length
                End If
            Catch ex As Exception
            End Try
        ElseIf channel = 251 Then ' reserved.

        End If
    End Sub

    Private Function HandleOutgoingInternalSystemMessage(ByVal Stream As NetworkStream, _
                                                         ByVal session As SessionCommunications) As Boolean
        Dim tmp(1) As Byte
        Dim _size As UShort
        'Static OurTurn As Boolean = False
        HandleOutgoingInternalSystemMessage = False

        ' Create a one time outgoing system message to syncronize packet size.
        If Not session.sendPacketSize Then
            SendExternalSystemMessage("blocksize:" & blockSize.ToString, session)
            session.sendPacketSize = True
        End If

        GetMoreFileBytesIfAvailable(session)

        ' Handle outgoing system stuff here
        If session.SystemBytesToBeSentAvailable = True Then
            HandleOutgoingInternalSystemMessage = True
            If session.SystemBytesToBeSent.Length > blockSize Then
                ' Send Channel
                tmp(0) = session.SystemOutputChannel
                Stream.Write(tmp, 0, 1)

                ' Send packet size
                _size = blockSize
                tmp = BitConverter.GetBytes(_size)
                Stream.Write(tmp, 0, 2)

                ' Send packet
                Stream.Write(GetSome(session.SystemBytesToBeSent, blockSize, session.SystemBytesToBeSentAvailable, session), 0, _size)
                session.bytesSentThisSecond += 3 + blockSize
            Else
                ' Send Channel
                tmp(0) = session.SystemOutputChannel
                Stream.Write(tmp, 0, 1)

                ' Send packet size
                _size = Convert.ToUInt16(session.SystemBytesToBeSent.Length)
                tmp = BitConverter.GetBytes(_size)
                Stream.Write(tmp, 0, 2)

                ' Send packet
                Stream.Write(session.SystemBytesToBeSent, 0, _size)
                session.bytesSentThisSecond += 3 + _size
                session.SystemBytesToBeSentAvailable = False
            End If
        End If

    End Function

    Private Function HandleOutgoingUserData(ByVal Stream As NetworkStream, ByVal session As SessionCommunications) As Boolean
        Dim tmp(1) As Byte
        Dim _size As UShort
        Dim notify As Boolean = False
        Static packet(0) As Byte
        Dim msg As message = Nothing
        Dim shutSessionDown As Boolean = False

        If Not session.UserBytesToBeSentAvailable Then
            If session.sendQueue.TryDequeue(msg) Then
                session.UserBytesToBeSentAvailable = True
                session.UserBytesToBeSent.SetLength(0)
                session.UserBytesToBeSent.Write(msg.bytes, 0, msg.bytes.Length)
                session.UserBytesToBeSent.Position = 0
                session.UserOutputChannel = msg.dataChannel
            End If
        End If

        If session.disConnect Then

            SystemMessage("Session Stopped. (" & session.sessionID.ToString & ")")
            session.machineId = ""
            session.machineIdValidated = False
            SessionCollection.CloseSession(session)

            session.UserBytesToBeSentAvailable = True
            Dim closeMessage As Byte() = chat_Utilities.StrToByteArray("close")
            session.UserBytesToBeSent.SetLength(0)
            session.UserBytesToBeSent.Write(closeMessage, 0, closeMessage.Length)
            session.UserBytesToBeSent.Position = 0
            session.UserOutputChannel = 254
            shutSessionDown = True
        End If

        If session.UserBytesToBeSentAvailable = True Then
            Try
                If (session.UserBytesToBeSent.Length - session.UserBytesToBeSent.Position) > blockSize Then
                    ' Send Channel
                    tmp(0) = session.UserOutputChannel
                    Stream.Write(tmp, 0, 1)

                    ' Send packet size
                    _size = blockSize
                    tmp = BitConverter.GetBytes(_size)
                    Stream.Write(tmp, 0, 2)

                    ' Send packet
                    If packet.Length <> _size Then ReDim packet(_size - 1)
                    session.UserBytesToBeSent.Read(packet, 0, _size)
                    'session.theClient.NoDelay = True
                    Stream.Write(packet, 0, _size)
                    session.bytesSentThisSecond += 3 + _size

                    ' Check to see if we've sent it all...
                    If session.UserBytesToBeSent.Length = session.UserBytesToBeSent.Position Then
                        session.UserBytesToBeSentAvailable = False
                        notify = True
                    End If
                Else
                    ' Send Channel
                    tmp(0) = session.UserOutputChannel
                    Stream.Write(tmp, 0, 1)

                    ' Send packet size
                    _size = Convert.ToUInt16(session.UserBytesToBeSent.Length - session.UserBytesToBeSent.Position)
                    tmp = BitConverter.GetBytes(_size)
                    Stream.Write(tmp, 0, 2)

                    ' Send packet
                    If packet.Length <> _size Then ReDim packet(_size - 1)
                    session.UserBytesToBeSent.Read(packet, 0, _size)
                    'session.theClient.NoDelay = True
                    Stream.Write(packet, 0, _size)
                    session.bytesSentThisSecond += 3 + _size

                    session.UserBytesToBeSentAvailable = False
                    notify = True
                End If
            Catch ex As Exception
                ' Report error attempting to send user data.
                Debug.WriteLine("Unexpected error in TcpCommServer\HandleOutgoingUserData: " & ex.Message)
            End Try

            ' Notify the user that the packet has been sent.
            If notify Then SystemMessage("UBS:" & session.sessionID & ":" & session.UserOutputChannel)

            ' This will drop execution out of the communications loop for this session, and 
            ' begin this session's shutdown process.
            If shutSessionDown Then
                'SystemMessage("Session Stopped. (" & session.sessionID.ToString & ")")
                'SessionCollection.CloseSession(session.sessionID)
                Throw New Exception("Shutting session down gracefully.")
            End If

            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetSome(ByRef bytes() As Byte, ByVal chunkToBreakOff As Integer, _
                             ByRef bytesToBeSentAvailable As Boolean, ByVal session As SessionCommunications, _
                             Optional ByVal theseAreUserBytes As Boolean = False) As Byte()

        Dim tmp(chunkToBreakOff - 1) As Byte
        Array.Copy(bytes, 0, tmp, 0, chunkToBreakOff)
        GetSome = tmp

        If bytes.Length = chunkToBreakOff Then
            bytesToBeSentAvailable = False
            If theseAreUserBytes Then SystemMessage("UBS:" & session.sessionID & ":" & session.UserOutputChannel)
        Else
            Dim tmp2(bytes.Length - chunkToBreakOff - 1) As Byte
            Array.Copy(bytes, chunkToBreakOff, tmp2, 0, bytes.Length - chunkToBreakOff)
            bytes = tmp2
        End If

    End Function

    Private Sub SystemMessage(ByVal MsgText As String)
        Dim bgMsg As New Thread(AddressOf BgMessage)
        bgMsg.IsBackground = True
        bgMsg.Start(MsgText)
    End Sub

    Private Sub BgMessage(ByVal _text As Object)
        Dim msg As String = CType(_text, String)
        RcvBytes(chat_Utilities.StrToByteArray(msg), Nothing, 255)
    End Sub

    'Private Sub SystemMessage(ByVal MsgText As String, ByRef session As SessionCommunications)
    '    RcvBytes(Utilities.StrToByteArray(MsgText), session, 255)
    'End Sub

    ' Check to see if our app is closing (set in FormClosing event)
    Private Function theServerIsStopping(ByVal Server As TcpClient, ByVal session As SessionCommunications) As Boolean

        If Not continue_running Or session.disConnect Then
            theServerIsStopping = True
        Else
            theServerIsStopping = False
        End If

    End Function



    Private Sub theListener()

        Try
            ' Start listening
            SystemMessage("Listening...")
            Listener = New TcpListener(localAddr, Port)
            'Listener = New TcpListener(My.Forms.main_form.localAddr, Convert.ToInt32(My.Forms.main_form.txt_chat_port.Text))
            Listener.Start()
            StartAccept()
        Catch ex As Exception
            errMsg = ex.Message
            serverState = currentState.err
            Exit Sub
        End Try

        serverState = currentState.running
    End Sub

    Private Function StartAccept() As Boolean

        ' Manage the rate at which 
        ' we accept new connections.
        Thread.Sleep(10)

        Try
            Listener.BeginAcceptTcpClient(AddressOf HandleAsyncConnection, Listener)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub HandleAsyncConnection(ByVal res As IAsyncResult)

        If Not StartAccept() Then Exit Sub

        Try
            SessionCollection.AddSession(Listener.EndAcceptTcpClient(res), New Thread(AddressOf Run), enforceUniqueMachineIds)
            GC.GetTotalMemory(True)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub WrapUpIncomingFile(ByVal session As SessionCommunications)

        If session.ReceivingFile Then
            Try
                session.fileWriter.Close()
                session.fileWriter = Nothing
                GC.GetTotalMemory(True)
            Catch ex As Exception
            End Try

            Try
                File.Delete(session.ReceivedFilesFolder & "\" & session.IncomingFileName)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Run(ByVal _session As Object)

        Dim session As SessionCommunications = DirectCast(_session, SessionCommunications)
        session.sendQueue = New ConcurrentQueue(Of message)
        session.messageIn = New SessionCommunications.MessageInQueue(ServerCallbackObject)

        Dim Server As TcpClient
        Dim Stream As NetworkStream
        Dim IpEndPoint As IPEndPoint
        Dim puck(1) As Byte : puck(0) = 0
        Dim theBuffer(blockSize - 1) As Byte
        Dim tmp(1) As Byte
        Dim dataChannel As Byte = 0
        Dim packetSize As UShort = 0
        Dim idleTimer, bandwidthTimer As Date
        Dim bytesread As Integer = 0
        Dim weHaveThePuck As Boolean = True
        Dim bandwidthUsedThisSecond As Int32 = 0
        Dim userOrSystemSwitcher As Integer = 0
        Dim packetReceiveTimeout As Date

        session.disConnect = False

        Try
            ' Create a local Server and Stream objects for clarity.
            Server = session.theClient
            Stream = Server.GetStream()
        Catch ex As Exception
            ' An unexpected error.
            Debug.WriteLine("Could not create local Server or Stream object in server. Message: " & ex.Message)
            Exit Sub
        End Try

        Stream.ReadTimeout = 5000

        Try

            ' Get the remote machine's IP address.
            IpEndPoint = CType(Server.Client.RemoteEndPoint, Net.IPEndPoint)
            session.remoteIpAddress = IpEndPoint.Address

            ' Set the send and receive buffers to the maximum
            ' size allowable in this application...
            Server.Client.ReceiveBufferSize = 65535
            Server.Client.SendBufferSize = 65535

            ' no delay on partially filled packets...
            ' Send it all as fast as possible.
            Server.NoDelay = True

            ' Set the timers...
            idleTimer = Now
            'bandwidthTimer = Now
            bandwidthTimer = Now.AddMilliseconds(250)

            session.IsRunning = True
            SystemMessage("Connected.")

            ' Start the communication loop
            Do

                ' Throttle network Mbps...
                bandwidthUsedThisSecond = session.bytesSentThisSecond + session.bytesRecievedThisSecond
                If bandwidthTimer >= Now And bandwidthUsedThisSecond >= (Mbps / 4) Then
                    While bandwidthTimer > Now
                        Thread.Sleep(1)
                    End While
                End If
                If bandwidthTimer <= Now Then
                    bandwidthTimer = Now.AddMilliseconds(250)
                    session.bytesRecievedThisSecond = 0
                    session.bytesSentThisSecond = 0
                    bandwidthUsedThisSecond = 0
                End If

                ' Normal communications...
                If weHaveThePuck Then

                    ' Send data if there is any to be sent...
                    userOrSystemSwitcher += 1
                    Select Case userOrSystemSwitcher
                        Case 1
                            If Not session.paused Then
                                If HandleOutgoingUserData(Stream, session) Then idleTimer = Now
                            End If
                        Case 2
                            If HandleOutgoingInternalSystemMessage(Stream, session) Then idleTimer = Now
                    End Select
                    If userOrSystemSwitcher > 1 Then userOrSystemSwitcher = 0

                    ' After sending out data, send the puck
                    Stream.Write(puck, 0, 1)
                    weHaveThePuck = False
                End If

                If theBuffer.Length < 2 Then ReDim theBuffer(1)

                ' Read in the control byte.
                Stream.Read(theBuffer, 0, 1)
                dataChannel = theBuffer(0)

                ' If it's just the puck (communictaion syncronization byte),
                ' set weHaveThePuck true, record the byte read for throttling,
                ' and that's all. dataChannel 0 is reserved for the puck.
                If dataChannel = 0 Then
                    weHaveThePuck = True
                    session.bytesRecievedThisSecond += 1
                Else
                    ' It's not the puck: It's an incoming packet.

                    ' Get the packet size:
                    tmp(0) = Convert.ToByte(Stream.ReadByte)
                    tmp(1) = Convert.ToByte(Stream.ReadByte)
                    packetSize = BitConverter.ToUInt16(tmp, 0)
                    session.bytesRecievedThisSecond += 2

                    ' Get the packet:
                    If theBuffer.Length <> packetSize Then ReDim theBuffer(packetSize - 1)

                    packetReceiveTimeout = Now.AddSeconds(3)
                    Do
                        ' Read bytes in...
                        bytesread += Stream.Read(theBuffer, bytesread, (packetSize - bytesread))

                        ' We've been waiting for moew data for 3 seconds... we've lost connection.
                        If packetReceiveTimeout < Now Then Throw New Exception("Timeout waiting for data from client. Connection lost.")
                    Loop While bytesread < packetSize
                    bytesread = 0

                    ' Record bytes read for throttling...
                    session.bytesRecievedThisSecond += packetSize

                    ' Handle the packet...
                    If dataChannel > 250 Then
                        ' this is an internal system packet
                        If Not theServerIsStopping(Server, session) Then HandleIncomingSystemMessages(theBuffer, dataChannel, session)
                    Else
                        ' Hand user data off to the calling thread.
                        If Not theServerIsStopping(Server, session) Then RcvBytes(theBuffer, session, dataChannel)
                    End If

                    idleTimer = Now
                End If

                ' Throttle CPU usage when idle.
                If Now > idleTimer.AddMilliseconds(500) Then
                    Thread.Sleep(50)
                End If

            Loop

        Catch ex As Exception
            ' An unexpected error.
            If ex IsNot Nothing Then Debug.WriteLine("Unexpected error in server: " & ex.Message)
        End Try

        session.machineIdValidated = False
        session.machineId = ""

        Try
            If session.fileReader IsNot Nothing Then session.fileReader.Close()
        Catch ex As Exception
        End Try

        Try
            Server.Client.Blocking = False
            Server.Client.Close()
        Catch ex As Exception
        End Try

        ' If we're in the middle of receiving a file,
        ' close the filestream, release the memory and
        ' delete the partial file.
        WrapUpIncomingFile(session)

        If session.disConnect = False Then
            SystemMessage("Session Stopped. (" & session.sessionID.ToString & ")")
            SessionCollection.CloseSession(session)
        End If

        session.messageIn.Close()

    End Sub

End Class