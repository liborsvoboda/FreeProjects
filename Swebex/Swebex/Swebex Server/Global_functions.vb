'Imports System
'Imports System.Net
'Imports System.Net.Sockets
'Imports System.Runtime.Serialization.Formatters.Binary
'Imports System.Runtime.Serialization
'Imports System.IO
'Imports System.DirectoryServices



Public Module Global_functions


    Function fn_GetUserDomain() As String
        If TypeOf My.User.CurrentPrincipal Is 
          System.Security.Principal.WindowsPrincipal Then
            ' My.User is using Windows authentication.
            ' The name format is DOMAIN\USERNAME.
            Dim parts() As String = Split(My.User.Name, "\")
            Dim domain As String = parts(0)
            Return domain
        Else
            ' My.User is using custom authentication.
            Return ""
        End If
    End Function



    Function fn_GetUserName() As String
        If TypeOf My.User.CurrentPrincipal Is 
          System.Security.Principal.WindowsPrincipal Then
            ' The application is using Windows authentication.
            ' The name format is DOMAIN\USERNAME.
            Dim parts() As String = Split(My.User.Name, "\")
            Dim username As String = parts(1)
            Return username
        Else
            ' The application is using custom authentication.
            Return My.User.Name
        End If
    End Function



    Function fn_GetIPv4Address() As String
        fn_GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                If (fn_GetIPv4Address = String.Empty) Then
                    fn_GetIPv4Address = ipheal.ToString()
                Else
                    fn_GetIPv4Address += ";" + ipheal.ToString()
                End If

            End If
        Next

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


    Function fn_DoGetHostEntry(hostName As [String])

        Dim host As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(hostName)

        Console.WriteLine("GetHostEntry(" + hostName + ") returns: ")

        Dim ip As System.Net.IPAddress() = host.AddressList

        Dim index As Integer
        For index = 0 To ip.Length - 1
            Console.WriteLine(ip(index))
        Next index
    End Function



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




    Function fn_check_file(ByVal file As String) As Boolean
        fn_check_file = System.IO.File.Exists(file)
    End Function


    Function fn_copy_file(ByVal source_file As String, ByVal destination_file As String) As Boolean
        fn_copy_file = False
        Try
            If System.IO.File.Exists(source_file) Then
                My.Computer.FileSystem.CopyFile(source_file, destination_file)
            End If
            If fn_check_file(destination_file) = True Then
                fn_copy_file = True
            End If
        Catch ex As Exception
        End Try
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




    Function fn_set_cursor(ByVal cursor_type As Cursor)
        Cursor.Current = cursor_type
    End Function




    Function fn_remove_bracket_symbol(ByVal variable As String) As String
        variable = variable.Replace("[", "")
        variable = variable.Replace("]", "")
        fn_remove_bracket_symbol = variable
    End Function

    Function fn_insert_bracket_symbol(ByVal variable As String) As String
        If variable <> Nothing Then variable = "[" + variable + "]"
        fn_insert_bracket_symbol = variable
    End Function



    Function fn_show_file(ByVal file As String) As Boolean
        Dim file_open As New ProcessStartInfo
        file_open.FileName = file
        file_open.UseShellExecute = True
        Process.Start(file_open)
    End Function




    Function fn_detect_file_encoding(ByVal FileName As String) As System.Text.Encoding


        Dim enc As String = ""
        If System.IO.File.Exists(FileName) Then
            Dim filein As New System.IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
            If (filein.CanSeek) Then
                Dim bom(4) As Byte
                filein.Read(bom, 0, 4)
                'EF BB BF       = utf-8
                'FF FE          = ucs-2le, ucs-4le, and ucs-16le
                'FE FF          = utf-16 and ucs-2
                '00 00 FE FF    = ucs-4
                If (((bom(0) = &HEF) And (bom(1) = &HBB) And (bom(2) = &HBF)) Or _
                    ((bom(0) = &HFF) And (bom(1) = &HFE)) Or _
                    ((bom(0) = &HFE) And (bom(1) = &HFF)) Or _
                    ((bom(0) = &H0) And (bom(1) = &H0) And (bom(2) = &HFE) And (bom(3) = &HFF))) Then
                    enc = "Unicode"
                Else
                    enc = "ASCII"
                End If
                'Position the file cursor back to the start of the file
                filein.Seek(0, System.IO.SeekOrigin.Begin)
                ' Do more stuff
            End If
            filein.Close()
        End If
        If enc = "Unicode" Then
            Return System.Text.Encoding.UTF8
        Else
            Return System.Text.Encoding.Default
        End If
    End Function




    Function fn_temp_directory()
        If fn_check_directory(Application.StartupPath() + "\temp") = True Then
            fn_delete_directory(Application.StartupPath() + "\temp")
        End If
        fn_create_directory(Application.StartupPath() + "\temp")
    End Function





    Function fn_run_vbscript_files()
        Dim source_folder As New System.IO.DirectoryInfo(Application.StartupPath() + "\vbscripts")
        Dim all_files As IO.FileInfo() = source_folder.GetFiles()
        Dim each_file As IO.FileInfo
        For Each each_file In all_files
            If InStr(each_file.ToString, ".vbs") <> 0 Then
                run_command(each_file.ToString)
            End If
        Next
        Application.Exit()
    End Function




    Function run_command(ByVal file As String)

        Dim pass As String = "password"
        Dim passString As New System.Security.SecureString()
        For Each c As Char In pass
            passString.AppendChar(c)
        Next
        Try
            Dim foo As New System.Diagnostics.Process
            foo.StartInfo.WorkingDirectory = "c:\"
            foo.StartInfo.FileName = "cmd.exe"
            foo.StartInfo.Arguments = "%comspec% /C cscript.exe //B //Nologo " + Application.StartupPath() + "\" + file
            ''MsgBox("%comspec% /C cscript.exe //B //Nologo " + Application.StartupPath() + "\" + file)
            foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            foo.StartInfo.CreateNoWindow = True
            foo.StartInfo.RedirectStandardOutput = True
            foo.StartInfo.RedirectStandardInput = True
            foo.StartInfo.RedirectStandardError = True
            foo.StartInfo.UseShellExecute = False
            'foo.StartInfo.Domain = "ldap"
            'foo.StartInfo.UserName = "administrator"
            foo.StartInfo.Password = passString
            foo.StartInfo.LoadUserProfile = True
            foo.EnableRaisingEvents = True
            foo.Start()
            foo.WaitForExit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function



    Function fn_get_file_line_count(ByVal file As String) As Double
        fn_get_file_line_count = 0
        If fn_check_file(file) = True Then
            Dim objReader As New System.IO.StreamReader(file, True)
            Dim lineCount As Integer = System.Text.RegularExpressions.Regex.Split(objReader.ReadToEnd(), Environment.NewLine).Length
            objReader.Close()
            objReader.Dispose()
            fn_get_file_line_count = lineCount
        End If
    End Function




    Function fn_load_data_file_to_array(ByVal file As String, ByVal separator As String, ByVal remove_bracket As Boolean) As Boolean
        fn_load_data_file_to_array = False

        If fn_check_file(file) = True Then

            Dim objReader As New System.IO.StreamReader(file, fn_detect_file_encoding(file))
            Try

                Dim record = objReader.ReadLine()
                ReDim My.Forms.main_form.file_array(fn_get_file_line_count(file) - 1, record.Split(separator).Length)
                Dim parts()
                My.Forms.main_form.line_no = 0
                Dim line_step = 0
                Do While Not (record Is Nothing And line_step < fn_get_file_line_count(file))
                    record = record.Trim() 'kontrola jestli neni pouze prazdny
                    If record.Length > 0 And record.Substring(0, 1) <> "#" Then
                        parts = Split(record, separator)
                        My.Forms.main_form.split_pointer = 0
                        For Each i As String In parts

                            If remove_bracket = True Then
                                My.Forms.main_form.file_array(My.Forms.main_form.line_no, My.Forms.main_form.split_pointer) = fn_remove_bracket_symbol(i.ToString)
                                fn_load_data_file_to_array = True
                            Else
                                My.Forms.main_form.file_array(My.Forms.main_form.line_no, My.Forms.main_form.split_pointer) = i.ToString
                                fn_load_data_file_to_array = True
                            End If
                            My.Forms.main_form.split_pointer += 1
                        Next

                        My.Forms.main_form.line_no += 1
                    End If

                    record = objReader.ReadLine()
                    line_step += 1
                Loop
                objReader.Close()
            Catch ex As Exception
                objReader.Close()
            End Try
        Else
            MessageBox.Show(fn_translate("cannot_load_dictionary_file"))
        End If
    End Function





    Function fn_save_data_file_from_array(ByVal file As String, ByVal separator As String, ByVal insert_bracket As Boolean) As Boolean
        Dim temp_line As String
        '        fn_save_data_file_from_array(language_file, ";", False, "")
        Dim objwriter As New System.IO.StreamWriter(file, False, System.Text.Encoding.UTF8)

        For i = 0 To My.Forms.main_form.file_array.GetLength(0) - 1
            temp_line = ""
            For j = 0 To (My.Forms.main_form.file_array.Length / My.Forms.main_form.file_array.GetLength(0)) - 2
                If temp_line.Length > 0 Then
                    temp_line += separator + My.Forms.main_form.file_array(i, j)
                Else
                    If insert_bracket = True Then
                        temp_line += fn_insert_bracket_symbol(My.Forms.main_form.file_array(i, j))
                    Else
                        temp_line += My.Forms.main_form.file_array(i, j)
                    End If
                End If
            Next
            If temp_line.Length > 0 Then objwriter.WriteLine(temp_line)
        Next
        objwriter.Close()





    End Function



    Function fn_load_languages() As Boolean
        fn_load_languages = False
        Try
            My.Forms.main_form.cb_languages.Items.Clear()
            For i = 1 To My.Forms.main_form.language_array.Length / My.Forms.main_form.language_array.GetLength(0)
                If My.Forms.main_form.language_array(0, i).Length > 0 Then
                    My.Forms.main_form.cb_languages.Items.Add(My.Forms.main_form.language_array(0, i))
                End If
            Next
        Catch ex As Exception
        End Try


        Try
            My.Forms.main_form.cb_word.Items.Clear()
            For i = 1 To My.Forms.main_form.language_array.GetLength(0)
                If My.Forms.main_form.language_array(i, 0).Length > 0 Then
                    My.Forms.main_form.cb_word.Items.Add(My.Forms.main_form.language_array(i, 0))
                End If
            Next
        Catch ex As Exception
        End Try

    End Function




    Function fn_translate(ByVal word As String) As String
        Dim language_index As Integer = Convert.ToInt32(My.Forms.main_form.lb_default_language.SelectedIndex + 1)
        Dim system_word = word
        Try
            For temp_i = 0 To My.Forms.main_form.language_array.GetLength(0)
                If My.Forms.main_form.language_array(temp_i, 0) = word Then
                    system_word = Replace(My.Forms.main_form.language_array(temp_i, language_index), "|", vbNewLine)
                End If
            Next
            Return system_word
        Catch ex As Exception
            Return system_word
        End Try
    End Function


    Function fn_translate_with_current_index(ByVal word As String, ByVal language_index As Integer) As String
        Dim system_word = word
        Try
            For temp_i = 0 To My.Forms.main_form.language_array.GetLength(0)
                If My.Forms.main_form.language_array(temp_i, 0) = word Then
                    system_word = Replace(My.Forms.main_form.language_array(temp_i, language_index), "|", vbNewLine)
                End If
            Next
            Return system_word
        Catch ex As Exception
            Return system_word
        End Try
    End Function



    Function fn_load_setting() As Boolean
        My.Forms.main_form.lb_default_language.Items.Clear()

        If fn_load_data_file_to_array(My.Forms.main_form.config_file, "=", True) = True Then

            ReDim My.Forms.main_form.config_array(My.Forms.main_form.file_array.GetLength(0) - 1, (My.Forms.main_form.file_array.Length / My.Forms.main_form.file_array.GetLength(0)) - 1)
            Array.Copy(My.Forms.main_form.file_array, My.Forms.main_form.config_array, My.Forms.main_form.file_array.Length)

            Try
                For i = 1 To (My.Forms.main_form.language_array.GetLength(0)) - 1 Step 1
                    If My.Forms.main_form.language_array(0, i).ToString.Length > 0 Then
                        My.Forms.main_form.lb_default_language.Items.Add(My.Forms.main_form.language_array(0, i).ToString)
                    End If
                Next
            Catch ex As Exception
            End Try

            Try
                For i = 0 To (My.Forms.main_form.config_array.Length * 0.5) Step 1
                    If My.Forms.main_form.config_array(i, 0) = "DEFAULT_LANGUAGE" Then
                        My.Forms.main_form.lb_default_language.SelectedItem = My.Forms.main_form.config_array(i, 1).ToString
                    End If
                    If My.Forms.main_form.config_array(i, 0) = "CHAT_PORT" Then
                        My.Forms.main_form.txt_chat_port.Text = My.Forms.main_form.config_array(i, 1).ToString
                    End If
                    If My.Forms.main_form.config_array(i, 0) = "LIMIT_CHAT_LOG_LINE_COUNT" Then
                        My.Forms.main_form.txt_chat_log_line_limit.Text = My.Forms.main_form.config_array(i, 1).ToString
                    End If
                    If My.Forms.main_form.config_array(i, 0) = "CHAT_LOG" Then
                        My.Forms.main_form.chb_logging_chat_messages.Checked = My.Forms.main_form.config_array(i, 1).ToString
                    End If
                    If My.Forms.main_form.config_array(i, 0) = "DELAY_NOTIFICATION" Then
                        My.Forms.main_form.txt_delay_notification.Text = My.Forms.main_form.config_array(i, 1).ToString
                    End If
                Next
            Catch ex As Exception
            End Try

        End If
    End Function


    Function fn_save_setting() As Boolean

        For i = 0 To My.Forms.main_form.config_array.GetLength(0) - 1
            If My.Forms.main_form.config_array(i, 0) = "CHAT_PORT" Then My.Forms.main_form.config_array(i, 1) = My.Forms.main_form.txt_chat_port.Text
            If My.Forms.main_form.config_array(i, 0) = "DEFAULT_LANGUAGE" Then My.Forms.main_form.config_array(i, 1) = My.Forms.main_form.lb_default_language.SelectedItem
            If My.Forms.main_form.config_array(i, 0) = "LIMIT_CHAT_LOG_LINE_COUNT" Then My.Forms.main_form.config_array(i, 1) = My.Forms.main_form.txt_chat_log_line_limit.Text
            If My.Forms.main_form.config_array(i, 0) = "CHAT_LOG" Then My.Forms.main_form.config_array(i, 1) = My.Forms.main_form.chb_logging_chat_messages.Checked
            If My.Forms.main_form.config_array(i, 0) = "DELAY_NOTIFICATION" Then My.Forms.main_form.config_array(i, 1) = My.Forms.main_form.txt_delay_notification.Text

        Next

        'SAVE LANGUAGE
        ReDim My.Forms.main_form.file_array(My.Forms.main_form.config_array.GetLength(0) - 1, (My.Forms.main_form.config_array.Length / My.Forms.main_form.config_array.GetLength(0)) - 1)
        Array.Copy(My.Forms.main_form.config_array, My.Forms.main_form.file_array, My.Forms.main_form.config_array.Length)
        fn_save_data_file_from_array(My.Forms.main_form.config_file, "=", True)

        'RELOAD SETTINGS
        My.Forms.main_form.lb_modules.Items.Clear()
        fn_load_setting()
        My.Forms.main_form.tb_datalist.TabPages.Clear()
        fn_show_module_list()
        fn_translate_app()


        'AUTOSTARTS
        If My.Forms.main_form.lat IsNot Nothing Then
            fn_start_chat_server()
            fn_start_chat_server()
        Else : fn_start_chat_server()
        End If

    End Function



    Function fn_start_chat_server()

        Try
            If My.Forms.main_form.tss_chat_server_status.BackColor = Color.Red Then
                My.Forms.main_form.server = New Swebex_Server.chat_Server(AddressOf My.Forms.main_form.UpdateUIServer, , True)
                My.Forms.main_form.lat = New Swebex_Server.chat_Utilities.LargeArrayTransferHelper(My.Forms.main_form.server)
                My.Forms.main_form.server.Start(Convert.ToInt32(My.Forms.main_form.txt_chat_port.Text))
                My.Forms.main_form.tss_chat_server_status.BackColor = Color.LightGreen
                My.Forms.main_form.tss_chat_server_status.Text = fn_translate("tss_listen_running")

            Else
                If My.Forms.main_form.server IsNot Nothing Then My.Forms.main_form.server.Close()
                My.Forms.main_form.lat = Nothing
                My.Forms.main_form.lv_chat_Client_list.Items.Clear()
                My.Forms.main_form.tss_chat_server_status.BackColor = Color.Red
                My.Forms.main_form.tss_chat_server_status.Text = fn_translate("tss_listen_stoped")
            End If
        Catch ex As Exception
            MessageBox.Show(fn_translate("msg_port_must_be_number"))
        End Try


    End Function


    Function fn_check_file_size(ByVal file As String, ByVal max_size As Double)
        Dim f As System.IO.FileInfo = New System.IO.FileInfo(file)
        If (f.Length >= (Convert.ToDouble(max_size) * 1048576) And max_size <> 0) Then
            'resize file
        End If
    End Function



    Function fn_remove_file_lines(ByVal file As String, ByVal max_line As Double)
        '        Dim counter = 0
        Dim file_content As String

        Dim objReader As New System.IO.StreamReader(file, fn_detect_file_encoding(file))
        Try
            Dim record = objReader.ReadLine()
            Dim line_step = 0
            Do While Not (record Is Nothing)
                If line_step < max_line Then
                    record = record.Trim() 'kontrola jestli neni pouze prazdny
                    If record.Length > 0 Then
                        file_content += record + vbNewLine
                    End If
                End If
                record = objReader.ReadLine()
                line_step += 1
            Loop
            objReader.Close()
        Catch ex As Exception
            objReader.Close()
        End Try
        If (System.IO.File.Exists(file)) Then
            Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(file, False, System.Text.Encoding.UTF8)
            sw.Write(file_content)
            sw.Close()
        End If
    End Function


    Function fn_open_file(ByVal file As String)
        System.Diagnostics.Process.Start(file)
    End Function


    Function fn_open_log_file(ByVal file As String)
        System.Diagnostics.Process.Start("notepad.exe", file)
    End Function


    Function fn_check_file_structure()
        fn_create_directory(My.Forms.main_form.FilesFolder)
        fn_create_directory(My.Forms.main_form.config_folder)
        fn_create_directory(My.Forms.main_form.log_folder)
        fn_create_directory(My.Forms.main_form.vbscript_folder)
        fn_create_file(My.Forms.main_form.language_file)
        fn_create_file(My.Forms.main_form.config_file)
        fn_create_file(My.Forms.main_form.chat_log_file)
        fn_create_file(My.Forms.main_form.clients_file)
    End Function



    Function fn_write_to_log(ByVal file As String, ByVal message As String) As Boolean
        fn_write_to_log = False
        Try
            fn_create_file(file)
            Dim afile As System.IO.FileStream = New System.IO.FileStream(file, System.IO.FileMode.Append, System.IO.FileAccess.Write)
            Dim StreamWriter = New System.IO.StreamWriter(afile)
            StreamWriter.WriteLine(message)
            StreamWriter.Close()
            fn_write_to_log = True
        Catch ex As Exception
            MessageBox.Show(fn_translate("msg_cannot_save_log_file"))
        End Try
    End Function


    Friend Function fn_connect_host_to_chat_service(ByVal host As String, ByVal port As String, ByVal user_name As String) As Boolean
        fn_connect_host_to_chat_service = False
        Dim errMsg As String = ""
        My.Forms.main_form.client = New Swebex_Server.chat_Client(AddressOf My.Forms.main_form.UpdateUIClient, False, 30)
        'If Not My.Forms.main_form.client.Connect(GetIPFromHostname(My.Forms.main_form.txt_pc_name.Text, False).ToString, Convert.ToInt32("8000"), My.Forms.main_form.txt_pc_name.Text + "\" + My.Forms.main_form.txt_server_user.Text, errMsg) Then
        If Not My.Forms.main_form.client.Connect(host, Convert.ToInt32(port), user_name, errMsg) Then
            If errMsg.Trim <> "" Then MsgBox(errMsg, MsgBoxStyle.Critical, "Test Tcp Communications App")
        End If

        My.Forms.main_form.lat = New Swebex_Server.chat_Utilities.LargeArrayTransferHelper(My.Forms.main_form.client)
        fn_connect_host_to_chat_service = True
    End Function



    Public Function fn_checkAllIP(ByVal checked_port As Integer) As String
        fn_checkAllIP = Nothing
        Dim alWorkGroups As New ArrayList
        Dim de As New DirectoryEntry
        Dim tcpipscan As TcpClient = Nothing
        Dim connect_result As IAsyncResult
        Dim connect_callback As AsyncCallback
        Dim connect_result_boll As Boolean = False
        Dim count As Integer = 0
        'de.Path = "WinNT://" & My.Forms.main_form.txt_domain.Text
        de.Path = "WinNT://ldap"
        For Each d As DirectoryEntry In de.Children
            connect_result_boll = False
            Try

                If d.SchemaClassName = "Computer" And d.Name <> UCase(System.Net.Dns.GetHostName()) Then
                    'If d.SchemaClassName = "Computer" And (d.Name = "PC-ADMIN" Or d.Name = "NB-14-04" Or d.Name = "NB-11-03") And d.Name <> UCase(System.Net.Dns.GetHostName()) Then
                    'If d.SchemaClassName = "Computer" And (d.Name = "PC-ADMIN" Or d.Name = "NB-14-04" Or d.Name = "NB-11-03") Then
                    If My.Computer.Network.Ping(d.Name) Then
                        tcpipscan = New TcpClient
                        connect_result = tcpipscan.BeginConnect(d.Name, checked_port, connect_callback, vbNull)
                        connect_result_boll = connect_result.AsyncWaitHandle.WaitOne(1000, False)
                        tcpipscan.Close()

                        If connect_result_boll = True Then
                            'fn_connect_host_to_chat_service(d.Name, checked_port, local_user) 'with hostname
                            If count > 0 Then
                                fn_checkAllIP = fn_checkAllIP + "|" + d.Name
                            Else : fn_checkAllIP = d.Name
                            End If
                            count += 1
                        End If
                    End If
                End If
            Catch ex As Exception
                ' MessageBox.Show("vyjimka fn")
            End Try
            d.Dispose()
        Next
    End Function



    Public Function fn_connect_to_checked_hostname(ByVal hostname As String, ByVal checked_port As Integer, ByVal local_user As String) As Boolean
        fn_connect_to_checked_hostname = False
        Try
            Dim tcpipscan As TcpClient = Nothing
            Dim connect_result As IAsyncResult
            Dim connect_callback As AsyncCallback
            Dim connect_result_boll As Boolean = False

            If My.Computer.Network.Ping(hostname) Then
                tcpipscan = New TcpClient
                connect_result = tcpipscan.BeginConnect(hostname, checked_port, connect_callback, vbNull)
                connect_result_boll = connect_result.AsyncWaitHandle.WaitOne(1000, False)
                tcpipscan.Close()

                If connect_result_boll = True Then
                    fn_connect_host_to_chat_service(hostname, checked_port, local_user) 'with hostname
                    fn_connect_to_checked_hostname = True
                End If
            End If
        Catch ex As Exception
        End Try

    End Function




    Function fn_connect_to_array_clients() As Boolean
        fn_connect_to_array_clients = False
        Dim count As Integer = 0
        Try
            count = (My.Forms.main_form.clients_array.Length / My.Forms.main_form.clients_array.GetLength(0)) - 1
        Catch ex As Exception
        End Try

        For f = 0 To count Step 1
            Try
                If My.Forms.main_form.clients_array(f, 0).Length > 0 Then My.Forms.main_form.clients_array(f, 3) = fn_connect_to_checked_hostname(My.Forms.main_form.clients_array(f, 0), My.Forms.main_form.clients_array(f, 1), My.Forms.main_form.clients_array(f, 2))
                '     MessageBox.Show(My.Forms.main_form.clients_array(f, 0) + " / " + My.Forms.main_form.clients_array(f, 3))
            Catch ex As Exception
                'MessageBox.Show("connect Vyjimka")
            End Try
        Next
        My.Forms.main_form.t_system_timer.Enabled = True
        fn_connect_to_array_clients = True
    End Function




    Public Function BeginToReceiveAFile(ByVal _path As String) As Boolean
        Dim readBuffer As Int32 = 0
        chat_Client.ReceivingFile = True
        BeginToReceiveAFile = True
        chat_Client.fileBytesRecieved = 0

        Try
            chat_Client.CreateFolders(_path)
            chat_Client.fileWriter = New chat_AsyncUnbuffWriter(_path, True, _
                            1024 * (chat_AsyncUnbuffWriter.GetPageSize()), chat_Client.IncomingFileSize)

        Catch ex As Exception
            _path = ex.Message
            chat_Client.ReceivingFile = False
        End Try

        If Not chat_Client.ReceivingFile Then
            Try
                chat_Client.fileWriter.Close()
            Catch ex As Exception
            End Try
            Return False
        End If
    End Function




    Function fn_insert_colored_chat_history(ByVal text As String)
        If InStr(text, "<||>") > 0 Then
            My.Forms.main_form.temp_str = Split(text, ">")
            My.Forms.main_form.rtb_chat_history.AppendText(My.Forms.main_form.temp_str(0).Substring(0, My.Forms.main_form.temp_str(0).Length - 3) + " " + fn_translate("msg_receiving_file") + " :")
            My.Forms.main_form.rtb_chat_history.SelectionColor = Color.Blue
            My.Forms.main_form.rtb_chat_history.SelectionFont = New Font(My.Forms.main_form.rtb_chat_history.SelectionFont, FontStyle.Underline)
            My.Forms.main_form.rtb_chat_history.AppendText("_" + My.Forms.main_form.temp_str(1) + "_" + vbNewLine)
            My.Forms.main_form.rtb_chat_history.SelectionColor = Color.Black
            My.Forms.main_form.rtb_chat_history.SelectionFont = New Font(My.Forms.main_form.rtb_chat_history.SelectionFont, FontStyle.Regular)
            If My.Forms.main_form.chb_logging_chat_messages.Checked = True Then fn_write_to_log(My.Forms.main_form.chat_log_file, My.Forms.main_form.temp_str(0).Substring(0, My.Forms.main_form.temp_str(0).Length - 3) + " " + fn_translate("msg_receiving_file") + " :" + "_" + My.Forms.main_form.temp_str(1) + "_" + vbNewLine)
            My.Forms.main_form.ni_tray_icon.BalloonTipText = fn_translate("msg_new_file_message")
            Try
                If IsNumeric(My.Forms.main_form.txt_delay_notification.Text) Then
                    My.Forms.main_form.ni_tray_icon.ShowBalloonTip(Convert.ToDouble(My.Forms.main_form.txt_delay_notification.Text) * 1000)
                Else
                    My.Forms.main_form.ni_tray_icon.ShowBalloonTip(15000)
                End If
            Catch ex As Exception
                My.Forms.main_form.ni_tray_icon.ShowBalloonTip(15000)
            End Try
        Else
            My.Forms.main_form.rtb_chat_history.AppendText(text + vbNewLine)
            If My.Forms.main_form.chb_logging_chat_messages.Checked = True Then fn_write_to_log(My.Forms.main_form.chat_log_file, text + vbNewLine)
            My.Forms.main_form.ni_tray_icon.BalloonTipText = fn_translate("msg_new_message")
            Try
                If IsNumeric(My.Forms.main_form.txt_delay_notification.Text) Then
                    My.Forms.main_form.ni_tray_icon.ShowBalloonTip(Convert.ToDouble(My.Forms.main_form.txt_delay_notification.Text) * 1000)
                Else
                    My.Forms.main_form.ni_tray_icon.ShowBalloonTip(15000)
                End If
            Catch ex As Exception
                My.Forms.main_form.ni_tray_icon.ShowBalloonTip(15000)
            End Try
        End If
    End Function


End Module
