<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_new_menu_item
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_new_menu_item))
        Me.lbl_new_menu_name = New System.Windows.Forms.Label()
        Me.txt_new_menu_name = New System.Windows.Forms.TextBox()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.lbl_menu_position = New System.Windows.Forms.Label()
        Me.txt_menu_position = New System.Windows.Forms.TextBox()
        Me.txt_db_note = New System.Windows.Forms.TextBox()
        Me.lbl_db_note = New System.Windows.Forms.Label()
        Me.chb_enabled = New System.Windows.Forms.CheckBox()
        Me.chb_released = New System.Windows.Forms.CheckBox()
        Me.chb_translate = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'lbl_new_menu_name
        '
        Me.lbl_new_menu_name.Location = New System.Drawing.Point(1, 25)
        Me.lbl_new_menu_name.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_new_menu_name.Name = "lbl_new_menu_name"
        Me.lbl_new_menu_name.Size = New System.Drawing.Size(289, 21)
        Me.lbl_new_menu_name.TabIndex = 0
        Me.lbl_new_menu_name.Text = "name"
        Me.lbl_new_menu_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_new_menu_name
        '
        Me.txt_new_menu_name.Location = New System.Drawing.Point(1, 49)
        Me.txt_new_menu_name.MaxLength = 150
        Me.txt_new_menu_name.Name = "txt_new_menu_name"
        Me.txt_new_menu_name.Size = New System.Drawing.Size(380, 20)
        Me.txt_new_menu_name.TabIndex = 20
        '
        'btn_save
        '
        Me.btn_save.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_save.Enabled = False
        Me.btn_save.Location = New System.Drawing.Point(1, 232)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(75, 23)
        Me.btn_save.TabIndex = 60
        Me.btn_save.Text = "save"
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'lbl_menu_position
        '
        Me.lbl_menu_position.Location = New System.Drawing.Point(1, 4)
        Me.lbl_menu_position.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_menu_position.Name = "lbl_menu_position"
        Me.lbl_menu_position.Size = New System.Drawing.Size(121, 21)
        Me.lbl_menu_position.TabIndex = 3
        Me.lbl_menu_position.Text = "position"
        Me.lbl_menu_position.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_menu_position
        '
        Me.txt_menu_position.Location = New System.Drawing.Point(125, 5)
        Me.txt_menu_position.Name = "txt_menu_position"
        Me.txt_menu_position.Size = New System.Drawing.Size(100, 20)
        Me.txt_menu_position.TabIndex = 10
        '
        'txt_db_note
        '
        Me.txt_db_note.Location = New System.Drawing.Point(1, 147)
        Me.txt_db_note.Multiline = True
        Me.txt_db_note.Name = "txt_db_note"
        Me.txt_db_note.Size = New System.Drawing.Size(380, 79)
        Me.txt_db_note.TabIndex = 50
        '
        'lbl_db_note
        '
        Me.lbl_db_note.Location = New System.Drawing.Point(1, 123)
        Me.lbl_db_note.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_db_note.Name = "lbl_db_note"
        Me.lbl_db_note.Size = New System.Drawing.Size(121, 21)
        Me.lbl_db_note.TabIndex = 6
        Me.lbl_db_note.Text = "note"
        Me.lbl_db_note.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chb_enabled
        '
        Me.chb_enabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_enabled.Checked = True
        Me.chb_enabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_enabled.Location = New System.Drawing.Point(242, 75)
        Me.chb_enabled.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_enabled.Name = "chb_enabled"
        Me.chb_enabled.Size = New System.Drawing.Size(139, 19)
        Me.chb_enabled.TabIndex = 30
        Me.chb_enabled.Text = "enabled"
        Me.chb_enabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_enabled.UseVisualStyleBackColor = True
        '
        'chb_released
        '
        Me.chb_released.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_released.Checked = True
        Me.chb_released.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_released.Location = New System.Drawing.Point(242, 94)
        Me.chb_released.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_released.Name = "chb_released"
        Me.chb_released.Size = New System.Drawing.Size(139, 19)
        Me.chb_released.TabIndex = 40
        Me.chb_released.Text = "release"
        Me.chb_released.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_released.UseVisualStyleBackColor = True
        '
        'chb_translate
        '
        Me.chb_translate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_translate.Checked = True
        Me.chb_translate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_translate.Location = New System.Drawing.Point(242, 113)
        Me.chb_translate.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_translate.Name = "chb_translate"
        Me.chb_translate.Size = New System.Drawing.Size(139, 19)
        Me.chb_translate.TabIndex = 61
        Me.chb_translate.Text = "enable_translate"
        Me.chb_translate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_translate.UseVisualStyleBackColor = True
        '
        'frm_new_menu_item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 260)
        Me.Controls.Add(Me.chb_translate)
        Me.Controls.Add(Me.chb_released)
        Me.Controls.Add(Me.chb_enabled)
        Me.Controls.Add(Me.lbl_db_note)
        Me.Controls.Add(Me.txt_db_note)
        Me.Controls.Add(Me.txt_menu_position)
        Me.Controls.Add(Me.lbl_menu_position)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.txt_new_menu_name)
        Me.Controls.Add(Me.lbl_new_menu_name)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_new_menu_item"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "add_new_menu_item"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbl_new_menu_name As System.Windows.Forms.Label
    Friend WithEvents txt_new_menu_name As System.Windows.Forms.TextBox
    Friend WithEvents btn_save As System.Windows.Forms.Button
    Friend WithEvents lbl_menu_position As System.Windows.Forms.Label
    Friend WithEvents txt_menu_position As System.Windows.Forms.TextBox
    Friend WithEvents txt_db_note As System.Windows.Forms.TextBox
    Friend WithEvents lbl_db_note As System.Windows.Forms.Label
    Friend WithEvents chb_enabled As System.Windows.Forms.CheckBox
    Friend WithEvents chb_released As System.Windows.Forms.CheckBox
    Friend WithEvents chb_translate As System.Windows.Forms.CheckBox
End Class
