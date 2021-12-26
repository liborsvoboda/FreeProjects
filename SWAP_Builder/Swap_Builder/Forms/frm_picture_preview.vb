Public Class frm_picture_preview


    Private Sub frm_picture_preview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Forms.Main_Form.Enabled = False
    End Sub


    Private Sub frm_picture_preview_Loaded(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Text = fn_translate(Me.Text)
        fn_cursor_waiting(False)
    End Sub


    Private Sub frm_picture_preview_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then Me.Close()
    End Sub


    Private Sub frm_picture_preview_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        My.Forms.Main_Form.Enabled = True
    End Sub

End Class