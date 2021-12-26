Public Class Form_Logon


    Private Sub Form_logon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Forms.Main_Form.Enabled = False
        Me.Focus()
        Me.cb_logon.Focus()

        'If fn_sql_connection_check() = True Then
        'fn_load_dbusers()
        'fn_load_job_list()
        'End If
    End Sub


    Private Sub cb_logon_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_logon.TextChanged
        If fn_check_match_logon_name(UCase(Me.cb_logon.Text.ToString)) = True Then
            btn_logon.Enabled = True
            btn_logon.Focus()
        Else
            btn_logon.Enabled = False
            Me.cb_logon.Focus()
        End If
    End Sub


    Private Sub btn_logon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_logon.Click
        If fn_check_match_logon_name(UCase(Me.cb_logon.Text.ToString)) = True Then
            My.Forms.Main_Form.Enabled = True
            My.Forms.Main_Form.Refresh()
            fn_load_setting_file()
            fn_set_admin_object()
            fn_load_import_info()

            If check_data_for_export() > 0 Then
                MsgBox("Byla nalezena neexportovaná data. " + vbNewLine + "Proveïte EXPORT")
            End If

            My.Forms.Main_Form.Focus()
            My.Forms.Main_Form.txt_request_id.Focus()
            Me.Hide()
        Else
            Me.btn_logon.Enabled = False
            Me.cb_logon.Focus()
        End If
    End Sub


    Private Sub Form_print_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If closing_app = False Then
            Dim result = MsgBox("Chcete Aplikaci skuteènì Ukonèit?", MsgBoxStyle.YesNo)
            If result = vbYes Then
                fn_close_form("Form_logon;Main_Form;")
            Else
                e.Cancel = True
            End If
        End If
    End Sub


End Class