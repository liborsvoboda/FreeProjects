<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_logon
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_logon))
        Me.lbl_password = New System.Windows.Forms.Label()
        Me.txt_password = New System.Windows.Forms.TextBox()
        Me.txt_username = New System.Windows.Forms.TextBox()
        Me.lbl_username = New System.Windows.Forms.Label()
        Me.btn_logon = New System.Windows.Forms.Button()
        Me.pb_eye = New System.Windows.Forms.PictureBox()
        CType(Me.pb_eye, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_password
        '
        Me.lbl_password.BackColor = System.Drawing.Color.Transparent
        Me.lbl_password.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_password.Location = New System.Drawing.Point(12, 74)
        Me.lbl_password.Name = "lbl_password"
        Me.lbl_password.Size = New System.Drawing.Size(102, 16)
        Me.lbl_password.TabIndex = 110
        Me.lbl_password.Text = "password"
        Me.lbl_password.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_password
        '
        Me.txt_password.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_password.Location = New System.Drawing.Point(150, 70)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.Size = New System.Drawing.Size(140, 22)
        Me.txt_password.TabIndex = 3
        Me.txt_password.Text = "a"
        Me.txt_password.UseSystemPasswordChar = True
        Me.txt_password.WordWrap = False
        '
        'txt_username
        '
        Me.txt_username.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_username.Location = New System.Drawing.Point(150, 22)
        Me.txt_username.Name = "txt_username"
        Me.txt_username.Size = New System.Drawing.Size(140, 22)
        Me.txt_username.TabIndex = 2
        Me.txt_username.Text = "admin"
        '
        'lbl_username
        '
        Me.lbl_username.BackColor = System.Drawing.Color.Transparent
        Me.lbl_username.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_username.Location = New System.Drawing.Point(15, 25)
        Me.lbl_username.Name = "lbl_username"
        Me.lbl_username.Size = New System.Drawing.Size(99, 19)
        Me.lbl_username.TabIndex = 100
        Me.lbl_username.Text = "username"
        Me.lbl_username.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btn_logon
        '
        Me.btn_logon.Location = New System.Drawing.Point(246, 120)
        Me.btn_logon.Name = "btn_logon"
        Me.btn_logon.Size = New System.Drawing.Size(75, 23)
        Me.btn_logon.TabIndex = 5
        Me.btn_logon.Text = "login"
        Me.btn_logon.UseVisualStyleBackColor = True
        '
        'pb_eye
        '
        Me.pb_eye.AccessibleDescription = ""
        Me.pb_eye.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pb_eye.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_eye.Image = Global.SWAPP_Builder.My.Resources.Resources.eye
        Me.pb_eye.Location = New System.Drawing.Point(296, 70)
        Me.pb_eye.Name = "pb_eye"
        Me.pb_eye.Size = New System.Drawing.Size(25, 22)
        Me.pb_eye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_eye.TabIndex = 6
        Me.pb_eye.TabStop = False
        Me.pb_eye.Tag = ""
        '
        'frm_logon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SWAPP_Builder.My.Resources.Resources.app_builder
        Me.ClientSize = New System.Drawing.Size(333, 155)
        Me.Controls.Add(Me.pb_eye)
        Me.Controls.Add(Me.btn_logon)
        Me.Controls.Add(Me.lbl_username)
        Me.Controls.Add(Me.txt_username)
        Me.Controls.Add(Me.txt_password)
        Me.Controls.Add(Me.lbl_password)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_logon"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "loginform"
        Me.TopMost = True
        CType(Me.pb_eye, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_password As System.Windows.Forms.Label
    Friend WithEvents txt_password As System.Windows.Forms.TextBox
    Friend WithEvents txt_username As System.Windows.Forms.TextBox
    Friend WithEvents lbl_username As System.Windows.Forms.Label
    Friend WithEvents btn_logon As System.Windows.Forms.Button
    Friend WithEvents pb_eye As System.Windows.Forms.PictureBox
End Class
