Imports Microsoft.VisualBasic
Imports System.Security


Module functions
    Dim string_temp As String
    Dim configuration_file As String = "\config.ini"


    Function fn_create_directory(ByVal directory As String)

        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Function


    Function fn_delete_directory(ByVal directory As String)
        If System.IO.Directory.Exists(directory) Then System.IO.Directory.Delete(directory, True)
    End Function

    Function fn_check_directory(ByVal directory As String) As Boolean
        fn_check_directory = System.IO.Directory.Exists(directory)
    End Function


    Function fn_load_setting() As Boolean
        fn_load_setting = False

        My.Forms.Main_Form.lb_user_list.Items.Clear()
        If fn_check_file(Application.StartupPath() + configuration_file) = False Then
            My.Forms.Main_Form.TabControl.SelectedIndex = 0
            My.Forms.Main_Form.txt_windows_terminal_server.Focus()
            MsgBox("Konfigurační soubor neexistuje, Proveďte konfiguraci")
        Else
            Dim objReader As New System.IO.StreamReader(Application.StartupPath() + configuration_file, True)
            string_temp = objReader.ReadLine()
            Dim count = 0
            Do While Not (string_temp Is Nothing)
                If count = 0 Then
                    My.Forms.Main_Form.txt_windows_terminal_server.Text = string_temp.ToString
                Else
                    My.Forms.Main_Form.lb_user_list.Items.Add(string_temp.ToString)
                End If

                string_temp = objReader.ReadLine()
                count = count + 1
            Loop
            objReader.Close()
            If count > 0 Then
                fn_load_setting = True
            Else
                My.Forms.Main_Form.TabControl.SelectedIndex = 0
                MsgBox("Konfigurační soubor neexistuje, Proveďte konfiguraci")
            End If
        End If
    End Function


    Function fn_check_file(ByVal file As String) As Boolean
        fn_check_file = System.IO.File.Exists(file)
    End Function


    Function fn_create_file(ByVal file As String) As Boolean
        If Not System.IO.File.Exists(file) Then
            System.IO.File.Create(file).Close()
        End If

        If fn_check_file(file) = True Then
            fn_create_file = True
        Else
            fn_create_file = False
        End If
    End Function


    Function fn_delete_file(ByVal file As String) As Boolean
        System.IO.File.Delete(file)

        If fn_check_file(file) = False Then
            fn_delete_file = True
        Else
            fn_delete_file = False
        End If
    End Function


    Function fn_save_file_setting() As Boolean
        fn_save_file_setting = False
        fn_delete_file(Application.StartupPath() + configuration_file)
        fn_create_file(Application.StartupPath() + configuration_file)

        Dim objWriter As New System.IO.StreamWriter(Application.StartupPath() + configuration_file, True)
        objWriter.WriteLine(My.Forms.Main_Form.txt_windows_terminal_server.Text)

        For i = 0 To (My.Forms.Main_Form.lb_user_list.Items.Count - 1) Step 1
            If CStr(My.Forms.Main_Form.lb_user_list.Items.Item(i).ToString) <> "" Then
                objWriter.WriteLine(My.Forms.Main_Form.lb_user_list.Items.Item(i).ToString)
            End If
        Next
        objWriter.Close()
        fn_save_file_setting = True
        If fn_check_file(Application.StartupPath() + configuration_file) = True Then
            MsgBox("Uložení proběhlo úspěšně")
            Application.Restart()
        Else
            MsgBox("Uložení se nezdařilo")
        End If
    End Function


    Public Function run_command(ByVal type As String, ByVal terminal_user As String, ByVal server_name As String) As Boolean
        run_command = False
        'administrator password
        Dim pass As String = "password"
        Dim passString As New Security.SecureString()
        For Each c As Char In pass
            passString.AppendChar(c)
        Next
        Try
            Dim foo As New System.Diagnostics.Process
            foo.StartInfo.WorkingDirectory = "c:\"
            foo.StartInfo.FileName = "cmd.exe"
            If (type = "SEARCH") Then
                foo.StartInfo.Arguments = "%comspec% /C qwinsta " + terminal_user + " /SERVER:" + server_name + " "
            End If
            If (type = "LOGOFF") Then
                foo.StartInfo.Arguments = "%comspec% /C rwinsta " + terminal_user + " /SERVER:" + server_name + " "
            End If
            foo.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            foo.StartInfo.CreateNoWindow = True
            foo.StartInfo.RedirectStandardOutput = True
            foo.StartInfo.RedirectStandardInput = True
            foo.StartInfo.RedirectStandardError = True
            foo.StartInfo.UseShellExecute = False
            'domain name
            foo.StartInfo.Domain = "ldap"
            'administrator name
            foo.StartInfo.UserName = "administrator"
            foo.StartInfo.Password = passString
            foo.StartInfo.LoadUserProfile = True
            foo.EnableRaisingEvents = True
            foo.Start()
            Dim sr As System.IO.StreamReader
            sr = foo.StandardOutput
            My.Forms.Main_Form.terminal_result = sr.ReadToEnd()
            foo.WaitForExit()
            run_command = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


End Module