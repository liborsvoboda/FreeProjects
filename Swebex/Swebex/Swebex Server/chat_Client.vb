'Imports System.Threading
'Imports System.Net
'Imports System.Net.Sockets
'Imports System.IO
'Imports System.Collections.Concurrent
'Imports System.Reflection
'Imports System.Runtime.InteropServices
'Imports System.Diagnostics
'Imports System.Collections.Generic

Public Class chat_Client
    Public errMsg As String

    ' Define the delegate type
    Public Delegate Sub ClientCallbackDelegate(ByVal bytes() As Byte, ByVal dataChannel As Byte)

    ' Create Delegate pointer
    Public ClientCallbackObject As ClientCallbackDelegate

    Private continue_running As Boolean = False
    Private bytes() As Byte
    Private blockSize As UInt16
    Private IP As System.Net.IPAddress
    Private Port As Integer
    Private localAddr As IPAddress
    Private Client As TcpClient
    Private Stream As NetworkStream
    Public Shared fileWriter As chat_AsyncUnbuffWriter
    Private fileReader As FileStream
    Private FileBeingSentPath As String
    Private weHaveThePuck As Boolean = False
    Private isRunning As Boolean = False
    Private UserBytesToBeSentAvailable As Boolean = False
    Private UserBytesToBeSent As New MemoryStream
    Private UserOutputChannel As Byte
    Private SystemBytesToBeSentAvailable As Boolean = False
    Private SystemBytesToBeSent() As Byte
    Private SystemOutputChannel As Byte
    Private SendingFile As Boolean = False
    Public Shared ReceivingFile As Boolean = False
    Private IncomingFileName As String
    Public Shared IncomingFileSize As Int64 = 0
    Private outgoingFileSize As UInt64 = 0
    Private outgoingFileName As String
    Public Shared fileBytesRecieved As Int64 = 0
    Private filebytesSent As Int64 = 0
    Private bytesSentThisSecond As Int32 = 0
    Private bytesReceivedThisSecond As Int32 = 0
    Private mbpsOneSecondAverage() As Int32
    Private userName As String
    Private password As String
    Private machineId As String
    Private mbpsSyncObject As New AutoResetEvent(False)
    Private sendQueue As ConcurrentQueue(Of message)
    Private messageIn As MessageInQueue
    Private shuttingDown As Boolean
    Private reconnectMonitorDetails As New ReconnectData
    Private silentShutdown As Boolean = False
    Private connectionAccepted As Boolean = False
    Private connectionRejected = False
    Private disConnectComplete As Boolean

    Private Class ReconnectData
        Public ReconnectDuration As TimeSpan
        Public attemptTimeStamp As Date
        Public Reconnecting As Boolean
        Public ReconnectOnDisconnection
        Public ipAddress As String
        Public port As Int16
        Public machineId As String
    End Class

    Private Sub ReconnectMonitor()
        Dim reconnectDots As Int16 = 0
        Dim attemptMessage As String = "Attempting to reconnect"

        If continue_running = False Then Exit Sub
        If Not connectionAccepted Then Exit Sub

        reconnectMonitorDetails.Reconnecting = True
        reconnectMonitorDetails.attemptTimeStamp = Now

        SystemMessage(attemptMessage)

        While Not Connect(reconnectMonitorDetails.ipAddress, reconnectMonitorDetails.port, reconnectMonitorDetails.machineId, "")
            If continue_running = False Then Exit While
            Thread.Sleep(1000)
            If Now > reconnectMonitorDetails.attemptTimeStamp.Add(reconnectMonitorDetails.ReconnectDuration) Then Exit While
            If continue_running = False Then Exit Sub

            attemptMessage += "."
            reconnectDots += 1

            If reconnectDots > 3 Then
                reconnectDots = 0
                attemptMessage = "Attempting to reconnect"
            End If

            SystemMessage(attemptMessage)
        End While

        reconnectMonitorDetails.Reconnecting = False

        If isRunning = False Then
            continue_running = False
            If Not silentShutdown Then SystemMessage("Disconnected.")
        End If

    End Sub

    Private Class message
        Public bytes() As Byte
        Public dataChannel As Byte
    End Class

    Private Class MessageInQueue
        Public queue As New ConcurrentQueue(Of message)
        Private bgThread As New Threading.Thread(AddressOf Pump)
        Private running As Boolean
        Private callBack As ClientCallbackDelegate

        Public Sub New(ByRef _callBack As ClientCallbackDelegate)
            callBack = _callBack
            running = True
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
                        callBack(msg.bytes, msg.dataChannel)
                    End If
                End If

                If queue.Count = 0 Then Thread.Sleep(2)
            End While
        End Sub

    End Class

    Public Function isClientRunning() As Boolean
        Return isRunning
    End Function

    Public Sub SetReceivedFilesFolder(ByVal _path As String)
        My.Forms.main_form.FilesFolder = _path
    End Sub

    Public Function GetIncomingFileName() As String
        Return IncomingFileName
    End Function

    Public Function GetOutgoingFileName() As String
        Return outgoingFileName
    End Function

    Public Function GetPercentOfFileReceived() As UInt16
        If ReceivingFile Then
            Return CUShort((fileBytesRecieved / IncomingFileSize) * 100)
        Else
            Return 0
        End If
    End Function

    Public Function GetPercentOfFileSent() As UInt16
        If SendingFile Then
            Return CUShort((filebytesSent / outgoingFileSize) * 100)
        Else
            Return 0
        End If
    End Function

    Public Function GetMbps() As String
        Dim currentMbps As Decimal = CalculateMbps(True)
        If currentMbps > 1000000 Then
            Return (currentMbps / 1000000).ToString("N2") & " Mbps"
        Else
            Return (currentMbps / 1000).ToString("N2") & " Kbps"
        End If
    End Function

    Public Function GetLocalIpAddress() As System.Net.IPAddress
        Dim strHostName As String
        Dim addresses() As System.Net.IPAddress

        strHostName = System.Net.Dns.GetHostName()
        addresses = System.Net.Dns.GetHostAddresses(strHostName)

        ' Find an IpV4 address
        For Each address As System.Net.IPAddress In addresses
            ' Return the first IpV4 IP Address we find in the list.
            If address.AddressFamily = AddressFamily.InterNetwork Then
                Return address
            End If
        Next

        ' No IpV4 address? Return the loopback address.
        Return System.Net.IPAddress.Loopback
    End Function

    Private Function GetIPFromHostname(ByVal hostname As String, Optional returnLoopbackOnFail As Boolean = True) As System.Net.IPAddress
        Dim addresses() As System.Net.IPAddress

        Try
            addresses = System.Net.Dns.GetHostAddresses(hostname)
        Catch ex As Exception
            If Not returnLoopbackOnFail Then Return Nothing
            Return System.Net.IPAddress.Loopback
        End Try

        ' Find an IpV4 address
        For Each address As System.Net.IPAddress In addresses
            ' Return the first IpV4 IP Address we find in the list.
            If address.AddressFamily = AddressFamily.InterNetwork Then
                Return address
            End If
        Next

        ' No IpV4 address? Return the loopback address.
        If returnLoopbackOnFail Then Return System.Net.IPAddress.Loopback
        Return Nothing
    End Function

    ''' <summary>
    ''' Starting a new client requires a callback sub, and optional reconnection cryteria. 
    ''' </summary>
    ''' <param name="callbackMethod"></param>
    ''' <param name="ReconnectOnDisconnection">Clients started whith ReconnectOnDisconnection = True will continue to attempt to reconnect for the time specifyed in ReconnectionDurationSeconds.</param>
    ''' <param name="ReconnectionDurationSeconds">The number of seconds to attempt to reconnect to the server in the event that connection is lost.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal callbackMethod As ClientCallbackDelegate, Optional ByVal ReconnectOnDisconnection As Boolean = False, Optional ReconnectionDurationSeconds As Int32 = 15)

        blockSize = 10000

        ' Initialize the delegate variable to point to the user's callback method.
        ClientCallbackObject = callbackMethod

        ' Reconnect code here:
        reconnectMonitorDetails.ReconnectOnDisconnection = ReconnectOnDisconnection
        reconnectMonitorDetails.ReconnectDuration = New TimeSpan(0, 0, 0, ReconnectionDurationSeconds)
    End Sub

    Public Function Connect(ByVal IP_Address As String, ByVal prt As Integer, Optional newMachineID As String = "", _
                            Optional ByRef errorMessage As String = "") As Boolean

        If isRunning Then
            errorMessage = "The client is already connected.'"
            Return False
        End If

        connectionAccepted = False
        connectionRejected = False
        disConnectComplete = False

        Try
            ' Attempt to get the ip address by parsing the IP_Address string:
            IP = System.Net.IPAddress.Parse(IP_Address)
        Catch ex As Exception
            ' We got an error - it's not an ip address.
            ' Maybe it's a hostname.
            IP = GetIPFromHostname(IP_Address, False)
        End Try

        If IP Is Nothing Then
            ' Handle invalid IP address passed here.
            errorMessage = "Could not connect to " & IP_Address & ". It is not a valid IP address or hostname on this network."
            Return False
        End If

        Port = prt
        continue_running = True
        errMsg = ""
        shuttingDown = False
        sendQueue = New ConcurrentQueue(Of message)
        messageIn = New MessageInQueue(ClientCallbackObject)

        Dim clientCommunicationThread As New Thread(AddressOf Run)
        clientCommunicationThread.Name = "ClientCommunication"
        'clientCommunicationThread.IsBackground = True
        clientCommunicationThread.Start()

        SetMachineID(newMachineID)

        ' Wait for connection...
        While isRunning = False And errMsg = ""
            Thread.Sleep(5)
        End While

        ' Are we connected?
        errorMessage = errMsg
        If isRunning = False Then
            messageIn.Close()
            Return False
        Else

            While connectionAccepted = False
                Thread.Sleep(1)

                If connectionRejected = True Then
                    errorMessage = errMsg
                    Return False
                End If
                If isRunning = False Then
                    errorMessage = errMsg
                    messageIn.Close()
                    Return False
                End If
            End While

        End If

        Return True
    End Function

    ''' <summary>
    ''' Closes the TCP connection. 
    ''' </summary>
    ''' <param name="shutDownSilently">Prevents all system messages from being passed to your callback
    ''' (including the disconnected notification) and retruns control quickly while the client shuts 
    ''' down in the background.</param>
    ''' <remarks></remarks>
    Public Sub Close(Optional ByVal shutDownSilently As Boolean = False)

        silentShutdown = shutDownSilently
        shuttingDown = True

        ' If we're not running at all...
        If isRunning = False Then
            continue_running = False
            If Not silentShutdown Then SystemMessage("Disconnected.")
            Exit Sub
        End If

        If messageIn IsNot Nothing Then messageIn.Close()

        If shutDownSilently = True Then
            Dim bgClose As New Thread(AddressOf FinishClosing)
            bgClose.Start()
            Return
        End If

        FinishClosing()

    End Sub

    Private Sub FinishClosing()
        Dim timeout As Date = Now.AddSeconds(3)

        Try
            While (sendQueue.Count > 0) Or (UserBytesToBeSentAvailable = True)
                Thread.Sleep(5)
                If Now > timeout Then Exit While
            End While
        Catch ex As Exception
            ' sendQueue is nothing... not interested in this error.
        End Try

        continue_running = False

        While disConnectComplete = False
            Thread.Sleep(5)
            If Now > timeout Then Exit While
        End While
    End Sub

    Private Sub DoInternalClose()
        Thread.Sleep(250)
        Close()
    End Sub

    Private Sub InternalClose()
        Dim bgClose As New Thread(AddressOf DoInternalClose)
        bgClose.Start()
    End Sub

    Public Function GetBlocksize() As UInt16
        Return blockSize
    End Function

    ''' <summary>
    ''' Returns the size of the sendqueue. Returns -1 if isRunning = False. 
    ''' CAUTION: Calling this function too often will result in decreased performance, and failing to call it at all may result
    ''' in an out of memory error. You can continue to add messages to the send queue for as long as the connection is active 
    ''' (isRunning = True), but that doesn't mean they are being sent as fast as you are adding them to the queue (or at all, for that matter). 
    ''' </summary>
    ''' <returns>An Int32</returns>
    ''' <remarks></remarks>
    Public Function GetSendQueueSize() As Int32
        Dim sendQueueSize As Int32 = -1
        If isRunning Then GetSendQueueSize = sendQueue.Count
        Return sendQueueSize
    End Function

    Public Sub GetFile(ByVal _path As String)

        sendQueue.Enqueue(New message With { _
                          .bytes = chat_Utilities.StrToByteArray("GFR:" & _path),
                          .dataChannel = 254
                      })

    End Sub

    Public Function SendFile(ByVal _path As String, Optional ByRef errMsg As String = "") As Boolean

        If shuttingDown Then
            errMsg = "The client is shutting down. Outgoing files will not be sent."
            Return False
        End If

        sendQueue.Enqueue(New message With { _
                          .bytes = chat_Utilities.StrToByteArray("SFR:" & _path),
                          .dataChannel = 254
                      })

        Return True
    End Function

    Public Sub CancelIncomingFileTransfer()

        sendQueue.Enqueue(New message With { _
                          .bytes = chat_Utilities.StrToByteArray("Abort->"),
                          .dataChannel = 254
                      })

        FinishReceivingTheFile()

        Dim killFileThread As New System.Threading.Thread(AddressOf KillIncomingFile)
        killFileThread.Start(My.Forms.main_form.FilesFolder & "\" & IncomingFileName)

    End Sub

    Private Sub KillIncomingFile(_path As Object)
        Dim filePath As String = CType(_path, String)

        Dim timeOut As New Stopwatch
        timeOut.Start()
        While timeOut.ElapsedMilliseconds < 1000
            Try
                If Not File.Exists(filePath) Then Exit While
                File.Delete(filePath)
            Catch ex As Exception
            End Try
        End While
    End Sub

    Public Sub CancelOutgoingFileTransfer()

        sendQueue.Enqueue(New message With { _
                          .bytes = chat_Utilities.StrToByteArray("Abort<-"),
                          .dataChannel = 254
                      })

        StopSendingTheFile()

    End Sub

    Public Sub SetMachineID(ByVal id As String)

        machineId = id

        If id = "" Then id = " "
        sendQueue.Enqueue(New message With { _
                          .bytes = chat_Utilities.StrToByteArray("MachineID:" & id),
                          .dataChannel = 254
                      })
    End Sub

    Public Function GetMachineID() As String
        Return machineId
    End Function

    Public Function GetErrorMessage() As String
        Return errMsg
    End Function

    Public Function SendBytes(ByVal bytes() As Byte, Optional ByVal channel As Byte = 1, Optional ByRef errMsg As String = "") As Boolean

        If shuttingDown Then
            errMsg = "This client is shutting down. Bytes can not be sent."
            Return False
        End If

        If channel = 0 Or channel > 250 Then
            errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
            Return False
        End If

        If bytes Is Nothing Or bytes.Length = 0 Then
            errMsg = "bytes() must contain more then 0 bytes, and not be nothing."
            Return False
        End If

        If shuttingDown Then
            errMsg = "The client is shutting down. Outgoing messages will not be accepted."
            Return False
        End If

        sendQueue.Enqueue(New message With { _
                          .bytes = bytes,
                          .dataChannel = channel
                      })

        Return True
    End Function

    Public Function SendBytes(ByRef bytes() As Byte, ByVal offset As Int32, ByVal count As Int32, Optional ByVal channel As Byte = 1, Optional ByRef errMsg As String = "") As Boolean

        If shuttingDown Then
            errMsg = "This client is shutting down. Bytes can not be sent."
            Return False
        End If

        If channel = 0 Or channel > 250 Then
            errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
            Return False
        End If

        If bytes Is Nothing Or bytes.Length = 0 Then
            errMsg = "bytes() must contain more then 0 bytes, and not be nothing."
            Return False
        End If

        If shuttingDown Then
            errMsg = "The client is shutting down. Outgoing messages will not be accepted."
            Return False
        End If

        Dim msg As New message()
        ReDim msg.bytes(count - 1)

        Buffer.BlockCopy(bytes, offset, msg.bytes, 0, count)

        msg.dataChannel = channel

        sendQueue.Enqueue(msg)

        Return True
    End Function

    Public Function SendBytes(ByRef streamBytes As MemoryStream, Optional ByVal channel As Byte = 1, Optional ByRef errMsg As String = "") As Boolean

        If shuttingDown Then
            errMsg = "This client is shutting down. Bytes can not be sent."
            Return False
        End If

        If channel = 0 Or channel > 250 Then
            errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
            Return False
        End If

        If bytes Is Nothing Or bytes.Length = 0 Then
            errMsg = "bytes() must contain more then 0 bytes, and not be nothing."
            Return False
        End If

        If shuttingDown Then
            errMsg = "The client is shutting down. Outgoing messages will not be accepted."
            Return False
        End If

        Dim msg As New message()
        ReDim msg.bytes(streamBytes.Length - 1)

        streamBytes.Position = 0
        streamBytes.Read(msg.bytes, 0, msg.bytes.Length)

        msg.dataChannel = channel

        sendQueue.Enqueue(msg)

        Return True
    End Function

    ''' <summary>
    ''' This is a convienience function that handles the work of converting the text you would like to send to a byte array. 
    ''' Passes back the return value and errMsg of SendBytes(). Returns True on success and False on falure. Check the errMsg 
    ''' string for send failure explanations.
    ''' </summary>
    ''' <param name="textMessage"></param>
    ''' <param name="channel"></param>
    ''' <param name="errMsg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendText(ByVal textMessage As String, Optional ByVal channel As Byte = 1, _
                             Optional ByRef errMsg As String = "") As Boolean

        If shuttingDown Then
            errMsg = "This client is shutting down. Text can not be sent."
            Return False
        End If

        If textMessage = "" Then
            errMsg = "Your text message must contain some text."
            Return False
        End If

        Return SendBytes(chat_Utilities.StrToByteArray(textMessage), channel, errMsg)
    End Function

    Private Function RcvBytes(ByVal data() As Byte, Optional ByVal dataChannel As Byte = 1) As Boolean
        ' dataType: >0 = data channel, 251 and up = internal messages. 0 is an invalid channel number (it's the puck)

        If dataChannel < 1 Or Not continue_running Then Return False

        Try
            Dim passedData(data.Length - 1) As Byte
            Array.Copy(data, passedData, data.Length)
            messageIn.queue.Enqueue(New message With { _
                                            .bytes = passedData,
                                            .dataChannel = dataChannel
                                        })
            'ClientCallbackObject(data, datachannel)
        Catch ex As Exception
            ' An unexpected error.
            Debug.WriteLine("Unexpected error in Client\RcvBytes: " & ex.Message)
            Return False
        End Try

        Return True
    End Function

    Public Shared Function CreateFolders(ByVal _path As String) As Boolean
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

    Private Sub SendExternalSystemMessage(ByVal message As String)

        SystemBytesToBeSent = chat_Utilities.StrToByteArray(message)
        SystemOutputChannel = 254 ' Text messages / commands on channel 254
        SystemBytesToBeSentAvailable = True

    End Sub

    'Private Function BeginToReceiveAFile(ByVal _path As String) As Boolean
    '    MessageBox.Show("incoming")
    '    My.Forms.main_form.lb_chat_history.Items.Add("neco")
    '    Dim readBuffer As Int32 = 0
    '    ReceivingFile = True
    '    BeginToReceiveAFile = True
    '    fileBytesRecieved = 0

    '    Try
    '        CreateFolders(_path)
    '        fileWriter = New chat_AsyncUnbuffWriter(_path, True, _
    '                        1024 * (chat_AsyncUnbuffWriter.GetPageSize()), IncomingFileSize)

    '    Catch ex As Exception
    '        _path = ex.Message
    '        ReceivingFile = False
    '    End Try

    '    If Not ReceivingFile Then
    '        Try
    '            fileWriter.Close()
    '        Catch ex As Exception
    '        End Try
    '        Return False
    '    End If
    'End Function



    Private Function HandleIncomingFileBytes(ByRef bytes() As Byte) As Boolean

        Try
            fileWriter.Write(bytes, bytes.Length)
            HandleIncomingFileBytes = True
        Catch ex As Exception
            HandleIncomingFileBytes = False
        End Try

    End Function

    Private Sub FinishReceivingTheFile()
        Try
            fileWriter.Close()
            fileWriter = Nothing
            ReceivingFile = False
        Catch ex As Exception
            ReceivingFile = False
        End Try
    End Sub

    Private Sub StopSendingTheFile()
        Try
            SendingFile = False
            fileReader.Close()
            fileReader = Nothing
            GC.GetTotalMemory(True)
        Catch ex As Exception
            SendingFile = False
            GC.GetTotalMemory(True)
        End Try
    End Sub

    Private Sub WrapUpIncomingFile()

        If ReceivingFile Then
            Try
                fileWriter.Close()
                fileWriter = Nothing
                GC.GetTotalMemory(True)
            Catch ex As Exception
            End Try

            Try
                File.Delete(My.Forms.main_form.FilesFolder & "\" & IncomingFileName)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Function CheckSessionPermissions(ByVal cmd As String) As Boolean
        ' Your security code here...

        Return True
    End Function

    Private Function BeginFileSend(ByVal _path As String, ByVal fileLength As Long) As Boolean
        filebytesSent = 0

        Try

            fileReader = New FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None, chat_AsyncUnbuffWriter.GetPageSize)
            SendingFile = True
            BeginFileSend = True

        Catch ex As Exception
            BeginFileSend = False
            _path = ex.Message
            SendingFile = False
        End Try

        Try
            If Not BeginFileSend Then fileReader.Close()
        Catch ex As Exception
        End Try

    End Function

    Private Sub GetMoreFileBytesIfAvailable()
        Dim bytesRead As Integer

        If SendingFile And Not SystemBytesToBeSentAvailable Then
            Try
                If SystemBytesToBeSent.Length <> blockSize Then ReDim SystemBytesToBeSent(blockSize - 1)
                bytesRead = fileReader.Read(SystemBytesToBeSent, 0, blockSize)
                If bytesRead <> blockSize Then ReDim Preserve SystemBytesToBeSent(bytesRead - 1)

                If bytesRead > 0 Then
                    SystemOutputChannel = 252 ' File transfer from client to server
                    SystemBytesToBeSentAvailable = True
                    filebytesSent += bytesRead
                Else

                    ReDim SystemBytesToBeSent(blockSize - 1)
                    SendExternalSystemMessage("<-Done") ' Send the server a completion notice.
                    SystemMessage("<-Done")
                    SendingFile = False

                    ' Clean up
                    fileReader.Close()
                    fileReader = Nothing
                    GC.GetTotalMemory(True)
                End If
            Catch ex As Exception
                SendExternalSystemMessage("ERR: " & ex.Message)

                ' We're finished.
                ReDim SystemBytesToBeSent(blockSize - 1)
                SendingFile = False
                fileReader.Close()
            End Try
        End If

    End Sub

    Private Function GetFilenameFromPath(ByVal filePath As String) As String
        Dim filePathParts() As String

        If filePath.Trim = "" Then Return ""

        filePathParts = Split(filePath, "\")
        GetFilenameFromPath = filePathParts(filePathParts.Length - 1)
    End Function


    Private Sub HandleIncomingSystemMessages(ByVal bytes() As Byte, ByVal channel As Byte)
        If channel = 254 Then ' Text commands / messages passed between server and client
            Dim message As String = chat_Utilities.BytesToString(bytes)
            Dim tmp As String = ""
            Dim filePath As String

            ' Get File Request: The server wants us to send them a file.
            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "GFR:" Then ' Get File Request
                ' Get file path...
                filePath = message.Substring(4, message.Length - 4)

                ' Does it exist?
                If File.Exists(message.Substring(4, message.Length - 4)) Then
                    ' Are we already busy sending them a file?
                    If Not SendingFile Then
                        Dim _theFilesInfo As New FileInfo(filePath)
                        outgoingFileName = GetFilenameFromPath(filePath)
                        outgoingFileSize = CULng(_theFilesInfo.Length)
                        If BeginFileSend(filePath, _theFilesInfo.Length) Then
                            ' Send only the file NAME. It will have a different path on the other side.
                            SendExternalSystemMessage("Sending:" & outgoingFileName & _
                                                      ":" & outgoingFileSize.ToString)
                            SystemMessage("Sending file:" & outgoingFileName)
                        Else
                            ' FilePath contains the error message.
                            SendExternalSystemMessage("ERR: " & filePath)
                        End If
                    Else
                        ' There's already a GFR in progress.
                        SendExternalSystemMessage("ERR: File: ''" & _
                                                  FileBeingSentPath & _
                                                  "'' is still in progress. Only one file " & _
                                                  "may be transfered (from client to server) at a time.")
                    End If
                Else
                    ' File doesn't exist. Send an error.
                    SendExternalSystemMessage("ERR: The requested file can not be found by the server.")
                End If
            End If

            If message.Length > 7 Then tmp = message.Substring(0, 8)
            If tmp = "Sending:" Then
                ' Strip away the headder...
                Dim msgParts() As String = Split(message, ":")
                IncomingFileSize = Convert.ToInt64(msgParts(2))
                IncomingFileName = msgParts(1)
                tmp = My.Forms.main_form.FilesFolder & "\" & IncomingFileName
                SystemMessage("Receiving file: " & IncomingFileName)
                If Not BeginToReceiveAFile(tmp) Then
                    SystemMessage("ERR: " & tmp)
                    SendExternalSystemMessage("Abort<-")
                End If
            End If

            If message.Length > 10 Then tmp = message.Substring(0, 10)
            If tmp = "blocksize:" Then
                Dim msgParts() As String = Split(message, ":")
                blockSize = Convert.ToUInt16(msgParts(1))
            End If

            If message = "->Done" Then
                FinishReceivingTheFile()
                SystemMessage("->Done")
            End If

            ' We've been notified that no file data will be forthcoming.
            If message = "Abort->" Then
                FinishReceivingTheFile()
                SystemMessage("->Aborted.")
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal
                Try
                    File.Delete(My.Forms.main_form.FilesFolder & "\" & IncomingFileName)
                Catch ex As Exception
                End Try
            End If

            ' Send File Request: The server wants to send us a file.
            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "SFR:" Then
                If CheckSessionPermissions("SFR") Then
                    Dim parts() As String
                    parts = Split(message, "SFR:")
                    SendExternalSystemMessage("GFR:" & parts(1))
                Else
                    ' This user doesn't have rights to this file. Send an error.
                    SendExternalSystemMessage("ERR: You do not have permission to send files. Access Denied.")
                End If
            End If

            ' Notification that the server has complied with our  
            ' request to stop sending bytes for this 
            ' (server->client) file transfer.
            If message = "->Aborted." Then
                SystemMessage("->Aborted.")
                WrapUpIncomingFile()
            End If

            ' Notification that the server has complied with our  
            ' request to stop recieving bytes for this 
            ' (client->server) file transfer.
            If message = "<-Aborted." Then
                SystemMessage("<-Aborted.")
            End If

            If message.Length > 4 Then tmp = message.Substring(0, 4)
            If tmp = "ERR:" Then ' The server has sent us an error message.
                ' Pass it on up to the user.
                SystemMessage(message)
            End If

            If message.Length > 5 Then tmp = message.Substring(0, 5)
            If tmp = "CERR:" Then ' The server has sent us a connection error message.
                ' Pass it on up to the user.
                errMsg = message.Replace("CERR:", "")
            End If

            ' New queue throttling code
            If message = "pause" Then
                'sendBuffer.PauseSending()
            End If

            If message = "resume" Then
                'sendBuffer.ResumeSending()
            End If

            ' Preform gracefull shutdown. 
            If message = "close" Then
                'SystemMessage("Disconnected by server.")
                continue_running = False
                'disConnectComplete = True
                Throw New Exception("Shutting down gracefully")
            End If

            If message = "connection:rejected" Then
                'continue_running = False
                connectionRejected = True
                InternalClose()
            End If

            If message = "connection:accepted" Then
                connectionAccepted = True
            End If

        ElseIf channel = 253 Then ' File transfer from server to client
            Try
                If ReceivingFile Then
                    HandleIncomingFileBytes(bytes)
                    fileBytesRecieved += bytes.LongLength
                End If
            Catch ex As Exception
            End Try
        ElseIf channel = 252 Then ' File transfer from client to server

        ElseIf channel = 251 Then ' reserved.

        End If
    End Sub

    Private Function HandleOutgoingInternalSystemMessage() As Boolean
        Dim tmp(1) As Byte
        HandleOutgoingInternalSystemMessage = False
        Dim _size As Integer

        GetMoreFileBytesIfAvailable()

        ' Handle outgoing system stuff here
        If SystemBytesToBeSentAvailable = True Then
            HandleOutgoingInternalSystemMessage = True
            If SystemBytesToBeSent.Length > blockSize Then
                ' Send Channel
                tmp(0) = SystemOutputChannel
                Stream.Write(tmp, 0, 1)
                bytesSentThisSecond += 1

                ' Send packet size
                _size = blockSize
                tmp = BitConverter.GetBytes(_size)
                Stream.Write(tmp, 0, 2)
                bytesSentThisSecond += 2

                ' Send packet
                Stream.Write(GetSome(SystemBytesToBeSent, blockSize, SystemBytesToBeSentAvailable), 0, _size)
                bytesSentThisSecond += _size
            Else
                ' Send Channel
                tmp(0) = SystemOutputChannel
                Stream.Write(tmp, 0, 1)
                bytesSentThisSecond += 1

                ' Send packet size
                _size = SystemBytesToBeSent.Length
                tmp = BitConverter.GetBytes(_size)
                Stream.Write(tmp, 0, 2)
                bytesSentThisSecond += 2

                ' Send packet
                Stream.Write(SystemBytesToBeSent, 0, _size)
                bytesSentThisSecond += _size
                SystemBytesToBeSentAvailable = False
            End If
        End If

    End Function

    Private Function HandleOutgoingUserData() As Boolean
        Dim tmp(1) As Byte
        Dim _size As UShort
        Dim notify As Boolean = False
        Static packet(0) As Byte
        Dim msg As message = Nothing
        Dim stopMessageSent As Boolean = False

        If Not UserBytesToBeSentAvailable Then
            If sendQueue.TryDequeue(msg) Then
                UserBytesToBeSentAvailable = True
                UserBytesToBeSent.SetLength(0)
                UserBytesToBeSent.Write(msg.bytes, 0, msg.bytes.Length)
                UserBytesToBeSent.Position = 0
                UserOutputChannel = msg.dataChannel
            End If
        End If

        If theClientIsStopping() Then
            UserBytesToBeSentAvailable = True
            Dim closeMessage As Byte() = chat_Utilities.StrToByteArray("close")
            UserBytesToBeSent.SetLength(0)
            UserBytesToBeSent.Write(closeMessage, 0, closeMessage.Length)
            UserBytesToBeSent.Position = 0
            UserOutputChannel = 254
            stopMessageSent = True
        End If

        If UserBytesToBeSentAvailable = True Then
            Try
                If (UserBytesToBeSent.Length - UserBytesToBeSent.Position) > blockSize Then
                    ' Send Channel
                    tmp(0) = UserOutputChannel
                    Stream.Write(tmp, 0, 1)

                    ' Send packet size
                    _size = blockSize
                    tmp = BitConverter.GetBytes(_size)
                    Stream.Write(tmp, 0, 2)

                    ' Send packet
                    If packet.Length <> _size Then ReDim packet(_size - 1)
                    UserBytesToBeSent.Read(packet, 0, _size)
                    'Client.NoDelay = True
                    Stream.Write(packet, 0, _size)
                    bytesSentThisSecond += 3 + _size

                    ' Check to see if we've sent it all...
                    If UserBytesToBeSent.Length = UserBytesToBeSent.Position Then
                        UserBytesToBeSentAvailable = False
                        notify = True
                    End If
                Else
                    ' Send Channel
                    tmp(0) = UserOutputChannel
                    Stream.Write(tmp, 0, 1)

                    ' Send packet size
                    _size = Convert.ToUInt16(UserBytesToBeSent.Length - UserBytesToBeSent.Position)
                    tmp = BitConverter.GetBytes(_size)
                    Stream.Write(tmp, 0, 2)

                    ' Send packet
                    If packet.Length <> _size Then ReDim packet(_size - 1)
                    UserBytesToBeSent.Read(packet, 0, _size)
                    'Client.NoDelay = True
                    Stream.Write(packet, 0, _size)
                    bytesSentThisSecond += 3 + _size

                    UserBytesToBeSentAvailable = False
                    notify = True
                End If
            Catch ex As Exception
                ' Report error attempting to send user data.
                Debug.WriteLine("Unexpected error in TcpCommClient\HandleOutgoingUserData: " & ex.Message)
            End Try

            ' Notify the user that the packet has been sent.
            If notify Then SystemMessage("UBS:" & UserOutputChannel)

            If stopMessageSent Then Throw New Exception("Client closing gracefully.")
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetSome(ByRef bytes() As Byte, ByVal chunkToBreakOff As Integer, _
                             ByRef bytesToBeSentAvailable As Boolean, _
                             Optional ByVal theseAreUserBytes As Boolean = False) As Byte()

        Dim tmp(chunkToBreakOff - 1) As Byte
        Array.Copy(bytes, 0, tmp, 0, chunkToBreakOff)
        GetSome = tmp

        If bytes.Length = chunkToBreakOff Then
            bytesToBeSentAvailable = False
            If theseAreUserBytes Then SystemMessage("UBS")
        Else
            Dim tmp2(bytes.Length - chunkToBreakOff - 1) As Byte
            Array.Copy(bytes, chunkToBreakOff, tmp2, 0, bytes.Length - chunkToBreakOff)
            bytes = tmp2
        End If

    End Function

    Private Sub SystemMessage(ByVal MsgText As String)
        If silentShutdown = True Then Return

        If isRunning Then
            RcvBytes(chat_Utilities.StrToByteArray(MsgText), 255)
        Else
            Dim bgMsg As New Thread(AddressOf BgMessage)
            bgMsg.IsBackground = True
            bgMsg.Start(MsgText)
        End If
    End Sub

    Private Sub BgMessage(ByVal _text As Object)
        Dim msg As String = CType(_text, String)
        ClientCallbackObject(chat_Utilities.StrToByteArray(msg), 255)
    End Sub

    ' Check to see if our app is closing (set in FormClosing event)
    Private Function theClientIsStopping() As Boolean

        If continue_running = False Then Return True
        Return False

    End Function

    Private Function CalculateMbps(Optional ByVal GetMbps As Boolean = False) As Decimal
        Static averagesCounter As Integer = 0
        Static tmr As Date = Now
        Static lastread As Int32 = 0
        Dim looper As Short = 0
        Dim tmp As Int32 = 0

        If mbpsOneSecondAverage Is Nothing Then ReDim mbpsOneSecondAverage(9)

        If Now >= tmr.AddMilliseconds(250) Then
            averagesCounter += 1
            If averagesCounter < 0 Then averagesCounter = 0
            Select Case averagesCounter
                Case 0
                    SyncLock (mbpsSyncObject)
                        Try
                            mbpsOneSecondAverage(averagesCounter) = bytesSentThisSecond + bytesReceivedThisSecond
                            bytesSentThisSecond = 0
                            bytesReceivedThisSecond = 0
                        Catch ex As Exception
                            averagesCounter = -1
                        End Try
                    End SyncLock
                Case 1
                    SyncLock (mbpsSyncObject)
                        Try
                            mbpsOneSecondAverage(averagesCounter) = bytesSentThisSecond + bytesReceivedThisSecond
                            bytesSentThisSecond = 0
                            bytesReceivedThisSecond = 0
                        Catch ex As Exception
                            averagesCounter = -1
                        End Try
                    End SyncLock
                Case 2
                    SyncLock (mbpsSyncObject)
                        Try
                            mbpsOneSecondAverage(averagesCounter) = bytesSentThisSecond + bytesReceivedThisSecond
                            bytesSentThisSecond = 0
                            bytesReceivedThisSecond = 0
                        Catch ex As Exception
                            averagesCounter = -1
                        End Try
                    End SyncLock
                Case 3
                    SyncLock (mbpsSyncObject)
                        Try
                            mbpsOneSecondAverage(averagesCounter) = bytesSentThisSecond + bytesReceivedThisSecond
                            bytesSentThisSecond = 0
                            bytesReceivedThisSecond = 0
                        Catch ex As Exception
                            averagesCounter = -1
                        End Try
                    End SyncLock
            End Select

            If averagesCounter > 2 Then averagesCounter = -1
            tmr = Now
        End If

        ' Did they ask us for the Mbps?
        If GetMbps Then
            For looper = 0 To 3
                SyncLock (mbpsSyncObject)
                    tmp += mbpsOneSecondAverage(looper)
                End SyncLock
            Next
            CalculateMbps = tmp
        Else
            CalculateMbps = 0
        End If

    End Function

    Private Sub Run()

        Dim puck(1) As Byte : puck(0) = 0
        Dim theBuffer(blockSize - 1) As Byte
        Dim tmp(1) As Byte
        Dim dataChannel As Byte = 0
        Dim packetSize As UShort = 0
        Dim bytesread As Integer
        Dim userOrSystemSwitcher As Integer = 0
        Dim PercentUsage As Short = -1
        Dim connectionLossTimer As Date

        'Dim CPUutil As New CpuMonitor
        'CPUutil.Start()

        Try

            Client = New TcpClient
            Client.SendTimeout = 5

            Client.Connect(IP, Port)

            ' Connection Accepted.
            Stream = Client.GetStream()
            Stream.ReadTimeout = 5000

            ' Set the send and receive buffers to the maximum
            ' size allowable in this application...
            Client.Client.ReceiveBufferSize = 65535
            Client.Client.SendBufferSize = 65535

            ' no delay on partially filled packets...
            ' Send it all as fast as possible.
            Client.NoDelay = True

            ' Pass a message up to the user about our status.
            isRunning = True
            SystemMessage("Connected.")

            ' Start the communication loop
            While True
                ' Check to see if our app is shutting down.
                'If theClientIsStopping() Then Exit Do

                ' Normal communications...
                If weHaveThePuck Then

                    ' Send user data if there is any to be sent.
                    userOrSystemSwitcher += 1
                    Select Case userOrSystemSwitcher
                        Case 1
                            HandleOutgoingUserData()
                        Case 2
                            HandleOutgoingInternalSystemMessage()
                    End Select
                    If userOrSystemSwitcher > 1 Then userOrSystemSwitcher = 0

                    ' After sending our data, send the puck
                    Stream.Write(puck, 0, 1)

                    ' Uncomment this to see control bit traffic as part of your Mbps 
                    'bytesSentThisSecond += 1
                    weHaveThePuck = False

                End If

                If theBuffer.Length < 2 Then ReDim theBuffer(1)

                ' Read in the control byte.
                Stream.Read(theBuffer, 0, 1)
                dataChannel = theBuffer(0)

                ' Uncomment this to see control bit traffic as part of your Mbps
                'bytesReceivedThisSecond += 1

                ' If it's just the puck (communictaion syncronization byte),
                ' set weHaveThePuck true and that's all. dataChannel 0 is 
                ' reserved for the puck.
                If dataChannel = 0 Then
                    weHaveThePuck = True
                Else
                    ' It's not the puck: It's an incoming packet.
                    ' Get the packet size:
                    tmp(0) = Convert.ToByte(Stream.ReadByte)
                    tmp(1) = Convert.ToByte(Stream.ReadByte)
                    packetSize = BitConverter.ToUInt16(tmp, 0)
                    If theBuffer.Length <> packetSize Then ReDim theBuffer(packetSize - 1)
                    bytesReceivedThisSecond += 2

                    ' Get the packet:
                    connectionLossTimer = Now
                    Do
                        ' Read bytes in...
                        bytesread += Stream.Read(theBuffer, bytesread, (packetSize - bytesread))

                        ' If it takes longer then 3 seconds to get a packet, we've lost connection.
                        If connectionLossTimer.AddSeconds(3) < Now Then Throw New Exception("Time out waiting for packet to arrive. Connection lost.")

                    Loop While bytesread < packetSize
                    bytesread = 0

                    ' Record bytes read for throttling...
                    bytesReceivedThisSecond += packetSize

                    ' Handle the packet...
                    If dataChannel > 250 And continue_running Then
                        ' this is an internal system packet
                        HandleIncomingSystemMessages(theBuffer, dataChannel)
                    Else
                        ' Hand data off to the calling thread.
                        RcvBytes(theBuffer, dataChannel)
                    End If

                End If

                CalculateMbps(False)

                ' Measure and display the CPU usage of the client (this thread).
                'If PercentUsage <> CPUutil.ThreadUsage Then
                '    PercentUsage = CPUutil.ThreadUsage
                '    SystemMessage("" & PercentUsage & "% Thread Usage (" & CPUutil.CPUusage & "% across all CPUs)")
                'End If

            End While
        Catch ex As Exception
            ' Handle thrown errors here:

            'If ex IsNot Nothing Then errMsg = "Error caught in run thread: " & ex.Message 'And ex.Message <> "Shutting down gracefully"
        End Try

        Try
            'CPUutil.StopWatcher()
            'If Not Client.Client Is Nothing Then Client.Client.Close()
            Client.Client.Dispose()
            Client.Close()
        Catch ex As Exception
            ' An unexpected error.
            Debug.WriteLine("Error atempting to shut down the theClient after Gracefull Disconnect: " & ex.Message)
        End Try

        Try
            If fileWriter IsNot Nothing Then fileWriter.Close()
        Catch ex As Exception
        End Try

        'Try
        '    'CPUutil.StopWatcher()
        '    'If Not Client.Client Is Nothing Then Client.Client.Close()
        'Catch ex As Exception
        '    ' An unexpected error.
        '    Debug.WriteLine("Unexpected error in Client\theClientIsStopping: " & ex.Message)
        'End Try

        WrapUpIncomingFile()
        isRunning = False
        If messageIn IsNot Nothing Then messageIn.Close()

        If reconnectMonitorDetails.ReconnectOnDisconnection And _
            Not reconnectMonitorDetails.Reconnecting And connectionAccepted Then

            reconnectMonitorDetails.ipAddress = IP.ToString()
            reconnectMonitorDetails.machineId = machineId
            reconnectMonitorDetails.port = Port

            Dim reconnectThread As New Thread(AddressOf ReconnectMonitor)
            reconnectThread.Start()

        End If

        ' Report disconnection here:
        If reconnectMonitorDetails.ReconnectOnDisconnection = False Then
            ' We've been disconnected and we are con configured to reconnect automatically, so we report it.
            SystemMessage("Disconnected.")
        Else
            ' We ARE configured to automatically reconnect, and we've been disconnected.

            ' If continue_running = False, then we've been deliberately disconnected
            ' by the server, and we should just report our status. 

            ' If continue_running = True, then we've LOST connection, possibly due to network
            ' conditions, and we should not report it HERE. The automatic reconnect system
            ' will either reconnect, or report that we are disconnected after the reconnect
            ' duration has expired.
            If continue_running = False Then SystemMessage("Disconnected.")
        End If

        disConnectComplete = True
    End Sub

End Class
