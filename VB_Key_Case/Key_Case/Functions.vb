Module Functions
    Friend slq_query_command As String

    Private Declare Function ActivateKeyboardLayout Lib "user32.dll" (ByVal myLanguage As Long, Flag As Boolean) As Long
    Const LANG_CZECH = 1029
    Const LANG_ENGLISH = 1033
    Const LANG_FRENCH = 1036
    Const LANG_GERMAN = 1031
    Const LANG_ITALIAN = 1040
    Const LANG_NORWEGIAN = 1043
    Const LANG_PORTUGUESE = 1046
    Const LANG_RUSSIAN = 1049
    Const LANG_SPANISH = 1034
    Const LANG_UKRAINE = 1058


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


    Function fn_app_load() As Boolean
        fn_app_load = False
        Try
            If fn_check_directory(Main_Form.user_app_folder) = False Then
                fn_create_directory(Main_Form.user_app_folder)
            End If

            If fn_check_directory(Main_Form.config_dir) = False Then
                fn_create_directory(Main_Form.config_dir)
            End If

            Main_Form.available_com_ports = IO.Ports.SerialPort.GetPortNames()
            Main_Form.cb_com_ports.Items.AddRange(Main_Form.available_com_ports)

            
            If fn_check_file(My.Forms.Main_Form.configuration_file) = False Then
                MsgBox("Konfigurační soubor neexistuje, Proveďte konfiguraci")
            Else
                Dim objReader As New System.IO.StreamReader(My.Forms.Main_Form.configuration_file, True)
                Main_Form.temp_string = objReader.ReadLine()
                objReader.Close()
                Dim temp As String() = Split(Main_Form.temp_string, "#")

                My.MySettings.Default.Item("sql_connection") = "Data Source=" + temp(2).ToString + ";Persist Security Info=True;User ID=" + temp(3).ToString + ";Password=" + temp(4).ToString + ""

                'set form after load
                Main_Form.cb_com_ports.SelectedItem = temp(0)
                Main_Form.selected_picture = temp(1)

                Select Case temp(1)
                    Case 1
                        Main_Form.pb_waiting_1.BorderStyle = BorderStyle.Fixed3D
                    Case 2
                        Main_Form.pb_waiting_2.BorderStyle = BorderStyle.Fixed3D
                    Case 3
                        Main_Form.pb_waiting_3.BorderStyle = BorderStyle.Fixed3D
                    Case 4
                        Main_Form.pb_waiting_4.BorderStyle = BorderStyle.Fixed3D
                    Case 5
                        Main_Form.pb_waiting_5.BorderStyle = BorderStyle.Fixed3D
                    Case Else
                        Main_Form.pb_waiting_1.BorderStyle = BorderStyle.Fixed3D
                End Select

                Main_Form.txt_mssql_server.Text = temp(2)
                Main_Form.txt_mssql_name.Text = temp(3)
                Main_Form.txt_mssql_password.Text = temp(4)
                Main_Form.txt_com_signal_length.Text = temp(5)

                Main_Form.chb_data_cable.Checked = temp(6)
            End If

            fn_app_load = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function




    Function fn_save_file_setting() As Boolean
        fn_save_file_setting = False
        Try

            fn_delete_file(My.Forms.Main_Form.configuration_file)
            fn_create_file(My.Forms.Main_Form.configuration_file)
            Dim objWriter As New System.IO.StreamWriter(My.Forms.Main_Form.configuration_file, True)

            If Main_Form.pb_waiting_1.BorderStyle = BorderStyle.Fixed3D Then
                Main_Form.temp_integer = 1
            ElseIf Main_Form.pb_waiting_2.BorderStyle = BorderStyle.Fixed3D Then
                Main_Form.temp_integer = 2
            ElseIf Main_Form.pb_waiting_3.BorderStyle = BorderStyle.Fixed3D Then
                Main_Form.temp_integer = 3
            ElseIf Main_Form.pb_waiting_4.BorderStyle = BorderStyle.Fixed3D Then
                Main_Form.temp_integer = 4
            ElseIf Main_Form.pb_waiting_5.BorderStyle = BorderStyle.Fixed3D Then
                Main_Form.temp_integer = 5
            Else
                Main_Form.temp_integer = 1
            End If

            If Main_Form.chb_data_cable.Checked = True Then
                Main_Form.temp_string = "#" + "True"
            Else
                Main_Form.temp_string = "#" + "False"
            End If

            objWriter.WriteLine(Main_Form.cb_com_ports.SelectedItem.ToString + "#" + Main_Form.temp_integer.ToString + "#" + Main_Form.txt_mssql_server.Text + "#" + Main_Form.txt_mssql_name.Text + "#" + Main_Form.txt_mssql_password.Text + "#" + Main_Form.txt_com_signal_length.Text + Main_Form.temp_string)
            objWriter.Close()
            My.MySettings.Default.Item("sql_connection") = "Data Source=" + Main_Form.txt_mssql_server.Text + ";Persist Security Info=True;User ID=" + Main_Form.txt_mssql_name.Text + ";Password=" + Main_Form.txt_mssql_password.Text + ""

            MessageBox.Show("Nastavení bylo uloženo")
            fn_save_file_setting = True
            My.Forms.Main_Form.tc_control.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Function


    Public Function fn_set_keyboard(ByVal sel_keyboard As String)
        Select Case sel_keyboard

            Case "LANG_CZECH"
                Call ActivateKeyboardLayout(LANG_CZECH, 0)
            Case "LANG_ENGLISH"
                Call ActivateKeyboardLayout(LANG_ENGLISH, 0)
            Case "LANG_FRENCH"
                Call ActivateKeyboardLayout(LANG_FRENCH, 0)
            Case "LANG_GERMAN"
                Call ActivateKeyboardLayout(LANG_GERMAN, 0)
            Case "LANG_ITALIAN"
                Call ActivateKeyboardLayout(LANG_ITALIAN, 0)
            Case "LANG_NORWEGIAN"
                Call ActivateKeyboardLayout(LANG_NORWEGIAN, 0)
            Case "LANG_PORTUGUESE"
                Call ActivateKeyboardLayout(LANG_PORTUGUESE, 0)
            Case "LANG_RUSSIAN"
                Call ActivateKeyboardLayout(LANG_RUSSIAN, 0)
            Case "LANG_SPANISH"
                Call ActivateKeyboardLayout(LANG_SPANISH, 0)
            Case "LANG_UKRAINE"
                Call ActivateKeyboardLayout(LANG_UKRAINE, 0)
            Case Else
        End Select
    End Function


    Function fn_number_keys(ByVal e As KeyPressEventArgs)
        If (Char.IsDigit(e.KeyChar) = False And e.KeyChar <> Chr(27) And e.KeyChar <> Chr(13) And e.KeyChar <> Chr(8)) Then
            e.KeyChar = Nothing
        End If
    End Function


    Function fn_load_cb_key_pack() As Boolean
        fn_load_cb_key_pack = False
        Main_Form.cb_key_pack.Items.Clear()

        If fn_sql_request("SELECT * FROM (SELECT uk.skupina FROM [DBNAME].dba.user_kliccis uk WHERE uk.skupina<>'' AND uk.platnost=1 AND uk.skupina NOT IN (SELECT upk.skupina FROM [DBNAME].[dba].[user_privyd_klice] upk WHERE upk.skupina = uk.skupina AND CAST (dat_pri AS DATE) = CAST('1901-01-01' AS DATE)) GROUP BY skupina UNION SELECT uk.chip FROM [DBNAME].dba.user_kliccis uk WHERE uk.chip<>'' AND uk.platnost=1 AND uk.skupina NOT IN (SELECT upk.skupina FROM [DBNAME].[dba].[user_privyd_klice] upk WHERE upk.skupina = uk.skupina AND CAST (dat_pri AS DATE) = CAST('1901-01-01' AS DATE)) GROUP BY chip) x ORDER BY skupina", "SELECT") = True Then
            Try
                For row As Integer = 0 To Main_Form.sql_array_count - 1 Step 1
                    Main_Form.cb_key_pack.Items.Add(Main_Form.sql_array(row, 0))
                Next
            Catch ex As Exception
            End Try
        End If

    End Function




    Function fn_detect_key_pack(ByVal value As String) As Boolean
        Main_Form.selected_key_group = ""
        fn_detect_key_pack = False
        Main_Form.txt_searched_pack.Text = ""
        Main_Form.temp_string = ""

        For Each i As String In Main_Form.cb_key_pack.Items
            If UCase(Main_Form.cb_key_pack.Text) = UCase(i) Then
                If fn_sql_request("SELECT skupina+'|'+nazev,skupina FROM [DBNAME].dba.user_kliccis WHERE (skupina='" + value + "' OR chip ='" + value + "') AND platnost=1", "SELECT") = True Then
                    If Main_Form.selected_key_group.Length = 0 Then
                        Main_Form.selected_key_group = Main_Form.sql_array(0, 1)
                    End If
                    For row As Integer = 0 To Main_Form.sql_array_count - 1 Step 1
                        Main_Form.temp_string += Main_Form.sql_array(row, 0) + vbNewLine
                    Next
                    Main_Form.txt_searched_pack.Text = Main_Form.temp_string
                    fn_detect_key_pack = True
                End If
                Exit For
            End If
        Next
    End Function



    Function fn_check_key_for_issue() As Boolean
        fn_check_key_for_issue = False
        Try
            fn_sql_request("SELECT * FROM (SELECT uk.skupina FROM [DBNAME].dba.user_kliccis uk WHERE uk.skupina<>'' AND uk.platnost=1 AND uk.skupina NOT IN (SELECT upk.skupina FROM [DBNAME].[dba].[user_privyd_klice] upk WHERE upk.skupina = uk.skupina AND CAST (dat_pri AS DATE) = CAST('1901-01-01' AS DATE)) GROUP BY skupina UNION SELECT uk.chip FROM [DBNAME].dba.user_kliccis uk WHERE uk.chip<>'' AND uk.platnost=1 AND uk.skupina NOT IN (SELECT upk.skupina FROM [DBNAME].[dba].[user_privyd_klice] upk WHERE upk.skupina = uk.skupina AND CAST (dat_pri AS DATE) = CAST('1901-01-01' AS DATE)) GROUP BY chip) x ORDER BY skupina", "SELECT")
            If Main_Form.sql_array_count > 0 Then
                fn_check_key_for_issue = True
            Else
            End If
        Catch ex As Exception
        End Try
    End Function


End Module
