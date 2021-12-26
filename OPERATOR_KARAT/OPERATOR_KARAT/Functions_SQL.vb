Module Functions_SQL

    Public dgw_table_schema
    Public dgw_source As New BindingSource




    Public Function fn_sql_request(ByVal query As String, ByVal type As String) As Boolean
        Try
            My.Forms.Main_Form.Cursor = Cursors.WaitCursor
            fn_sql_request = False
            Dim cycle As Integer
            If My.Forms.Main_Form.chb_sql_debug.Checked = True Then MsgBox(query)

            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection(My.Settings.sql_connection)
            Dim cmd As New System.Data.SqlClient.SqlCommand(query, sqlConnection_string)
            cmd.CommandTimeout = 300
            Dim reader As System.Data.SqlClient.SqlDataReader
            sqlConnection_string.Open()
            reader = cmd.ExecuteReader()
            dgw_table_schema = reader.GetSchemaTable()

            If type = "SELECT" Or type = "SELECTONEITEM" Then

                'dgw_table_data.AutoGenerateColumns = True
                'dgw_table_data.Load(reader)
                'dgw_table_data.DataSource = reader
                'dgw_table_data.Refresh()

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
                    type = "INSERT/UPDATE/DELETE"
                End If


                If type = "SELECT" Then


                    If count > 0 Then dgw_source.DataSource = reader


                    ReDim My.Forms.Main_Form.sql_array(count.ToString, dgw_table_schema.Rows.count - 1)

                    count = 0
                    If reader.HasRows = True Or reader.RecordsAffected > 0 Then fn_sql_request = True

                    While reader.Read()
                        'MessageBox.Show((reader.GetInt32(0) & ", " & reader.GetString(1)))
                        'MessageBox.Show(reader.GetInt16(0))
                        'MsgBox(reader.GetString(1))
                        cycle = 0

                        While cycle < dgw_table_schema.Rows.count
                            'MessageBox.Show(CStr(reader.GetName(row))) 'column name 
                            My.Forms.Main_Form.sql_array(count, cycle) = reader(cycle).ToString()
                            cycle += 1
                        End While

                        count += 1
                    End While
                    My.Forms.Main_Form.sql_array_count = count
                End If


                If type = "SELECTONEITEM" Then
                    ReDim My.Forms.Main_Form.sql_array(count.ToString, dgw_table_schema.Rows.count - 1)

                    count = 0
                    If reader.HasRows = True Then fn_sql_request = True

                    While reader.Read()
                        cycle = 0

                        While cycle < dgw_table_schema.Rows.count
                            'MessageBox.Show(CStr(reader.GetName(row))) 'column name 
                            My.Forms.Main_Form.sql_array(count, cycle) = reader(cycle).ToString()
                            cycle += 1
                        End While

                        count += 1
                    End While
                    My.Forms.Main_Form.sql_array_count = count
                End If

            End If



            reader.Close()

            sqlConnection_string.Close()
            My.Forms.Main_Form.Cursor = Cursors.Default

            If slq_query_message.Length > 0 Then MessageBox.Show(slq_query_message)

        Catch ex As Exception
            My.Forms.Main_Form.Cursor = Cursors.Default
            MessageBox.Show("Chyba provedení SQL příkazu" + vbNewLine + ex.Message)
        End Try
    End Function


End Module
