Public Class frm_new_favorite_item


    Private Sub frm_new_favorite_item_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        fn_reload_favorite_menu(True)
        Main_Form.Enabled = True
        fn_cursor_waiting(False)
    End Sub


    Private Sub frm_new_favorite_item_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub

    Private Sub frm_new_favorite_item_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = fn_translate(Me.Text)
        For Each ctrl_object As Control In Me.Controls
            Try
                ctrl_object.Text = fn_translate(ctrl_object.Text)
            Catch ex As Exception
            End Try
        Next

        Me.tv_menu.Nodes.Clear()

        Dim level As Integer = 0

        'load menu tree
        While fn_sql_request("SELECT TOP 1 id FROM [dbo].[menu_list] WHERE level =" + level.ToString, "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True
            fn_sql_request("SELECT ml.id,ml.menu_name,ml.parent_menu_id,ml.position,ml.[enabled],ml.[released] FROM [dbo].[menu_list] ml WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR ml.right_menu IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.[right] = ml.right_menu)) AND ml.level =" + level.ToString + " ORDER BY ml.level,ml.position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False)
            For i = 0 To My.Forms.Main_Form.sql_array_count - 1

                If My.Forms.Main_Form.sql_array(i, 2).Length = 0 Then
                    If (My.Forms.Main_Form.sql_array(i, 4) = True And My.Forms.Main_Form.sql_array(i, 5) = True) Then
                        Me.tv_menu.Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                    End If
                Else
                    If (My.Forms.Main_Form.sql_array(i, 4) = True And My.Forms.Main_Form.sql_array(i, 5) = True) Then
                        Me.tv_menu.Nodes(My.Forms.Main_Form.sql_array(i, 2)).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                    End If
                End If
            Next

            level += 1
        End While

        'load forms
        For Each node As TreeNode In Me.tv_menu.Nodes
            If fn_sql_request("SELECT id,form_name,form_type,position,[enabled],[released] FROM [dbo].[form_list] WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR right_read IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id )) AND parent_menu_id = " + node.Name.Replace("SQL", "").ToString + " ORDER BY position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                    If (My.Forms.Main_Form.sql_array(i, 4) = True And My.Forms.Main_Form.sql_array(i, 5) = True) Then
                        Me.tv_menu.Nodes(node.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))
                    End If
                Next
            End If
            For Each subnode As TreeNode In node.Nodes
                If fn_sql_request("SELECT id,form_name,form_type,position,[enabled],[released] FROM [dbo].[form_list] WHERE ('True' = '" + Main_Form.system_account.ToString + "' OR right_read IN (SELECT ur.[right] FROM [dbo].users usr,[dbo].[users_right] ur WHERE usr.user_name ='" + fn_search_substitution("sub[user_name]") + "' AND usr.id = ur.user_id AND ur.enabled=1 AND ur.released = 1)) AND parent_menu_id = " + subnode.Name.Replace("SQL", "").ToString + " ORDER BY position ASC", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
                    For i = 0 To My.Forms.Main_Form.sql_array_count - 1
                        If (My.Forms.Main_Form.sql_array(i, 4) = True And My.Forms.Main_Form.sql_array(i, 5) = True) Then

                            Me.tv_menu.Nodes(node.Name).Nodes(subnode.Name).Nodes.Add(My.Forms.Main_Form.sql_array(i, 0) + My.Forms.Main_Form.sql_array(i, 2), fn_translate(My.Forms.Main_Form.sql_array(i, 1)))

                        End If
                    Next
                End If
            Next
        Next
    End Sub


    Private Sub frm_new_menu_item_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Text = fn_translate("add_new_favorite_item")
        fn_cursor_waiting(False)
    End Sub



    Private Sub tv_menu_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles tv_menu.DoubleClick
        If IsNumeric(Me.tv_menu.SelectedNode.Name) = True Then
            'tree menu
        Else
            fn_sql_request("INSERT INTO [dbo].[favorite_form_list] (favorite_name,group_id,form_id,position,note,enabled,creator,released,enable_translate)VALUES('" + txt_new_menu_name.Text + "'," + Main_Form.tv_favorites_menu.SelectedNode.Name + ",'" + Me.tv_menu.SelectedNode.Name.ToString() + "'," + txt_menu_position.Text + ",'" + txt_db_note.Text.Replace("'", "") + "'," + CInt(Int(chb_enabled.Checked)).ToString + ",'" + fn_search_substitution("sub[user_name]") + "'," + CInt(Int(chb_released.Checked)).ToString + "," + CInt(Int(chb_translate.Checked)).ToString + ")", "INSERT", "local", False, True, Main_Form.sql_parameter, False, False)
        End If
        fn_cursor_waiting(False)
        Me.Close()
    End Sub

    Private Sub inputs_TextChanged(sender As Object, e As EventArgs) Handles txt_menu_position.TextChanged, txt_new_menu_name.TextChanged
        If String.IsNullOrWhiteSpace(txt_menu_position.Text) OrElse String.IsNullOrWhiteSpace(txt_new_menu_name.Text) Then
            tv_menu.Enabled = False
        Else
            tv_menu.Enabled = True
        End If
    End Sub

    Public Sub react_isdigit(sender As Object, e As KeyPressEventArgs)
        fn_numner_keys(e)
    End Sub

End Class