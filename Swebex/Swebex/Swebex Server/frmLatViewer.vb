Public Class frmLatViewer

    Public bytes() As Byte

    Public Sub PassBytes(ByVal _bytes() As byte)
        bytes = _bytes
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close
    End Sub

    Private Sub frmLatViewer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.tbLatViewer.Text = "Transfer Complete. Loading array into this viewer..."
        Application.DoEvents

        Me.lblBytesCount.Text = "Bytes in this message: " & bytes.Length.ToString()
        Me.tbLatViewer.Text = Swebex_Server.chat_Utilities.BytesToString(bytes)
    End Sub
End Class