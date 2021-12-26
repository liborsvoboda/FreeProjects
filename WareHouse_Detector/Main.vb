Public Class warehouse_detector
    Public user_app_folder As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Warehouse Detector")
    Public config_dir As String = System.IO.Path.Combine(user_app_folder, "DATA")
    Public config_file As String = System.IO.Path.Combine(config_dir, "config.ini")
    Public offline_warehouse_file As String = System.IO.Path.Combine(config_dir, "warehouse.list")
    Public offline_attribute_file As String = System.IO.Path.Combine(config_dir, "attribute.list")
    Public printed_file As String = System.IO.Path.Combine(config_dir, "temp.prn")

    Public doc_name As String = "printed_doc.prn"
    Public datatype As String = "Text"

    Public sql_array_count As Integer = 0
    Public sql_array(0, 0) As String
    Public my_result, time_check
    Public status = "ONLINE"
    Public app_loading = False


    'PRINTER LABEL SECTION
    Const ESC = Chr(27)
    Const CR = Chr(13)
    Const LF = Chr(10)
    Const UnderlineOn As String = ESC & Chr(&H2D) & Chr(&H31)
    Const UnderlineOff As String = ESC & Chr(&H2D) & Chr(&H30)
    'Const Paper_Cut As String = Chr(29) + "V" + Chr(1)
    Const Paper_Cut As String = Chr(29) + "V" + Chr(1)




    'Public printed_document As New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm

    ' load form
    Private Sub Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fn_load_setting()
        Me.txt_product.Focus()
        fn_query_result_check()
    End Sub
    'end of load form


    Private Sub btn_off_line_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_off_line_import.Click
        Me.pb_data_status.Value = 0
        If fn_load_warehouse_list(True) = True Then
            fn_load_attribute_list(True)
            MsgBox("Import DAT pro OFFLINE použití byl dokonèen")
        End If
    End Sub


    Private Sub lb_warehouse_Doubleclick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lb_warehouse.DoubleClick
        my_result = True
        If (Me.lb_warehouse.SelectedIndex >= 0) Then
            For row As Integer = 0 To Me.lb_selected_warehouse.Items.Count - 1 Step 1
                If Me.lb_selected_warehouse.Items.Item(row).ToString = Me.lb_warehouse.SelectedItem Then
                    my_result = False
                End If
            Next
            If my_result = True Then
                Me.lb_selected_warehouse.Items.Add(Me.lb_warehouse.SelectedItem)
                fn_save_config()
            Else
                MsgBox("Tento SKLAD je již v Seznamu")
            End If
        End If
    End Sub



    Private Sub lb_warehouse_entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lb_warehouse.KeyPress
        If Asc(e.KeyChar) = 13 Then
            my_result = True
            If (Me.lb_warehouse.SelectedIndex >= 0) Then
                For row As Integer = 0 To Me.lb_selected_warehouse.Items.Count - 1 Step 1
                    If Me.lb_selected_warehouse.Items.Item(row).ToString = Me.lb_warehouse.SelectedItem Then
                        my_result = False
                    End If
                Next
                If my_result = True Then
                    Me.lb_selected_warehouse.Items.Add(Me.lb_warehouse.SelectedItem)
                    fn_save_config()
                Else
                    MsgBox("Tento SKLAD je již v Seznamu")
                End If
            End If
        End If
    End Sub


    Private Sub lb_selected_warehouse_Doubleclick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lb_selected_warehouse.DoubleClick
        If (Me.lb_selected_warehouse.SelectedIndex >= 0) Then
            Dim result = MsgBox("Chcete SKLAD:" + Me.lb_selected_warehouse.SelectedItem + " skuteènì Odstranit?", MsgBoxStyle.YesNo)
            If result = vbYes Then
                Me.lb_selected_warehouse.Items.RemoveAt(Me.lb_selected_warehouse.SelectedIndex)
                fn_save_config()
            End If
        End If
    End Sub

    Private Sub lb_selected_warehouse_entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles lb_selected_warehouse.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If (Me.lb_selected_warehouse.SelectedIndex >= 0) Then
                Dim result = MsgBox("Chcete SKLAD:" + Me.lb_selected_warehouse.SelectedItem + " skuteènì Odstranit?", MsgBoxStyle.YesNo)
                If result = vbYes Then
                    Me.lb_selected_warehouse.Items.RemoveAt(Me.lb_selected_warehouse.SelectedIndex)
                    fn_save_config()
                End If
            End If
        End If
    End Sub


    Private Sub chb_zero_value_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_zero_value.Click
        fn_save_config()
    End Sub


    Private Sub chb_nnv_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_nnv.Click
        fn_save_config()
    End Sub


    Private Sub chb_negative_count_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_negative_count.Click
        fn_save_config()
    End Sub


    Private Sub cb_no_location_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_no_location.Click
        fn_save_config()
    End Sub

    Private Sub txt_product_entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_product.KeyPress
        If Asc(e.KeyChar) = 13 Then

            Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
            Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
            Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
            Me.txt_product.Text = UCase(Me.txt_product.Text)
            If Me.txt_product.Text.Length > 0 Then
                fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, "")
            End If

            If InStr(Me.txt_product.Text, "1000000000") = 0 Then
                Me.txt_atr1.Focus()
            Else
                Me.txt_product.Select(0, 1000)
            End If
        End If

    End Sub

    Private Sub txt_product_keydown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown 'txt_product.KeyDown
        If e.KeyCode = Keys.F2 Then 'And btn_open_draw.Enabled = True Then
            e.Handled = True
            Me.btn_open_draw_Click(sender, e)
        End If

        If e.KeyCode = Keys.F3 Then 'And btn_open_packing_list.Enabled = True Then
            btn_open_packing_list_Click(sender, e)
        End If
    End Sub

    Function fn_txt_product_entered(ByVal type As String)

        Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
        Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
        Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
        Me.txt_product.Text = UCase(Me.txt_product.Text)
        If Me.txt_product.Text.Length > 0 Then
            If Type = "SEMIPRODUCT" Then
                'Me.txt_product.Text = Me.txt_product.Text.Substring(0, Me.txt_product.Text.Length - 2)
            End If
            fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, Type)
        End If
        If InStr(Me.txt_product.Text, "1000000000") = 0 Then
            Me.txt_atr1.Focus()
        Else
            Me.txt_product.Select(0, 1000)
        End If
    End Function


    Private Sub txt_atr1_entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_atr1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
            Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
            Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
            Me.txt_product.Text = UCase(Me.txt_product.Text)
            If Me.txt_product.Text.Length > 0 Then
                fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, "")
            End If

            If InStr(Me.txt_product.Text, "1000000000") = 0 Then
                Me.txt_atr2.Focus()
            Else
                Me.txt_product.Focus()
            End If
        End If
    End Sub


    Function fn_txt_atr1_entered()
        Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
        Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
        Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
        Me.txt_product.Text = UCase(Me.txt_product.Text)
        If Me.txt_product.Text.Length > 0 Then
            fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, "")
        End If
        If InStr(Me.txt_product.Text, "1000000000") = 0 Then
            Me.txt_atr2.Focus()
        Else
            Me.txt_product.Focus()
        End If
    End Function


    Private Sub txt_atr2_entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_atr2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
            Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
            Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
            Me.txt_product.Text = UCase(Me.txt_product.Text)
            If Me.txt_product.Text.Length > 0 Then
                fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, "")
            End If
            Me.txt_product.Focus()
        End If
    End Sub


    Function fn_txt_atr2_entered()
        Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
        Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
        Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
        Me.txt_product.Text = UCase(Me.txt_product.Text)
        If Me.txt_product.Text.Length > 0 Then
            fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, "")
        End If
        Me.txt_product.Focus()
    End Function

    Private Sub btn_status_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_status.Click
        If Me.status = "ONLINE" Then
            Me.status = "OFFLINE"
            Me.btn_status.Text = "OFFLINE"
            Me.btn_status.ForeColor = Color.Red
        Else
            fn_load_setting()
        End If

    End Sub

    Private Sub btn_status1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_status1.Click
        If Me.status = "ONLINE" Then
            Me.status = "OFFLINE"
            Me.btn_status1.Text = "OFFLINE"
            Me.btn_status1.ForeColor = Color.Red
        Else
            fn_load_setting()
        End If
    End Sub


    Private Sub btn_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_search.Click
        Me.psx_waiting.Visible = True
        Me.txt_product.Text = remove_barcode_symbol(Me.txt_product.Text)
        Me.txt_atr1.Text = remove_barcode_symbol(Me.txt_atr1.Text)
        Me.txt_atr2.Text = remove_barcode_symbol(Me.txt_atr2.Text)
        Me.txt_product.Text = UCase(Me.txt_product.Text)
        If Me.txt_product.Text.Length > 0 Then
            fn_check_product(Me.txt_product.Text, Me.txt_atr1.Text, Me.txt_atr2.Text, "")
        End If
        Me.txt_product.Focus()
    End Sub

    Private Sub txt_product_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_product.TextChanged
        If txt_product.Text.Length > 0 And Me.txt_epdm_drawing_path.Text.Length > 0 Then
            Me.btn_open_draw.Enabled = True
            Me.btn_open_packing_list.Enabled = True
        Else
            Me.btn_open_draw.Enabled = False
            Me.btn_open_packing_list.Enabled = False
        End If

        If InStr(txt_product.Text, "]") > 0 Then
            fn_txt_product_entered("")
        End If
    End Sub

    Private Sub txt_atr1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_atr1.TextChanged
        If InStr(txt_atr1.Text, "]") > 0 Then
            fn_txt_atr1_entered()
        End If
    End Sub

    Private Sub txt_atr2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_atr2.TextChanged
        If InStr(txt_atr2.Text, "]") > 0 Then
            fn_txt_atr2_entered()
        End If
    End Sub


    Private Sub txt_location_entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_location.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.txt_location.Text = remove_barcode_symbol(Me.txt_location.Text)
            Me.txt_location.Text = UCase(Me.txt_location.Text)
            If Me.txt_location.Text.Length > 0 Then
                fn_location_items(Me.txt_location.Text)
            End If
            Me.txt_location.Select(0, 1000)
        End If
    End Sub

    Function fn_txt_location_entered()
        Me.txt_location.Text = remove_barcode_symbol(Me.txt_location.Text)
        Me.txt_location.Text = UCase(Me.txt_location.Text)
        If Me.txt_location.Text.Length > 0 Then
            fn_location_items(Me.txt_location.Text)
        End If
        Me.txt_location.Select(0, 1000)
    End Function

    Private Sub txt_location_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_location.TextChanged
        If InStr(txt_location.Text, "]") > 0 Then
            fn_txt_location_entered()
        End If
    End Sub

    Private Sub btn_loc_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_loc_search.Click
        fn_txt_location_entered()
        Me.txt_location.Focus()
    End Sub

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            Me.txt_product.Focus()
            Me.txt_product.Select(0, 1000)
        End If
        If TabControl1.SelectedIndex = 1 Then
            Me.txt_location.Focus()
            Me.txt_location.Select(0, 1000)
        End If
    End Sub

    Private Sub btn_minus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_minus.Click
        Me.txt_product.Text = Me.txt_product.Text + "-"
    End Sub

    Private Sub btn_semiproduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_semiproduct.Click
        fn_txt_product_entered("SEMIPRODUCT")
    End Sub


    Private Sub btn_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        txt_product.Text = ""
        txt_atr1.Text = ""
        txt_atr2.Text = ""
        lb_query_result.Items.Clear()
        txt_product.Focus()
        fn_query_result_check()
    End Sub

    Private Sub btn_print_to_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_print_to.Click

        fn_delete_file(Me.printed_file)
        fn_create_file(Me.printed_file)
        Dim objWriter As New System.IO.StreamWriter(Me.printed_file, True, System.Text.Encoding.Default)
        objWriter.WriteLine("Hledáno: " + txt_product.Text)
        For l_index As Integer = 0 To lb_query_result.Items.Count - 1
            objWriter.WriteLine(lb_query_result.Items(l_index))
        Next

        For i As Integer = 0 To nud_min_row_count.Value
            objWriter.WriteLine("")
        Next

        If Me.chb_paper_cut.Checked = True Then objWriter.WriteLine(Paper_Cut)
        objWriter.Close()

        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            SendFileToPrinter(PrintDialog1.PrinterSettings.PrinterName, Me.printed_file)
        End If

        'printed_document.Form = My.Forms.print_form
        'If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    printed_document.PrinterSettings = PrintDialog1.PrinterSettings
        '    printed_document.Print()
        'End If
        'My.Forms.print_form.Close()

    End Sub


    Private Sub btn_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_print.Click
        Try

            fn_delete_file(Me.printed_file)
            fn_create_file(Me.printed_file)

            Dim objWriter As New System.IO.StreamWriter(Me.printed_file, True, System.Text.Encoding.Default)
            objWriter.WriteLine("Hledáno: " + txt_product.Text)

            For l_index As Integer = 0 To lb_query_result.Items.Count - 1
                objWriter.WriteLine(lb_query_result.Items(l_index))
            Next

            For i As Integer = 0 To nud_min_row_count.Value
                objWriter.WriteLine("")
            Next

            If Me.chb_paper_cut.Checked = True Then objWriter.WriteLine(Paper_Cut)
            objWriter.Close()

            SendFileToPrinter(DefaultPrinterName, Me.printed_file)

            'My.Forms.print_form.Refresh()
            'printed_document.Form = My.Forms.print_form
            'Me.printed_document.Print()
            'My.Forms.print_form.Close()
        Catch ex As Exception
            MessageBox.Show("Pøedchozí tisková úloha nebyla dokonèena." + vbNewLine + "Zkontrolujte nastavení tiskového procesoru.")
        End Try
    End Sub


    Private Sub cb_printer_processor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_printer_processor.SelectedIndexChanged
        If app_loading = False Then fn_save_config()
    End Sub

    Private Sub chb_paper_cut_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chb_paper_cut.Click
        fn_save_config()
    End Sub

    Private Sub nud_min_row_count_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nud_min_row_count.ValueChanged
        If app_loading = False Then fn_save_config()
    End Sub


    Private Sub btn_set_epdm_path_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_set_epdm_path.Click
        If txt_epdm_drawing_path.Text.Length > 0 Then
            fbd_folder_list.SelectedPath = txt_epdm_drawing_path.Text
        Else
            fbd_folder_list.SelectedPath = "\\server5\EPDM_PDF\EPDM_CMP\"
        End If

        Dim result As DialogResult = fbd_folder_list.ShowDialog()
        If (result = DialogResult.OK) Then
            txt_epdm_drawing_path.Text = fbd_folder_list.SelectedPath
            fn_save_config()
        End If
    End Sub


    Private Sub btn_clean_path_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clean_path.Click
        txt_epdm_drawing_path.Enabled = True
        txt_epdm_drawing_path.Text = ""
        fn_save_config()
    End Sub

    Private Sub txt_epdm_drawing_path_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_epdm_drawing_path.TextChanged
        fn_save_config()
    End Sub

    Private Sub btn_open_draw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_open_draw.Click
        Try
            If fn_run_cmd_command(System.IO.Path.Combine(Me.txt_epdm_drawing_path.Text, Me.txt_product.Text + ".pdf"), "") = True Then Exit Sub
        Catch ex As Exception
        End Try
        If fn_sql_request("SELECT opv.postup,nom.user_cislo_vykresu FROM dba.v_opvvyrza opv,dba.nomenklatura nom WHERE opv.opv= '" + txt_product.Text + "' AND nom.cislo_nomenklatury = opv.nomenklatura", 2) Then
            Try
                If fn_run_cmd_command(System.IO.Path.Combine(Me.txt_epdm_drawing_path.Text, sql_array(0, 0).Replace(" ", "") + ".pdf"), "") = False Then
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
            Try
                If fn_run_cmd_command(System.IO.Path.Combine(Me.txt_epdm_drawing_path.Text, sql_array(0, 1).Replace(" ", "") + ".pdf"), "") = False Then
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub st_product_finder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles st_product_finder.Click

    End Sub


    Private Sub btn_open_packing_list_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_open_packing_list.Click
        Try
            If fn_run_cmd_command(System.IO.Path.Combine(Me.txt_packing_list_path.Text, Me.txt_product.Text + ".pdf"), "") = True Then Exit Sub
        Catch ex As Exception
        End Try
        If fn_sql_request("SELECT opv.postup,nom.user_cislo_vykresu FROM dba.v_opvvyrza opv,dba.nomenklatura nom WHERE opv.opv= '" + txt_product.Text + "' AND nom.cislo_nomenklatury = opv.nomenklatura", 2) Then
            Try
                If fn_run_cmd_command(System.IO.Path.Combine(Me.txt_packing_list_path.Text, sql_array(0, 0).Replace(" ", "") + ".pdf"), "") = False Then
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
            Try
                If fn_run_cmd_command(System.IO.Path.Combine(Me.txt_packing_list_path.Text, sql_array(0, 1).Replace(" ", "") + ".pdf"), "") = False Then
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub txt_packing_list_path_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_packing_list_path.TextChanged
        fn_save_config()
    End Sub


    Private Sub btn_set_packing_list_path_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_set_packing_list_path.Click
        If txt_packing_list_path.Text.Length > 0 Then
            fbd_folder_list.SelectedPath = txt_packing_list_path.Text
        Else
            fbd_folder_list.SelectedPath = "\\server5\EPDM_PDF\EPDM_CMP\BALICI_PR\"
        End If

        Dim result As DialogResult = fbd_folder_list.ShowDialog()
        If (result = DialogResult.OK) Then
            txt_packing_list_path.Text = fbd_folder_list.SelectedPath
            fn_save_config()
        End If
    End Sub


    Private Sub btn_clean_packing_list_path_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clean_packing_list_path.Click
        txt_packing_list_path.Enabled = True
        txt_packing_list_path.Text = ""
        fn_save_config()
    End Sub


End Class
