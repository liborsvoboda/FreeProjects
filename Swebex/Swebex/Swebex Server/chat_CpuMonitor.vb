'Imports System.Threading
'Imports System.Runtime.InteropServices

Public Class chat_CpuMonitor

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