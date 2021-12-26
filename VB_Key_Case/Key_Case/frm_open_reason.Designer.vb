<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_open_reason
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_open_reason))
        Me.lbl_open_reason = New System.Windows.Forms.Label()
        Me.txt_open_reason = New System.Windows.Forms.TextBox()
        Me.btn_save_open_reason = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lbl_open_reason
        '
        Me.lbl_open_reason.AutoSize = True
        Me.lbl_open_reason.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_open_reason.Location = New System.Drawing.Point(-1, 6)
        Me.lbl_open_reason.Name = "lbl_open_reason"
        Me.lbl_open_reason.Size = New System.Drawing.Size(115, 16)
        Me.lbl_open_reason.TabIndex = 0
        Me.lbl_open_reason.Text = "Důvod Otevření"
        '
        'txt_open_reason
        '
        Me.txt_open_reason.Location = New System.Drawing.Point(2, 25)
        Me.txt_open_reason.Multiline = True
        Me.txt_open_reason.Name = "txt_open_reason"
        Me.txt_open_reason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_open_reason.Size = New System.Drawing.Size(317, 90)
        Me.txt_open_reason.TabIndex = 10
        '
        'btn_save_open_reason
        '
        Me.btn_save_open_reason.Enabled = False
        Me.btn_save_open_reason.Location = New System.Drawing.Point(2, 121)
        Me.btn_save_open_reason.Name = "btn_save_open_reason"
        Me.btn_save_open_reason.Size = New System.Drawing.Size(75, 23)
        Me.btn_save_open_reason.TabIndex = 20
        Me.btn_save_open_reason.Text = "Ulož Důvod"
        Me.btn_save_open_reason.UseVisualStyleBackColor = True
        '
        'frm_open_reason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 146)
        Me.Controls.Add(Me.btn_save_open_reason)
        Me.Controls.Add(Me.txt_open_reason)
        Me.Controls.Add(Me.lbl_open_reason)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_open_reason"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Důvod otevření skříně"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_open_reason As System.Windows.Forms.Label
    Friend WithEvents txt_open_reason As System.Windows.Forms.TextBox
    Friend WithEvents btn_save_open_reason As System.Windows.Forms.Button
End Class
