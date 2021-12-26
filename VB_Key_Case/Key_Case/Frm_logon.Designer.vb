<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_logon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_logon))
        Me.btn_logon = New System.Windows.Forms.Button()
        Me.cb_user_list = New System.Windows.Forms.ComboBox()
        Me.lbl_logon_user = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_logon
        '
        Me.btn_logon.Location = New System.Drawing.Point(253, 62)
        Me.btn_logon.Name = "btn_logon"
        Me.btn_logon.Size = New System.Drawing.Size(75, 23)
        Me.btn_logon.TabIndex = 21
        Me.btn_logon.Text = "Přihlásit"
        Me.btn_logon.UseVisualStyleBackColor = True
        '
        'cb_user_list
        '
        Me.cb_user_list.FormattingEnabled = True
        Me.cb_user_list.Location = New System.Drawing.Point(138, 19)
        Me.cb_user_list.Name = "cb_user_list"
        Me.cb_user_list.Size = New System.Drawing.Size(190, 21)
        Me.cb_user_list.TabIndex = 22
        '
        'lbl_logon_user
        '
        Me.lbl_logon_user.BackColor = System.Drawing.Color.Transparent
        Me.lbl_logon_user.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_logon_user.Location = New System.Drawing.Point(12, 19)
        Me.lbl_logon_user.Name = "lbl_logon_user"
        Me.lbl_logon_user.Size = New System.Drawing.Size(105, 21)
        Me.lbl_logon_user.TabIndex = 23
        Me.lbl_logon_user.Text = "Uživatel"
        Me.lbl_logon_user.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Frm_logon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Key_Case.My.Resources.Resources.app_builder
        Me.ClientSize = New System.Drawing.Size(340, 97)
        Me.Controls.Add(Me.lbl_logon_user)
        Me.Controls.Add(Me.cb_user_list)
        Me.Controls.Add(Me.btn_logon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_logon"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Přihlášení"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_logon As System.Windows.Forms.Button
    Friend WithEvents cb_user_list As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_logon_user As System.Windows.Forms.Label
End Class
