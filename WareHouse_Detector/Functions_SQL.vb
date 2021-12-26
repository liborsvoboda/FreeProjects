Module functions_SQL
    Dim app_config As String = Global.Warehouse_Detector.My.Resources.DB_connect


    Public Function fn_sql_request(ByVal query As String, ByVal coll As Integer) As Boolean
        fn_sql_request = False
        Dim count As Integer = 0

        If My.Forms.warehouse_detector.chb_sql_debugger.Checked = True Then MsgBox(query)

        Try
            Dim sqlConnection_string As New System.Data.SqlClient.SqlConnection(app_config)
            Dim cmd As New System.Data.SqlClient.SqlCommand(query, sqlConnection_string)
            sqlConnection_string.Open()
            Dim reader As System.Data.SqlClient.SqlDataReader
            reader = cmd.ExecuteReader()

            'return record count
            While reader.Read()
                count += 1
            End While
            'end of return record count

            reader.Close()
            reader = cmd.ExecuteReader()

            ReDim My.Forms.warehouse_detector.sql_array(count.ToString, (coll - 1))
            count = 0

            While reader.Read()
                'MessageBox.Show((reader.GetInt32(0) & ", " & reader.GetString(1)))
                'MessageBox.Show(reader.GetInt16(0))
                'MsgBox(reader.GetString(1))
                For row As Integer = 0 To (coll - 1) Step 1
                    My.Forms.warehouse_detector.sql_array(count, row) = reader(row).ToString()
                Next
                count += 1
            End While

        Catch ex As Exception
            MessageBox.Show("Nelze navázat spojení se serverem")
        End Try


        If count > 0 Then
            fn_sql_request = True
        Else
            fn_sql_request = False
        End If
        My.Forms.warehouse_detector.sql_array_count = count

    End Function







    'Function fn_load_karat_data() As Boolean
    '    fn_load_karat_data = False
    '    My.Forms.warehouse_detector.txt_inout_note.Text = "Naèítám Èíselníky z IS KARAT"
    '    My.Forms.warehouse_detector.pb_data_status.Value = 0

    '    If fn_sql_request("SELECT doklad FROM [dba].[maj_inv_dkl] WHERE [stav_inventury] = 20 ", 1) = True Then
    '        fn_load_karat_data = True
    '        fn_delete_file(My.Forms.warehouse_detector.ivn_doc_file)
    '        fn_create_file(My.Forms.warehouse_detector.ivn_doc_file)
    '        Dim objWriter As New System.IO.StreamWriter(My.Forms.warehouse_detector.ivn_doc_file, True)

    '        For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count Step 1
    '            Try
    '                objWriter.WriteLine(My.Forms.warehouse_detector.sql_array((row), 0))
    '                My.Forms.warehouse_detector.pb_data_status.Value = Math.Round(row / (My.Forms.warehouse_detector.sql_array_count / 100))
    '            Catch ex As IndexOutOfRangeException
    '            End Try
    '        Next
    '        objWriter.Close()
    '        My.Forms.warehouse_detector.pb_data_status.Value = 0
    '    End If

    '    If fn_sql_request("SELECT [umisteni],[nazev] FROM [dba].[maj_umisteni] WHERE platnost= 1 AND [umisteni]<>'' ORDER BY [umisteni] ", 2) = True Then
    '        fn_load_karat_data = True

    '        fn_delete_file(My.Forms.warehouse_detector.location_file)
    '        fn_create_file(My.Forms.warehouse_detector.location_file)

    '        Dim objWriter As New System.IO.StreamWriter(My.Forms.warehouse_detector.location_file, True)

    '        For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count Step 1
    '            Try
    '                If Replace(My.Forms.warehouse_detector.sql_array((row), 0), " ", "") <> "" Then objWriter.WriteLine(Replace(My.Forms.warehouse_detector.sql_array((row), 0), " ", "") + "#" + My.Forms.warehouse_detector.sql_array((row), 1))
    '                My.Forms.warehouse_detector.pb_data_status.Value = Math.Round(row / (My.Forms.warehouse_detector.sql_array_count / 100))
    '            Catch ex As IndexOutOfRangeException
    '            End Try
    '        Next
    '        objWriter.Close()
    '        My.Forms.warehouse_detector.pb_data_status.Value = 0
    '    Else
    '    End If

    '    If fn_load_karat_data = True Then
    '        My.Forms.warehouse_detector.txt_inout_note.Text = "Èíselníky z IS KARAT byly úspìšnì naèteny"
    '    Else
    '        My.Forms.warehouse_detector.txt_inout_note.Text = "Èíselníky z IS KARAT se nepodaøilo Naèíst"
    '    End If

    'End Function







End Module



