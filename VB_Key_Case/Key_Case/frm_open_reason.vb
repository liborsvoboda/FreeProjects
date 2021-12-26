Public Class frm_open_reason
    Private status As Boolean = False

    Private Sub btn_save_open_reason_Click(sender As Object, e As EventArgs) Handles btn_save_open_reason.Click
        Main_Form.open_reason = txt_open_reason.Text
        status = True
        Me.Close()
    End Sub

    Private Sub txt_open_reason_TextChanged(sender As Object, e As EventArgs) Handles txt_open_reason.TextChanged
        If txt_open_reason.Text.Length > 0 Then
            btn_save_open_reason.Enabled = True
        Else
            btn_save_open_reason.Enabled = False
        End If
    End Sub


    Private Sub frm_open_reason_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If status = False Then
            Dim result1 = MsgBox("Chcet zrušit smázání formuláře?", MsgBoxStyle.YesNo, "Zrušení Smazání Formuláře")
            If result1 = vbYes Then
                Main_Form.Enabled = True
            Else
                e.Cancel = True
            End If
        Else
            Main_Form.Enabled = True
        End If
    End Sub


    Private Sub frm_open_reason_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub

    Private Sub frm_open_reason_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_open_reason.Text = ""
        status = False
    End Sub


End Class