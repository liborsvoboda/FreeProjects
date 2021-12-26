<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_picture_preview
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_picture_preview))
        Me.pb_image_preview = New System.Windows.Forms.PictureBox()
        CType(Me.pb_image_preview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pb_image_preview
        '
        Me.pb_image_preview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb_image_preview.Location = New System.Drawing.Point(0, 0)
        Me.pb_image_preview.Name = "pb_image_preview"
        Me.pb_image_preview.Size = New System.Drawing.Size(607, 583)
        Me.pb_image_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pb_image_preview.TabIndex = 0
        Me.pb_image_preview.TabStop = False
        '
        'frm_picture_preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 583)
        Me.Controls.Add(Me.pb_image_preview)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frm_picture_preview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "picture_preview"
        Me.TopMost = True
        CType(Me.pb_image_preview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pb_image_preview As System.Windows.Forms.PictureBox
End Class
