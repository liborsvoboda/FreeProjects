Public Class Main_Form

    Public config_dir As String = "\DATA"
    Public config_file As String = "\config.ini"

    Public users_file As String = fn_get_application_path() + config_dir + "\users.dat"
    Public job_list_file As String = fn_get_application_path() + config_dir + "\job_list.dat"
    Public scanned_file As String = fn_get_application_path() + config_dir + "\scanned.dat"
    Public log_file As String = fn_get_application_path() + config_dir + "\work.log"

    Public sql_array_count As Integer = 0
    Public sql_array(0, 0) As String
    'Public my_result, time_check
    Public status = "ONLINE"
    Public admin_right As Boolean = False
    Public sql_error As Boolean = False

    ' load form

    Private Sub Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fn_load_setting_file()
        My.Forms.Form_Logon.Show()
        My.Forms.Form_Logon.cb_logon.Focus()
        check_data_for_export()
    End Sub
    'end of load form


    Private Sub btn_import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_import.Click
        If fn_sql_connection_check() = True Then
            fn_load_dbusers()
            fn_load_job_list()

        Else
            MessageBox.Show("Import nelze provést ve stavu OFFLINE")
        End If

    End Sub


    'Private Sub txt_request_id_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_request_id.TextChanged
    '    If txt_request_id.Text.Length > 0 Then
    '        Me.txt_request_id.Text = fn_remove_barcode_symbol(Me.txt_request_id.Text)

    '        If IsNumeric(Me.txt_request_id.Text) = True Then
    '            fn_search_id_poz(Me.txt_request_id.Text)
    '        Else
    '            MessageBox.Show("ID Požadavku musí být èíslo")
    '        End If
    '    End If
    'End Sub


    'Private Sub txt_request_id_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_request_id.LostFocus
    '    If txt_request_id.Text.Length > 0 Then
    '        Me.txt_request_id.Text = fn_remove_barcode_symbol(Me.txt_request_id.Text)

    '        If IsNumeric(Me.txt_request_id.Text) = True Then
    '            If fn_search_id_poz(Me.txt_request_id.Text) = False Then Me.txt_request_id.Focus()
    '        Else
    '            MessageBox.Show("ID Požadavku musí být èíslo LOST")
    '            Me.txt_request_id.Focus()
    '        End If
    '    End If
    'End Sub


    Private Sub txt_request_id_Entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_request_id.KeyDown
        If txt_request_id.Text.Length > 0 And e.KeyCode = Keys.Enter Then
            Me.txt_request_id.Text = fn_remove_barcode_symbol(fn_remove_prefix_symbol(Me.txt_request_id.Text))

            If IsNumeric(Me.txt_request_id.Text) = True Then
                If fn_search_id_poz(Me.txt_request_id.Text) = False Then Me.txt_request_id.Focus()
            Else
                MessageBox.Show("ID Požadavku musí být èíslo ENTER")
                Me.txt_request_id.Focus()
                Me.txt_request_id.SelectAll()
            End If
        End If
    End Sub


    Private Sub lb_control_type_Entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lb_control_type.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.txt_controled_pcs.Focus()
        End If

    End Sub


    Private Sub txt_controled_pcs_Entered(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_controled_pcs.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.btn_save.Focus()
        End If
    End Sub


    Private Sub txt_controled_pcs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_controled_pcs.TextChanged
        Try
            If Me.txt_controled_pcs.Text.Length > 0 Then
                If IsNumeric(Me.txt_controled_pcs.Text) = False Then
                    MessageBox.Show("Hodnota musí být kladné èíslo")
                    Me.btn_save.Enabled = False
                    Exit Sub
                Else
                    If CDbl(Me.txt_controled_pcs.Text) > 0 Then
                        If CDbl(txt_planvyroba.Text) >= CDbl(Me.txt_controled_pcs.Text) Then
                            Me.btn_save.Enabled = True
                        Else
                            MessageBox.Show("Pøesáhli jste max. kontrolované množství.")
                            Me.btn_save.Enabled = False
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Hodnota musí být kladné èíslo")
                        Me.btn_save.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        'check request_id
        If txt_request_id.Text.Length > 0 Then
            Me.txt_request_id.Text = fn_remove_barcode_symbol(fn_remove_prefix_symbol(Me.txt_request_id.Text))

            If IsNumeric(Me.txt_request_id.Text) = True Then
                If fn_search_id_poz(Me.txt_request_id.Text) = False Then
                    MessageBox.Show("Id požadavku nebylo nalezeno")
                    Me.txt_request_id.Focus()
                    Exit Sub
                End If
            Else
                MessageBox.Show("ID Požadavku musí být èíslo ENTER")
                Me.txt_request_id.Focus()
                Exit Sub
            End If
        End If


        fn_save_new_record_to_file()

        If btn_status.Text = "ONLINE" Then
            Dim linecount = check_data_for_export()
            If linecount > 0 Then
                fn_insert_rec_to_db(linecount, False)
            End If
        End If

    End Sub


    Private Sub btn_status_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_status.Click
        If btn_status.Text = "OFFLINE" Then
            fn_sql_connection_check()
        Else
            btn_status.Text = "OFFLINE"
            Me.btn_status.ForeColor = Color.Red
        End If

    End Sub



    Private Sub btn_export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_export.Click
        Dim linecount = check_data_for_export()
        If linecount > 0 And fn_sql_connection_check() = True Then
            fn_insert_rec_to_db(linecount, True)
        End If
    End Sub


    Private Sub Main_Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If closing_app = False Then
            If check_data_for_export() > 0 Then
                Dim export_result = MsgBox("Nìkterá data nebyla Exportována do IS KARAT" + vbNewLine + "Chcete pokraèovat? ", MsgBoxStyle.YesNo)
                If export_result = vbNo Then
                    fn_close_form("Form_logon;Inventura_majetku;")
                Else
                    e.Cancel = True
                End If
            Else
                Dim result = MsgBox("Chcete Aplikaci skuteènì Ukonèit?", MsgBoxStyle.YesNo)
                If result = vbYes Then
                    fn_close_form("Form_logon;Inventura_majetku;")
                Else
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

End Class
