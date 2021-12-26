Public Class frm_form_list


    Private Sub frm_form_list_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Main_Form.Enabled = True
    End Sub


    Private Sub frm_form_list_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub

    Private Sub frm_form_list_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.tv_menu.Nodes.Clear()
        Dim level As Integer = 0
    End Sub


    Private Sub frm_new_menu_item_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Text = fn_translate("select_form")
        txt_menu_search.Text = fn_translate("search")
        fn_load_menu("")
        fn_cursor_waiting(False)
    End Sub



    Private Sub txt_menu_search_selection(sender As Object, e As EventArgs) Handles txt_menu_search.MouseClick, txt_menu_search.GotFocus
        If Me.Visible = True Then
            If txt_menu_search.Text = fn_translate("search") Then
                txt_menu_search.SelectAll()
            End If
        End If
    End Sub


    Private Sub txt_menu_search_TextChanged(sender As Object, e As EventArgs) Handles txt_menu_search.TextChanged
        If Me.Visible = True Then
            If txt_menu_search.Text = fn_translate("search") Then
                txt_menu_search.ForeColor = Color.Gray
            Else
                txt_menu_search.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub btn_search_menu_Click(sender As Object, e As EventArgs) Handles btn_search_menu.Click
        fn_load_menu(txt_menu_search.Text)
        tv_menu.ExpandAll()
        fn_cursor_waiting(False)
    End Sub


    Private Sub btn_search_clear_Click(sender As Object, e As EventArgs) Handles btn_search_clear.Click
        txt_menu_search.Text = fn_translate("search")
        txt_menu_search.ForeColor = Color.Gray
        fn_load_menu("")
        fn_cursor_waiting(False)
    End Sub


    Private Function fn_load_menu(ByVal search_str As String) As Boolean
        tv_menu.Nodes.Clear()

        Dim level As Integer = 0

        'load menu tree
        While fn_sql_request("SELECT TOP 1 id FROM [dbo].[menu_list] WHERE level =" & level.ToString, "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)
            fn_sql_request("SELECT ml.id,ml.menu_name,ml.parent_menu_id,ml.position,ml.[enabled],ml.[released],ml.[enable_translate] FROM [dbo].[menu_list] ml WHERE ('True' = '" & Main_Form.system_account.ToString & "' OR ml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" & fn_search_substitution("sub[user_name]") & "' AND usr.id = ur.user_id AND ur.[right] = ml.right_menu)) AND ml.level =" & level.ToString & " ORDER BY ml.level,ml.position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)
            For i = 0 To My.Forms.Main_Form.sql_array_count - 1

                If My.Forms.Main_Form.sql_array(i, 2).Length = 0 Then
                    If My.Forms.Main_Form.sql_array(i, 4) = True Then
                        If My.Forms.Main_Form.sql_array(i, 6) Then
                            tv_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                        Else
                            tv_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(My.Forms.Main_Form.sql_array(i, 1)))
                        End If
                    End If

                Else
                    If My.Forms.Main_Form.sql_array(i, 4) = True Then
                        If My.Forms.Main_Form.sql_array(i, 6) Then
                            tv_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))))
                        Else
                            tv_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), UCase(My.Forms.Main_Form.sql_array(i, 1)))
                        End If
                    End If
                End If
            Next

            level += 1
        End While

        'load forms
        For Each node As TreeNode In tv_menu.Nodes
            If fn_sql_request("SELECT id,form_name,form_type,position,[enabled],[released],[enable_translate] FROM [dbo].[form_list] WHERE [Id] <>" + Main_Form.lbl_dev_form_id.Text + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR right_read IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND parent_menu_id = " + node.Name.Replace("SQL", "").ToString + " ORDER BY position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                    If My.Forms.Main_Form.sql_array(i, 4) = True Then
                        If search_str.Length > 0 Then
                            If UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))).StartsWith(UCase(search_str)) Then
                                If My.Forms.Main_Form.sql_array(i, 6) Then
                                    If Main_Form.dev_lv_subform_list.FindItemWithText(fn_translate(My.Forms.Main_Form.sql_array(i, 1))) Is Nothing Then
                                        tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                    End If
                                Else
                                    If Main_Form.dev_lv_subform_list.FindItemWithText(My.Forms.Main_Form.sql_array(i, 1)) Is Nothing Then
                                        tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                    End If
                                End If
                            End If
                        Else
                            If My.Forms.Main_Form.sql_array(i, 6) Then
                                If Main_Form.dev_lv_subform_list.FindItemWithText(fn_translate(My.Forms.Main_Form.sql_array(i, 1))) Is Nothing Then
                                    tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                End If
                            Else
                                If Main_Form.dev_lv_subform_list.FindItemWithText(My.Forms.Main_Form.sql_array(i, 1)) Is Nothing Then
                                    tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            For Each subnode As TreeNode In node.Nodes
                If fn_sql_request("SELECT id,form_name,form_type,position,[enabled],[released],[enable_translate] FROM [dbo].[form_list] WHERE [Id] <>" + Main_Form.lbl_dev_form_id.Text + " AND ('True' = '" + Main_Form.system_account.ToString + "' OR right_read IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.enabled=1 AND ur.released = 1)) AND parent_menu_id = " + subnode.Name.Replace("SQL", "").ToString + " ORDER BY position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                        If My.Forms.Main_Form.sql_array(i, 4) = True Then
                            If search_str.Length > 0 Then
                                If UCase(fn_translate(My.Forms.Main_Form.sql_array(i, 1))).StartsWith(UCase(search_str)) Then
                                    If My.Forms.Main_Form.sql_array(i, 6) Then
                                        If Main_Form.dev_lv_subform_list.FindItemWithText(fn_translate(My.Forms.Main_Form.sql_array(i, 1))) Is Nothing Then
                                            tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                        End If
                                    Else
                                        If Main_Form.dev_lv_subform_list.FindItemWithText(My.Forms.Main_Form.sql_array(i, 1)) Is Nothing Then
                                            tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                        End If
                                    End If
                                End If
                            Else
                                If My.Forms.Main_Form.sql_array(i, 6) Then
                                    If Main_Form.dev_lv_subform_list.FindItemWithText(fn_translate(My.Forms.Main_Form.sql_array(i, 1))) Is Nothing Then
                                        tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                                    End If
                                Else
                                    If Main_Form.dev_lv_subform_list.FindItemWithText(My.Forms.Main_Form.sql_array(i, 1)) Is Nothing Then
                                        tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), My.Forms.Main_Form.sql_array(i, 1))
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Next
        Next
    End Function

    Private Sub tv_menu_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_menu.MouseDoubleClick
        Dim item As New ListViewItem
        If Not tv_menu.SelectedNode Is Nothing Then
            If Not IsNumeric(tv_menu.SelectedNode.Name) = True Then
                item.Text = tv_menu.SelectedNode.Text
                item.Name = tv_menu.SelectedNode.Name
                Main_Form.dev_lv_subform_list.Items.Add(item)

                Main_Form.dev_lb_source_field_list.Items.Clear()
                For Each col As DataGridViewColumn In Main_Form.dgv_dev_sql_preview.Columns
                    Main_Form.dev_lb_source_field_list.Items.Add(col.Name)
                Next

                new_subform = "new,tv_menu.SelectedNode.Name"
                Me.Close()
            End If
        End If
    End Sub
End Class