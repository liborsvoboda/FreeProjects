Public Class frm_new_menu_item

    Friend Menu_id = ""
    Friend menu_type = "" 'MM - main menu, FM - filter_menu



    Private Sub frm_new_menu_item_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If menu_type = "FAVM" Then fn_reload_favorite_menu(True)
        If menu_type = "RAVM" Then
            If Not Main_Form.dgw_query_view.SelectedCells Is Nothing Then
                fn_reload_report_menu(True, True)
            Else
                fn_reload_report_menu(False, True)
            End If
        End If

        If menu_type = "PAVM" Then
            If Not Main_Form.dgw_query_view.SelectedCells Is Nothing Then
                fn_reload_print_menu(True, True)
            Else
                fn_reload_print_menu(False, True)
            End If
        End If

        If menu_type <> "FM" Then
            Main_Form.Enabled = True
        ElseIf menu_type = "FM" Then
            fn_load_filter_list()
            For Each n As TreeNode In Main_Form.tv_filter_menu.Nodes
                If n.Text = txt_new_menu_name.Text Then
                    Main_Form.tv_filter_menu.SelectedNode = n
                    Exit For
                End If
            Next
        End If
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
        If menu_type = "MM" Then
            If Menu_id.length > 0 Then
                fn_sql_request("SELECT id,menu_name,position,enabled,released,note,enable_translate FROM [dbo].[menu_list] WHERE Id=" + Menu_id.ToString, "SELECTONEITEM", "local", False, True, Main_Form.sql_parameter, False, False)
                txt_menu_position.Text = Main_Form.sql_array(0, 2)
                txt_new_menu_name.Text = Main_Form.sql_array(0, 1)
                chb_enabled.Checked = Main_Form.sql_array(0, 3)
                chb_released.Checked = Main_Form.sql_array(0, 4)
                txt_db_note.Text = Main_Form.sql_array(0, 5)
                chb_translate.Checked = Main_Form.sql_array(0, 6)
            End If
        ElseIf menu_type = "FAVM" Then
            
        ElseIf menu_type = "RAVM" Then

        ElseIf menu_type = "PAVM" Then

        ElseIf menu_type = "FM" Then
            If Menu_id.length > 0 Then
                fn_sql_request("SELECT id,systemname,position,enabled,released,note,enable_translate FROM [dbo].[form_filter] WHERE id=" + Menu_id.ToString, "SELECTONEITEM", "local", False, True, Main_Form.sql_parameter, False, False)
                txt_menu_position.Text = Main_Form.sql_array(0, 2)
                txt_new_menu_name.Text = Main_Form.sql_array(0, 1)
                chb_enabled.Checked = Main_Form.sql_array(0, 3)
                chb_released.Checked = Main_Form.sql_array(0, 4)
                txt_db_note.Text = Main_Form.sql_array(0, 5)
                chb_translate.Checked = Main_Form.sql_array(0, 6)
            End If
        End If
    End Sub


    Private Sub frm_new_menu_item_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If menu_type = "MM" Then
            If Menu_id = "" Then
                Me.Text = fn_translate("add_new_menu_item")
            Else
                If chb_translate.Checked Then
                    Me.Text = fn_translate("update_menu_item") + ": " + fn_translate(Main_Form.sql_array(0, 1))
                Else
                    Me.Text = fn_translate("update_menu_item") + ": " + Main_Form.sql_array(0, 1)
                End If
            End If
        ElseIf menu_type = "FAVM" Then
            Me.Text = fn_translate("add_new_favorite_group")

        ElseIf menu_type = "RAVM" Then
            Me.Text = fn_translate("add_new_report_group")

        ElseIf menu_type = "PAVM" Then
            Me.Text = fn_translate("add_new_print_group")

        ElseIf menu_type = "FM" Then
            If Menu_id = "" Then
                Me.Text = fn_translate("add_new_filter") + ": " + Main_Form.temp_array(0, 0)
            Else
                Me.Text = fn_translate("update_filter") + ": " + Menu_id
            End If
        End If

        fn_cursor_waiting(False)
    End Sub


    Private Sub txt_new_menu_name_TextChanged(sender As Object, e As EventArgs) Handles txt_new_menu_name.TextChanged
        If txt_new_menu_name.Text.Length = 0 Then
            btn_save.Enabled = False
        Else
            btn_save.Enabled = True
        End If
    End Sub


    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim parent_node As String
        If menu_type = "MM" Then

            If Menu_id = "" Then
                If IsNumeric(Main_Form.tv_dev_menu.SelectedNode.Name) = True Then
                    'tree menu
                    If Main_Form.temp_array(0, 1) = "under" Then
                        fn_sql_request("INSERT INTO [dbo].[menu_list] (menu_name,parent_menu_id,position,level,note,enabled,creator,released,enable_translate)VALUES('" + txt_new_menu_name.Text + "'," + Main_Form.tv_dev_menu.SelectedNode.Name + "," + txt_menu_position.Text + "," + (Main_Form.temp_array(0, 2) + 1).ToString + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + "," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
                    Else
                        Try
                            If Main_Form.tv_dev_menu.SelectedNode.Parent.Name.Length > 0 Then
                                parent_node = Main_Form.tv_dev_menu.SelectedNode.Parent.Name
                            End If
                        Catch ex As Exception
                            parent_node = "NULL"
                        End Try
                        fn_sql_request("INSERT INTO [dbo].[menu_list] (menu_name,parent_menu_id,position,level,note,enabled,creator,released,enable_translate)VALUES('" + txt_new_menu_name.Text + "'," + parent_node.ToString + "," + txt_menu_position.Text + "," + (Main_Form.temp_array(0, 2)).ToString + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + "," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
                    End If

                ElseIf Main_Form.tv_dev_menu.SelectedNode.Name.Contains("SQL") Then


                ElseIf Main_Form.tv_dev_menu.SelectedNode.Name.Contains("TERMINAL") Then
                    'load special form for terminal using
                End If

            ElseIf Menu_id.length <> 0 Then

                If IsNumeric(Main_Form.tv_dev_menu.SelectedNode.Name) = True Then
                    fn_sql_request("UPDATE [dbo].[menu_list] SET position=" + txt_menu_position.Text + ", menu_name ='" + txt_new_menu_name.Text + "', enabled =" + CInt(Int(chb_enabled.Checked)).ToString + ", released =" + CInt(Int(chb_released.Checked)).ToString + ", creator ='" + fn_search_substitution("sub[user_name]") + "',created=GETDATE(),note='" + txt_db_note.Text.Replace("'", "") + "',enable_translate=" + CInt(Int(chb_translate.Checked)).ToString + " WHERE id=" + Main_Form.sql_array(0, 0) + "", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
                ElseIf Main_Form.tv_dev_menu.SelectedNode.Name.Contains("SQL") Then

                ElseIf Main_Form.tv_dev_menu.SelectedNode.Name.Contains("TERMINAL") Then
                    'load special form for terminal using
                End If
            End If

        ElseIf menu_type = "FAVM" Then
            fn_sql_request("INSERT INTO [dbo].[favorite_menu_list] (menu_name,parent_menu_id,position,level,note,enabled,creator,released,enable_translate)VALUES('" + txt_new_menu_name.Text + "',NULL," + txt_menu_position.Text + "," + (Main_Form.temp_array(0, 2)).ToString + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + "," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)

        ElseIf menu_type = "RAVM" Then
            fn_sql_request("INSERT INTO [dbo].[report_menu_list] (form_id,menu_name,parent_menu_id,position,level,note,enabled,creator,released,enable_translate)VALUES(" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + ",'" + txt_new_menu_name.Text + "',NULL," + txt_menu_position.Text + "," + (Main_Form.temp_array(0, 2)).ToString + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + "," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)

        ElseIf menu_type = "PAVM" Then
            fn_sql_request("INSERT INTO [dbo].[print_menu_list] (form_id,menu_name,parent_menu_id,position,level,note,enabled,creator,released,enable_translate)VALUES(" + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + ",'" + txt_new_menu_name.Text + "',NULL," + txt_menu_position.Text + "," + (Main_Form.temp_array(0, 2)).ToString + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + "," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)

        ElseIf menu_type = "FM" Then
            Dim definition_field As String = ""

            If Not Main_Form.where_array(0, 0) Is Nothing Then
                For i = 0 To Main_Form.where_array.Length / Main_Form.where_array.GetLength(0) - 1
                    If Not Main_Form.where_array(4, i) Is Nothing Then
                        definition_field &= Main_Form.where_array(0, i) + "|" + Main_Form.where_array(1, i) + "|" + Main_Form.where_array(2, i) + "|" + Main_Form.where_array(3, i) + "|" + Main_Form.where_array(4, i) + "|" + Main_Form.where_array(5, i) + "||"
                    End If
                Next
            End If

            If Menu_id = "" Then
                If Main_Form.tv_menu.SelectedNode.Name.Contains("SQL") Then
                    fn_sql_request("INSERT INTO [dbo].[form_filter] (systemname,form_id,position,note,enabled,creator,released,definition_cmd,definition_field,enable_translate)VALUES('" + txt_new_menu_name.Text + "'," + Main_Form.tv_menu.SelectedNode.Name.Replace("SQL", "").ToString() + "," + txt_menu_position.Text + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + ",'" + user_where.Replace("'", "''") + "','" + definition_field.Replace("'", "''") + "'," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
                ElseIf Main_Form.tv_menu.SelectedNode.Name.Contains("TERMINAL") Then
                    'load special form for terminal using
                End If

            ElseIf Menu_id.length <> 0 Then
                If Main_Form.tv_menu.SelectedNode.Name.Contains("SQL") Then
                    fn_sql_request("UPDATE [dbo].[form_filter] SET position =" + txt_menu_position.Text + ",systemname='" + txt_new_menu_name.Text + "',note='" + txt_db_note.Text.Replace("'", "") + "',enabled=" + CInt(Int(chb_enabled.Checked)).ToString + ",released=" + CInt(Int(chb_released.Checked)).ToString + ",definition_cmd='" + user_where.Replace("'", "''") + "',definition_field='" + definition_field.Replace("'", "''") + "',enable_translate=" + CInt(Int(chb_translate.Checked)).ToString + ", creator ='" + fn_search_substitution("sub[user_name]") + "',created=GETDATE() WHERE id=" + Menu_id + " ", "UPDATE", "local", False, True, Main_Form.sql_parameter, False, False)
                ElseIf Main_Form.tv_menu.SelectedNode.Name.Contains("TERMINAL") Then
                    'load special form for terminal using
                End If
            End If
            fn_load_filter_list()
        End If

        fn_cursor_waiting(False)
        Me.Close()

    End Sub


    Private Sub txt_menu_position_TextChanged(sender As Object, e As KeyPressEventArgs) Handles txt_menu_position.KeyPress
        If (Char.IsDigit(e.KeyChar) = False And e.KeyChar <> Chr(27) And e.KeyChar <> Chr(13) And e.KeyChar <> Chr(8)) Then
            e.KeyChar = Nothing
        End If
    End Sub

End Class