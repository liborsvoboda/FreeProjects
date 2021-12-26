'Imports System.Threading
'Imports System.Net
'Imports System.Net.Sockets
'Imports System.IO
'Imports System.Collections.Concurrent
'Imports System.Reflection
'Imports System.Runtime.InteropServices
'Imports System.Diagnostics
'Imports System.Collections.Generic

Public Class chat_AsyncUnbuffWriter

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

    Public Function Write(ByVal someBytes() As Byte, numToWrite As Integer) As Boolean
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

        inputBuffer.Close()
        finishedWriting.Set()
        inputBuffer.Dispose()
        inputBuffer = Nothing
        internalBuffer = Nothing
        target = Nothing

        GC.GetTotalMemory(True)
    End Sub
End Class
