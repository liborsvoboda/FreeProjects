Imports System.Data

Public Class frm_waiting

    Private cmd As New SqlClient.SqlCommand
    Private sqlConnection_string As New System.Data.SqlClient.SqlConnection
    Private reader As System.Data.SqlClient.SqlDataReader
    Private pict_no = "1"

    Public Sub New()
        InitializeComponent()
        sqlConnection_string = New System.Data.SqlClient.SqlConnection("Data Source=(LocalDB)\v13.0;AttachDbFilename=" & IO.Path.Combine(Application.StartupPath, "App_structure.mdf") & ";Initial Catalog=App_structure;Integrated Security=True;Persist Security Info=True;Connect Timeout=60;Context Connection=False")
        cmd = New SqlClient.SqlCommand("SELECT configuration FROM [dbo].[app_setting] WHERE name = 'default_waiting_picture' AND enabled=1 AND released =1 ", sqlConnection_string)
        sqlConnection_string.Open()
        reader = cmd.ExecuteReader()
        reader.Read()
        pict_no = reader(0).ToString
        pb_waiting_picture.ImageLocation = IO.Path.Combine(Application.StartupPath, "images", "waiting" & pict_no.ToString & ".gif")
        reader.Close()
        sqlConnection_string.Close()

    End Sub

    Private Sub frm_waiting_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class


