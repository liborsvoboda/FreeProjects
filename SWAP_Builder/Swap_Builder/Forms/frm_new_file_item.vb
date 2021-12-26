Public Class frm_new_file_item

    Friend Menu_id = ""
    Friend menu_type = "" 'RAVM - report menu, PAVM - print menu



    Private Sub frm_new_menu_item_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If menu_type = "RAVM" Then
            If Main_Form.dgw_query_view.SelectedCells IsNot Nothing Then
                fn_reload_report_menu(True, True)
            Else
                fn_reload_report_menu(False, True)
            End If
        End If

        If menu_type = "PAVM" Then
            If Main_Form.dgw_query_view.SelectedCells IsNot Nothing Then
                fn_reload_print_menu(True, True)
            Else
                fn_reload_print_menu(False, True)
            End If
        End If


            Main_Form.Enabled = True
            fn_cursor_waiting(False)
    End Sub


    Private Sub frm_new_menu_item_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub


    Private Sub frm_new_menu_item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctrl_object As Control In Me.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next
    End Sub


    Private Sub frm_new_menu_item_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If menu_type = "RAVM" Then
            Me.Text = fn_translate("add_new_report")
            chb_default.Enabled = False
        ElseIf menu_type = "PAVM" Then
            Me.Text = fn_translate("add_new_print_doc")
        End If

        For Each col As DataGridViewColumn In Main_Form.dgw_query_view.Columns
            lb_datafield_list.Items.Add(col.Name)
        Next
        fn_cursor_waiting(False)
    End Sub


    Private Sub txt_new_menu_name_TextChanged(sender As Object, e As EventArgs) Handles txt_new_menu_name.TextChanged, txt_selected_filename.TextChanged, txt_menu_position.TextChanged
        If txt_new_menu_name.Text.Length = 0 OrElse txt_selected_filename.Text.Length = 0 OrElse txt_menu_position.Text.Length = 0 Then
            btn_save.Enabled = False
        Else
            btn_save.Enabled = True
        End If
    End Sub


    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim data_binding As String = ""

        For item = 0 To lb_datafield_list.SelectedItems.Count - 1
            If data_binding.Length = 0 Then
                data_binding &= lb_datafield_list.SelectedItems(item).ToString
            Else
                data_binding &= "," & lb_datafield_list.SelectedItems(item).ToString
            End If
        Next

        If menu_type = "RAVM" Then
            If Main_Form.tv_report_menu.SelectedNode IsNot Nothing AndAlso IsNumeric(Main_Form.tv_report_menu.SelectedNode.Name) Then
                fn_sql_request("INSERT INTO [dbo].[report_form_list] (group_id,form_id,report_name,position,report_path,note,enabled,creator,released,enable_translate,data_binding)VALUES(" & Main_Form.tv_report_menu.SelectedNode.Name & "," & Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() & ",'" & txt_new_menu_name.Text & "'," & txt_menu_position.Text & ",'" & txt_selected_filename.Text & "','" & txt_db_note.Text.Replace("'", "") & "'," & CInt(Int(chb_enabled.Checked)).ToString & ",'" & fn_search_substitution("sub[user_name]") & "'," & CInt(Int(chb_released.Checked)).ToString & "," & CInt(Int(chb_translate.Checked)).ToString & ",'" & data_binding & "')", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
            Else
                fn_sql_request("INSERT INTO [dbo].[report_form_list] (group_id,form_id,report_name,position,report_path,note,enabled,creator,released,enable_translate,data_binding)VALUES(0," & Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() & ",'" & txt_new_menu_name.Text & "'," & txt_menu_position.Text & ",'" & txt_selected_filename.Text & "','" & txt_db_note.Text.Replace("'", "") & "'," & CInt(Int(chb_enabled.Checked)).ToString & ",'" & fn_search_substitution("sub[user_name]") & "'," & CInt(Int(chb_released.Checked)).ToString & "," & CInt(Int(chb_translate.Checked)).ToString & ",'" & data_binding & "')", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
            End If
        ElseIf menu_type = "PAVM" Then
            If chb_default.Checked Then  'disable another default
                fn_sql_request("UPDATE [dbo].[print_form_list] SET [default_document]=0 WHERE form_id='" & Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() & "'", "UPDATE", "local", False, True, Main_Form.sql_parameter, False, False)
            End If
            If Main_Form.tv_print_menu.SelectedNode IsNot Nothing AndAlso IsNumeric(Main_Form.tv_print_menu.SelectedNode.Name) Then
                fn_sql_request("INSERT INTO [dbo].[print_form_list] (group_id,form_id,print_name,position,print_path,note,enabled,creator,released,enable_translate,default_document,data_binding)VALUES(" & Main_Form.tv_print_menu.SelectedNode.Name & "," & Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() & ",'" & txt_new_menu_name.Text & "'," & txt_menu_position.Text & ",'" & txt_selected_filename.Text & "','" & txt_db_note.Text.Replace("'", "") & "'," & CInt(Int(chb_enabled.Checked)).ToString & ",'" & fn_search_substitution("sub[user_name]") & "'," & CInt(Int(chb_released.Checked)).ToString & "," & CInt(Int(chb_translate.Checked)).ToString & "," & CInt(Int(chb_default.Checked)).ToString & ",'" & data_binding & "')", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
            Else
                fn_sql_request("INSERT INTO [dbo].[print_form_list] (group_id,form_id,print_name,position,print_path,note,enabled,creator,released,enable_translate,default_document,data_binding)VALUES(0," & Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() & ",'" & txt_new_menu_name.Text & "'," & txt_menu_position.Text & ",'" & txt_selected_filename.Text & "','" & txt_db_note.Text.Replace("'", "") & "'," & CInt(Int(chb_enabled.Checked)).ToString & ",'" & fn_search_substitution("sub[user_name]") & "'," & CInt(Int(chb_released.Checked)).ToString & "," & CInt(Int(chb_translate.Checked)).ToString & "," & CInt(Int(chb_default.Checked)).ToString & ",'" & data_binding & "')", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
            End If
        End If
            fn_cursor_waiting(False)
            Me.Close()
    End Sub


    Private Sub txt_menu_position_TextChanged(sender As Object, e As KeyPressEventArgs) Handles txt_menu_position.KeyPress
        If Not (Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Chr(27) AndAlso e.KeyChar <> Chr(13) AndAlso e.KeyChar <> Chr(8)) Then
            e.KeyChar = Nothing
        End If
    End Sub

    Private Sub btn_select_file_Click(sender As Object, e As EventArgs) Handles btn_select_file.Click
        Dim res As String
        Main_Form.ofd_open_file.Filter = "rdl|*.rdl"
        res = Main_Form.ofd_open_file.ShowDialog()
        If res = vbOK Then
            txt_selected_filename.Text = IO.Path.GetFileName(Main_Form.ofd_open_file.FileName)
        End If
    End Sub

End Class