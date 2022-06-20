Imports System.Drawing.Printing
Imports System.IO
Imports System.Drawing
Imports System.Runtime.InteropServices


Module Main

    Public params_array(6, 1) As String
    Dim default_printer As New PrintDocument
    Dim param_no As Integer = 100

    Public Sub Main(ByVal sArgs() As String)
        'sArgs.Length - parameters count
        params_array(0, 0) = "file"
        params_array(1, 0) = "printer"
        params_array(2, 0) = "string"
        params_array(3, 0) = "statusinfo"
        params_array(4, 0) = "datatype"
        params_array(5, 0) = "docname"
        params_array(6, 0) = "pause"



        If sArgs.Length = 0 Then ' without params
            Console.WriteLine("")
            Console.WriteLine("param list, must me between ' ")
            Console.WriteLine("params: 'file:' 'printer:' 'string:' 'statusinfo:' 'datatype:' 'docname:'")
            Console.WriteLine("")
            Console.WriteLine("'file:fullfilepath'")
            Console.WriteLine("example 'file:\\xxxx\xxx.txt' or 'file:C:\dir\xxxxx.txt' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("'printer:*' or printername or netpath")
            Console.WriteLine("example 'printer:*' - is default printer, 'printer:printername' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("'string:xxxxxx'")
            Console.WriteLine("example 'string:hello word' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("'datatype:RAW/Text' default is RAW")
            Console.WriteLine("example 'datatype:RAW' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("'docname:xxxx' default is namefile")
            Console.WriteLine("example 'docname:myfilename' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("statusinfo:hidden/full/hw  default is hidden")
            Console.WriteLine("example 'statusinfo:full' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("pause:yes  default is no")
            Console.WriteLine("example 'pause:yes' OPTIONAL")
            Console.WriteLine("")
            Console.WriteLine("command exmaples:")
            Console.WriteLine("direct_print.exe 'file:c:\log.txt' 'datatype:Text' 'printer:*' 'statusinfo:full' ")
            Console.WriteLine("direct_print.exe 'file:c:\log.txt' 'datatype:Text' 'printer:\\192.168.1.1\HP3015' 'statusinfo:full' ")
            Console.Write("Press any key to continue . . . ")
            Console.ReadKey(True)
        Else
            Dim i As Integer = 0

            While i < sArgs.Length

                'START OF PARAMS READING

                If (sArgs(i).Contains("file:") And param_no = 100) Or param_no = 1 Then
                    If String.IsNullOrEmpty(params_array(1, 1)) = True Then
                        params_array(0, 1) = LCase(sArgs(i).ToString.Replace("file:", "").Replace("'", ""))
                    Else
                        params_array(0, 1) = params_array(0, 1) + " " + LCase(sArgs(i).ToString.Replace("file:", "").Replace("'", ""))
                    End If
                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 1
                    End If
                End If

                If (sArgs(i).Contains("printer:") And param_no = 100) Or param_no = 2 Then
                    If String.IsNullOrEmpty(params_array(1, 1)) = True Then
                        params_array(1, 1) = sArgs(i).ToString.Replace("printer:", "").Replace("'", "")
                    Else
                        params_array(1, 1) = params_array(1, 1) + " " + sArgs(i).ToString.Replace("printer:", "").Replace("'", "")
                    End If

                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 2
                    End If
                End If

                If (sArgs(i).Contains("string:") And param_no = 100) Or param_no = 3 Then

                    If String.IsNullOrEmpty(params_array(2, 1)) = True Then
                        params_array(2, 1) = LCase(sArgs(i).ToString.Replace("string:", "").Replace("'", ""))
                    Else
                        params_array(2, 1) = params_array(2, 1) + " " + LCase(sArgs(i).ToString.Replace("string:", "").Replace("'", ""))
                    End If

                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 3
                    End If
                End If

                If (sArgs(i).Contains("statusinfo:") And param_no = 100) Or param_no = 4 Then
                    If String.IsNullOrEmpty(params_array(3, 1)) = True Then
                        params_array(3, 1) = LCase(sArgs(i).ToString.Replace("statusinfo:", "").Replace("'", ""))
                    Else
                        params_array(3, 1) = params_array(3, 1) + " " + LCase(sArgs(i).ToString.Replace("statusinfo:", "").Replace("'", ""))
                    End If

                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 4
                    End If
                End If

                If (sArgs(i).Contains("datatype:") And param_no = 100) Or param_no = 5 Then
                    If String.IsNullOrEmpty(params_array(4, 1)) = True Then
                        params_array(4, 1) = sArgs(i).ToString.Replace("datatype:", "").Replace("'", "")
                    Else
                        params_array(4, 1) = params_array(4, 1) + " " + sArgs(i).ToString.Replace("datatype:", "").Replace("'", "")
                    End If

                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 5
                    End If
                End If


                If (sArgs(i).Contains("docname:") And param_no = 100) Or param_no = 6 Then
                    If String.IsNullOrEmpty(params_array(5, 1)) = True Then
                        params_array(5, 1) = sArgs(i).ToString.Replace("docname:", "").Replace("'", "")
                    Else
                        params_array(5, 1) = params_array(5, 1) + " " + sArgs(i).ToString.Replace("docname:", "").Replace("'", "")
                    End If

                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 6
                    End If
                End If

                If (sArgs(i).Contains("pause:") And param_no = 100) Or param_no = 7 Then
                    If String.IsNullOrEmpty(params_array(6, 1)) = True Then
                        params_array(6, 1) = LCase(sArgs(i).ToString.Replace("pause:", "").Replace("'", ""))
                    Else
                        params_array(6, 1) = params_array(6, 1) + " " + LCase(sArgs(i).ToString.Replace("pause:", "").Replace("'", ""))
                    End If

                    If sArgs(i)(sArgs(i).Length - 1) = "'" Then
                        param_no = 100
                    Else
                        param_no = 7
                    End If
                End If

                i = i + 1
            End While

            'END OF PARAMS READING


            'OPTIONAL PARAMS
            'PRINTER
            If String.IsNullOrEmpty(params_array(1, 1)) = True Then params_array(1, 1) = DefaultPrinterName().ToString
            If String.IsNullOrEmpty(params_array(1, 1)) = False Then
                If params_array(1, 1).ToString = "*" Then params_array(1, 1) = DefaultPrinterName().ToString
            End If

            'STATUSINFO
            If String.IsNullOrEmpty(params_array(3, 1)) = True Then params_array(3, 1) = "hidden"

            'DATATYPE
            If String.IsNullOrEmpty(params_array(4, 1)) = True Then params_array(4, 1) = "RAW"

            'DOCNAME
            If String.IsNullOrEmpty(params_array(5, 1)) = True And String.IsNullOrEmpty(params_array(0, 1)) = False Then
                params_array(5, 1) = Path.GetFileName(params_array(0, 1))
            ElseIf String.IsNullOrEmpty(params_array(5, 1)) = True Then
                params_array(5, 1) = "String_print"
            End If

            'PAUSE
            If String.IsNullOrEmpty(params_array(6, 1)) = True Then params_array(6, 1) = "no"

            If String.IsNullOrEmpty(DefaultPrinterName().ToString) Then
                If String.IsNullOrEmpty(params_array(3, 1)) = True Then show_message(2)
            End If


            Try


                'print string
                If String.IsNullOrEmpty(params_array(2, 1)) = False And String.IsNullOrEmpty(params_array(1, 1)) = False Then
                    SendStringToPrinter(params_array(1, 1).ToString, params_array(2, 1).ToString)
                End If


                'print file
                If String.IsNullOrEmpty(params_array(0, 1)) = False And String.IsNullOrEmpty(params_array(1, 1)) = False Then
                    SendFileToPrinter(params_array(1, 1).ToString, params_array(0, 1))
                End If


                If params_array(3, 1) = "full" Or params_array(3, 1) = "hw" Then show_message(3)
                If params_array(6, 1) = "yes" Then show_message(4)

            Catch ex As Exception

            End Try

        End If

    End Sub



End Module




