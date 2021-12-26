Imports Microsoft
Imports System
Imports System.Data

Module functions

    Dim newline As String
    Dim record As String
    Dim loaded As Boolean = False

    Function fn_get_application_path() As String
        Dim path As String
        path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.GetName.CodeBase)
        fn_get_application_path = path.Replace("file:\", "")
    End Function

    Function fn_create_directory(ByVal directory As String) As Boolean
        If Not System.IO.Directory.Exists(directory) Then
            System.IO.Directory.CreateDirectory(directory)
        End If
    End Function


    Function fn_check_directory(ByVal directory As String) As Boolean
        fn_check_directory = System.IO.Directory.Exists(directory)
    End Function

    Function fn_create_file(ByVal file As String) As Boolean
        If Not System.IO.File.Exists(file) Then
            System.IO.File.Create(file).Close()
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





    Function fn_load_setting() As Boolean
        My.Forms.warehouse_detector.app_loading = True
        If fn_check_directory(My.Forms.warehouse_detector.config_dir) = False Then
            fn_create_directory(My.Forms.warehouse_detector.config_dir)
        End If

        If fn_check_file(My.Forms.warehouse_detector.config_file) = False Then
            fn_create_file(My.Forms.warehouse_detector.config_file)
        End If

        If fn_check_file(My.Forms.warehouse_detector.offline_warehouse_file) = False Then
            fn_create_file(My.Forms.warehouse_detector.offline_warehouse_file)
        End If

        If fn_check_file(My.Forms.warehouse_detector.printed_file) = False Then
            fn_create_file(My.Forms.warehouse_detector.printed_file)
        End If

        fn_load_config_file()
        If fn_load_warehouse_list(False) = True Then
            My.Forms.warehouse_detector.sb_last_load.Text = "Poslední Naètení:" + DateTime.Now.ToString
            My.Forms.warehouse_detector.lbl_last_load.Text = "Poslední Naètení:" + DateTime.Now.ToString
            '  fn_load_attribute_list(True)
        Else
            ' offline load
            My.Forms.warehouse_detector.btn_status.Text = "OFFLINE"
            My.Forms.warehouse_detector.btn_status.ForeColor = Color.Red
            My.Forms.warehouse_detector.btn_status1.Text = "OFFLINE"
            My.Forms.warehouse_detector.btn_status1.ForeColor = Color.Red
            MsgBox("Nelze Naèíst Seznam Skladù. Program NEMÁ DATA")
        End If
        My.Forms.warehouse_detector.app_loading = False
    End Function




    Function fn_load_warehouse_list(ByVal status As Boolean) As Boolean
        fn_load_warehouse_list = False
        My.Forms.warehouse_detector.lb_warehouse.Items.Clear()
        record = ""
        If fn_sql_request("SELECT RTRIM([sklad]) FROM [dba].[skladycis] WHERE sklad <>'' AND sklad <>'-' ", 1) = True Then

            fn_delete_file(My.Forms.warehouse_detector.offline_warehouse_file)
            fn_create_file(My.Forms.warehouse_detector.offline_warehouse_file)
            Dim objWriter As New System.IO.StreamWriter(My.Forms.warehouse_detector.offline_warehouse_file, True)
            For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                Try
                    My.Forms.warehouse_detector.lb_warehouse.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                    objWriter.WriteLine(My.Forms.warehouse_detector.sql_array(row, 0))
                    If status = True Then
                        My.Forms.warehouse_detector.pb_data_status.Value = Math.Round(row / (My.Forms.warehouse_detector.sql_array_count / 100))
                    End If
                Catch ex As ArgumentException
                End Try
            Next
            objWriter.Close()
            fn_load_warehouse_list = True
            My.Forms.warehouse_detector.btn_status.Text = "ONLINE"
            My.Forms.warehouse_detector.btn_status.ForeColor = Color.ForestGreen
            My.Forms.warehouse_detector.btn_status1.Text = "ONLINE"
            My.Forms.warehouse_detector.btn_status1.ForeColor = Color.ForestGreen
            My.Forms.warehouse_detector.status = "ONLINE"
            My.Forms.warehouse_detector.sb_last_load.Text = "Poslední Naètení:" + DateTime.Now.ToString
            My.Forms.warehouse_detector.lbl_last_load.Text = "Poslední Naètení:" + DateTime.Now.ToString
            My.Forms.warehouse_detector.lbl_last_load1.Text = "Poslední Naètení:" + DateTime.Now.ToString
            fn_save_config()
        Else
            Dim objReader As New System.IO.StreamReader(My.Forms.warehouse_detector.offline_warehouse_file, True)
            record = objReader.ReadLine()
            Dim count = 0
            Do While Not (record Is Nothing)
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    My.Forms.warehouse_detector.lb_warehouse.Items.Add(record)
                End If
                record = objReader.ReadLine()
                count = count + 1
            Loop
            If count > 0 Then
                MsgBox("Nelze Naèíst Seznam Skladù. Program Bìží s DATY OFFLINE")
                My.Forms.warehouse_detector.btn_status.Text = "OFFLINE"
                My.Forms.warehouse_detector.btn_status.ForeColor = Color.Red
                My.Forms.warehouse_detector.btn_status1.Text = "OFFLINE"
                My.Forms.warehouse_detector.btn_status1.ForeColor = Color.Red
                My.Forms.warehouse_detector.status = "OFFLINE"
                fn_load_warehouse_list = True
            End If
            objReader.Close()

        End If

    End Function


    Function fn_load_attribute_list(ByVal status As Boolean) As Boolean
        fn_load_attribute_list = False
        If fn_sql_request("SELECT karty.karta,karty.sklad,atributy.atribut_1,atributy.atribut_2,atributy.atribut_3,CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt)),(select zp.id_poz from dba.zdr_poz zp where zp.doklad = atributy.atribut_1 and zp.polozka =  case when (0) = 1 then  	(select max(ope.polozka)  	from dba.v_opvoper ope where ope.opv = atributy.atribut_1  and ope.polozka = (case when karty.typ_atributu = 'NVZP' then convert(integer,right(atributy.atribut_2,6)) else 0 end))  when (0) = 0 then (case when karty.typ_atributu = 'NVZP' then convert(integer,right(atributy.atribut_2,6)) else 0 end) end) id_pozadavku FROM dba.[atributy] atributy,dba.[karty] karty WHERE atributy.id_karty = karty.id_karty AND atributy.sklad = karty.sklad AND karty.karta <>'' AND karty.sklad <>'-' ", 7) = True Then
            fn_delete_file(My.Forms.warehouse_detector.offline_attribute_file)
            fn_create_file(My.Forms.warehouse_detector.offline_attribute_file)
            Dim objWriter As New System.IO.StreamWriter(My.Forms.warehouse_detector.offline_attribute_file, True)
            For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                Try
                    objWriter.WriteLine(CStr(My.Forms.warehouse_detector.sql_array(row, 0)) + "#" + CStr(My.Forms.warehouse_detector.sql_array(row, 1).Replace(" ", "")) + "#" + CStr(My.Forms.warehouse_detector.sql_array(row, 2)) + "#" + CStr(My.Forms.warehouse_detector.sql_array(row, 3)) + "#" + CStr(My.Forms.warehouse_detector.sql_array(row, 4)) + "#" + CStr(My.Forms.warehouse_detector.sql_array(row, 5)) + "#" + CStr(My.Forms.warehouse_detector.sql_array(row, 6)))
                    If status = True Then
                        My.Forms.warehouse_detector.pb_data_status.Value = Math.Round(row / (My.Forms.warehouse_detector.sql_array_count / 100))
                    End If
                Catch ex As ArgumentException
                End Try
            Next
            objWriter.Close()
            fn_load_attribute_list = True
        Else
            MsgBox("Nelze Naèíst Seznam Atributù. Program Bìží s DATY OFFLINE")
            My.Forms.warehouse_detector.btn_status.Text = "OFFLINE"
            My.Forms.warehouse_detector.btn_status1.Text = "OFFLINE"
            My.Forms.warehouse_detector.btn_status.ForeColor = Color.Red
            My.Forms.warehouse_detector.btn_status1.ForeColor = Color.Red
            My.Forms.warehouse_detector.status = "OFFLINE"
            fn_load_attribute_list = False
        End If
    End Function




    Function fn_load_config_file() As Boolean
        Dim objReader As New System.IO.StreamReader(My.Forms.warehouse_detector.config_file, True)
        Try
            record = objReader.ReadLine()
            Dim count = 0
            Do While Not (record Is Nothing)
                record = record.Trim() 'kontrola jestli neni pouze prazdny
                If record.Length > 0 Then
                    If count <= 9 Then
                        If record = "on" And count = 0 Then
                            My.Forms.warehouse_detector.chb_zero_value.Checked = True
                        End If

                        If record = "on" And count = 1 Then
                            My.Forms.warehouse_detector.cb_no_location.Checked = True
                        End If

                        If record = "on" And count = 2 Then
                            My.Forms.warehouse_detector.chb_negative_count.Checked = True
                        End If

                        If record = "on" And count = 3 Then
                            My.Forms.warehouse_detector.chb_nnv.Checked = True
                        End If

                        If record = "on" And count = 4 Then
                            My.Forms.warehouse_detector.chb_paper_cut.Checked = True
                        End If

                        If count = 5 Then
                            My.Forms.warehouse_detector.nud_min_row_count.Value = record
                        End If

                        If count = 6 Then
                            My.Forms.warehouse_detector.cb_printer_processor.SelectedIndex = record
                        End If

                        If count = 7 Then
                            My.Forms.warehouse_detector.txt_epdm_drawing_path.Text = record
                        End If

                        If count = 8 Then
                            My.Forms.warehouse_detector.txt_packing_list_path.Text = record
                        End If

                        If count = 9 And My.Forms.warehouse_detector.status = "OFFLINE" Then
                            My.Forms.warehouse_detector.sb_last_load.Text = record
                            My.Forms.warehouse_detector.lbl_last_load.Text = record
                        End If
                    Else
                        My.Forms.warehouse_detector.lb_selected_warehouse.Items.Add(record)
                    End If

                End If
                record = objReader.ReadLine()
                count = count + 1
            Loop
            objReader.Close()
        Catch ex As Exception
            objReader.Close()
        End Try
    End Function



    Function fn_save_config() As Boolean
        If My.Forms.warehouse_detector.app_loading = False Then
            fn_delete_file(My.Forms.warehouse_detector.config_file)
            fn_create_file(My.Forms.warehouse_detector.config_file)

            Dim objWriter As New System.IO.StreamWriter(My.Forms.warehouse_detector.config_file, True)

            If My.Forms.warehouse_detector.chb_zero_value.Checked = True Then
                objWriter.WriteLine("on")
            Else
                objWriter.WriteLine("off")
            End If

            If My.Forms.warehouse_detector.cb_no_location.Checked = True Then
                objWriter.WriteLine("on")
            Else
                objWriter.WriteLine("off")
            End If

            If My.Forms.warehouse_detector.chb_negative_count.Checked = True Then
                objWriter.WriteLine("on")
            Else
                objWriter.WriteLine("off")
            End If

            If My.Forms.warehouse_detector.chb_nnv.Checked = True Then
                objWriter.WriteLine("on")
            Else
                objWriter.WriteLine("off")
            End If

            If My.Forms.warehouse_detector.chb_paper_cut.Checked = True Then
                objWriter.WriteLine("on")
            Else
                objWriter.WriteLine("off")
            End If

            objWriter.WriteLine(My.Forms.warehouse_detector.nud_min_row_count.Value)

            If My.Forms.warehouse_detector.cb_printer_processor.SelectedIndex = -1 Then
                objWriter.WriteLine(0)
            Else
                objWriter.WriteLine(My.Forms.warehouse_detector.cb_printer_processor.SelectedIndex)
            End If


            objWriter.WriteLine(My.Forms.warehouse_detector.txt_epdm_drawing_path.Text)
            objWriter.WriteLine(My.Forms.warehouse_detector.txt_packing_list_path.Text)
            objWriter.WriteLine(My.Forms.warehouse_detector.lbl_last_load.Text)

            For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                objWriter.WriteLine(My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row).ToString())
            Next

            objWriter.Close()
        End If
    End Function



    Function fn_check_product(ByVal product As String, ByVal atr1 As String, ByVal atr2 As String, ByVal type As String) As Boolean
        Dim query = ""
        Dim query_filter = ""
        My.Forms.warehouse_detector.lbl_found_location.Text = "LOKACE | KS"
        My.Forms.warehouse_detector.lb_query_result.Font = New Font("Tahoma", 15, FontStyle.Regular)
        If My.Forms.warehouse_detector.status = "ONLINE" Then 'ONLINE MODE
            If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = " AND karty.sklad IN ("
            For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                If row = 0 Then
                    query_filter = query_filter + "'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                Else
                    query_filter = query_filter + ",'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                End If
            Next
            If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = query_filter + ")"
            If My.Forms.warehouse_detector.chb_zero_value.Checked = False Then query_filter = query_filter + " AND atributy.stav_rt <> 0 "
            If My.Forms.warehouse_detector.cb_no_location.Checked = False Then query_filter = query_filter + " AND atributy.atribut_3 <> '' "

            My.Forms.warehouse_detector.lb_query_result.Items.Clear()
            If type = "" Then
                If InStr(product, "1000000000") = 0 Then
                    'START REPLACE 1
                    'jardovina 

                    My.Forms.warehouse_detector.lbl_found_location.Text = "ZAK|LOK/ZAK FIN|KS"
                    My.Forms.warehouse_detector.lb_query_result.Font = New Font("Tahoma", 8, FontStyle.Regular)
                    'MsgBox(My.Forms.warehouse_detector.lb_query_result.Font.Size.ToString)

                    If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = " AND atributy.sklad IN ("
                    For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                        If row = 0 Then
                            query_filter = query_filter + "'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                        Else
                            query_filter = query_filter + ",'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                        End If
                    Next
                    If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = query_filter + ")"
                    If My.Forms.warehouse_detector.chb_zero_value.Checked = False Then query_filter = query_filter + " AND atributy.stav_rt <> 0 "
                    If My.Forms.warehouse_detector.chb_negative_count.Checked = False Then query_filter = query_filter + " AND atributy.stav_rt > 0 "
                    If My.Forms.warehouse_detector.chb_nnv.Checked = False Then query_filter = query_filter + " AND atributy.id_karty <> '0100000000000001' AND atributy.id_karty <> '0100000000000007' "
                    If My.Forms.warehouse_detector.cb_no_location.Checked = False Then query_filter = query_filter + " AND atributy.atribut_3 <> '' "

                    'AND v_opvvyrza.odvedeno <> 0
                    query = "SELECT REPLACE(v_opvvyrza.opv,' ','') + '|' +RTRIM(atributy.atribut_3),REPLACE(v_opvvyrza.opvfinal,' ','') + '|' + CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt)) FROM dba.v_opvvyrza v_opvvyrza,dba.[atributy] atributy WHERE v_opvvyrza.opv = atributy.atribut_1 AND atributy.atribut_2 LIKE 'KNO%' AND v_opvvyrza.nomenklatura like '%" + product + "%' " + query_filter + " "

                    If atr1.Length > 0 Then query = query + " AND atributy.[atribut_1] = '" + atr1 + "'"
                    If atr2.Length > 0 Then query = query + " AND atributy.[atribut_2] = '" + atr2 + "'"

                    If fn_sql_request(query, 2) = True And query.ToString.Length > 0 Then

                        For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                            Try
                                My.Forms.warehouse_detector.lb_query_result.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                                My.Forms.warehouse_detector.lb_query_result.Items.Add(My.Forms.warehouse_detector.sql_array(row, 1))
                                My.Forms.warehouse_detector.lb_query_result.Items.Add("-----------------------------")

                            Catch ex As ArgumentException
                            End Try
                        Next
                    End If
                    'END OF REPLACE 1

                    'jardovina'
                Else
                    My.Forms.warehouse_detector.txt_atr1.Text = ""
                    My.Forms.warehouse_detector.txt_atr2.Text = ""
                    query = "SELECT RTRIM(atributy.atribut_3) + ' | ' + CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt)) FROM dba.[atributy] atributy,dba.[karty] karty WHERE atributy.id_karty = karty.id_karty AND atributy.sklad = karty.sklad AND (select zp.id_poz from dba.zdr_poz zp where zp.id_poz = " + product.Replace("1000000000", "") + " AND zp.doklad = atributy.atribut_1 and zp.polozka =  case when (0) = 1 then  	(select max(ope.polozka)  	from dba.v_opvoper ope where ope.opv = atributy.atribut_1  and ope.polozka = (case when karty.typ_atributu = 'NVZP' then convert(integer,right(atributy.atribut_2,6)) else 0 end))  when (0) = 0 then (case when karty.typ_atributu = 'NVZP' then convert(integer,right(atributy.atribut_2,6)) else 0 end) end) <> 0 " + query_filter + " "
                    If fn_sql_request(query, 1) = True And query.ToString.Length > 0 Then

                        For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                            Try
                                My.Forms.warehouse_detector.lb_query_result.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                            Catch ex As ArgumentException
                            End Try
                        Next
                    End If
                End If
                'MsgBox(query)

                'empty previous sql resutlt 

                ' START REPLACE 2
                query = ""
                query_filter = ""
                'My.Forms.warehouse_detector.lbl_found_location.Text = "KARTA|LOKACE|KS"
                If My.Forms.warehouse_detector.sql_array_count = 0 Then
                    My.Forms.warehouse_detector.lb_query_result.Font = New Font("Tahoma", 8, FontStyle.Regular)
                End If

                If My.Forms.warehouse_detector.status = "ONLINE" Then 'ONLINE MODE
                    If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = " AND karty.sklad IN ("
                    For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                        If row = 0 Then
                            query_filter = query_filter + "'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                        Else
                            query_filter = query_filter + ",'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                        End If
                    Next
                    If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = query_filter + ")"
                    If My.Forms.warehouse_detector.chb_zero_value.Checked = False Then query_filter = query_filter + " AND atributy.stav_rt <> 0 "
                    If My.Forms.warehouse_detector.chb_negative_count.Checked = False Then query_filter = query_filter + " AND atributy.stav_rt > 0 "
                    If My.Forms.warehouse_detector.chb_nnv.Checked = False Then query_filter = query_filter + " AND atributy.id_karty <> '0100000000000001' AND atributy.id_karty <> '0100000000000007' "
                    If My.Forms.warehouse_detector.cb_no_location.Checked = False Then query_filter = query_filter + " AND atributy.atribut_3 <> '' "


                    'My.Forms.warehouse_detector.lb_query_result.Items.Clear()

                    query = "SELECT ISNULL((SELECT v_opvvyrza.nomenklatura FROM dba.v_opvvyrza WHERE v_opvvyrza.opv=atributy.atribut_1 ),'') + '|' + RTRIM(atributy.atribut_3) + '|' + CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt)) FROM dba.[atributy] atributy,dba.[karty] karty WHERE atributy.id_karty = karty.id_karty AND atributy.sklad = karty.sklad AND (karty.karta like '%" + product + "%' OR atributy.atribut_1 like '%" + product + "%' ) " + query_filter + " "
                    'query = "SELECT ISNULL((SELECT v_opvvyrza.nomenklatura FROM dba.v_opvvyrza WHERE v_opvvyrza.opv=atributy.atribut_1 ),'') + '|' + RTRIM(atributy.atribut_3) + '|' + CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt)) FROM dba.[atributy] atributy,dba.[karty] karty WHERE atributy.id_karty = karty.id_karty AND atributy.atribut_2 LIKE 'KNO%' AND atributy.sklad = karty.sklad AND (karty.karta like '%" + product + "%' OR atributy.atribut_1 like '%" + product + "%' ) " + query_filter + " "
                    If atr1.Length > 0 Then query = query + " AND atributy.[atribut_1] = '" + atr1 + "'"
                    If atr2.Length > 0 Then query = query + " AND atributy.[atribut_2] = '" + atr2 + "'"
                    If fn_sql_request(query, 1) = True And query.ToString.Length > 0 Then

                        For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                            Try
                                My.Forms.warehouse_detector.lb_query_result.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                            Catch ex As ArgumentException
                            End Try
                        Next
                    End If
                    ' END OF REPLACE 2 

                End If
            Else
                query = "SELECT opvdil.nomenklatura+'|'+CAST (atributy.atribut_3 as varchar)+'|'+CAST(SUM(atributy.stav_rt) as varchar) FROM dba.v_opvdil opvdil,dba.atributy atributy,dba.karty karty WHERE opvdil.opv = '" + product + "' AND opvdil.id_nomen = atributy.id_karty AND opvdil.nomenklatura = karty.karta AND karty.sklad = atributy.sklad " + query_filter + " GROUP BY opvdil.nomenklatura,atributy.atribut_3 ORDER BY opvdil.nomenklatura,atributy.atribut_3"
                If atr1.Length > 0 Then query = query + " AND atributy.[atribut_1] = '" + atr1 + "'"
                If atr2.Length > 0 Then query = query + " AND atributy.[atribut_2] = '" + atr2 + "'"
                My.Forms.warehouse_detector.lb_query_result.Font = New Font("Tahoma", 8, FontStyle.Regular)
                If fn_sql_request(query, 1) = True And query.ToString.Length > 0 Then

                    For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                        Try
                            My.Forms.warehouse_detector.lb_query_result.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                        Catch ex As ArgumentException
                        End Try
                    Next
                End If
            End If
        Else 'OFFLINE MODE
            My.Forms.warehouse_detector.lb_query_result.Items.Clear()
            Dim objReader As New System.IO.StreamReader(My.Forms.warehouse_detector.offline_attribute_file, True)
            If InStr(product, "1000000000") = 0 Then
                record = objReader.ReadLine()
                Dim count = 0
                Do While Not (record Is Nothing)
                    record = record.Trim()
                    If record.Length > 0 Then
                        Dim parts()
                        parts = Split(record, "#")
                        If (product = parts(0) And (My.Forms.warehouse_detector.txt_atr1.Text = "" Or (My.Forms.warehouse_detector.txt_atr1.Text = parts(2))) And (My.Forms.warehouse_detector.txt_atr2.Text = "" Or (My.Forms.warehouse_detector.txt_atr2.Text = parts(3))) And (My.Forms.warehouse_detector.chb_zero_value.Checked = True Or (parts(5) <> "0")) And (My.Forms.warehouse_detector.cb_no_location.Checked = True Or (parts(4) <> ""))) Then
                            If My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count > 0 Then
                                For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                                    If parts(1) = My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) Then
                                        My.Forms.warehouse_detector.lb_query_result.Items.Add(parts(4) & " | " & parts(5))
                                    End If
                                Next
                            Else
                                My.Forms.warehouse_detector.lb_query_result.Items.Add(parts(4) & " | " & parts(5))
                            End If
                        End If
                    End If
                    record = objReader.ReadLine()
                    count = count + 1
                Loop
            Else 'ID_pozadavku
                record = objReader.ReadLine()
                Dim count = 0
                Do While Not (record Is Nothing)
                    record = record.Trim()
                    If record.Length > 0 Then
                        Dim parts()
                        parts = Split(record, "#")
                        If (product.Replace("1000000000", "") = parts(6) And (My.Forms.warehouse_detector.chb_zero_value.Checked = True Or (parts(5) <> "0"))) Then
                            If My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count > 0 Then
                                For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                                    If parts(1) = My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) Then
                                        My.Forms.warehouse_detector.lb_query_result.Items.Add(parts(4) & " | " & parts(5))
                                    End If
                                Next
                            Else
                                My.Forms.warehouse_detector.lb_query_result.Items.Add(parts(4) & " | " & parts(5))
                            End If
                        End If
                    End If
                    record = objReader.ReadLine()
                    count = count + 1
                Loop
            End If
            objReader.Close()



        End If
        fn_query_result_check()
    End Function



    Function remove_barcode_symbol(ByVal variable As String) As String
        variable = variable.Replace("%", "1000000000")
        variable = variable.Replace("[", "")
        variable = variable.Replace("]", "")
        remove_barcode_symbol = variable
    End Function




    Function fn_location_items(ByVal location As String)
        Dim query = ""
        Dim query_filter = ""
        My.Forms.warehouse_detector.lb_found_items.Items.Clear()
        If My.Forms.warehouse_detector.status = "ONLINE" Then 'ONLINE MODE
            If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = " AND karty.sklad IN ("
            For row As Integer = 0 To My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1 Step 1
                If row = 0 Then
                    query_filter = query_filter + "'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                Else
                    query_filter = query_filter + ",'" + My.Forms.warehouse_detector.lb_selected_warehouse.Items.Item(row) + "'"
                End If
            Next
            If (My.Forms.warehouse_detector.lb_selected_warehouse.Items.Count - 1) >= 0 Then query_filter = query_filter + ")"
            If My.Forms.warehouse_detector.chb_zero_value.Checked = False Then query_filter = query_filter + " AND atributy.stav_rt <> 0 "
            If My.Forms.warehouse_detector.chb_nnv.Checked = False Then query_filter = query_filter + " AND atributy.id_karty <> '0100000000000001' AND atributy.id_karty <> '0100000000000007' "
            query = "SELECT (karty.karta + '|' + CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt))+ '|'+ atributy.atribut_1 + '|' + + atributy.atribut_2) FROM dba.[atributy] atributy,dba.[karty] karty WHERE atributy.id_karty = karty.id_karty AND karty.karta NOT LIKE 'NNV%' AND  atributy.sklad = karty.sklad AND atributy.atribut_3 = '" + location + "' " + query_filter + " "

            If fn_sql_request(query, 1) = True And location.Length > 0 Then
                For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                    Try
                        My.Forms.warehouse_detector.lb_found_items.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                    Catch ex As ArgumentException
                    End Try
                Next
            End If

            query = "SELECT (opvvyrza.nomenklatura + '|' + CONVERT (VARCHAR,CONVERT (FLOAT,atributy.stav_rt))+ '|'+ atributy.atribut_1 + '|' + + atributy.atribut_2) FROM dba.[atributy] atributy,dba.[karty] karty, dba.[v_opvvyrza] opvvyrza WHERE opvvyrza.opv = atributy.atribut_1 AND atributy.id_karty = karty.id_karty AND karty.karta LIKE 'NNV%' AND  atributy.sklad = karty.sklad AND atributy.atribut_3 = '" + location + "' " + query_filter + " "
            If fn_sql_request(query, 1) = True And location.Length > 0 Then
                My.Forms.warehouse_detector.lb_found_items.Font = New Font("Tahoma", 10, FontStyle.Regular)
                For row As Integer = 0 To My.Forms.warehouse_detector.sql_array_count - 1 Step 1
                    Try
                        My.Forms.warehouse_detector.lb_found_items.Items.Add(My.Forms.warehouse_detector.sql_array(row, 0))
                    Catch ex As ArgumentException
                    End Try
                Next
            End If



        Else  ' OFFLINE MODE
            Dim objReader As New System.IO.StreamReader(My.Forms.warehouse_detector.offline_attribute_file, True)
            record = objReader.ReadLine()
            Dim count = 0
            Do While Not (record Is Nothing)
                record = record.Trim()
                If record.Length > 0 Then
                    Dim parts()
                    parts = Split(record, "#")
                    If (InStr(parts(0), "NNV0000") = 0 And location = parts(4) And (My.Forms.warehouse_detector.chb_zero_value.Checked = True Or (parts(5) <> "0"))) Then
                        My.Forms.warehouse_detector.lb_found_items.Items.Add(parts(0) & "|" & parts(5) & "|" & parts(2) & "|" & parts(3))
                    End If
                End If
                record = objReader.ReadLine()
                count = count + 1
            Loop
            objReader.Close()
        End If
    End Function

    Function fn_query_result_check()
        Try
            If My.Forms.warehouse_detector.lb_query_result.Items.Count > 0 Then
                My.Forms.warehouse_detector.btn_print.Enabled = True
                My.Forms.warehouse_detector.btn_print_to.Enabled = True
            Else
                My.Forms.warehouse_detector.btn_print.Enabled = False
                My.Forms.warehouse_detector.btn_print_to.Enabled = False
            End If
        Catch ex As Exception

        End Try

    End Function



    Function fn_run_cmd_command(ByVal command As String, ByVal arg As String) As Boolean
        fn_run_cmd_command = False
        Try
            Dim foo As New System.Diagnostics.Process
            'foo.StartInfo.FileName() = "cmd.exe"
            foo.StartInfo.FileName() = command
            foo.StartInfo.Arguments = arg
            foo.StartInfo.UseShellExecute = True
            foo.EnableRaisingEvents = True
            foo.Start()
            foo.WaitForExit()
            fn_run_cmd_command = True
        Catch ex As Exception
        End Try
    End Function


End Module