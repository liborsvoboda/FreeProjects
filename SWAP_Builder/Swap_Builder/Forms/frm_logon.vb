Imports System.Data
Imports System.Data.SqlClient

Public Class frm_logon

    Protected password As Byte()

    Private Sub frm_logon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Forms.Main_Form.Enabled = False
    End Sub


    Private Sub frm_logon_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Focus()
        Me.Text = fn_translate(Me.Text)
        fn_translate_login_form()
        fn_cursor_waiting(False)
    End Sub


    Private Sub pb_eye_Click(sender As Object, e As EventArgs) Handles pb_eye.Click
        If txt_password.UseSystemPasswordChar = True Then txt_password.UseSystemPasswordChar = False Else txt_password.UseSystemPasswordChar = True

    End Sub

    Private Sub frm_new_menu_item_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub

    Private Sub frm_logon_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim result = MsgBox(fn_translate("close_app?"), MsgBoxStyle.YesNo, fn_translate("close_app"))
        If result = vbYes Then
            Application.Exit()
        Else
            e.Cancel = True
        End If
    End Sub


    Private Sub btn_logon_Click(sender As Object, e As EventArgs) Handles btn_logon.Click
        password = System.Text.Encoding.UTF8.GetBytes(Me.txt_password.Text)
        If fn_sql_request("SELECT user_name,name,surname,sysaccount FROM [dbo].[users] WHERE user_name = '" + txt_username.Text + "' AND password ='" + System.Convert.ToBase64String(password).ToString + "' AND enabled=1 AND released = 1", "SELECT", "local", False, True, Main_Form.sql_parameter, False, False) = True Then
            My.Forms.Main_Form.Enabled = True
            My.Forms.Main_Form.username = txt_username.Text
            fn_insert_substitution("sub[user_name]", txt_username.Text)
            My.Forms.Main_Form.system_account = Main_Form.sql_array(0, 3)
            Main_Form.lbl_system_info.Text = fn_translate("logged") + ": " + Main_Form.sql_array(0, 1) + " " + Main_Form.sql_array(0, 2)
            fn_insert_substitution("sub[user_fullname]", Main_Form.sql_array(0, 1) + " " + Main_Form.sql_array(0, 2))

            If fn_load_user_setting() = False Then
                My.Forms.Main_Form.tc_data.SelectedIndex = 1
            Else
                My.Forms.Main_Form.tc_data.SelectedIndex = 0
                fn_load_menu("")
                If My.Forms.Main_Form.cb_global_settings_default_keyboard.SelectedItem.length > 0 Then fn_set_keyboard(My.Forms.Main_Form.cb_global_settings_default_keyboard.SelectedItem)
                'fn_set_app_size()
                My.Forms.Main_Form.tv_menu.SelectedNode = Nothing
            End If

            fn_add_login_substitution()
            Main_Form.tc_data.SelectedIndex = 1
            Main_Form.tc_data.SelectedIndex = 0
            Main_Form.app_loaded = True
            Main_Form.Visible = True
            Me.Dispose()
            My.Forms.Main_Form.Focus()

        Else
            MsgBox(fn_translate("bad_login"), MsgBoxStyle.Information, fn_translate("loginform"))
        End If

        fn_cursor_waiting(False)
    End Sub

End Class