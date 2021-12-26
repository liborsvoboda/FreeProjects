<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLatViewer
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
        Me.tbLatViewer = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblBytesCount = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'tbLatViewer
        '
        Me.tbLatViewer.Location = New System.Drawing.Point(13, 13)
        Me.tbLatViewer.Multiline = true
        Me.tbLatViewer.Name = "tbLatViewer"
        Me.tbLatViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbLatViewer.Size = New System.Drawing.Size(421, 378)
        Me.tbLatViewer.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(359, 398)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Ok"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'lblBytesCount
        '
        Me.lblBytesCount.AutoSize = true
        Me.lblBytesCount.Location = New System.Drawing.Point(13, 398)
        Me.lblBytesCount.Name = "lblBytesCount"
        Me.lblBytesCount.Size = New System.Drawing.Size(111, 13)
        Me.lblBytesCount.TabIndex = 2
        Me.lblBytesCount.Text = "Bytes in this message:"
        '
        'frmLatViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(446, 433)
        Me.Controls.Add(Me.lblBytesCount)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.tbLatViewer)
        Me.Name = "frmLatViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Large Array Transfer Viewer"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents tbLatViewer As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblBytesCount As System.Windows.Forms.Label
End Class
