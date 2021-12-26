Imports System.Drawing.Printing
Imports System.IO
Imports System.Drawing
Imports System.Runtime.InteropServices

Module Functions

    Public Function DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings

        Try
            DefaultPrinterName = oPS.PrinterName
        Catch ex As System.Exception
            DefaultPrinterName = ""
        Finally
            oPS = Nothing
        End Try
    End Function


    Function fn_check_file(ByVal file As String) As Boolean
        fn_check_file = System.IO.File.Exists(file)
        If fn_check_file = False Then show_message(1)
    End Function


    Function show_message(mess_no As Integer) 'hidden/full/hw
        If mess_no = 1 And Main.params_array(3, 1) = "full" Then Console.WriteLine("file doesn't exist")
        If mess_no = 2 And (Main.params_array(3, 1) = "full" Or Main.params_array(3, 1) = "hw") Then Console.WriteLine("you doesn't have default printer")
        If mess_no = 3 And Main.params_array(3, 1) = "full" Then Console.WriteLine("your default printer is: " + DefaultPrinterName.ToString)
        If mess_no = 3 And Main.params_array(3, 1) = "full" And String.IsNullOrEmpty(params_array(1, 1)) = False Then Console.WriteLine("your selected printer is: " + params_array(1, 1).Replace("*", "Default printer"))
        If (mess_no = 3 And Main.params_array(3, 1) = "full" Or Main.params_array(3, 1) = "hw") And String.IsNullOrEmpty(params_array(0, 1)) = False Then Console.WriteLine("your file is: " + params_array(0, 1))
        If mess_no = 3 And Main.params_array(3, 1) = "full" And String.IsNullOrEmpty(params_array(2, 1)) = False Then Console.WriteLine("your string is: " + params_array(2, 1))
        If (mess_no = 3 And Main.params_array(3, 1) = "full" Or Main.params_array(3, 1) = "hw") And String.IsNullOrEmpty(params_array(4, 1)) = False Then Console.WriteLine("your Data type is: " + params_array(4, 1))
        If (mess_no = 3 And Main.params_array(3, 1) = "full" Or Main.params_array(3, 1) = "hw") And String.IsNullOrEmpty(params_array(5, 1)) = False Then Console.WriteLine("your docname is: " + params_array(5, 1))

        If mess_no = 4 Then
            Console.Write("Press any key to continue . . . ")
            Console.ReadKey(True)
        End If

    End Function



    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Long) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterW", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Int32, ByRef pDI As Component1.DOCINFOW) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="WritePrinter", _
       SetLastError:=True, CharSet:=CharSet.Unicode, _
       ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByRef dwWritten As Int32) As Boolean
    End Function



    Public Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByVal doc_name As String, ByVal datatype As String) As Boolean
        Dim hPrinter As IntPtr      ' The printer handle.
        Dim dwError As Int32        ' Last error - in case there was trouble.
        Dim di As Component1.DOCINFOW          ' Describes your document (name, port, data type).
        Dim dwWritten As Int32      ' The number of bytes written by WritePrinter().
        Dim bSuccess As Boolean     ' Your success code.

        ' Set up the DOCINFO structure.

        With di
            .pDocName = doc_name
            .pDataType = datatype
            '.pDataType = "RAW"
            '.pDataType = "RAW [FF appended]"
            '.pDataType = "RAW [FF auto]"
            '.pDataType = "Text"
        End With
        ' Assume failure unless you specifically succeed.
        bSuccess = False
        Try
            If OpenPrinter(szPrinterName, hPrinter, 0) Then
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        ' Write your printer-specific bytes to the printer.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                        EndPagePrinter(hPrinter)
                    End If
                    EndDocPrinter(hPrinter)
                End If
                ClosePrinter(hPrinter)
            End If
        Catch ex As Exception

        End Try

        ' If you did not succeed, GetLastError may give more information
        ' about why not.
        If bSuccess = False Then
            dwError = Marshal.GetLastWin32Error()
            Console.WriteLine(dwError)
        End If
        Return bSuccess
    End Function ' SendBytesToPrinter()


    ' SendFileToPrinter()
    ' When the function is given a file name and a printer name, 
    ' the function reads the contents of the file and sends the
    ' contents to the printer.
    ' Presumes that the file contains printer-ready data.
    ' Shows how to use the SendBytesToPrinter function.
    ' Returns True on success or False on failure.
    Public Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
        ' Open the file.
        Dim fs As New FileStream(szFileName, FileMode.Open)
        ' Create a BinaryReader on the file.
        Dim br As New BinaryReader(fs)
        ' Dim an array of bytes large enough to hold the file's contents.
        Dim bytes(fs.Length) As Byte
        Dim bSuccess As Boolean
        ' Your unmanaged pointer.
        Dim pUnmanagedBytes As IntPtr

        ' Read the contents of the file into the array.
        bytes = br.ReadBytes(fs.Length)
        ' Allocate some unmanaged memory for those bytes.
        pUnmanagedBytes = Marshal.AllocCoTaskMem(fs.Length)
        ' Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, fs.Length)
        ' Send the unmanaged bytes to the printer.
        bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fs.Length, params_array(5, 1), params_array(4, 1))
        ' Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes)
        Return bSuccess
    End Function ' SendFileToPrinter()

    ' When the function is given a string and a printer name,
    ' the function sends the string to the printer as raw bytes.

    Public Function SendStringToPrinter(ByVal szPrinterName As String, ByVal szString As String)
        Dim pBytes As IntPtr
        Dim dwCount As Int32
        ' How many characters are in the string?
        dwCount = szString.Length()
        ' Assume that the printer is expecting ANSI text, and then convert
        ' the string to ANSI text.
        pBytes = Marshal.StringToCoTaskMemAnsi(szString)
        ' Send the converted ANSI string to the printer.
        SendBytesToPrinter(szPrinterName, pBytes, dwCount, params_array(5, 1), params_array(4, 1))
        Marshal.FreeCoTaskMem(pBytes)
    End Function







End Module

