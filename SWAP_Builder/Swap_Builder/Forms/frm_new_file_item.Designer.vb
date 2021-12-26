<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_new_file_item
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_new_file_item))
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
        Me.txt_selected_filename = New System.Windows.Forms.TextBox()
        Me.btn_select_file = New System.Windows.Forms.Button()
        Me.chb_default = New System.Windows.Forms.CheckBox()
        Me.lb_datafield_list = New System.Windows.Forms.ListBox()
        Me.lb_document_key = New System.Windows.Forms.Label()
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
        Me.btn_save.Location = New System.Drawing.Point(1, 258)
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
        Me.txt_db_note.Location = New System.Drawing.Point(1, 173)
        Me.txt_db_note.Multiline = True
        Me.txt_db_note.Name = "txt_db_note"
        Me.txt_db_note.Size = New System.Drawing.Size(380, 79)
        Me.txt_db_note.TabIndex = 50
        '
        'lbl_db_note
        '
        Me.lbl_db_note.Location = New System.Drawing.Point(1, 149)
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
        Me.chb_enabled.Location = New System.Drawing.Point(242, 101)
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
        Me.chb_released.Location = New System.Drawing.Point(242, 120)
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
        Me.chb_translate.Location = New System.Drawing.Point(242, 139)
        Me.chb_translate.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_translate.Name = "chb_translate"
        Me.chb_translate.Size = New System.Drawing.Size(139, 19)
        Me.chb_translate.TabIndex = 61
        Me.chb_translate.Text = "enable_translate"
        Me.chb_translate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_translate.UseVisualStyleBackColor = True
        '
        'txt_selected_filename
        '
        Me.txt_selected_filename.Location = New System.Drawing.Point(2, 71)
        Me.txt_selected_filename.MaxLength = 150
        Me.txt_selected_filename.Name = "txt_selected_filename"
        Me.txt_selected_filename.Size = New System.Drawing.Size(254, 20)
        Me.txt_selected_filename.TabIndex = 62
        '
        'btn_select_file
        '
        Me.btn_select_file.Location = New System.Drawing.Point(262, 71)
        Me.btn_select_file.Name = "btn_select_file"
        Me.btn_select_file.Size = New System.Drawing.Size(119, 23)
        Me.btn_select_file.TabIndex = 63
        Me.btn_select_file.Text = "select_file"
        Me.btn_select_file.UseVisualStyleBackColor = True
        '
        'chb_default
        '
        Me.chb_default.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_default.Location = New System.Drawing.Point(242, 27)
        Me.chb_default.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_default.Name = "chb_default"
        Me.chb_default.Size = New System.Drawing.Size(139, 19)
        Me.chb_default.TabIndex = 64
        Me.chb_default.Text = "default_document"
        Me.chb_default.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_default.UseVisualStyleBackColor = True
        '
        'lb_datafield_list
        '
        Me.lb_datafield_list.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lb_datafield_list.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lb_datafield_list.FormattingEnabled = True
        Me.lb_datafield_list.ItemHeight = 20
        Me.lb_datafield_list.Location = New System.Drawing.Point(385, 31)
        Me.lb_datafield_list.Name = "lb_datafield_list"
        Me.lb_datafield_list.ScrollAlwaysVisible = True
        Me.lb_datafield_list.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.lb_datafield_list.Size = New System.Drawing.Size(197, 244)
        Me.lb_datafield_list.TabIndex = 65
        '
        'lb_document_key
        '
        Me.lb_document_key.Location = New System.Drawing.Point(382, 7)
        Me.lb_document_key.Margin = New System.Windows.Forms.Padding(0)
        Me.lb_document_key.Name = "lb_document_key"
        Me.lb_document_key.Size = New System.Drawing.Size(121, 21)
        Me.lb_document_key.TabIndex = 66
        Me.lb_document_key.Text = "data_binding"
        Me.lb_document_key.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frm_new_file_item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 282)
        Me.Controls.Add(Me.lb_document_key)
        Me.Controls.Add(Me.lb_datafield_list)
        Me.Controls.Add(Me.chb_default)
        Me.Controls.Add(Me.btn_select_file)
        Me.Controls.Add(Me.txt_selected_filename)
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
        Me.Name = "frm_new_file_item"
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
    Friend WithEvents txt_selected_filename As System.Windows.Forms.TextBox
    Friend WithEvents btn_select_file As System.Windows.Forms.Button
    Friend WithEvents chb_default As System.Windows.Forms.CheckBox
    Friend WithEvents lb_datafield_list As System.Windows.Forms.ListBox
    Friend WithEvents lb_document_key As System.Windows.Forms.Label
End Class
