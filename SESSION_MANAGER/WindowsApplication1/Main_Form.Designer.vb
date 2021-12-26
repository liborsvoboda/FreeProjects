<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Form
    Inherits System.Windows.Forms.Form

    'Formulář přepisuje metodu Dispose, aby vyčistil seznam součástí.
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

    'Vyžadováno Návrhářem Windows Form
    Private components As System.ComponentModel.IContainer

    'POZNÁMKA: Následující procedura je vyžadována Návrhářem Windows Form
    'Může být upraveno pomocí Návrháře Windows Form.  
    'Neupravovat pomocí editoru kódu.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Form))
        Me.dgv_connected_users = New System.Windows.Forms.DataGridView()
        Me.user = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.session_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.release = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl = New System.Windows.Forms.TabControl()
        Me.tb_wtsm = New System.Windows.Forms.TabPage()
        Me.btn_refresh = New System.Windows.Forms.Button()
        Me.tb_setting = New System.Windows.Forms.TabPage()
        Me.btn_add_new_user = New System.Windows.Forms.Button()
        Me.txt_new_user = New System.Windows.Forms.TextBox()
        Me.lbl_new_user = New System.Windows.Forms.Label()
        Me.btn_save_setting = New System.Windows.Forms.Button()
        Me.lb_user_list = New System.Windows.Forms.ListBox()
        Me.lbl_user_list = New System.Windows.Forms.Label()
        Me.txt_windows_terminal_server = New System.Windows.Forms.TextBox()
        Me.lbl_terminal_server = New System.Windows.Forms.Label()
        CType(Me.dgv_connected_users, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl.SuspendLayout()
        Me.tb_wtsm.SuspendLayout()
        Me.tb_setting.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgv_connected_users
        '
        Me.dgv_connected_users.AllowUserToAddRows = False
        Me.dgv_connected_users.AllowUserToDeleteRows = False
        Me.dgv_connected_users.AllowUserToResizeColumns = False
        Me.dgv_connected_users.AllowUserToResizeRows = False
        Me.dgv_connected_users.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgv_connected_users.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv_connected_users.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgv_connected_users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgv_connected_users.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.user, Me.session_id, Me.status, Me.release})
        Me.dgv_connected_users.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_connected_users.EnableHeadersVisualStyles = False
        Me.dgv_connected_users.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.dgv_connected_users.Location = New System.Drawing.Point(0, 26)
        Me.dgv_connected_users.MultiSelect = False
        Me.dgv_connected_users.Name = "dgv_connected_users"
        Me.dgv_connected_users.ReadOnly = True
        Me.dgv_connected_users.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgv_connected_users.RowHeadersVisible = False
        Me.dgv_connected_users.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgv_connected_users.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgv_connected_users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_connected_users.ShowCellErrors = False
        Me.dgv_connected_users.ShowCellToolTips = False
        Me.dgv_connected_users.ShowEditingIcon = False
        Me.dgv_connected_users.ShowRowErrors = False
        Me.dgv_connected_users.Size = New System.Drawing.Size(359, 251)
        Me.dgv_connected_users.TabIndex = 0
        Me.dgv_connected_users.TabStop = False
        '
        'user
        '
        Me.user.HeaderText = "Uživatel"
        Me.user.Name = "user"
        Me.user.ReadOnly = True
        Me.user.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.user.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'session_id
        '
        Me.session_id.HeaderText = "ID Připojení"
        Me.session_id.Name = "session_id"
        Me.session_id.ReadOnly = True
        Me.session_id.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.session_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.session_id.Width = 80
        '
        'status
        '
        Me.status.HeaderText = "Stav"
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.status.Width = 80
        '
        'release
        '
        Me.release.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.release.HeaderText = "Uvolnit"
        Me.release.Name = "release"
        Me.release.ReadOnly = True
        Me.release.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(351, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Připojení Uživatelé"
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.tb_wtsm)
        Me.TabControl.Controls.Add(Me.tb_setting)
        Me.TabControl.Location = New System.Drawing.Point(0, 1)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(370, 337)
        Me.TabControl.TabIndex = 0
        '
        'tb_wtsm
        '
        Me.tb_wtsm.AccessibleDescription = ""
        Me.tb_wtsm.Controls.Add(Me.btn_refresh)
        Me.tb_wtsm.Controls.Add(Me.Label1)
        Me.tb_wtsm.Controls.Add(Me.dgv_connected_users)
        Me.tb_wtsm.Location = New System.Drawing.Point(4, 22)
        Me.tb_wtsm.Name = "tb_wtsm"
        Me.tb_wtsm.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_wtsm.Size = New System.Drawing.Size(362, 311)
        Me.tb_wtsm.TabIndex = 0
        Me.tb_wtsm.Text = "WTSM"
        Me.tb_wtsm.ToolTipText = "Windows Terminal Session Manager"
        Me.tb_wtsm.UseVisualStyleBackColor = True
        '
        'btn_refresh
        '
        Me.btn_refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_refresh.Location = New System.Drawing.Point(266, 283)
        Me.btn_refresh.Name = "btn_refresh"
        Me.btn_refresh.Size = New System.Drawing.Size(96, 28)
        Me.btn_refresh.TabIndex = 3
        Me.btn_refresh.Text = "OBNOVIT"
        Me.btn_refresh.UseVisualStyleBackColor = True
        '
        'tb_setting
        '
        Me.tb_setting.Controls.Add(Me.btn_add_new_user)
        Me.tb_setting.Controls.Add(Me.txt_new_user)
        Me.tb_setting.Controls.Add(Me.lbl_new_user)
        Me.tb_setting.Controls.Add(Me.btn_save_setting)
        Me.tb_setting.Controls.Add(Me.lb_user_list)
        Me.tb_setting.Controls.Add(Me.lbl_user_list)
        Me.tb_setting.Controls.Add(Me.txt_windows_terminal_server)
        Me.tb_setting.Controls.Add(Me.lbl_terminal_server)
        Me.tb_setting.Location = New System.Drawing.Point(4, 22)
        Me.tb_setting.Name = "tb_setting"
        Me.tb_setting.Padding = New System.Windows.Forms.Padding(3)
        Me.tb_setting.Size = New System.Drawing.Size(362, 311)
        Me.tb_setting.TabIndex = 1
        Me.tb_setting.Text = "NASTAVENÍ"
        Me.tb_setting.UseVisualStyleBackColor = True
        '
        'btn_add_new_user
        '
        Me.btn_add_new_user.Location = New System.Drawing.Point(216, 177)
        Me.btn_add_new_user.Name = "btn_add_new_user"
        Me.btn_add_new_user.Size = New System.Drawing.Size(75, 23)
        Me.btn_add_new_user.TabIndex = 7
        Me.btn_add_new_user.Text = "Přidat"
        Me.btn_add_new_user.UseVisualStyleBackColor = True
        '
        'txt_new_user
        '
        Me.txt_new_user.Location = New System.Drawing.Point(215, 150)
        Me.txt_new_user.Name = "txt_new_user"
        Me.txt_new_user.Size = New System.Drawing.Size(134, 20)
        Me.txt_new_user.TabIndex = 6
        '
        'lbl_new_user
        '
        Me.lbl_new_user.AutoSize = True
        Me.lbl_new_user.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_new_user.Location = New System.Drawing.Point(212, 134)
        Me.lbl_new_user.Name = "lbl_new_user"
        Me.lbl_new_user.Size = New System.Drawing.Size(104, 16)
        Me.lbl_new_user.TabIndex = 0
        Me.lbl_new_user.Text = "Nový Uživatel"
        '
        'btn_save_setting
        '
        Me.btn_save_setting.Location = New System.Drawing.Point(9, 279)
        Me.btn_save_setting.Name = "btn_save_setting"
        Me.btn_save_setting.Size = New System.Drawing.Size(114, 23)
        Me.btn_save_setting.TabIndex = 4
        Me.btn_save_setting.Text = "Uložit Nastavení"
        Me.btn_save_setting.UseVisualStyleBackColor = True
        '
        'lb_user_list
        '
        Me.lb_user_list.FormattingEnabled = True
        Me.lb_user_list.Location = New System.Drawing.Point(11, 86)
        Me.lb_user_list.Name = "lb_user_list"
        Me.lb_user_list.Size = New System.Drawing.Size(198, 186)
        Me.lb_user_list.TabIndex = 3
        '
        'lbl_user_list
        '
        Me.lbl_user_list.AutoSize = True
        Me.lbl_user_list.Location = New System.Drawing.Point(11, 69)
        Me.lbl_user_list.Name = "lbl_user_list"
        Me.lbl_user_list.Size = New System.Drawing.Size(198, 13)
        Me.lbl_user_list.TabIndex = 2
        Me.lbl_user_list.Text = "SEZNAM SLEDOVANÝCH UŽIVATELŮ"
        '
        'txt_windows_terminal_server
        '
        Me.txt_windows_terminal_server.Location = New System.Drawing.Point(11, 31)
        Me.txt_windows_terminal_server.Name = "txt_windows_terminal_server"
        Me.txt_windows_terminal_server.Size = New System.Drawing.Size(342, 20)
        Me.txt_windows_terminal_server.TabIndex = 5
        '
        'lbl_terminal_server
        '
        Me.lbl_terminal_server.AutoSize = True
        Me.lbl_terminal_server.Location = New System.Drawing.Point(8, 14)
        Me.lbl_terminal_server.Name = "lbl_terminal_server"
        Me.lbl_terminal_server.Size = New System.Drawing.Size(168, 13)
        Me.lbl_terminal_server.TabIndex = 0
        Me.lbl_terminal_server.Text = "WINDOWS TERMINAL SERVER"
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(369, 338)
        Me.Controls.Add(Me.TabControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Main_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TERMINAL SESSION MANAGER"
        CType(Me.dgv_connected_users, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl.ResumeLayout(False)
        Me.tb_wtsm.ResumeLayout(False)
        Me.tb_setting.ResumeLayout(False)
        Me.tb_setting.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents tb_wtsm As System.Windows.Forms.TabPage
    Friend WithEvents tb_setting As System.Windows.Forms.TabPage
    Friend WithEvents dgv_connected_users As System.Windows.Forms.DataGridView
    Friend WithEvents btn_save_setting As System.Windows.Forms.Button
    Friend WithEvents lb_user_list As System.Windows.Forms.ListBox
    Friend WithEvents lbl_user_list As System.Windows.Forms.Label
    Friend WithEvents txt_windows_terminal_server As System.Windows.Forms.TextBox
    Friend WithEvents lbl_terminal_server As System.Windows.Forms.Label
    Friend WithEvents btn_add_new_user As System.Windows.Forms.Button
    Friend WithEvents txt_new_user As System.Windows.Forms.TextBox
    Friend WithEvents lbl_new_user As System.Windows.Forms.Label
    Friend WithEvents btn_refresh As System.Windows.Forms.Button
    Friend WithEvents user As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents session_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents release As System.Windows.Forms.DataGridViewButtonColumn

End Class
