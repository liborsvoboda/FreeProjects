Module FunctionSQL
    Dim app_config As String = Global.Kontrola_Kvality.My.Resources.DB_connect



    Function fn_sql_connection_check() As Boolean
        My.Forms.Main_Form.btn_status.Text = "OFFLINE"
        My.Forms.Main_Form.btn_status.ForeColor = Color.Red
        fn_sql_connection_check = False
        Try
            Cursor.Current = Cursors.WaitCursor
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection(app_config)
            Dim cmd As New System.Data.SqlClient.SqlCommand("SELECT 1", sqlConnection_string)
            Dim reader As System.Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()
            reader = cmd.ExecuteReader()
            sqlConnection_string.Close()
            fn_sql_connection_check = True
            My.Forms.Main_Form.btn_status.Text = "ONLINE"
            My.Forms.Main_Form.btn_status.ForeColor = Color.Green
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            fn_sql_connection_check = False
            Cursor.Current = Cursors.Default
        End Try
    End Function


    Public Function fn_sql_request(ByVal query As String, ByVal coll As Integer, ByVal type As String, ByVal check_message As Boolean) As Boolean
        fn_sql_request = False

        Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection(app_config)
        Dim cmd As New System.Data.SqlClient.SqlCommand(query, sqlConnection_string)
        If My.Forms.Main_Form.chb_sql_debug.Checked = True Then MessageBox.Show(query)
        Try
            sqlConnection_string.Open()
            Dim reader As System.Data.SqlClient.SqlDataReader
            reader = cmd.ExecuteReader()

            If type = "INSERT" And reader.RecordsAffected > 0 Then
                fn_sql_request = True
            End If

            'If type = "UPDATE" And reader.RecordsAffected > 0 Then
            '    fn_sql_request = True
            'End If

            If type = "SELECT" Then
                Dim count As Integer = 0

                While reader.Read()
                    count += 1
                End While

                reader.Close()
                reader = cmd.ExecuteReader()
                ReDim My.Forms.Main_Form.sql_array(count.ToString, (coll - 1))

                count = 0
                While reader.Read()
                    For row As Integer = 0 To (coll - 1) Step 1
                        If Replace(reader(row).ToString(), " ", "").Length = 0 Then
                            My.Forms.Main_Form.sql_array(count, row) = "ZERO"
                        Else
                            My.Forms.Main_Form.sql_array(count, row) = reader(row).ToString()
                        End If
                    Next
                    count += 1
                End While
                My.Forms.Main_Form.sql_array_count = count
                If count > 0 Then
                    fn_sql_request = True
                Else
                    fn_sql_request = False
                End If
            End If

            reader.Close()
            sqlConnection_string.Close()
            My.Forms.Main_Form.sql_error = False
        Catch ex As Exception
            My.Forms.Main_Form.sql_error = True
            If check_message = True Then MessageBox.Show("Nelze navázat spojení se serverem: " + ex.Message)
        End Try

    End Function





    Function fn_load_dbusers() As Boolean
        Cursor.Current = Cursors.WaitCursor
        My.Forms.Main_Form.pb_data_status.Value = 0
        My.Forms.Main_Form.lbl_load_percent.Text = "0%"

        Try
            fn_delete_file(My.Forms.Main_Form.users_file)
            fn_create_file(My.Forms.Main_Form.users_file)

            If fn_sql_request("SELECT users.id_uzivatele, users.nazev, users.spravce FROM dba.users users,dba.users_rel users_rel WHERE (users_rel.id_skupiny='_KONTR' OR spravce = 1) AND users.id_uzivatele <> 'DBA' AND users_rel.id_uzivatele = users.id_uzivatele and users.typ<> 2 AND users_rel.uroven<> 0 AND users.platnost = 1", 3, "SELECT", True) = True Then
                If My.Forms.Main_Form.sql_array_count > 0 Then 'And fn_load_karat_data() = True
                    fn_load_dbusers = True
                Else
                    fn_load_dbusers = False
                End If

                Dim objWriter As New System.IO.StreamWriter(My.Forms.Main_Form.users_file, True)

                For row As Integer = 0 To My.Forms.Main_Form.sql_array_count Step 1
                    Try
                        If Replace(My.Forms.Main_Form.sql_array((row), 0), " ", "") <> "" Then objWriter.WriteLine(Replace(My.Forms.Main_Form.sql_array((row), 0), " ", "") + "#" + My.Forms.Main_Form.sql_array((row), 1) + "#" + My.Forms.Main_Form.sql_array((row), 2))
                        My.Forms.Main_Form.pb_data_status.Value = Math.Round(row / (My.Forms.Main_Form.sql_array_count / 100))
                        My.Forms.Main_Form.lbl_load_percent.Text = CStr(Math.Round(((100 / My.Forms.Main_Form.sql_array_count) * row))) + "%"
                        My.Forms.Main_Form.lbl_load_percent.Refresh()
                    Catch ex As IndexOutOfRangeException
                    End Try
                Next
                objWriter.Close()
            End If

            If My.Forms.Main_Form.sql_array_count = 0 Then MsgBox("Nebyl naèten èíselník Uživatelù " + vbNewLine + "Pøipojte se k Síti")
            Cursor.Current = Cursors.Default
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            If My.Forms.Main_Form.sql_array_count = 0 Then MsgBox("Nebyl naèten èíselník Uživatelù " + vbNewLine + "Pøipojte se k Síti")
        End Try

    End Function


    Function fn_insert_rec_to_db(ByVal linecount As Double, ByVal msg_show As Boolean) As Boolean
        Dim on_line_no As Double = 0
        fn_insert_rec_to_db = False
        Dim bad_record_save As String = ""
        Dim query = ""
        Dim parts()
        If fn_check_file(My.Forms.Main_Form.scanned_file) = True Then
            Dim objReader As New System.IO.StreamReader(My.Forms.Main_Form.scanned_file, True)
            record = objReader.ReadLine()
            Do While Not (record Is Nothing)
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    parts = Split(record, "|")
                    query = "INSERT INTO [dba].user_otk_sber_dat ([id],[id_poz], [opv], [polozka], [planvyroba], [cislo_kontroly], [kontrolovano], [id_uzivatele], [datum], [poznamka])VALUES(ISNULL((SELECT TOP 1 MAX(id+1) FROM [dba].user_otk_sber_dat),1)," + parts(0) + ",'" + parts(1) + "'," + parts(2) + "," + Replace(parts(3), ",", ".") + "," + parts(4) + "," + Replace(parts(6), ",", ".") + ",'" + UCase(Replace(parts(7), " ", "")) + "','" + Format(DateTime.Parse(parts(9)), "yyyy-MM-dd hh:mm:ss") + "','') "
                    If fn_sql_request(query, 1, "INSERT", True) = False Then
                        bad_record_save = bad_record_save + record + vbNewLine
                    End If
                End If

                on_line_no = on_line_no + 1
                My.Forms.Main_Form.pb_data_status.Value = Math.Round((100 / linecount) * on_line_no)
                My.Forms.Main_Form.lbl_load_percent.Text = CStr(Math.Round((100 / linecount) * on_line_no)) + "%"
                record = objReader.ReadLine()
                fn_insert_rec_to_db = True
            Loop
            objReader.Close()

            fn_delete_file(My.Forms.Main_Form.scanned_file)
            fn_create_file(My.Forms.Main_Form.scanned_file)
            Dim objWriter As New System.IO.StreamWriter(My.Forms.Main_Form.scanned_file, True)
            objWriter.Write(bad_record_save)
            objWriter.Close()

            If bad_record_save.Length > 0 Then
                If msg_show = True Then MessageBox.Show("Nìkteré záznamy se nepodaøilo uložit. Informujte IT.")
            Else
                If msg_show = True Then MessageBox.Show("Export do IS KARAT byl úspìšnì proveden")
                My.Forms.Main_Form.btn_export.Enabled = False
            End If

        End If
    End Function


End Module
