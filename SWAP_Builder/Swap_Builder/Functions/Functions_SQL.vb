Module Functions_SQL



    Public dgw_subform_data0, dgw_subform_data1, dgw_subform_data2, dgw_subform_data3, dgw_subform_data4, dgw_subform_data5, dgw_subform_data6, dgw_subform_data7, dgw_subform_data8, dgw_subform_data9 As New BindingSource With {.AllowNew = False}
    Public dgw_table_schema_sql_preview As Data.DataTable
    Public dgw_source_sql_preview As New BindingSource With {.AllowNew = True}
    Private dgw_source_sql_preview_addon As New BindingSource With {.AllowNew = True}
    Public dgw_table_schema, dgw_subtable_schema As Data.DataTable
    Public dgw_table_schema_addon As Data.DataTable
    Public dgw_source As New BindingSource With {.AllowNew = True}
    Public query_correct As Boolean


    Function fn_sql_load_subform_field(ByVal query As String, ByVal subsql_local As Boolean, ByVal subform_no As Integer, ByVal param As Data.SqlClient.SqlCommand)
        Try
            fn_cursor_waiting(True)
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection
            If My.Forms.Main_Form.chb_sql_debug.Checked Then MsgBox(query)

            If subsql_local Then
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
            Else
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
            End If
            Dim cmd = New Data.SqlClient.SqlCommand(query, sqlConnection_string) With {.CommandTimeout = 300}
            Dim subcmd = New Data.SqlClient.SqlCommand(query, sqlConnection_string) With {.CommandTimeout = 300}

            Dim reader As Data.SqlClient.SqlDataReader
            Dim subreader As Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()
            reader = cmd.ExecuteReader()
            dgw_subtable_schema = reader.GetSchemaTable()

            Dim cycle As Integer
            Dim count As Integer = 0
            If reader.RecordsAffected = -1 Then
                While reader.Read()
                    count += 1
                End While
                reader.Close()
                reader = cmd.ExecuteReader()
            Else count = reader.RecordsAffected
            End If

            Select Case subform_no
                Case 0
                    ReDim Main_Form.sql_subarray0(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 1
                    ReDim Main_Form.sql_subarray1(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 2
                    ReDim Main_Form.sql_subarray2(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 3
                    ReDim Main_Form.sql_subarray3(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 4
                    ReDim Main_Form.sql_subarray4(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 5
                    ReDim Main_Form.sql_subarray5(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 6
                    ReDim Main_Form.sql_subarray6(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 7
                    ReDim Main_Form.sql_subarray7(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 8
                    ReDim Main_Form.sql_subarray8(count.ToString, dgw_subtable_schema.Rows.Count - 1)
                Case 9
                    ReDim Main_Form.sql_subarray9(count.ToString, dgw_subtable_schema.Rows.Count - 1)
            End Select

            count = 0

            'load subform
            While reader.Read()
                cycle = 0
                While cycle < dgw_subtable_schema.Rows.Count
                    Select Case subform_no
                        Case 0
                            Main_Form.sql_subarray0(count, cycle) = reader(cycle).ToString()
                        Case 1
                            Main_Form.sql_subarray1(count, cycle) = reader(cycle).ToString()
                        Case 2
                            Main_Form.sql_subarray2(count, cycle) = reader(cycle).ToString()
                        Case 3
                            Main_Form.sql_subarray3(count, cycle) = reader(cycle).ToString()
                        Case 4
                            Main_Form.sql_subarray4(count, cycle) = reader(cycle).ToString()
                        Case 5
                            Main_Form.sql_subarray5(count, cycle) = reader(cycle).ToString()
                        Case 6
                            Main_Form.sql_subarray6(count, cycle) = reader(cycle).ToString()
                        Case 7
                            Main_Form.sql_subarray7(count, cycle) = reader(cycle).ToString()
                        Case 8
                            Main_Form.sql_subarray8(count, cycle) = reader(cycle).ToString()
                        Case 9
                            Main_Form.sql_subarray9(count, cycle) = reader(cycle).ToString()
                    End Select
                    cycle += 1
                End While

                If reader(1) Then
                    sqlConnection_string = New Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
                Else
                    sqlConnection_string = New Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
                End If

                subcmd = New Data.SqlClient.SqlCommand(reader(0).ToString, sqlConnection_string) With {.CommandTimeout = 300}

                If Main_Form.sql_parameter.Parameters.Count > 0 Then
                    For i = 0 To Main_Form.sql_parameter.Parameters.Count - 1
                        subcmd.CommandText += " WHERE " & Main_Form.sql_parameter.Parameters(i).ParameterName & "='" & Main_Form.sql_parameter.Parameters(i).Value & "'"
                        'subcmd.Parameters.AddWithValue(Main_Form.sql_parameter.Parameters(i).ParameterName, Main_Form.sql_parameter.Parameters(i).Value)
                    Next
                    Main_Form.sql_parameter.Parameters.Clear()
                End If
                sqlConnection_string.Open()
                subreader = subcmd.ExecuteReader()

            End While

            reader.Close()
            cmd.Connection.Close()

            Select Case subform_no
                Case 0
                    If subreader.HasRows Then dgw_subform_data0.DataSource = subreader Else dgw_subform_data0.DataSource = Nothing
                Case 1
                    If subreader.HasRows Then dgw_subform_data1.DataSource = subreader Else dgw_subform_data1.DataSource = Nothing
                Case 2
                    If subreader.HasRows Then dgw_subform_data2.DataSource = subreader Else dgw_subform_data2.DataSource = Nothing
                Case 3
                    If subreader.HasRows Then dgw_subform_data3.DataSource = subreader Else dgw_subform_data3.DataSource = Nothing
                Case 4
                    If subreader.HasRows Then dgw_subform_data4.DataSource = subreader Else dgw_subform_data4.DataSource = Nothing
                Case 5
                    If subreader.HasRows Then dgw_subform_data5.DataSource = subreader Else dgw_subform_data5.DataSource = Nothing
                Case 6
                    If subreader.HasRows Then dgw_subform_data6.DataSource = subreader Else dgw_subform_data6.DataSource = Nothing
                Case 7
                    If subreader.HasRows Then dgw_subform_data7.DataSource = subreader Else dgw_subform_data7.DataSource = Nothing
                Case 8
                    If subreader.HasRows Then dgw_subform_data8.DataSource = subreader Else dgw_subform_data8.DataSource = Nothing
                Case 9
                    If subreader.HasRows Then dgw_subform_data9.DataSource = subreader Else dgw_subform_data9.DataSource = Nothing
            End Select

            subreader.Close()
            subcmd.Connection.Close()
            sqlConnection_string.Close()
            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("subform_data_sql_command_error") & ": " & ex.Message.ToString & vbNewLine & query)
        End Try
    End Function



    Function fn_sql_check_button(ByVal query As String, ByVal connection_type As String, ByVal direct_disable As Boolean) As Boolean
        Dim cmd As New Data.SqlClient.SqlCommand With {.CommandTimeout = 300}
        fn_sql_check_button = False
        'If My.Forms.Main_Form.chb_sql_debug.Checked = True And query.Length > 0 Then MsgBox(query)
        fn_cursor_waiting(True)
        Try
            If Not direct_disable Then

                query_correct = True
                Dim sqlConnection_check_string As New System.Data.SqlClient.SqlConnection


                If UCase(connection_type) = "LOCAL" Then
                    sqlConnection_check_string = New System.Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
                Else
                    sqlConnection_check_string = New System.Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
                End If
                cmd = New Data.SqlClient.SqlCommand(query, sqlConnection_check_string) With {.CommandTimeout = 300}

                Dim check_reader As System.Data.SqlClient.SqlDataReader
                sqlConnection_check_string.Open()
                check_reader = cmd.ExecuteReader()

                Dim count As Integer = 0
                If check_reader.RecordsAffected = -1 Then
                    While check_reader.Read()
                        count += 1
                    End While
                    check_reader.Close()
                    check_reader = cmd.ExecuteReader()
                Else
                    count = check_reader.RecordsAffected
                    fn_sql_check_button = True
                End If
                If check_reader.HasRows OrElse check_reader.RecordsAffected > 0 OrElse count > 0 Then fn_sql_check_button = True

                check_reader.Close()
                sqlConnection_check_string.Close()

                If fn_sql_check_button Then
                    'Main_Form.btn_main_btn_1.Enabled = True
                    Main_Form.btn_main_btn_2.Enabled = True
                    Main_Form.btn_main_btn_3.Enabled = True
                    Main_Form.btn_main_create_copy.Enabled = False
                Else
                    Main_Form.btn_main_btn_1.Enabled = True
                    Main_Form.btn_main_btn_2.Enabled = False
                    Main_Form.btn_main_btn_3.Enabled = False
                    Main_Form.btn_main_create_copy.Enabled = True
                End If

            Else
                Main_Form.btn_main_btn_1.Enabled = False
                Main_Form.btn_main_btn_2.Enabled = False
                Main_Form.btn_main_btn_3.Enabled = False
                Main_Form.btn_main_create_copy.Enabled = False
            End If

            fn_cursor_waiting(False)
        Catch ex As Exception

            Main_Form.btn_main_btn_1.Enabled = False
            Main_Form.btn_main_btn_2.Enabled = False
            Main_Form.btn_main_btn_3.Enabled = False
            Main_Form.btn_main_create_copy.Enabled = False
            fn_cursor_waiting(False)
        End Try

    End Function

    Public Function fn_run_sql_transaction(ByVal query As String, ByVal local As Boolean) As Boolean
        Dim cmd As New Data.SqlClient.SqlCommand With {.CommandTimeout = 300}
        fn_run_sql_transaction = False
        Try
            fn_cursor_waiting(True)
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection
            If My.Forms.Main_Form.chb_sql_debug.Checked Then MsgBox(query)

            If local Then
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
            Else
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
            End If
            cmd = New Data.SqlClient.SqlCommand(query, sqlConnection_string) With {.CommandTimeout = 300}

            Dim reader As System.Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()
            reader = cmd.ExecuteReader()
            reader.Close()
            cmd.Connection.Close()
            sqlConnection_string.Close()

            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("sql_transaction_error") & ": '" & ex.Message.ToString & vbNewLine & query)
        End Try


    End Function


    Public Function fn_load_sql_addon(ByVal query As String, ByVal subsql_local As Boolean, ByVal object_name As String) As Boolean
        Dim cmd As New Data.SqlClient.SqlCommand With {.CommandTimeout = 300}
        fn_load_sql_addon = False
        Try
            fn_cursor_waiting(True)
            Dim cycle
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection
            If My.Forms.Main_Form.chb_sql_debug.Checked Then MsgBox(query)

            If subsql_local Then
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
            Else
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
            End If
            cmd = New Data.SqlClient.SqlCommand(query, sqlConnection_string) With {.CommandTimeout = 300}

            Dim reader As System.Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()
            reader = cmd.ExecuteReader()
            dgw_table_schema_addon = reader.GetSchemaTable()

            Dim count As Integer = 0
            If reader.RecordsAffected = -1 Then
                While reader.Read()
                    count += 1
                End While
                reader.Close()
                reader = cmd.ExecuteReader()
            Else
                count = reader.RecordsAffected
            End If


            dgw_source_sql_preview_addon.DataSource = reader
            reader.Close()
            reader = cmd.ExecuteReader()


            'FOR FILLING COMBO
            ReDim My.Forms.Main_Form.sql_array_addon(count.ToString, dgw_table_schema_addon.Rows.Count - 1)

            count = 0
            While reader.Read()
                cycle = 0
                While cycle < dgw_table_schema_addon.Rows.Count
                    My.Forms.Main_Form.sql_array_addon(count, cycle) = reader(cycle).ToString()
                    cycle += 1
                End While
                count += 1
            End While

            My.Forms.Main_Form.sql_array_addon_count = count
            reader.Close()
            cmd.Connection.Close()
            sqlConnection_string.Close()
            fn_load_sql_addon = True
            fn_cursor_waiting(False)
        Catch ex As Exception
            fn_cursor_waiting(False)
            MessageBox.Show(fn_translate("sql_command_addon_error") & ": '" & object_name & "': " & ex.Message.ToString & vbNewLine & query)
        End Try
    End Function


    Public Function fn_sql_request(ByVal query As String, ByVal type As String, ByVal connection_type As String, ByVal create_bck As Boolean, ByVal system_type As Boolean, ByVal param As Data.SqlClient.SqlCommand, ByVal user_view As Boolean, ByVal check_attachment As Boolean) As Boolean ', ByVal attachment As Boolean) As Boolean
        fn_sql_request = False
        Dim cmd As New Data.SqlClient.SqlCommand With {.CommandTimeout = 300}
        Try

            Dim attachment As String = Nothing
            Dim cycle As Integer
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection
            bar_process_start = True
            fn_cursor_change(True, bar_process_start)
            query_correct = True


            If type = "SELECT" AndAlso Not query.Contains("TOP") Then
                'If check_attachment Then attachment = " 0 as sys_attachment,"
                query = " SELECT TOP " & Main_Form.tstb_records_count.Text & attachment & query.Substring(6, query.Length - 6)
            End If

            If My.Forms.Main_Form.chb_sql_debug.Checked AndAlso Not system_type Then MsgBox(query)


            If UCase(connection_type) = "LOCAL" Then
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
            Else
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
            End If

            If {"INSERT", "UPDATE", "DELETE"}.Contains(UCase(type)) Then
                query = "BEGIN TRANSACTION" & vbNewLine & "GO" & vbNewLine & query & vbNewLine & "COMMIT TRANSACTION"
            End If

            cmd = New Data.SqlClient.SqlCommand(query, sqlConnection_string) With {.CommandTimeout = 300}

            Dim reader As System.Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()

            If {"INSERT", "UPDATE"}.Contains(UCase(type)) AndAlso Main_Form.sql_parameter.Parameters.Count > 0 Then
                For i = 0 To Main_Form.sql_parameter.Parameters.Count - 1
                    cmd.Parameters.AddWithValue(Main_Form.sql_parameter.Parameters(i).ParameterName, Main_Form.sql_parameter.Parameters(i).Value)
                Next
                reader = cmd.ExecuteReader()
                Main_Form.sql_parameter.Parameters.Clear()
            Else
                reader = cmd.ExecuteReader()
                dgw_table_schema = reader.GetSchemaTable()
            End If


            If Not system_type Then
                dgw_table_schema_sql_preview = reader.GetSchemaTable()
            Else
                dgw_table_schema_sql_preview = Nothing
            End If

            If {"INSERT", "UPDATE"}.Contains(UCase(type)) Then
                If reader.RecordsAffected > 0 Then fn_sql_request = True
            End If

            If {"DELETE"}.Contains(UCase(type)) Then
                If reader.RecordsAffected >= 0 Then fn_sql_request = True
            End If

            If type = "SELECT" OrElse type = "SELECTONEITEM" Then
                Dim count As Integer = 0

                If reader.RecordsAffected = -1 Then
                    While reader.Read()
                        count += 1
                    End While
                    reader.Close()
                    reader = cmd.ExecuteReader()
                Else
                    count = reader.RecordsAffected
                    fn_sql_request = True
                    type = ""
                End If

                If type = "SELECT" Then

                    If reader.HasRows Then
                        If user_view Then
                            dgw_source.DataSource = reader
                        End If


                        If Not system_type Then
                            reader.Close()
                            reader = cmd.ExecuteReader()
                            If count > 0 AndAlso Not user_view Then
                                dgw_source_sql_preview.DataSource = reader
                            ElseIf count = 0 AndAlso Not user_view Then
                                dgw_source_sql_preview.DataSource = Nothing
                            End If
                        End If
                    Else
                        If user_view Then
                            dgw_source.DataSource = Nothing
                        End If
                    End If

                    ReDim My.Forms.Main_Form.sql_array(count.ToString, dgw_table_schema.Rows.Count - 1)

                    If reader.HasRows OrElse reader.RecordsAffected > 0 Then fn_sql_request = True
                    count = 0

                    If UCase(connection_type) = "LOCAL" Then
                        reader.Close()
                        reader = cmd.ExecuteReader()
                    End If

                    While reader.Read()
                        cycle = 0

                        While cycle < dgw_table_schema.Rows.Count
                            'MessageBox.Show(CStr(reader.GetName(row))) 'column name 

                            My.Forms.Main_Form.sql_array(count, cycle) = reader(cycle).ToString()
                            cycle += 1
                        End While

                        count += 1

                    End While

                    My.Forms.Main_Form.sql_array_count = count

                    If create_bck Then
                        ReDim My.Forms.Main_Form.sql_array_bck(count.ToString, dgw_table_schema.Rows.Count - 1)
                        Array.Copy(My.Forms.Main_Form.sql_array_bck, Main_Form.sql_array_bck, My.Forms.Main_Form.sql_array_bck.Length)
                    End If

                End If


                If type = "SELECTONEITEM" Then 'FOR FILLING COMBO
                    ReDim My.Forms.Main_Form.sql_array(count.ToString, dgw_table_schema.Rows.Count - 1)

                    count = 0


                    While reader.Read()
                        cycle = 0

                        While cycle < dgw_table_schema.Rows.Count
                            'MessageBox.Show(CStr(reader.GetName(row))) 'column name 
                            My.Forms.Main_Form.sql_array(count, cycle) = reader(cycle).ToString()
                            cycle += 1
                        End While

                        count += 1
                    End While
                    If reader.HasRows AndAlso count > 0 Then fn_sql_request = True
                    My.Forms.Main_Form.sql_array_count = count

                    If create_bck Then
                        ReDim My.Forms.Main_Form.sql_array_bck(count.ToString, dgw_table_schema.Rows.Count - 1)
                        Array.Copy(My.Forms.Main_Form.sql_array_bck, Main_Form.sql_array_bck, My.Forms.Main_Form.sql_array_bck.Length)

                    End If
                End If

            End If

            reader.Close()
            cmd.Connection.Close()
            sqlConnection_string.Close()
            Main_Form.sql_parameter.Parameters.Clear()
            fn_cursor_change(False, bar_process_start)
        Catch ex As Exception
            Main_Form.sql_parameter.Parameters.Clear()
            query_correct = False
            MessageBox.Show(fn_translate("sql_command_error") & ": " & ex.Message.ToString & vbNewLine & query)
            fn_cursor_change(False, bar_process_start)
        End Try
    End Function

    Function fn_get_sql_index() As Integer
        Dim idIndex As Long = 0
        Dim cmd As New Data.SqlClient.SqlCommand With {.CommandTimeout = 300}
        Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection

        Try
            If UCase(fn_search_substitution("sub[user_dataview_db_type]")) = "LOCAL" Then
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.internal_sql_connection)
            Else
                sqlConnection_string = New System.Data.SqlClient.SqlConnection(My.Settings.external_sql_connection)
            End If

            cmd = New Data.SqlClient.SqlCommand("SELECT MAX([id]) FROM " & fn_search_substitution("sub[user_dataview_table]"), sqlConnection_string) With {.CommandTimeout = 300}
            Dim reader As System.Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()
            reader = cmd.ExecuteReader()
            reader.Read()

            idIndex += reader.GetValue(0) + 1

            reader.Close()
            cmd.Connection.Close()
            sqlConnection_string.Close()
            Return (idIndex)
        Catch ex As Exception
            cmd.Connection.Close()
            sqlConnection_string.Close()
        End Try
    End Function

End Module
