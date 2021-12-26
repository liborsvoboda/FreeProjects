Imports Microsoft.VisualBasic
Imports System
Imports System.Diagnostics
Imports System.ComponentModel

Public Class Main_Form
    Public terminal_result, terminal_id_result, terminal_id_result_last, term_status


    Public Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If fn_load_setting() = False Then
            Me.TabControl.SelectedIndex = 1
        Else
            btn_refresh.PerformClick()
        End If
    End Sub


    Public Sub btn_save_setting_Click(sender As Object, e As EventArgs) Handles btn_save_setting.Click
        fn_save_file_setting()
    End Sub



    Public Sub btn_add_new_user_Click(sender As Object, e As EventArgs) Handles btn_add_new_user.Click
        Me.lb_user_list.Items.Add(Me.txt_new_user.Text)
        Me.txt_new_user.Text = ""
        Me.txt_new_user.Focus()
    End Sub



    Public Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        Dim trp, trp1, trp2
        Me.dgv_connected_users.Rows.Clear()
        Try

            For i = 1 To (Me.lb_user_list.Items.Count - 1) Step 1
                If CStr(Me.lb_user_list.Items.Item(i).ToString) <> "" Then
                    run_command("SEARCH", Me.lb_user_list.Items.Item(i).ToString, Me.txt_windows_terminal_server.Text)

                    If Me.terminal_result.ToString.Length > 0 Then
                        trp = Split(Me.terminal_result, Me.lb_user_list.Items.Item(i).ToString)
                        For count = 1 To trp.Length - 1 Step 1
                            Me.term_status = "Aktivní"
                            trp1 = trp(count).split("A")
                            If trp1(0).ToString.Length = trp(count).ToString.Length Then
                                trp1 = trp(count).split("Dis")
                                Me.term_status = "Odpojeno"
                            End If
                            terminal_id_result = CStr(trp1(0))
                            If terminal_id_result_last <> terminal_id_result Then
                                trp2 = Replace(trp1(0), lb_user_list.Items.Item(i).ToString, "")
                                trp2 = trp2.Replace(" ", "")
                                Me.dgv_connected_users.Rows.Add(Me.lb_user_list.Items.Item(i).ToString, CStr(trp2), CStr(Me.term_status), "Ukončit Relaci")
                                Me.dgv_connected_users.Update()
                            End If
                            terminal_id_result_last = terminal_id_result
                        Next
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub


    Public Sub dg_btn_release_user_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_connected_users.CellContentClick
        If e.ColumnIndex = 3 Then
            Dim result As DialogResult
            result = MessageBox.Show("Chcete Připojení :  " + Me.dgv_connected_users.Rows(e.RowIndex).Cells(e.ColumnIndex - 2).Value() + " " + Me.dgv_connected_users.Rows(e.RowIndex).Cells(e.ColumnIndex - 3).Value() + " skutečně Ukončit?", "TERMINAL SERVER - Ukončení Připojení", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                run_command("LOGOFF", Me.dgv_connected_users.Rows(e.RowIndex).Cells(e.ColumnIndex - 2).Value(), Me.txt_windows_terminal_server.Text)
                Me.btn_refresh.PerformClick()
            End If
        End If
    End Sub


End Class

