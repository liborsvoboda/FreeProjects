<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_waiting
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pb_waiting_picture = New System.Windows.Forms.PictureBox()
        CType(Me.pb_waiting_picture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pb_waiting_picture
        '
        Me.pb_waiting_picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb_waiting_picture.ImageLocation = ""
        Me.pb_waiting_picture.Location = New System.Drawing.Point(0, 0)
        Me.pb_waiting_picture.Name = "pb_waiting_picture"
        Me.pb_waiting_picture.Size = New System.Drawing.Size(403, 196)
        Me.pb_waiting_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_waiting_picture.TabIndex = 3
        Me.pb_waiting_picture.TabStop = False
        Me.pb_waiting_picture.UseWaitCursor = True
        '
        'frm_waiting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 196)
        Me.ControlBox = False
        Me.Controls.Add(Me.pb_waiting_picture)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_waiting"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.UseWaitCursor = True
        CType(Me.pb_waiting_picture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pb_waiting_picture As System.Windows.Forms.PictureBox
End Class
