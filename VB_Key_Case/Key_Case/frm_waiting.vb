Public Class frm_waiting


    Private Sub frm_waiting_Load(sender As Object, e As EventArgs) Handles Me.Load
        fn_app_load()

        'Dim pict_no = 1

        Select Case Main_Form.selected_picture
            Case 1
                pb_waiting_picture.Image = My.Resources.waiting1
            Case 2
                pb_waiting_picture.Image = My.Resources.waiting2
            Case 3
                pb_waiting_picture.Image = My.Resources.waiting3
            Case 4
                pb_waiting_picture.Image = My.Resources.waiting4
            Case 5
                pb_waiting_picture.Image = My.Resources.waiting5
            Case Else
                pb_waiting_picture.Image = My.Resources.waiting1
        End Select


    End Sub

End Class


