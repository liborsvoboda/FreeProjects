<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_new_favorite_item
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_new_favorite_item))
        Me.tv_menu = New System.Windows.Forms.TreeView()
        Me.chb_translate = New System.Windows.Forms.CheckBox()
        Me.chb_released = New System.Windows.Forms.CheckBox()
        Me.chb_enabled = New System.Windows.Forms.CheckBox()
        Me.lbl_db_note = New System.Windows.Forms.Label()
        Me.txt_db_note = New System.Windows.Forms.TextBox()
        Me.txt_menu_position = New System.Windows.Forms.TextBox()
        Me.lbl_menu_position = New System.Windows.Forms.Label()
        Me.txt_new_menu_name = New System.Windows.Forms.TextBox()
        Me.lbl_new_menu_name = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tv_menu
        '
        Me.tv_menu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tv_menu.Enabled = False
        Me.tv_menu.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tv_menu.FullRowSelect = True
        Me.tv_menu.HideSelection = False
        Me.tv_menu.HotTracking = True
        Me.tv_menu.LineColor = System.Drawing.Color.DarkGray
        Me.tv_menu.Location = New System.Drawing.Point(395, -1)
        Me.tv_menu.Name = "tv_menu"
        Me.tv_menu.Size = New System.Drawing.Size(244, 360)
        Me.tv_menu.TabIndex = 2
        '
        'chb_translate
        '
        Me.chb_translate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_translate.Checked = True
        Me.chb_translate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_translate.Location = New System.Drawing.Point(250, 118)
        Me.chb_translate.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_translate.Name = "chb_translate"
        Me.chb_translate.Size = New System.Drawing.Size(139, 19)
        Me.chb_translate.TabIndex = 70
        Me.chb_translate.Text = "enable_translate"
        Me.chb_translate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_translate.UseVisualStyleBackColor = True
        '
        'chb_released
        '
        Me.chb_released.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_released.Checked = True
        Me.chb_released.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_released.Location = New System.Drawing.Point(250, 99)
        Me.chb_released.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_released.Name = "chb_released"
        Me.chb_released.Size = New System.Drawing.Size(139, 19)
        Me.chb_released.TabIndex = 68
        Me.chb_released.Text = "release"
        Me.chb_released.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_released.UseVisualStyleBackColor = True
        '
        'chb_enabled
        '
        Me.chb_enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_enabled.Checked = True
        Me.chb_enabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_enabled.Location = New System.Drawing.Point(250, 80)
        Me.chb_enabled.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_enabled.Name = "chb_enabled"
        Me.chb_enabled.Size = New System.Drawing.Size(139, 19)
        Me.chb_enabled.TabIndex = 67
        Me.chb_enabled.Text = "enabled"
        Me.chb_enabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_enabled.UseVisualStyleBackColor = True
        '
        'lbl_db_note
        '
        Me.lbl_db_note.Location = New System.Drawing.Point(9, 128)
        Me.lbl_db_note.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_db_note.Name = "lbl_db_note"
        Me.lbl_db_note.Size = New System.Drawing.Size(121, 21)
        Me.lbl_db_note.TabIndex = 64
        Me.lbl_db_note.Text = "note"
        Me.lbl_db_note.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_db_note
        '
        Me.txt_db_note.Location = New System.Drawing.Point(9, 152)
        Me.txt_db_note.Multiline = True
        Me.txt_db_note.Name = "txt_db_note"
        Me.txt_db_note.Size = New System.Drawing.Size(380, 207)
        Me.txt_db_note.TabIndex = 69
        '
        'txt_menu_position
        '
        Me.txt_menu_position.Location = New System.Drawing.Point(133, 10)
        Me.txt_menu_position.Name = "txt_menu_position"
        Me.txt_menu_position.Size = New System.Drawing.Size(100, 20)
        Me.txt_menu_position.TabIndex = 65
        '
        'lbl_menu_position
        '
        Me.lbl_menu_position.Location = New System.Drawing.Point(9, 9)
        Me.lbl_menu_position.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_menu_position.Name = "lbl_menu_position"
        Me.lbl_menu_position.Size = New System.Drawing.Size(121, 21)
        Me.lbl_menu_position.TabIndex = 63
        Me.lbl_menu_position.Text = "position"
        Me.lbl_menu_position.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_new_menu_name
        '
        Me.txt_new_menu_name.Location = New System.Drawing.Point(9, 54)
        Me.txt_new_menu_name.MaxLength = 150
        Me.txt_new_menu_name.Name = "txt_new_menu_name"
        Me.txt_new_menu_name.Size = New System.Drawing.Size(380, 20)
        Me.txt_new_menu_name.TabIndex = 66
        '
        'lbl_new_menu_name
        '
        Me.lbl_new_menu_name.Location = New System.Drawing.Point(9, 30)
        Me.lbl_new_menu_name.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_new_menu_name.Name = "lbl_new_menu_name"
        Me.lbl_new_menu_name.Size = New System.Drawing.Size(289, 21)
        Me.lbl_new_menu_name.TabIndex = 62
        Me.lbl_new_menu_name.Text = "name"
        Me.lbl_new_menu_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frm_new_favorite_item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 359)
        Me.Controls.Add(Me.chb_translate)
        Me.Controls.Add(Me.chb_released)
        Me.Controls.Add(Me.chb_enabled)
        Me.Controls.Add(Me.lbl_db_note)
        Me.Controls.Add(Me.txt_db_note)
        Me.Controls.Add(Me.txt_menu_position)
        Me.Controls.Add(Me.lbl_menu_position)
        Me.Controls.Add(Me.txt_new_menu_name)
        Me.Controls.Add(Me.lbl_new_menu_name)
        Me.Controls.Add(Me.tv_menu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_new_favorite_item"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "add_new_favorite_item"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tv_menu As System.Windows.Forms.TreeView
    Friend WithEvents chb_translate As System.Windows.Forms.CheckBox
    Friend WithEvents chb_released As System.Windows.Forms.CheckBox
    Friend WithEvents chb_enabled As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_db_note As System.Windows.Forms.Label
    Friend WithEvents txt_db_note As System.Windows.Forms.TextBox
    Friend WithEvents txt_menu_position As System.Windows.Forms.TextBox
    Friend WithEvents lbl_menu_position As System.Windows.Forms.Label
    Friend WithEvents txt_new_menu_name As System.Windows.Forms.TextBox
    Friend WithEvents lbl_new_menu_name As System.Windows.Forms.Label
End Class
