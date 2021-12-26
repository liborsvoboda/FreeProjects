Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Collections.Concurrent
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Collections.Generic

Public Class chat_Comm

    Public Shared Function BytesToString(ByVal data() As Byte) As String
        Dim enc As New System.Text.UTF8Encoding()
        BytesToString = enc.GetString(data)
    End Function

    Public Shared Function StrToByteArray(ByVal text As String) As Byte()
        Dim encoding As New System.Text.UTF8Encoding()
        StrToByteArray = encoding.GetBytes(text)
    End Function

    Public Class clsAsyncUnbuffWriter

        '''' We need the page size for best performance - so we use GetSystemInfo and dwPageSize
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Public Class clsSystemInfo

            Private Class WinApi
                <DllImport("kernel32.dll")> _
                Public Shared Sub GetSystemInfo(<MarshalAs(UnmanagedType.Struct)> ByRef lpSystemInfo As SYSTEM_INFO)
                End Sub

                <StructLayout(LayoutKind.Sequential)> _
                Public Structure SYSTEM_INFO
                    Friend uProcessorInfo As _PROCESSOR_INFO_UNION
                    Public dwPageSize As UInteger
                    Public lpMinimumApplicationAddress As IntPtr
                    Public lpMaximumApplicationAddress As IntPtr
                    Public dwActiveProcessorMask As IntPtr
                    Public dwNumberOfProcessors As UInteger
                    Public dwProcessorType As UInteger
                    Public dwAllocationGranularity As UInteger
                    Public dwProcessorLevel As UShort
                    Public dwProcessorRevision As UShort
                End Structure

                <StructLayout(LayoutKind.Explicit)> _
                Public Structure _PROCESSOR_INFO_UNION
                    <FieldOffset(0)> _
                    Friend dwOemId As UInteger
                    <FieldOffset(0)> _
                    Friend wProcessorArchitecture As UShort
                    <FieldOffset(2)> _
                    Friend wReserved As UShort
                End Structure
            End Class

            Public Shared Function GetPageSize() As Integer
                Dim sysinfo As New WinApi.SYSTEM_INFO()
                WinApi.GetSystemInfo(sysinfo)
                Return CInt(sysinfo.dwPageSize)
            End Function
        End Class

        Private target As FileStream
        Private inputBuffer As MemoryStream
        Private bufferSize As Integer
        Private running As Boolean
        Private writing As Boolean
        Private readWait As Threading.ManualResetEvent
        Private writeWait As Threading.ManualResetEvent
        Private finishedWriting As Threading.ManualResetEvent
        Private totalWritten As Int64
        Private writeTimer As Stopwatch

        Public Function GetTotalBytesWritten() As Int64
            Return totalWritten
        End Function

        Public Function IsRunning() As Boolean
            Return running
        End Function

        Public Sub Close()
            writing = False
            writeWait.Set()
            finishedWriting.WaitOne()
            readWait.Set()
        End Sub

        Public Function GetActiveMiliseconds() As Int64
            Try
                Return writeTimer.ElapsedMilliseconds
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function GetPageSize() As Integer
            Return clsSystemInfo.GetPageSize
        End Function

        Public Sub New(ByVal dest As String, _
                    Optional ByVal unbuffered As Boolean = False, _
                    Optional ByVal _bufferSize As Integer = (1024 * 1024), _
                    Optional ByVal setLength As Int64 = 0)

            bufferSize = _bufferSize
            Dim options As FileOptions = FileOptions.SequentialScan
            If unbuffered Then options = FileOptions.WriteThrough Or FileOptions.SequentialScan
            readWait = New Threading.ManualResetEvent(False)
            writeWait = New Threading.ManualResetEvent(False)
            finishedWriting = New Threading.ManualResetEvent(False)

            readWait.Set()
            writeWait.Reset()
            finishedWriting.Reset()

            target = New FileStream(dest, _
                                            FileMode.Create, FileAccess.Write, FileShare.None, GetPageSize, options)
            If setLength > 0 Then target.SetLength(setLength)

            totalWritten = 0
            inputBuffer = New MemoryStream(bufferSize)
            running = True
            writing = True
            writeTimer = New Stopwatch

            Dim asyncWriter As New Threading.Thread(AddressOf WriteThread)

            With asyncWriter
                .Priority = Threading.ThreadPriority.Lowest
                .IsBackground = True
                .Name = "AsyncCopy writer"
                .Start()
            End With

        End Sub

        Public Function Write(ByVal someBytes() As Byte, ByVal numToWrite As Integer) As Boolean
            If Not running Then Return False
            If numToWrite < 1 Then Return False

            If numToWrite > inputBuffer.Capacity Then
                Throw New Exception("clsAsyncUnbuffWriter: someBytes() can not be larger then buffer capacity")
            End If

            If (inputBuffer.Length + numToWrite) > inputBuffer.Capacity Then
                If inputBuffer.Length > 0 Then
                    readWait.Reset()
                    writeWait.Set()
                    readWait.WaitOne()
                    If Not running Then Return False
                    inputBuffer.Write(someBytes, 0, numToWrite)
                End If
            Else
                inputBuffer.Write(someBytes, 0, numToWrite)
            End If

            Return True
        End Function

        Private Sub WriteThread()

            Dim bytesThisTime As Int32 = 0
            Dim internalBuffer(bufferSize) As Byte

            writeTimer.Stop()
            writeTimer.Reset()
            writeTimer.Start()

            Do
                writeWait.WaitOne()
                writeWait.Reset()

                bytesThisTime = CInt(inputBuffer.Length)

                Buffer.BlockCopy(inputBuffer.GetBuffer, 0, internalBuffer, 0, bytesThisTime)

                inputBuffer.SetLength(0)
                readWait.Set()

                target.Write(internalBuffer, 0, bytesThisTime)
                totalWritten += bytesThisTime

            Loop While writing

            ' Flush inputBuffer
            If inputBuffer.Length > 0 Then
                bytesThisTime = CInt(inputBuffer.Length)
                Buffer.BlockCopy(inputBuffer.GetBuffer, 0, internalBuffer, 0, bytesThisTime)
                target.Write(internalBuffer, 0, bytesThisTime)
                totalWritten += bytesThisTime
            End If

            running = False
            writeTimer.Stop()

            Try
                target.Close()
                target.Dispose()
            Catch ex As Exception
            End Try

            finishedWriting.Set()
            inputBuffer.Close()
            inputBuffer.Dispose()
            inputBuffer = Nothing
            internalBuffer = Nothing
            target = Nothing

            GC.GetTotalMemory(True)
        End Sub
    End Class

    Public Class Server
        Public errMsg As String

        ' Define the callback delegate type
        Public Delegate Sub ServerCallbackDelegate(ByVal bytes() As Byte, ByVal sessionID As Int32, ByVal dataChannel As Byte)
        'Private Delegate Sub SendQueueDelegate(ByVal bytes() As Byte, ByVal dataChannel As Byte, ByVal sessionID As Int32)

        ' Create Delegate object
        Public ServerCallbackObject As ServerCallbackDelegate
        'Private SendCallback As SendQueueDelegate

        Private Listener As TcpListener
        Private continue_running As Boolean = False
        Private blockSize As UInt16
        Private Port As Integer
        Private localAddr As IPAddress
        Private Mbps As UInt32
        Private newSessionId As Int32 = 0
        Public IsRunning As Boolean = False
        Private serverState As currentState = currentState.stopped

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

            Public Sub AddSession(ByVal theNewSession As SessionCommunications)
                Dim thisTask = System.Threading.Tasks.Task.Factory
                thisTask.StartNew(Sub()
                                      bgAddSession(theNewSession)
                                  End Sub)
            End Sub

            Public Function GetReusableSessionID() As Int32
                Dim sessionNumber As Int32 = -1

                If reusableSessions.TryDequeue(sessionNumber) Then
                    Return sessionNumber
                End If

                Return -1
            End Function

            Private Sub bgAddSession(ByVal theNewSession As SessionCommunications)
                SyncLock sessionLockObject
                    If sessionCollection.Count > theNewSession.sessionID Then
                        sessionCollection.Item(theNewSession.sessionID) = Nothing
                        sessionCollection.Item(theNewSession.sessionID) = theNewSession
                    Else
                        sessionCollection.Add(theNewSession)
                    End If
                End SyncLock
            End Sub

            Public Sub ReuseSessionNumber(ByVal sessionNumber As Int32)
                reusableSessions.Enqueue(sessionNumber)
            End Sub

            Public Function GetSession(ByVal sessionID As Int32, ByRef session As SessionCommunications) As Boolean
                Try
                    session = sessionCollection.Item(sessionID)
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
                        If connectedSession.IsRunning And connectedSession.machineId = MachineID Then
                            session = connectedSession
                            Exit For
                        End If
                    Next
                End SyncLock

                If session Is Nothing Then Return False
                Return True
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
                        'If sessionCollection.Item(i).IsRunning then thisCopy.Add(sessionCollection.Item(i))
                        thisCopy.Add(sessionCollection.Item(i))
                    Next
                End SyncLock

                Return thisCopy
            End Function

            Public Sub ShutDown()
                SyncLock sessionLockObject
                    For Each session As SessionCommunications In sessionCollection
                        Try
                            If session IsNot Nothing AndAlso session.IsRunning Then session.Close()
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
            Public fileWriter As clsAsyncUnbuffWriter
            Public ReceivedFilesFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) & "\ServerReceivedFiles"
            Public userName As String
            Public password As String
            Public paused As Boolean
            Public pauseSent As Boolean
            Public sendQueue As ConcurrentQueue(Of message)
            Public messageIn As MessageInQueue
            Public machineId As String

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
                    Dim lastSuccessfullPump As New Date
                    Dim msg As message = Nothing

                    While running
                        If queue.TryDequeue(msg) Then
                            callBack(msg.bytes, msg.sessionID, msg.dataChannel)
                            lastSuccessfullPump = Now
                        End If

                        If Now > lastSuccessfullPump.AddMilliseconds(5) Then Thread.Sleep(1)
                    End While
                End Sub

            End Class

            Public Sub New(ByVal _theClient As TcpClient, ByVal _sessionID As Int32)
                theClient = _theClient
                sessionID = _sessionID
                paused = False
                pauseSent = False
            End Sub

            Public Sub Close(Optional ByVal wait As Int32 = 500)
                Dim bgThread As New Thread(AddressOf WaitClose)
                bgThread.Start(wait)
            End Sub

            Private Sub WaitClose(ByVal waitmilliseconds As Object)
                Dim wait As Int32 = CType(waitmilliseconds, Int32)
                Thread.Sleep(wait)
                disConnect = True
            End Sub
        End Class

        Private SessionCollection As New Sessions
        Private SessionCollectionLocker As New Object

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
        Public Sub New(ByVal callbackMethod As ServerCallbackDelegate, Optional ByVal _throttledBytesPerSecond As UInt32 = 9000000)

            Mbps = _throttledBytesPerSecond

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

            If textMessage = "" Then
                errMsg = "Your text message must contain some text."
                Return False
            End If

            Return SendBytes(StrToByteArray(textMessage), channel, sessionid, errMsg)
        End Function

        Public Function Start(ByVal prt As Integer, Optional ByRef errorMessage As String = "") As Boolean

            If serverState = currentState.running Then
                errorMessage = "The server is already running."
                Return False
            End If

            serverState = currentState.idle

            Dim listenerThread As New Thread(AddressOf theListener)

            Try
                Port = prt
                localAddr = GetLocalIpAddress()
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

            ServerCallbackObject(StrToByteArray("Server Stopped."), -1, 255)
            serverState = currentState.stopped
        End Sub

        Private Function GetLocalIpAddress() As System.Net.IPAddress
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
                                          .bytes = StrToByteArray("GFR:" & _path),
                                          .sessionID = sessionID,
                                          .dataChannel = 254
                                      })
            Else
                Return False
            End If

            Return True
        End Function

        Public Function SendFile(ByVal _path As String, ByVal sessionID As Int32) As Boolean

            Dim thisSession As SessionCommunications = Nothing
            If SessionCollection.GetSession(sessionID, thisSession) Then
                If thisSession Is Nothing Then Return False
                If Not thisSession.IsRunning Then Return False

                thisSession.sendQueue.Enqueue(New message With { _
                                          .bytes = StrToByteArray("SFR:" & _path),
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

            If sessionID > -1 Then
                Dim targetSession As SessionCommunications = Nothing
                If SessionCollection.GetSession(sessionID, targetSession) Then
                    targetSession.sendQueue.Enqueue(New message With { _
                                                    .bytes = bytes,
                                                    .dataChannel = channel,
                                                    .sessionID = sessionID
                                                })
                    Return True
                End If
            Else
                SessionCollection.Broadcast(New message With { _
                                                    .bytes = bytes,
                                                    .dataChannel = channel,
                                                    .sessionID = sessionID
                                                })
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

                If session.sessionID > -1 Then
                    session.messageIn.queue.Enqueue(New message With { _
                                                                    .bytes = passedData,
                                                                    .dataChannel = dataChannel,
                                                                    .sessionID = session.sessionID
                                                                })
                Else
                    ' These are internal system messages. There is no session associated with them
                    ServerCallbackObject(data, session.sessionID, dataChannel)
                End If

            Catch ex As Exception
                ' An unexpected error.
                Debug.WriteLine("Unexpected error in server\RcvBytes: " & ex.Message)
                Return False
            End Try

            Return True
        End Function

        Private Sub SendExternalSystemMessage(ByVal message As String, ByVal session As SessionCommunications)

            session.SystemBytesToBeSent = StrToByteArray(message)
            session.SystemOutputChannel = 254 ' Text messages / commands on channel 254
            session.SystemBytesToBeSentAvailable = True

        End Sub

        Private Function CheckSessionPermissions(ByVal session As SessionCommunications, ByVal cmd As String) As Boolean
            ' Your security code here...

            Return True
        End Function

        Private Function BeginFileSend(ByVal _path As String, ByVal session As SessionCommunications, ByVal fileLength As Long) As Boolean

            Try

                session.fileReader = New FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None, clsAsyncUnbuffWriter.GetPageSize)
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
                session.fileWriter = New clsAsyncUnbuffWriter(_path, True, 1024 * 256, session.IncomingFileSize)

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
                Dim message As String = BytesToString(bytes)
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
                    session.machineId = message
                    SystemMessage("Session#" & session.sessionID & " MachineID:" & session.machineId)
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
                    Throw New Exception("Gracefull shutdown in progress.")
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
                    session.UserBytesToBeSent = New MemoryStream(msg.bytes)
                    session.UserOutputChannel = msg.dataChannel
                End If
            End If

            If session.disConnect Then
                session.UserBytesToBeSentAvailable = True
                session.UserBytesToBeSent = New MemoryStream(StrToByteArray("close"))
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
                If shutSessionDown Then Throw New Exception("Shutting session down gracefully.")
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
            RcvBytes(StrToByteArray(msg), New SessionCommunications(New TcpClient, -1), 255)
        End Sub

        Private Sub SystemMessage(ByVal MsgText As String, ByVal sessionId As Int32)
            RcvBytes(StrToByteArray(MsgText), New SessionCommunications(New TcpClient, sessionId), 255)
        End Sub

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
            Try
                Listener.BeginAcceptTcpClient(AddressOf HandleAsyncConnection, Listener)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Private Sub HandleAsyncConnection(ByVal res As IAsyncResult)
            Dim client As TcpClient

            If Not StartAccept() Then Exit Sub
            client = Listener.EndAcceptTcpClient(res)

            Dim thisTask = System.Threading.Tasks.Task.Factory
            thisTask.StartNew(Sub()
                                  HandleNewConnection(client)
                              End Sub)
        End Sub

        Private sessionIdIncrementLock As New Object
        Private Sub HandleNewConnection(ByVal client As TcpClient)
            Dim thisSessionId As Int32 = -1
            Dim session As SessionCommunications

            thisSessionId = SessionCollection.GetReusableSessionID
            If thisSessionId = -1 Then
                SyncLock sessionIdIncrementLock
                    thisSessionId = newSessionId
                    newSessionId += 1
                End SyncLock
            End If

            Dim newSession As New Thread(AddressOf Run)
            session = New SessionCommunications(client, thisSessionId)
            newSession.IsBackground = True
            newSession.Name = "Server Session #" & thisSessionId
            newSession.Start(session)

            SessionCollection.AddSession(session)
            'SystemMessage("Connected.")
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

            Try
                ' Create a local Server and Stream objects for clarity.
                Server = session.theClient
                Stream = Server.GetStream()
            Catch ex As Exception
                ' An unexpected error.
                Debug.WriteLine("Could not create local Server or Stream object in server. Message: " & ex.Message)
                Exit Sub
            End Try

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
                bandwidthTimer = Now

                session.IsRunning = True
                SystemMessage("Connected.")

                ' Start the communication loop
                Do

                    ' Throttle network Mbps...
                    bandwidthUsedThisSecond = session.bytesSentThisSecond + session.bytesRecievedThisSecond
                    If bandwidthTimer.AddMilliseconds(250) >= Now And bandwidthUsedThisSecond >= (Mbps / 4) Then
                        While bandwidthTimer.AddMilliseconds(250) > Now
                            Thread.Sleep(0)
                        End While
                    End If
                    If bandwidthTimer.AddMilliseconds(250) <= Now Then
                        bandwidthTimer = Now
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
                        Do
                            ' Read bytes in...
                            bytesread += Stream.Read(theBuffer, bytesread, (packetSize - bytesread))
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
                Debug.WriteLine("Unexpected error in server: " & ex.Message)
            End Try

            Try
                session.fileReader.Close()
            Catch ex As Exception
            End Try

            Try
                Server.Client.Close()
                Server.Client.Blocking = False
            Catch ex As Exception
            End Try

            ' If we're in the middle of receiving a file,
            ' close the filestream, release the memory and
            ' delete the partial file.
            WrapUpIncomingFile(session)

            session.IsRunning = False
            session.machineId = ""
            SystemMessage("Session Stopped. (" & session.sessionID.ToString & ")")
            session.messageIn.Close()
            SessionCollection.ReuseSessionNumber(session.sessionID)
        End Sub

    End Class

    Public Class Client
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
        Private fileWriter As clsAsyncUnbuffWriter
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
        Private ReceivingFile As Boolean = False
        Private IncomingFileName As String
        Private IncomingFileSize As Int64 = 0
        Private outgoingFileSize As UInt64 = 0
        Private outgoingFileName As String
        Private fileBytesRecieved As Int64 = 0
        Private filebytesSent As Int64 = 0
        Private bytesSentThisSecond As Int32 = 0
        Private bytesReceivedThisSecond As Int32 = 0
        Private mbpsOneSecondAverage() As Int32
        Private ReceivedFilesFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) & "\ClientReceivedFiles"
        Private userName As String
        Private password As String
        Private machineId As String

        Public Shared ReadOnly Property AssemblyDirectory() As String
            Get
                Dim codeBase As String = Assembly.GetExecutingAssembly().CodeBase
                Dim uri__1 As New UriBuilder(codeBase)
                Dim path__2 As String = Uri.UnescapeDataString(uri__1.Path)
                Return Path.GetDirectoryName(path__2)
            End Get
        End Property

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
                Dim lastSuccessfullPump As New Date
                Dim msg As message = Nothing

                While running
                    If queue.TryDequeue(msg) Then
                        callBack(msg.bytes, msg.dataChannel)
                        lastSuccessfullPump = Now
                    End If

                    If Now > lastSuccessfullPump.AddMilliseconds(25) Then Thread.Sleep(1)
                End While
            End Sub

        End Class

        Private sendQueue As ConcurrentQueue(Of message)
        Private mbpsSyncObject As New AutoResetEvent(False)
        Private messageIn As MessageInQueue

        Public Function isClientRunning() As Boolean
            Return isRunning
        End Function

        Public Sub SetReceivedFilesFolder(ByVal _path As String)
            ReceivedFilesFolder = _path
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

        Private Function GetIPFromHostname(ByVal hostname As String, Optional ByVal returnLoopbackOnFail As Boolean = True) As System.Net.IPAddress
            Dim addresses() As System.Net.IPAddress

            Try
                addresses = System.Net.Dns.GetHostAddresses(hostname)
            Catch ex As Exception
                If returnLoopbackOnFail Then Return System.Net.IPAddress.Loopback
                Return Nothing
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

        Public Sub New(ByRef callbackMethod As ClientCallbackDelegate)

            blockSize = 10000

            ' Initialize the delegate variable to point to the user's callback method.
            ClientCallbackObject = callbackMethod
        End Sub

        Public Function Connect(ByVal IP_Address As String, ByVal prt As Integer, Optional ByVal newMachineID As String = "", _
                                Optional ByRef errorMessage As String = "") As Boolean

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

            sendQueue = New ConcurrentQueue(Of message)
            messageIn = New MessageInQueue(ClientCallbackObject)

            Dim clientCommunicationThread As New Thread(AddressOf Run)
            clientCommunicationThread.Name = "ClientCommunication"
            clientCommunicationThread.Start()

            If Not newMachineID.Equals("") Then
                SetMachineID(newMachineID)
            End If

            ' Wait for connection...
            While Not isRunning And errMsg = ""
                Thread.Sleep(5)
            End While

            ' Are we connected?
            errorMessage = errMsg
            If Not isRunning Then
                messageIn.Close()
                Return False
            End If
            Return True
        End Function

        Public Sub Close()
            continue_running = False
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
                              .bytes = StrToByteArray("GFR:" & _path),
                              .dataChannel = 254
                          })

        End Sub

        Public Sub SendFile(ByVal _path As String)

            sendQueue.Enqueue(New message With { _
                              .bytes = StrToByteArray("SFR:" & _path),
                              .dataChannel = 254
                          })

        End Sub

        Public Sub CancelIncomingFileTransfer()

            sendQueue.Enqueue(New message With { _
                              .bytes = StrToByteArray("Abort->"),
                              .dataChannel = 254
                          })

            FinishReceivingTheFile()

            Dim killFileThread As New System.Threading.Thread(AddressOf KillIncomingFile)
            killFileThread.Start(ReceivedFilesFolder & "\" & IncomingFileName)

        End Sub

        Private Sub KillIncomingFile(ByVal _path As Object)
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
                              .bytes = StrToByteArray("Abort<-"),
                              .dataChannel = 254
                          })

            StopSendingTheFile()

        End Sub

        Public Sub SetMachineID(ByVal id As String)

            machineId = id
            sendQueue.Enqueue(New message With { _
                              .bytes = StrToByteArray("MachineID:" & id),
                              .dataChannel = 254
                          })
        End Sub

        Public Function GetErrorMessage() As String
            Return errMsg
        End Function

        Public Function SendBytes(ByVal bytes() As Byte, Optional ByVal channel As Byte = 1, Optional ByRef errMsg As String = "") As Boolean

            If channel = 0 Or channel > 250 Then
                errMsg = "Data can not be sent using channel numbers less then 1 or greater then 250."
                Return False
            End If

            If bytes Is Nothing Or bytes.Length = 0 Then
                errMsg = "bytes() must contain more then 0 bytes, and not be nothing."
                Return False
            End If

            sendQueue.Enqueue(New message With { _
                              .bytes = bytes,
                              .dataChannel = channel
                          })

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

            If textMessage = "" Then
                errMsg = "Your text message must contain some text."
                Return False
            End If

            Return SendBytes(StrToByteArray(textMessage), channel, errMsg)
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

        Private Sub SendExternalSystemMessage(ByVal message As String)

            SystemBytesToBeSent = StrToByteArray(message)
            SystemOutputChannel = 254 ' Text messages / commands on channel 254
            SystemBytesToBeSentAvailable = True

        End Sub

        Private Function BeginToReceiveAFile(ByVal _path As String) As Boolean
            Dim readBuffer As Int32 = 0
            ReceivingFile = True
            BeginToReceiveAFile = True
            fileBytesRecieved = 0

            Try
                CreateFolders(_path)
                fileWriter = New clsAsyncUnbuffWriter(_path, True, _
                                1024 * (clsAsyncUnbuffWriter.GetPageSize()), IncomingFileSize)

            Catch ex As Exception
                _path = ex.Message
                ReceivingFile = False
            End Try

            If Not ReceivingFile Then
                Try
                    fileWriter.Close()
                Catch ex As Exception
                End Try
                Return False
            End If
        End Function

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
                    File.Delete(ReceivedFilesFolder & "\" & IncomingFileName)
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

                fileReader = New FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None, clsAsyncUnbuffWriter.GetPageSize)
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
                Dim message As String = BytesToString(bytes)
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
                    tmp = ReceivedFilesFolder & "\" & IncomingFileName
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
                        File.Delete(ReceivedFilesFolder & "\" & IncomingFileName)
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

                ' New queue throttling code
                If message = "pause" Then
                    'sendBuffer.PauseSending()
                End If

                If message = "resume" Then
                    'sendBuffer.ResumeSending()
                End If

                ' Preform gracefull shutdown. 
                If message = "close" Then
                    Throw New Exception("Server initiated gracefull shutdown.")
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
                    UserBytesToBeSent = New MemoryStream(msg.bytes)
                    UserOutputChannel = msg.dataChannel
                End If
            End If

            If theClientIsStopping() Then
                UserBytesToBeSentAvailable = True
                UserBytesToBeSent = New MemoryStream(StrToByteArray("close"))
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
            RcvBytes(StrToByteArray(MsgText), 255)
        End Sub

        ' Check to see if our app is closing (set in FormClosing event)
        Private Function theClientIsStopping() As Boolean

            If continue_running = False Then
                theClientIsStopping = True
            Else
                theClientIsStopping = False
            End If

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

            Dim CPUutil As New CpuMonitor
            CPUutil.Start()

            Try

                Client = New TcpClient
                Client.Connect(IP, Port)

                ' Connection Accepted.
                Stream = Client.GetStream()

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
                Do
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
                            If connectionLossTimer.AddSeconds(3) < Now Then Exit Try

                        Loop While bytesread < packetSize
                        bytesread = 0

                        ' Record bytes read for throttling...
                        bytesReceivedThisSecond += packetSize

                        ' Handle the packet...
                        If dataChannel > 250 Then
                            ' this is an internal system packet
                            HandleIncomingSystemMessages(theBuffer, dataChannel)
                        Else
                            ' Hand data off to the calling thread.
                            RcvBytes(theBuffer, dataChannel)
                        End If

                    End If

                    CalculateMbps(False)

                    ' Measure and display the CPU usage of the client (this thread).
                    If PercentUsage <> CPUutil.ThreadUsage Then
                        PercentUsage = CPUutil.ThreadUsage
                        SystemMessage("" & PercentUsage & "% Thread Usage (" & CPUutil.CPUusage & "% across all CPUs)")
                    End If

                Loop
            Catch ex As Exception
                ' An unexpected error.
                errMsg = "Error in run thread: " & ex.Message
            End Try

            Try
                fileWriter.Close()
            Catch ex As Exception
            End Try

            Try
                CPUutil.StopWatcher()
                Client.Client.Close()
                SystemMessage("Disconnected.")
            Catch ex As Exception
                ' An unexpected error.
                Debug.WriteLine("Unexpected error in Client\theClientIsStopping: " & ex.Message)
            End Try

            WrapUpIncomingFile()

            isRunning = False
            messageIn.Close()
        End Sub

    End Class

    Private Class CpuMonitor

        Private Class Win32
            <DllImport("kernel32.dll")> _
            Public Shared Function GetCurrentThreadId() As Integer
            End Function
        End Class

        Private NativeThreadID As Integer
        Private WatcherRunning As Boolean = False
        Private th1 As Thread
        Private _CPUusage As Double
        Private _ThreadUsage As Double

        Public Sub New()
            NativeThreadID = Win32.GetCurrentThreadId
        End Sub

        Public Sub New(ByVal _NativeThreadID As Int16)
            NativeThreadID = _NativeThreadID
        End Sub

        Private Function GetCurrentNativeThreadID() As Integer
            GetCurrentNativeThreadID = Win32.GetCurrentThreadId
        End Function

        ' Set the native ID of a process thread to be watched, or get your native thread id
        Public Property GetNativeThreadID() As Integer
            Get
                Return GetCurrentNativeThreadID()
            End Get
            Set(ByVal value As Integer)
                NativeThreadID = value
            End Set
        End Property
        Public ReadOnly Property IsRunning() As Boolean
            Get
                Return WatcherRunning
            End Get
        End Property

        Public ReadOnly Property ThreadUsage() As Short
            Get
                Return CShort(_ThreadUsage)
            End Get
        End Property

        Public ReadOnly Property CPUusage() As Short
            Get
                Return CShort(_CPUusage)
            End Get
        End Property

        Public Sub StopWatcher()
            WatcherRunning = False
        End Sub
        Public Sub Start()
            th1 = New System.Threading.Thread(AddressOf StartWatcher)
            th1.Start()
        End Sub
        Private Sub StartWatcher()
            'Dim threadCollection As System.Diagnostics.ProcessThreadCollection
            Dim threadCollection As System.Diagnostics.ProcessThreadCollection
            Dim CPUs, t As Int16
            Dim count, managedThreadID As Integer
            Dim CPUtimeEnd, CPUtimeStart, CurrentTimeSpent, onePercent, average(4), tmp As Double

            CPUs = Convert.ToInt16(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"))
            threadCollection = System.Diagnostics.Process.GetCurrentProcess().Threads
            managedThreadID = 0

            For count = 0 To threadCollection.Count - 1
                If threadCollection.Item(count).Id = NativeThreadID Then
                    managedThreadID = count
                End If
            Next

            If managedThreadID = 0 Then
                ' An unexpected error.
                Debug.WriteLine("Unexpected error in ThreadCPUusageWatcher\StartWatcher: Thread could not be found.")
                Exit Sub
            End If

            WatcherRunning = True
            count = 0

            Try
                onePercent = 2

                Do While WatcherRunning = True

                    ' Check the cpu usage every 200 msecs...
                    CPUtimeStart = threadCollection.Item(managedThreadID).TotalProcessorTime.TotalMilliseconds
                    Thread.Sleep(200)
                    CPUtimeEnd = threadCollection.Item(managedThreadID).TotalProcessorTime.TotalMilliseconds

                    ' Average the thread's CPU usage out over 1 second...
                    CurrentTimeSpent = CPUtimeEnd - CPUtimeStart
                    average(count) = CurrentTimeSpent / onePercent
                    count += 1

                    tmp = 0
                    For t = 0 To 4
                        tmp += average(t)
                    Next
                    tmp = tmp / 5

                    _ThreadUsage = tmp

                    If _ThreadUsage > 100 Then _ThreadUsage = 100
                    _CPUusage = _ThreadUsage / CPUs

                    If count = 5 Then
                        count = 0
                    End If

                Loop
            Catch ex As Exception
                ' An unexpected error.
                Debug.WriteLine("Unexpected error in ThreadCPUusageWatcher\StartWatcher: " & ex.Message)
            End Try
        End Sub

    End Class

    Public Class ServerRequest
        Private serverIp As String
        Private port As Integer
        Private un As String
        Private pw As String
        Private serverReply As String
        Private replyComplete As Boolean
        Private request As String
        Private thisReplyList As List(Of String)

        Public Sub New(ByVal _serverIpAddress As String, ByVal _serverPort As Integer)
            serverIp = _serverIpAddress
            port = _serverPort
            serverReply = ""
            replyComplete = False
        End Sub

        Public Sub ImportReplyString(ByVal replyString As String)
            serverReply = replyString
        End Sub

        Public Sub AddRequestItem(ByVal key As String, ByVal value As String, Optional ByVal separator As String = vbCrLf)

            If request = "" Then request = separator

            If key <> "" And value <> "" Then
                request += key & "=" & value & separator
            End If
        End Sub

        Public Function Send(Optional ByVal timeoutSeconds As Integer = 5, Optional ByRef errMsg As String = "") As Boolean
            If request.Length > 0 Then
                If thisReplyList IsNot Nothing Then thisReplyList.Clear()
                serverReply = ""
                Dim reply As String = SendRequest(request, timeoutSeconds, errMsg)
                If reply <> "N/C" And reply <> "N/R" Then
                    Return True
                Else
                    If errMsg = "" Then errMsg = "Reply from " & serverIp.ToString & " was: " & reply
                End If
            Else
                errMsg = "Request string can not be empty"
            End If

            Return False
        End Function

        Public Function GetReplyStringItems(Optional ByVal separator As String = vbCrLf) As List(Of String)
            If thisReplyList Is Nothing Then thisReplyList = New List(Of String)

            If thisReplyList.Count = 0 Then
                Try
                    If serverReply.Length > 0 Then
                        Dim theseItems() As String = Split(serverReply, separator)
                        If theseItems.Length > 0 Then thisReplyList.AddRange(theseItems)
                    End If
                Catch ex As Exception
                End Try
            End If

            'Dim tmp As String = ""
            'For Each item As String In thisReplyList
            '    tmp += item & " / "
            'Next

            'log("Replystrings found:" & tmp, "Bric Video Service", EventLogEntryType.Information)

            Return thisReplyList
        End Function

        Public Function GetReplyStringItem(ByVal key As String, Optional ByVal separator As String = vbCrLf) As String
            Dim keyValueItems As List(Of String) = GetReplyStringItems(separator)
            Dim keyValuePair() As String

            If keyValueItems.Count > 0 Then
                For Each item As String In keyValueItems

                    Try
                        keyValuePair = Split(item, "=")
                        If keyValuePair(0) = key Then
                            'log("Asked for:" & key & ", returning:" & keyValuePair(1), "Bric Video Service", EventLogEntryType.Information)
                            Return keyValuePair(1)
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End If

            'log("Asked for:" & key & ", returning ''", "Bric Video Service", EventLogEntryType.Information)

            Return ""
        End Function

        Public Function GetReplyStringItemAsShort(ByVal key As String, Optional ByVal separator As String = vbCrLf) As Short
            Dim value As String = GetReplyStringItem(key, separator)

            Try
                If value <> "" Then Return Convert.ToInt16(value)
            Catch ex As Exception
            End Try

            Return Nothing
        End Function

        Public Function GetReplyStringItemAsDate(ByVal key As String, Optional ByVal separator As String = vbCrLf) As Date
            Dim value As String = GetReplyStringItem(key, separator)

            Try
                If value <> "" Then Return Convert.ToDateTime(value)
            Catch ex As Exception
            End Try

            Return Nothing
        End Function

        Public Function GetReplyString() As String
            Return serverReply
        End Function

        ' Send "cmd=stuff here" & vbCrLf & "something else=more stuff" & vbCrLf... 
        Public Function SendRequest(ByVal requestString As String, Optional ByVal timeoutSeconds As Integer = 5, _
                                    Optional ByRef errMsg As String = "") As String

            ' Handle TCP communication here:
            Dim client As New Tcp.Comm.Client(AddressOf ClientCallback)

            ' Attempt to connect to the server. If not - return N/C (No Connection)
            If Not client.Connect(serverIp, port, "", errMsg) Then Return "N/C"

            ' Send our request, and wait for a reply...
            client.SendBytes(StrToByteArray("<text>" & requestString & "</text>"), 10)

            Dim timeOut As Date = Now
            While Not replyComplete
                If Now > timeOut.AddSeconds(timeoutSeconds) Then Exit While ' Bail after timeoutSeconds seconds.
                Threading.Thread.Sleep(5)
            End While

            client.Close()

            If serverReply.Length > 0 Then
                ' If we got a good reply...
                If replyComplete Then ' Remove the tags.
                    serverReply = serverReply.Replace("<text>", "")
                    serverReply = serverReply.Replace("</text>", "")
                End If

                Return serverReply
            End If

            Return "N/R"
        End Function

        Private Sub ClientCallback(ByVal bytes() As Byte, ByVal dataChannel As Integer)

            If dataChannel = 10 Then
                ' Our data arrived.
                serverReply += BytesToString(bytes)
                If serverReply.Contains("</text>") Then replyComplete = True
            End If

        End Sub

    End Class
End Class

