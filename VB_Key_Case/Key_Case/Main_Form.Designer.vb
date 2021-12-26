<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Form
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Form))
        Me.ComPort = New System.IO.Ports.SerialPort(Me.components)
        Me.lbl_request_name = New System.Windows.Forms.Label()
        Me.btn_save_rec = New System.Windows.Forms.Button()
        Me.tc_control = New System.Windows.Forms.TabControl()
        Me.tp_issue_request = New System.Windows.Forms.TabPage()
        Me.btn_form_clean = New System.Windows.Forms.Button()
        Me.lbl_note = New System.Windows.Forms.Label()
        Me.txt_note = New System.Windows.Forms.TextBox()
        Me.txt_searched_pack = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tspb_status = New System.Windows.Forms.ToolStripProgressBar()
        Me.txt_selected_name = New System.Windows.Forms.Label()
        Me.rb_receipt = New System.Windows.Forms.RadioButton()
        Me.rb_issue = New System.Windows.Forms.RadioButton()
        Me.cb_key_pack = New System.Windows.Forms.ComboBox()
        Me.lbl_issued_key = New System.Windows.Forms.Label()
        Me.dt_request_datetime = New System.Windows.Forms.DateTimePicker()
        Me.lbl_date_time = New System.Windows.Forms.Label()
        Me.btn_door_open = New System.Windows.Forms.Button()
        Me.txt_request_name = New System.Windows.Forms.TextBox()
        Me.tp_issued_keys_list = New System.Windows.Forms.TabPage()
        Me.dgv_released_keys = New System.Windows.Forms.DataGridView()
        Me.tp_settings = New System.Windows.Forms.TabPage()
        Me.chb_data_cable = New System.Windows.Forms.CheckBox()
        Me.txt_com_signal_length = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_sql_login_test = New System.Windows.Forms.Button()
        Me.txt_mssql_server = New System.Windows.Forms.TextBox()
        Me.lbl_server = New System.Windows.Forms.Label()
        Me.txt_mssql_password = New System.Windows.Forms.TextBox()
        Me.lbl_mssql_password = New System.Windows.Forms.Label()
        Me.txt_mssql_name = New System.Windows.Forms.TextBox()
        Me.lbl_mssql_name = New System.Windows.Forms.Label()
        Me.btn_save_setting = New System.Windows.Forms.Button()
        Me.pb_waiting_1 = New System.Windows.Forms.PictureBox()
        Me.pb_waiting_5 = New System.Windows.Forms.PictureBox()
        Me.pb_waiting_4 = New System.Windows.Forms.PictureBox()
        Me.pb_waiting_3 = New System.Windows.Forms.PictureBox()
        Me.pb_waiting_2 = New System.Windows.Forms.PictureBox()
        Me.lb_default_wait_picture = New System.Windows.Forms.Label()
        Me.lbl_com_port = New System.Windows.Forms.Label()
        Me.cb_com_ports = New System.Windows.Forms.ComboBox()
        Me.chb_format_debbuger = New System.Windows.Forms.CheckBox()
        Me.chb_sql_debug = New System.Windows.Forms.CheckBox()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.tc_control.SuspendLayout()
        Me.tp_issue_request.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.tp_issued_keys_list.SuspendLayout()
        CType(Me.dgv_released_keys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_settings.SuspendLayout()
        CType(Me.pb_waiting_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_waiting_5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_waiting_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_waiting_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_waiting_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_request_name
        '
        Me.lbl_request_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_request_name.Location = New System.Drawing.Point(6, 63)
        Me.lbl_request_name.Name = "lbl_request_name"
        Me.lbl_request_name.Size = New System.Drawing.Size(200, 20)
        Me.lbl_request_name.TabIndex = 12
        Me.lbl_request_name.Text = "Žadatel"
        Me.lbl_request_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_save_rec
        '
        Me.btn_save_rec.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_save_rec.Enabled = False
        Me.btn_save_rec.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_save_rec.Location = New System.Drawing.Point(6, 276)
        Me.btn_save_rec.Name = "btn_save_rec"
        Me.btn_save_rec.Size = New System.Drawing.Size(99, 23)
        Me.btn_save_rec.TabIndex = 50
        Me.btn_save_rec.Text = "Uložit Výdej"
        Me.btn_save_rec.UseVisualStyleBackColor = True
        '
        'tc_control
        '
        Me.tc_control.Controls.Add(Me.tp_issue_request)
        Me.tc_control.Controls.Add(Me.tp_issued_keys_list)
        Me.tc_control.Controls.Add(Me.tp_settings)
        Me.tc_control.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tc_control.Location = New System.Drawing.Point(0, 0)
        Me.tc_control.Name = "tc_control"
        Me.tc_control.SelectedIndex = 0
        Me.tc_control.Size = New System.Drawing.Size(444, 352)
        Me.tc_control.TabIndex = 5
        '
        'tp_issue_request
        '
        Me.tp_issue_request.Controls.Add(Me.btn_form_clean)
        Me.tp_issue_request.Controls.Add(Me.lbl_note)
        Me.tp_issue_request.Controls.Add(Me.txt_note)
        Me.tp_issue_request.Controls.Add(Me.txt_searched_pack)
        Me.tp_issue_request.Controls.Add(Me.StatusStrip1)
        Me.tp_issue_request.Controls.Add(Me.txt_selected_name)
        Me.tp_issue_request.Controls.Add(Me.rb_receipt)
        Me.tp_issue_request.Controls.Add(Me.rb_issue)
        Me.tp_issue_request.Controls.Add(Me.cb_key_pack)
        Me.tp_issue_request.Controls.Add(Me.lbl_issued_key)
        Me.tp_issue_request.Controls.Add(Me.dt_request_datetime)
        Me.tp_issue_request.Controls.Add(Me.lbl_date_time)
        Me.tp_issue_request.Controls.Add(Me.btn_door_open)
        Me.tp_issue_request.Controls.Add(Me.txt_request_name)
        Me.tp_issue_request.Controls.Add(Me.lbl_request_name)
        Me.tp_issue_request.Controls.Add(Me.btn_save_rec)
        Me.tp_issue_request.Location = New System.Drawing.Point(4, 22)
        Me.tp_issue_request.Name = "tp_issue_request"
        Me.tp_issue_request.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_issue_request.Size = New System.Drawing.Size(436, 326)
        Me.tp_issue_request.TabIndex = 0
        Me.tp_issue_request.Text = "Výdej Klíče"
        Me.tp_issue_request.UseVisualStyleBackColor = True
        '
        'btn_form_clean
        '
        Me.btn_form_clean.BackgroundImage = Global.Key_Case.My.Resources.Resources.button_cancel
        Me.btn_form_clean.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btn_form_clean.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_form_clean.Enabled = False
        Me.btn_form_clean.Location = New System.Drawing.Point(9, 35)
        Me.btn_form_clean.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_form_clean.Name = "btn_form_clean"
        Me.btn_form_clean.Size = New System.Drawing.Size(31, 23)
        Me.btn_form_clean.TabIndex = 1167
        Me.btn_form_clean.UseVisualStyleBackColor = True
        '
        'lbl_note
        '
        Me.lbl_note.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_note.Location = New System.Drawing.Point(3, 193)
        Me.lbl_note.Name = "lbl_note"
        Me.lbl_note.Size = New System.Drawing.Size(200, 18)
        Me.lbl_note.TabIndex = 1166
        Me.lbl_note.Text = "Poznámka"
        Me.lbl_note.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_note
        '
        Me.txt_note.Enabled = False
        Me.txt_note.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_note.Location = New System.Drawing.Point(6, 214)
        Me.txt_note.Multiline = True
        Me.txt_note.Name = "txt_note"
        Me.txt_note.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_note.Size = New System.Drawing.Size(427, 56)
        Me.txt_note.TabIndex = 45
        '
        'txt_searched_pack
        '
        Me.txt_searched_pack.Enabled = False
        Me.txt_searched_pack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_searched_pack.Location = New System.Drawing.Point(212, 152)
        Me.txt_searched_pack.Multiline = True
        Me.txt_searched_pack.Name = "txt_searched_pack"
        Me.txt_searched_pack.ReadOnly = True
        Me.txt_searched_pack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_searched_pack.Size = New System.Drawing.Size(221, 56)
        Me.txt_searched_pack.TabIndex = 1164
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspb_status})
        Me.StatusStrip1.Location = New System.Drawing.Point(3, 301)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(430, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 1163
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tspb_status
        '
        Me.tspb_status.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tspb_status.AutoToolTip = True
        Me.tspb_status.MarqueeAnimationSpeed = 50
        Me.tspb_status.Maximum = 50
        Me.tspb_status.Name = "tspb_status"
        Me.tspb_status.RightToLeftLayout = True
        Me.tspb_status.Size = New System.Drawing.Size(100, 16)
        Me.tspb_status.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'txt_selected_name
        '
        Me.txt_selected_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_selected_name.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.txt_selected_name.Location = New System.Drawing.Point(225, 86)
        Me.txt_selected_name.Margin = New System.Windows.Forms.Padding(0)
        Me.txt_selected_name.Name = "txt_selected_name"
        Me.txt_selected_name.Size = New System.Drawing.Size(200, 14)
        Me.txt_selected_name.TabIndex = 1162
        Me.txt_selected_name.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'rb_receipt
        '
        Me.rb_receipt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rb_receipt.Enabled = False
        Me.rb_receipt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rb_receipt.Location = New System.Drawing.Point(343, 36)
        Me.rb_receipt.Name = "rb_receipt"
        Me.rb_receipt.Size = New System.Drawing.Size(85, 22)
        Me.rb_receipt.TabIndex = 1161
        Me.rb_receipt.Text = "Příjem"
        Me.rb_receipt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rb_receipt.UseVisualStyleBackColor = True
        '
        'rb_issue
        '
        Me.rb_issue.Checked = True
        Me.rb_issue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rb_issue.Enabled = False
        Me.rb_issue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rb_issue.Location = New System.Drawing.Point(228, 35)
        Me.rb_issue.Name = "rb_issue"
        Me.rb_issue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.rb_issue.Size = New System.Drawing.Size(84, 24)
        Me.rb_issue.TabIndex = 1160
        Me.rb_issue.TabStop = True
        Me.rb_issue.Text = "Výdej"
        Me.rb_issue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rb_issue.UseVisualStyleBackColor = True
        '
        'cb_key_pack
        '
        Me.cb_key_pack.Enabled = False
        Me.cb_key_pack.FormattingEnabled = True
        Me.cb_key_pack.Location = New System.Drawing.Point(6, 152)
        Me.cb_key_pack.Name = "cb_key_pack"
        Me.cb_key_pack.Size = New System.Drawing.Size(200, 21)
        Me.cb_key_pack.TabIndex = 40
        '
        'lbl_issued_key
        '
        Me.lbl_issued_key.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_issued_key.Location = New System.Drawing.Point(6, 129)
        Me.lbl_issued_key.Name = "lbl_issued_key"
        Me.lbl_issued_key.Size = New System.Drawing.Size(200, 20)
        Me.lbl_issued_key.TabIndex = 1159
        Me.lbl_issued_key.Text = "Vydaný Svazek klíčů"
        Me.lbl_issued_key.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dt_request_datetime
        '
        Me.dt_request_datetime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dt_request_datetime.CustomFormat = "dd.MM.yyyy HH:mm:ss"
        Me.dt_request_datetime.Enabled = False
        Me.dt_request_datetime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dt_request_datetime.Location = New System.Drawing.Point(228, 11)
        Me.dt_request_datetime.Name = "dt_request_datetime"
        Me.dt_request_datetime.Size = New System.Drawing.Size(200, 20)
        Me.dt_request_datetime.TabIndex = 10
        Me.dt_request_datetime.TabStop = False
        '
        'lbl_date_time
        '
        Me.lbl_date_time.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_date_time.Location = New System.Drawing.Point(6, 11)
        Me.lbl_date_time.Name = "lbl_date_time"
        Me.lbl_date_time.Size = New System.Drawing.Size(182, 20)
        Me.lbl_date_time.TabIndex = 1157
        Me.lbl_date_time.Text = "Datum Požadavku"
        Me.lbl_date_time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_door_open
        '
        Me.btn_door_open.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_door_open.Enabled = False
        Me.btn_door_open.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_door_open.Location = New System.Drawing.Point(310, 103)
        Me.btn_door_open.Name = "btn_door_open"
        Me.btn_door_open.Size = New System.Drawing.Size(118, 23)
        Me.btn_door_open.TabIndex = 30
        Me.btn_door_open.Text = "Otevřít Skříň"
        Me.btn_door_open.UseVisualStyleBackColor = True
        '
        'txt_request_name
        '
        Me.txt_request_name.AcceptsReturn = True
        Me.txt_request_name.Location = New System.Drawing.Point(228, 63)
        Me.txt_request_name.Name = "txt_request_name"
        Me.txt_request_name.Size = New System.Drawing.Size(200, 20)
        Me.txt_request_name.TabIndex = 20
        '
        'tp_issued_keys_list
        '
        Me.tp_issued_keys_list.Controls.Add(Me.dgv_released_keys)
        Me.tp_issued_keys_list.Location = New System.Drawing.Point(4, 22)
        Me.tp_issued_keys_list.Name = "tp_issued_keys_list"
        Me.tp_issued_keys_list.Size = New System.Drawing.Size(436, 326)
        Me.tp_issued_keys_list.TabIndex = 2
        Me.tp_issued_keys_list.Text = "Vydané Klíče"
        Me.tp_issued_keys_list.UseVisualStyleBackColor = True
        '
        'dgv_released_keys
        '
        Me.dgv_released_keys.AllowUserToAddRows = False
        Me.dgv_released_keys.AllowUserToDeleteRows = False
        Me.dgv_released_keys.AllowUserToOrderColumns = True
        Me.dgv_released_keys.AllowUserToResizeRows = False
        Me.dgv_released_keys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgv_released_keys.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgv_released_keys.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dgv_released_keys.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_released_keys.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgv_released_keys.Location = New System.Drawing.Point(0, 0)
        Me.dgv_released_keys.Margin = New System.Windows.Forms.Padding(0)
        Me.dgv_released_keys.MultiSelect = False
        Me.dgv_released_keys.Name = "dgv_released_keys"
        Me.dgv_released_keys.ReadOnly = True
        Me.dgv_released_keys.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.dgv_released_keys.RowHeadersVisible = False
        Me.dgv_released_keys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.dgv_released_keys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_released_keys.ShowCellErrors = False
        Me.dgv_released_keys.ShowCellToolTips = False
        Me.dgv_released_keys.ShowEditingIcon = False
        Me.dgv_released_keys.ShowRowErrors = False
        Me.dgv_released_keys.Size = New System.Drawing.Size(436, 326)
        Me.dgv_released_keys.StandardTab = True
        Me.dgv_released_keys.TabIndex = 0
        '
        'tp_settings
        '
        Me.tp_settings.Controls.Add(Me.chb_data_cable)
        Me.tp_settings.Controls.Add(Me.txt_com_signal_length)
        Me.tp_settings.Controls.Add(Me.Label1)
        Me.tp_settings.Controls.Add(Me.btn_sql_login_test)
        Me.tp_settings.Controls.Add(Me.txt_mssql_server)
        Me.tp_settings.Controls.Add(Me.lbl_server)
        Me.tp_settings.Controls.Add(Me.txt_mssql_password)
        Me.tp_settings.Controls.Add(Me.lbl_mssql_password)
        Me.tp_settings.Controls.Add(Me.txt_mssql_name)
        Me.tp_settings.Controls.Add(Me.lbl_mssql_name)
        Me.tp_settings.Controls.Add(Me.btn_save_setting)
        Me.tp_settings.Controls.Add(Me.pb_waiting_1)
        Me.tp_settings.Controls.Add(Me.pb_waiting_5)
        Me.tp_settings.Controls.Add(Me.pb_waiting_4)
        Me.tp_settings.Controls.Add(Me.pb_waiting_3)
        Me.tp_settings.Controls.Add(Me.pb_waiting_2)
        Me.tp_settings.Controls.Add(Me.lb_default_wait_picture)
        Me.tp_settings.Controls.Add(Me.lbl_com_port)
        Me.tp_settings.Controls.Add(Me.cb_com_ports)
        Me.tp_settings.Controls.Add(Me.chb_format_debbuger)
        Me.tp_settings.Controls.Add(Me.chb_sql_debug)
        Me.tp_settings.Location = New System.Drawing.Point(4, 22)
        Me.tp_settings.Name = "tp_settings"
        Me.tp_settings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_settings.Size = New System.Drawing.Size(436, 326)
        Me.tp_settings.TabIndex = 1
        Me.tp_settings.Text = "Nastavení"
        Me.tp_settings.UseVisualStyleBackColor = True
        '
        'chb_data_cable
        '
        Me.chb_data_cable.AutoSize = True
        Me.chb_data_cable.Location = New System.Drawing.Point(209, 63)
        Me.chb_data_cable.Name = "chb_data_cable"
        Me.chb_data_cable.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_data_cable.Size = New System.Drawing.Size(79, 17)
        Me.chb_data_cable.TabIndex = 1179
        Me.chb_data_cable.Text = "Data Cable"
        Me.chb_data_cable.UseVisualStyleBackColor = True
        '
        'txt_com_signal_length
        '
        Me.txt_com_signal_length.AcceptsReturn = True
        Me.txt_com_signal_length.Location = New System.Drawing.Point(173, 37)
        Me.txt_com_signal_length.Name = "txt_com_signal_length"
        Me.txt_com_signal_length.Size = New System.Drawing.Size(115, 20)
        Me.txt_com_signal_length.TabIndex = 1178
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 16)
        Me.Label1.TabIndex = 1177
        Me.Label1.Text = "COM Signal (x 100ms)"
        '
        'btn_sql_login_test
        '
        Me.btn_sql_login_test.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_sql_login_test.Location = New System.Drawing.Point(323, 213)
        Me.btn_sql_login_test.Name = "btn_sql_login_test"
        Me.btn_sql_login_test.Size = New System.Drawing.Size(103, 23)
        Me.btn_sql_login_test.TabIndex = 1176
        Me.btn_sql_login_test.Text = "Test Spojení s DB"
        Me.btn_sql_login_test.UseVisualStyleBackColor = True
        '
        'txt_mssql_server
        '
        Me.txt_mssql_server.AcceptsReturn = True
        Me.txt_mssql_server.Location = New System.Drawing.Point(249, 137)
        Me.txt_mssql_server.Name = "txt_mssql_server"
        Me.txt_mssql_server.Size = New System.Drawing.Size(177, 20)
        Me.txt_mssql_server.TabIndex = 1173
        '
        'lbl_server
        '
        Me.lbl_server.Location = New System.Drawing.Point(7, 137)
        Me.lbl_server.Name = "lbl_server"
        Me.lbl_server.Size = New System.Drawing.Size(139, 19)
        Me.lbl_server.TabIndex = 1172
        Me.lbl_server.Text = "MSSQL Server:"
        '
        'txt_mssql_password
        '
        Me.txt_mssql_password.AcceptsReturn = True
        Me.txt_mssql_password.Location = New System.Drawing.Point(249, 187)
        Me.txt_mssql_password.Name = "txt_mssql_password"
        Me.txt_mssql_password.Size = New System.Drawing.Size(177, 20)
        Me.txt_mssql_password.TabIndex = 1175
        Me.txt_mssql_password.UseSystemPasswordChar = True
        '
        'lbl_mssql_password
        '
        Me.lbl_mssql_password.Location = New System.Drawing.Point(7, 187)
        Me.lbl_mssql_password.Name = "lbl_mssql_password"
        Me.lbl_mssql_password.Size = New System.Drawing.Size(136, 16)
        Me.lbl_mssql_password.TabIndex = 1171
        Me.lbl_mssql_password.Text = "Heslo:"
        '
        'txt_mssql_name
        '
        Me.txt_mssql_name.AcceptsReturn = True
        Me.txt_mssql_name.Location = New System.Drawing.Point(249, 163)
        Me.txt_mssql_name.Name = "txt_mssql_name"
        Me.txt_mssql_name.Size = New System.Drawing.Size(177, 20)
        Me.txt_mssql_name.TabIndex = 1174
        '
        'lbl_mssql_name
        '
        Me.lbl_mssql_name.Location = New System.Drawing.Point(7, 163)
        Me.lbl_mssql_name.Name = "lbl_mssql_name"
        Me.lbl_mssql_name.Size = New System.Drawing.Size(136, 19)
        Me.lbl_mssql_name.TabIndex = 1170
        Me.lbl_mssql_name.Text = "Jméno:"
        '
        'btn_save_setting
        '
        Me.btn_save_setting.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_save_setting.Location = New System.Drawing.Point(318, 300)
        Me.btn_save_setting.Name = "btn_save_setting"
        Me.btn_save_setting.Size = New System.Drawing.Size(108, 23)
        Me.btn_save_setting.TabIndex = 1169
        Me.btn_save_setting.Text = "Uložit Nastavení"
        Me.btn_save_setting.UseVisualStyleBackColor = True
        '
        'pb_waiting_1
        '
        Me.pb_waiting_1.BackColor = System.Drawing.Color.Transparent
        Me.pb_waiting_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_waiting_1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_waiting_1.Image = Global.Key_Case.My.Resources.Resources.waiting1
        Me.pb_waiting_1.Location = New System.Drawing.Point(126, 85)
        Me.pb_waiting_1.Name = "pb_waiting_1"
        Me.pb_waiting_1.Size = New System.Drawing.Size(55, 42)
        Me.pb_waiting_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_waiting_1.TabIndex = 1168
        Me.pb_waiting_1.TabStop = False
        '
        'pb_waiting_5
        '
        Me.pb_waiting_5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_waiting_5.Image = Global.Key_Case.My.Resources.Resources.waiting5
        Me.pb_waiting_5.Location = New System.Drawing.Point(371, 85)
        Me.pb_waiting_5.Name = "pb_waiting_5"
        Me.pb_waiting_5.Size = New System.Drawing.Size(55, 42)
        Me.pb_waiting_5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_waiting_5.TabIndex = 1167
        Me.pb_waiting_5.TabStop = False
        '
        'pb_waiting_4
        '
        Me.pb_waiting_4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_waiting_4.Image = Global.Key_Case.My.Resources.Resources.waiting4
        Me.pb_waiting_4.Location = New System.Drawing.Point(309, 85)
        Me.pb_waiting_4.Name = "pb_waiting_4"
        Me.pb_waiting_4.Size = New System.Drawing.Size(55, 42)
        Me.pb_waiting_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_waiting_4.TabIndex = 1166
        Me.pb_waiting_4.TabStop = False
        '
        'pb_waiting_3
        '
        Me.pb_waiting_3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_waiting_3.Image = Global.Key_Case.My.Resources.Resources.waiting3
        Me.pb_waiting_3.Location = New System.Drawing.Point(248, 85)
        Me.pb_waiting_3.Name = "pb_waiting_3"
        Me.pb_waiting_3.Size = New System.Drawing.Size(55, 42)
        Me.pb_waiting_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_waiting_3.TabIndex = 1165
        Me.pb_waiting_3.TabStop = False
        '
        'pb_waiting_2
        '
        Me.pb_waiting_2.BackColor = System.Drawing.Color.Transparent
        Me.pb_waiting_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_waiting_2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_waiting_2.Image = Global.Key_Case.My.Resources.Resources.waiting2
        Me.pb_waiting_2.Location = New System.Drawing.Point(187, 85)
        Me.pb_waiting_2.Name = "pb_waiting_2"
        Me.pb_waiting_2.Size = New System.Drawing.Size(55, 42)
        Me.pb_waiting_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_waiting_2.TabIndex = 1164
        Me.pb_waiting_2.TabStop = False
        '
        'lb_default_wait_picture
        '
        Me.lb_default_wait_picture.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lb_default_wait_picture.Location = New System.Drawing.Point(3, 98)
        Me.lb_default_wait_picture.Name = "lb_default_wait_picture"
        Me.lb_default_wait_picture.Size = New System.Drawing.Size(117, 20)
        Me.lb_default_wait_picture.TabIndex = 1159
        Me.lb_default_wait_picture.Text = "Výchozí Čekej"
        '
        'lbl_com_port
        '
        Me.lbl_com_port.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_com_port.Location = New System.Drawing.Point(6, 14)
        Me.lbl_com_port.Name = "lbl_com_port"
        Me.lbl_com_port.Size = New System.Drawing.Size(112, 20)
        Me.lbl_com_port.TabIndex = 1158
        Me.lbl_com_port.Text = "COM port"
        Me.lbl_com_port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cb_com_ports
        '
        Me.cb_com_ports.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cb_com_ports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_com_ports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cb_com_ports.FormattingEnabled = True
        Me.cb_com_ports.Location = New System.Drawing.Point(126, 10)
        Me.cb_com_ports.Name = "cb_com_ports"
        Me.cb_com_ports.Size = New System.Drawing.Size(162, 24)
        Me.cb_com_ports.TabIndex = 1156
        '
        'chb_format_debbuger
        '
        Me.chb_format_debbuger.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chb_format_debbuger.AutoSize = True
        Me.chb_format_debbuger.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chb_format_debbuger.Location = New System.Drawing.Point(318, 14)
        Me.chb_format_debbuger.Name = "chb_format_debbuger"
        Me.chb_format_debbuger.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_format_debbuger.Size = New System.Drawing.Size(108, 17)
        Me.chb_format_debbuger.TabIndex = 1153
        Me.chb_format_debbuger.Text = "Format Debugger"
        Me.chb_format_debbuger.UseVisualStyleBackColor = True
        '
        'chb_sql_debug
        '
        Me.chb_sql_debug.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chb_sql_debug.AutoSize = True
        Me.chb_sql_debug.Cursor = System.Windows.Forms.Cursors.Hand
        Me.chb_sql_debug.Location = New System.Drawing.Point(329, 37)
        Me.chb_sql_debug.Name = "chb_sql_debug"
        Me.chb_sql_debug.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_sql_debug.Size = New System.Drawing.Size(97, 17)
        Me.chb_sql_debug.TabIndex = 1154
        Me.chb_sql_debug.Text = "SQL Debugger"
        Me.chb_sql_debug.UseVisualStyleBackColor = True
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 10
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 352)
        Me.Controls.Add(Me.tc_control)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Main_Form"
        Me.Text = "Key Case"
        Me.tc_control.ResumeLayout(False)
        Me.tp_issue_request.ResumeLayout(False)
        Me.tp_issue_request.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.tp_issued_keys_list.ResumeLayout(False)
        CType(Me.dgv_released_keys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_settings.ResumeLayout(False)
        Me.tp_settings.PerformLayout()
        CType(Me.pb_waiting_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_waiting_5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_waiting_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_waiting_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_waiting_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComPort As System.IO.Ports.SerialPort
    Friend WithEvents lbl_request_name As System.Windows.Forms.Label
    Friend WithEvents btn_save_rec As System.Windows.Forms.Button
    Friend WithEvents tc_control As System.Windows.Forms.TabControl
    Friend WithEvents tp_issue_request As System.Windows.Forms.TabPage
    Friend WithEvents tp_settings As System.Windows.Forms.TabPage
    Friend WithEvents cb_com_ports As System.Windows.Forms.ComboBox
    Friend WithEvents chb_format_debbuger As System.Windows.Forms.CheckBox
    Friend WithEvents chb_sql_debug As System.Windows.Forms.CheckBox
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents txt_request_name As System.Windows.Forms.TextBox
    Friend WithEvents dt_request_datetime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_date_time As System.Windows.Forms.Label
    Friend WithEvents btn_door_open As System.Windows.Forms.Button
    Friend WithEvents lbl_com_port As System.Windows.Forms.Label
    Friend WithEvents cb_key_pack As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_issued_key As System.Windows.Forms.Label
    Friend WithEvents btn_save_setting As System.Windows.Forms.Button
    Friend WithEvents pb_waiting_1 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_waiting_5 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_waiting_4 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_waiting_3 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_waiting_2 As System.Windows.Forms.PictureBox
    Friend WithEvents lb_default_wait_picture As System.Windows.Forms.Label
    Friend WithEvents rb_receipt As System.Windows.Forms.RadioButton
    Friend WithEvents rb_issue As System.Windows.Forms.RadioButton
    Friend WithEvents tp_issued_keys_list As System.Windows.Forms.TabPage
    Friend WithEvents dgv_released_keys As System.Windows.Forms.DataGridView
    Friend WithEvents btn_sql_login_test As System.Windows.Forms.Button
    Friend WithEvents txt_mssql_server As System.Windows.Forms.TextBox
    Friend WithEvents lbl_server As System.Windows.Forms.Label
    Friend WithEvents txt_mssql_password As System.Windows.Forms.TextBox
    Friend WithEvents lbl_mssql_password As System.Windows.Forms.Label
    Friend WithEvents txt_mssql_name As System.Windows.Forms.TextBox
    Friend WithEvents lbl_mssql_name As System.Windows.Forms.Label
    Friend WithEvents txt_selected_name As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tspb_status As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents txt_com_signal_length As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_searched_pack As System.Windows.Forms.TextBox
    Friend WithEvents lbl_note As System.Windows.Forms.Label
    Friend WithEvents txt_note As System.Windows.Forms.TextBox
    Friend WithEvents btn_form_clean As System.Windows.Forms.Button
    Friend WithEvents chb_data_cable As System.Windows.Forms.CheckBox

End Class
