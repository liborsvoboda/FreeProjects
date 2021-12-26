Module Functions
    Public closing_app = False
    Dim newline As String
    Public record As String

    Function fn_get_application_path() As String
        Dim path As String
        path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
        fn_get_application_path = path.Replace("file:\", "")
    End Function

    Function fn_set_cursor(ByVal cursor_type As Cursor)
        Cursor.Current = cursor_type
    End Function

    Function fn_create_directory(ByVal directory As String) As Boolean
        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Function

    Function fn_show_file(ByVal file As String) As Boolean
        Dim file_open As New ProcessStartInfo
        file_open.FileName = file
        file_open.UseShellExecute = True
        Process.Start(file_open)
    End Function

    Function fn_check_directory(ByVal directory As String) As Boolean
        fn_check_directory = System.IO.Directory.Exists(directory)
    End Function

    Function fn_create_file(ByVal file As String) As Boolean
        fn_create_file = False
        If Not System.IO.File.Exists(file) Then
            System.IO.File.Create(file).Close()
            Dim objReader As New System.IO.StreamReader(file, True)
            If objReader.Read = True Then
                fn_create_file = True
            End If
            objReader.Close()
        End If
    End Function


    Function fn_copy_file(ByVal file1 As String, ByVal file2 As String) As Boolean
        System.IO.File.Copy(file1, file2, True)
    End Function


    Function fn_delete_file(ByVal file As String) As Boolean
        If System.IO.File.Exists(file) Then System.IO.File.Delete(file)
    End Function


    Function fn_check_file(ByVal file As String) As Boolean
        fn_check_file = System.IO.File.Exists(file)
    End Function



    Function fn_load_setting_file() As Boolean
        fn_set_cursor(Cursors.WaitCursor)
        fn_load_setting_file = False
        If fn_check_file(fn_get_application_path() + My.Forms.Main_Form.config_dir + My.Forms.Main_Form.config_file) = True Then
            Dim objReader As New System.IO.StreamReader(fn_get_application_path() + My.Forms.Main_Form.config_dir + My.Forms.Main_Form.config_file, True)
            record = objReader.ReadLine()
            My.Forms.Main_Form.lb_control_type.Items.Clear()
            Dim parts()
            Do While Not (record Is Nothing)
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    'parts = Split(fn_recode_charset(record), "::")
                    parts = Split(record, "::")

                    If (parts(0) = "CONTROL_TYPE") Then
                        My.Forms.Main_Form.lb_control_type.Items.Add(parts(1))
                    End If
                    fn_load_setting_file = True
                End If
                record = objReader.ReadLine()
            Loop
            objReader.Close()
            If fn_load_setting_file = False Then MessageBox.Show("Chybný formát souboru konfigurace: " + fn_get_application_path() + My.Forms.Main_Form.config_dir + My.Forms.Main_Form.config_file)
        End If
        If fn_load_setting_file = False Then MessageBox.Show("Chybí soubor konfigurace: " + fn_get_application_path() + My.Forms.Main_Form.config_dir + My.Forms.Main_Form.config_file)
        fn_set_cursor(Cursors.Default)
    End Function



    Function fn_recode_charset(ByVal variable As String) As String
        Dim iso As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")
        Dim utf8 As System.Text.Encoding = System.Text.Encoding.GetEncoding("WINDOWS-1250")
        Dim utfBytes As Byte() = utf8.GetBytes(variable)
        Dim isoBytes As Byte() = System.Text.Encoding.Convert(utf8, iso, utfBytes)
        Dim msg As String = iso.GetString(isoBytes, 0, variable.Length)
        fn_recode_charset = msg
    End Function



    Function fn_close_form(ByVal variable As String) As Boolean
        closing_app = True
        If InStr(variable, "Form_logon") <> 0 Then My.Forms.Form_Logon.Close()
        If InStr(variable, "Main_Form") <> 0 Then My.Forms.Main_Form.Close()
    End Function

    Function fn_save_import_info() As Boolean
        fn_create_file(My.Forms.Main_Form.log_file)
        Dim objWriter As New System.IO.StreamWriter(My.Forms.Main_Form.log_file, True)
        objWriter.WriteLine(My.Forms.Main_Form.sb_application.Text)
        objWriter.Close()
    End Function


    Function fn_load_import_info() As Boolean
        Dim load_info_temp As String = ""
        fn_load_import_info = False
        If fn_check_file(My.Forms.Main_Form.log_file) = True Then
            Dim objReader As New System.IO.StreamReader(My.Forms.Main_Form.log_file, True)
            record = objReader.ReadLine()
            Do While Not (record Is Nothing)
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    fn_load_import_info = True
                    load_info_temp = record
                End If
                record = objReader.ReadLine()
            Loop
            objReader.Close()
        End If

        If fn_load_import_info = False Then
            MessageBox.Show("Nejdøíve proveïte IMPORT dat.")
        Else
            My.Forms.Main_Form.sb_application.Text = load_info_temp
        End If

    End Function


    Function fn_check_match_logon_name(ByVal logon_name As String) As Boolean
        fn_set_cursor(Cursors.WaitCursor)
        fn_check_match_logon_name = False
        My.Forms.Main_Form.admin_right = False
        Dim count = 0
        Try
            If logon_name.Length > 0 Then
                If fn_check_file(My.Forms.Main_Form.users_file) = True Then
                    Dim objReader As New System.IO.StreamReader(My.Forms.Main_Form.users_file, True)
                    record = objReader.ReadLine()
                    Dim parts()
                    Do While Not (record Is Nothing)
                        record = record.Trim() 'kontrola jestli neni pouze prazdny
                        If record.Length > 0 Then
                            parts = Split(record, "#")
                            If parts(0) = logon_name Then
                                My.Forms.Main_Form.lbl_logged_info.Text = parts(0) + " | " + parts(1)
                                My.Forms.Main_Form.lbl_logged_time_info.Text = DateTime.Now
                                fn_check_match_logon_name = True
                                If parts(2) = 1 Then
                                    My.Forms.Main_Form.admin_right = True
                                    Exit Do
                                End If
                            End If
                        End If
                        record = objReader.ReadLine()
                        count = count + 1
                    Loop
                    objReader.Close()
                End If
            End If
            fn_set_cursor(Cursors.Default)
            If count = 0 Then MsgBox("Nebyl naèten èíselník Uživatelù " + vbNewLine + "Pøipojte se k Síti")
        Catch ex As Exception
            MsgBox("Nebyl naèten èíselník Uživatelù " + vbNewLine + "Pøipojte se k Síti")
        End Try
    End Function



    Function fn_remove_barcode_symbol(ByVal variable As String) As String
        variable = variable.Replace("[", "")
        variable = variable.Replace("]", "")
        fn_remove_barcode_symbol = variable
    End Function

    Function fn_remove_prefix_symbol(ByVal variable As String) As String
        variable = variable.Replace("1000000000", "")
        fn_remove_prefix_symbol = variable
    End Function




    Function fn_load_job_list() As Boolean
        fn_load_job_list = False
        fn_set_cursor(Cursors.WaitCursor)
        My.Forms.Main_Form.pb_data_status.Value = 0
        My.Forms.Main_Form.lbl_load_percent.Text = "0%"

        Dim query = "SELECT zdr_poz.id_poz,v_opvoper.opv,v_opvoper.polozka,v_opvoper.planvyroba FROM [dba].[v_opvoper] v_opvoper ,[dba].[zdr_poz] zdr_poz,[dba].[v_opvvyrza] v_opvvyrza WHERE zdr_poz.xplan = v_opvoper.xplan AND zdr_poz.doklad = v_opvoper.opv AND zdr_poz.polozka = v_opvoper.polozka AND v_opvvyrza.opv = v_opvoper.opv AND v_opvvyrza.xplan = v_opvoper.xplan AND v_opvvyrza.storno = 0 AND v_opvvyrza.xukonceno = 0 AND v_opvvyrza.xuzavreno = 0"
        If fn_sql_request(query, 4, "SELECT", True) = True Then
            fn_delete_file(My.Forms.Main_Form.job_list_file)
            fn_create_file(My.Forms.Main_Form.job_list_file)
            Dim objWriter As New System.IO.StreamWriter(My.Forms.Main_Form.job_list_file, True)
            For row As Integer = 0 To (My.Forms.Main_Form.sql_array_count - 1) Step 1
                Try
                    If My.Forms.Main_Form.sql_array((row), 0).Length > 0 Then
                        objWriter.WriteLine(My.Forms.Main_Form.sql_array((row), 0) + "|" + My.Forms.Main_Form.sql_array((row), 1) + "|" + My.Forms.Main_Form.sql_array((row), 2) + "|" + My.Forms.Main_Form.sql_array((row), 3))

                        My.Forms.Main_Form.pb_data_status.Value = Math.Round(row / (My.Forms.Main_Form.sql_array_count / 100))
                        My.Forms.Main_Form.lbl_load_percent.Text = CStr(Math.Round(row / (My.Forms.Main_Form.sql_array_count / 100))) + "%"
                        My.Forms.Main_Form.lbl_load_percent.Refresh()
                    End If
                Catch ex As IndexOutOfRangeException
                    fn_set_cursor(Cursors.Default)
                End Try
            Next
            My.Forms.Main_Form.sb_application.Text = "Posl.Aktualizace: " + DateTime.Now
            objWriter.Close()
            fn_save_import_info()
            fn_load_job_list = True
        End If
        fn_set_cursor(Cursors.Default)
    End Function



    Function fn_search_id_poz(ByVal search_value As String) As Boolean
        fn_set_cursor(Cursors.WaitCursor)
        fn_search_id_poz = False
        My.Forms.Main_Form.txt_job_no.Text = ""
        My.Forms.Main_Form.txt_operation.Text = ""
        My.Forms.Main_Form.lb_control_type.Enabled = True
        My.Forms.Main_Form.txt_controled_pcs.Enabled = True
        My.Forms.Main_Form.lb_control_type.Enabled = False
        My.Forms.Main_Form.txt_controled_pcs.Enabled = False
        My.Forms.Main_Form.btn_save.Enabled = False

        If fn_check_file(My.Forms.Main_Form.job_list_file) = True Then
            Dim objReader As New System.IO.StreamReader(My.Forms.Main_Form.job_list_file, True)
            record = objReader.ReadLine()
            Dim parts()
            Do While Not (record Is Nothing)
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    parts = Split(record, "|")
                    If parts(0) = search_value Then

                        My.Forms.Main_Form.txt_job_no.Text = Replace(parts(1), " ", "")
                        My.Forms.Main_Form.txt_operation.Text = Replace(parts(2), " ", "")
                        My.Forms.Main_Form.txt_planvyroba.Text = Replace(parts(3), " ", "")
                        My.Forms.Main_Form.lb_control_type.Enabled = True
                        My.Forms.Main_Form.txt_controled_pcs.Enabled = True
                        My.Forms.Main_Form.lb_control_type.Focus()
                        My.Forms.Main_Form.lb_control_type.SelectedIndex = 0
                        fn_search_id_poz = True
                        Exit Do
                    End If
                End If
                record = objReader.ReadLine()
            Loop
            objReader.Close()
        Else
            MessageBox.Show("Soubor se seznamem rozpracovaných zakázek neexistuje." + vbNewLine + "NejdøíveNaimportujte Data")
        End If
        If fn_search_id_poz = False Then My.Forms.Main_Form.txt_request_id.SelectAll()
        fn_set_cursor(Cursors.Default)
    End Function


    Function fn_save_new_record_to_file() As Boolean
        fn_save_new_record_to_file = False
        fn_set_cursor(Cursors.WaitCursor)
        Try

            If fn_check_file(My.Forms.Main_Form.scanned_file) = False Then
                If fn_create_file(My.Forms.Main_Form.scanned_file) = False Then
                    MessageBox.Show("Záznam se nepodaøilo Uložit, zkontrolujte práva zápisu do složky programu.")
                    Exit Function
                End If
            End If

            Dim objWriter As New System.IO.StreamWriter(My.Forms.Main_Form.scanned_file, True)
            newline = My.Forms.Main_Form.txt_request_id.Text + "|" + My.Forms.Main_Form.txt_job_no.Text + "|" + My.Forms.Main_Form.txt_operation.Text + "|" + My.Forms.Main_Form.txt_planvyroba.Text + "|" + My.Forms.Main_Form.lb_control_type.SelectedItem + "|" + My.Forms.Main_Form.txt_controled_pcs.Text + "|" + My.Forms.Main_Form.lbl_logged_info.Text + "|" + DateTime.Now
            objWriter.WriteLine(newline)
            objWriter.Close()

            check_data_for_export()

            My.Forms.Main_Form.txt_controled_pcs.Text = ""
            My.Forms.Main_Form.txt_request_id.Text = ""
            My.Forms.Main_Form.btn_save.Enabled = False
            My.Forms.Main_Form.lb_control_type.SelectedIndex = -1
            My.Forms.Main_Form.lb_control_type.Enabled = False
            My.Forms.Main_Form.txt_controled_pcs.Enabled = False
            My.Forms.Main_Form.txt_request_id.Focus()
        Catch ex As Exception
            fn_set_cursor(Cursors.Default)
            MessageBox.Show("Záznam se nepodaøilo Uložit, zkontrolujte práva zápisu do složky programu.")
        End Try
        fn_set_cursor(Cursors.Default)
    End Function


    Function check_data_for_export() As Double
        Dim lineCount = 0
        If fn_check_file(My.Forms.Main_Form.scanned_file) = True Then
            Dim objReader As New System.IO.StreamReader(My.Forms.Main_Form.scanned_file, True)
            record = objReader.ReadLine()
            Do While Not (record Is Nothing)
                If record.Length > 0 Then lineCount = lineCount + 1
                record = objReader.ReadLine()
            Loop
            objReader.Close()
        End If
        If lineCount = 0 Then
            My.Forms.Main_Form.btn_export.Enabled = False
        Else
            My.Forms.Main_Form.btn_export.Enabled = True
        End If
        Return lineCount
    End Function


    Function fn_set_admin_object()
        If My.Forms.Main_Form.admin_right = True Then
            My.Forms.Main_Form.chb_sql_debug.Enabled = True
        Else
            My.Forms.Main_Form.chb_sql_debug.Enabled = False
        End If
    End Function

End Module
