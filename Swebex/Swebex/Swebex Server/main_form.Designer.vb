<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class main_form
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
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Connected Clients", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Disconnected Clients", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(main_form))
        Me.t_system_timer = New System.Windows.Forms.Timer(Me.components)
        Me.lb_modules = New System.Windows.Forms.ListBox()
        Me.tb_datalist = New System.Windows.Forms.TabControl()
        Me.tp_chat_application = New System.Windows.Forms.TabPage()
        Me.rtb_chat_history = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_chat_disconnect_all = New System.Windows.Forms.Button()
        Me.btn_chat_clear_history = New System.Windows.Forms.Button()
        Me.lv_chat_Client_list = New System.Windows.Forms.ListView()
        Me.lv_chat_Client_list_Status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lv_chat_Client_list_sessionid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lv_chat_client_list_machineid = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cms_chat_RightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cms_send_text = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_send_file = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestLargeArrayTransferHelperToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_disconnect_client = New System.Windows.Forms.ToolStripMenuItem()
        Me.il_chat_Images = New System.Windows.Forms.ImageList(Me.components)
        Me.lbl_chat_connections_count = New System.Windows.Forms.Label()
        Me.btn_chat_send_to_All = New System.Windows.Forms.Button()
        Me.txt_chat_message = New System.Windows.Forms.TextBox()
        Me.logTextBox = New System.Windows.Forms.TextBox()
        Me.tp_appsetting = New System.Windows.Forms.TabPage()
        Me.btn_app_settings_save = New System.Windows.Forms.Button()
        Me.lb_default_language = New System.Windows.Forms.ListBox()
        Me.lbl_default_lang = New System.Windows.Forms.Label()
        Me.tp_dictionary = New System.Windows.Forms.TabPage()
        Me.cb_word = New System.Windows.Forms.ComboBox()
        Me.cb_languages = New System.Windows.Forms.ComboBox()
        Me.btn_clear_form = New System.Windows.Forms.Button()
        Me.btn_dictionary_save = New System.Windows.Forms.Button()
        Me.txt_translate = New System.Windows.Forms.TextBox()
        Me.lbl_translate = New System.Windows.Forms.Label()
        Me.lbl_word = New System.Windows.Forms.Label()
        Me.lbl_language = New System.Windows.Forms.Label()
        Me.tp_chat_settings = New System.Windows.Forms.TabPage()
        Me.btn_show_files = New System.Windows.Forms.Button()
        Me.btn_open_log_file = New System.Windows.Forms.Button()
        Me.txt_chat_log_line_limit = New System.Windows.Forms.TextBox()
        Me.lbl_lbl_log_line_limit = New System.Windows.Forms.Label()
        Me.chb_logging_chat_messages = New System.Windows.Forms.CheckBox()
        Me.btn_chat_save = New System.Windows.Forms.Button()
        Me.txt_chat_port = New System.Windows.Forms.TextBox()
        Me.lbl_chat_port = New System.Windows.Forms.Label()
        Me.txt_pc_name = New System.Windows.Forms.TextBox()
        Me.lbl_pc_name = New System.Windows.Forms.Label()
        Me.txt_ipaddresses = New System.Windows.Forms.TextBox()
        Me.lbl_ip_address = New System.Windows.Forms.Label()
        Me.txt_server_user = New System.Windows.Forms.TextBox()
        Me.lbl_server_user = New System.Windows.Forms.Label()
        Me.txt_domain = New System.Windows.Forms.TextBox()
        Me.lbl_domain = New System.Windows.Forms.Label()
        Me.st_status_app_bar = New System.Windows.Forms.StatusStrip()
        Me.tss_chat_server_status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tss_search_clients = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lbl_modules = New System.Windows.Forms.Label()
        Me.chat_ofdSendFileToClient = New System.Windows.Forms.OpenFileDialog()
        Me.ni_tray_icon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cms_tray_menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ts_show_hidden = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_about_app = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_exit_app = New System.Windows.Forms.ToolStripMenuItem()
        Me.txt_delay_notification = New System.Windows.Forms.TextBox()
        Me.lbl_delay_notification = New System.Windows.Forms.Label()
        Me.tb_datalist.SuspendLayout()
        Me.tp_chat_application.SuspendLayout()
        Me.cms_chat_RightClickMenu.SuspendLayout()
        Me.tp_appsetting.SuspendLayout()
        Me.tp_dictionary.SuspendLayout()
        Me.tp_chat_settings.SuspendLayout()
        Me.st_status_app_bar.SuspendLayout()
        Me.cms_tray_menu.SuspendLayout()
        Me.SuspendLayout()
        '
        't_system_timer
        '
        Me.t_system_timer.Interval = 1000
        '
        'lb_modules
        '
        Me.lb_modules.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lb_modules.FormattingEnabled = True
        Me.lb_modules.Location = New System.Drawing.Point(2, 12)
        Me.lb_modules.Name = "lb_modules"
        Me.lb_modules.Size = New System.Drawing.Size(133, 433)
        Me.lb_modules.TabIndex = 0
        '
        'tb_datalist
        '
        Me.tb_datalist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tb_datalist.Controls.Add(Me.tp_chat_application)
        Me.tb_datalist.Controls.Add(Me.tp_appsetting)
        Me.tb_datalist.Controls.Add(Me.tp_dictionary)
        Me.tb_datalist.Controls.Add(Me.tp_chat_settings)
        Me.tb_datalist.Location = New System.Drawing.Point(141, 13)
        Me.tb_datalist.Name = "tb_datalist"
        Me.tb_datalist.SelectedIndex = 0
        Me.tb_datalist.Size = New System.Drawing.Size(356, 432)
        Me.tb_datalist.TabIndex = 1
        '
        'tp_chat_application
        '
        Me.tp_chat_application.Controls.Add(Me.rtb_chat_history)
        Me.tp_chat_application.Controls.Add(Me.Label1)
        Me.tp_chat_application.Controls.Add(Me.btn_chat_disconnect_all)
        Me.tp_chat_application.Controls.Add(Me.btn_chat_clear_history)
        Me.tp_chat_application.Controls.Add(Me.lv_chat_Client_list)
        Me.tp_chat_application.Controls.Add(Me.lbl_chat_connections_count)
        Me.tp_chat_application.Controls.Add(Me.btn_chat_send_to_All)
        Me.tp_chat_application.Controls.Add(Me.txt_chat_message)
        Me.tp_chat_application.Controls.Add(Me.logTextBox)
        Me.tp_chat_application.Location = New System.Drawing.Point(4, 22)
        Me.tp_chat_application.Name = "tp_chat_application"
        Me.tp_chat_application.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_chat_application.Size = New System.Drawing.Size(348, 406)
        Me.tp_chat_application.TabIndex = 1
        Me.tp_chat_application.Text = "chat_application"
        Me.tp_chat_application.UseVisualStyleBackColor = True
        '
        'rtb_chat_history
        '
        Me.rtb_chat_history.AcceptsTab = True
        Me.rtb_chat_history.AutoWordSelection = True
        Me.rtb_chat_history.CausesValidation = False
        Me.rtb_chat_history.Cursor = System.Windows.Forms.Cursors.Hand
        Me.rtb_chat_history.Location = New System.Drawing.Point(3, 179)
        Me.rtb_chat_history.Name = "rtb_chat_history"
        Me.rtb_chat_history.ReadOnly = True
        Me.rtb_chat_history.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtb_chat_history.Size = New System.Drawing.Size(342, 169)
        Me.rtb_chat_history.TabIndex = 20
        Me.rtb_chat_history.Tag = ""
        Me.rtb_chat_history.Text = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 272)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 19
        '
        'btn_chat_disconnect_all
        '
        Me.btn_chat_disconnect_all.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_chat_disconnect_all.Enabled = False
        Me.btn_chat_disconnect_all.Location = New System.Drawing.Point(115, 383)
        Me.btn_chat_disconnect_all.Name = "btn_chat_disconnect_all"
        Me.btn_chat_disconnect_all.Size = New System.Drawing.Size(110, 23)
        Me.btn_chat_disconnect_all.TabIndex = 18
        Me.btn_chat_disconnect_all.Text = "Disconnect_All"
        Me.btn_chat_disconnect_all.UseVisualStyleBackColor = True
        '
        'btn_chat_clear_history
        '
        Me.btn_chat_clear_history.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_chat_clear_history.Location = New System.Drawing.Point(3, 383)
        Me.btn_chat_clear_history.Name = "btn_chat_clear_history"
        Me.btn_chat_clear_history.Size = New System.Drawing.Size(102, 23)
        Me.btn_chat_clear_history.TabIndex = 17
        Me.btn_chat_clear_history.Text = "Clear History"
        Me.btn_chat_clear_history.UseVisualStyleBackColor = True
        '
        'lv_chat_Client_list
        '
        Me.lv_chat_Client_list.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lv_chat_Client_list.Alignment = System.Windows.Forms.ListViewAlignment.[Default]
        Me.lv_chat_Client_list.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lv_chat_Client_list_Status, Me.lv_chat_Client_list_sessionid, Me.lv_chat_client_list_machineid})
        Me.lv_chat_Client_list.ContextMenuStrip = Me.cms_chat_RightClickMenu
        Me.lv_chat_Client_list.Dock = System.Windows.Forms.DockStyle.Top
        Me.lv_chat_Client_list.FullRowSelect = True
        ListViewGroup3.Header = "Connected Clients"
        ListViewGroup3.Name = "ConnectedClients"
        ListViewGroup4.Header = "Disconnected Clients"
        ListViewGroup4.Name = "DisconnectedClients"
        Me.lv_chat_Client_list.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lv_chat_Client_list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_chat_Client_list.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lv_chat_Client_list.Location = New System.Drawing.Point(3, 3)
        Me.lv_chat_Client_list.Name = "lv_chat_Client_list"
        Me.lv_chat_Client_list.Size = New System.Drawing.Size(342, 180)
        Me.lv_chat_Client_list.SmallImageList = Me.il_chat_Images
        Me.lv_chat_Client_list.TabIndex = 16
        Me.lv_chat_Client_list.UseCompatibleStateImageBehavior = False
        Me.lv_chat_Client_list.View = System.Windows.Forms.View.Details
        '
        'lv_chat_Client_list_Status
        '
        Me.lv_chat_Client_list_Status.Text = "Status"
        Me.lv_chat_Client_list_Status.Width = 89
        '
        'lv_chat_Client_list_sessionid
        '
        Me.lv_chat_Client_list_sessionid.Text = "SessionId"
        Me.lv_chat_Client_list_sessionid.Width = 69
        '
        'lv_chat_client_list_machineid
        '
        Me.lv_chat_client_list_machineid.Text = "MachineId"
        Me.lv_chat_client_list_machineid.Width = 164
        '
        'cms_chat_RightClickMenu
        '
        Me.cms_chat_RightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cms_send_text, Me.cms_send_file, Me.TestLargeArrayTransferHelperToolStripMenuItem, Me.cms_disconnect_client})
        Me.cms_chat_RightClickMenu.Name = "sessionRightClickMenu"
        Me.cms_chat_RightClickMenu.Size = New System.Drawing.Size(168, 92)
        '
        'cms_send_text
        '
        Me.cms_send_text.Name = "cms_send_text"
        Me.cms_send_text.Size = New System.Drawing.Size(167, 22)
        Me.cms_send_text.Text = "Send Message"
        '
        'cms_send_file
        '
        Me.cms_send_file.Name = "cms_send_file"
        Me.cms_send_file.Size = New System.Drawing.Size(167, 22)
        Me.cms_send_file.Text = "Send A file"
        '
        'TestLargeArrayTransferHelperToolStripMenuItem
        '
        Me.TestLargeArrayTransferHelperToolStripMenuItem.Name = "TestLargeArrayTransferHelperToolStripMenuItem"
        Me.TestLargeArrayTransferHelperToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.TestLargeArrayTransferHelperToolStripMenuItem.Text = "Send a large array"
        '
        'cms_disconnect_client
        '
        Me.cms_disconnect_client.Name = "cms_disconnect_client"
        Me.cms_disconnect_client.Size = New System.Drawing.Size(167, 22)
        Me.cms_disconnect_client.Text = "Disconnect Client"
        '
        'il_chat_Images
        '
        Me.il_chat_Images.ImageStream = CType(resources.GetObject("il_chat_Images.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.il_chat_Images.TransparentColor = System.Drawing.Color.Transparent
        Me.il_chat_Images.Images.SetKeyName(0, "user-available.ico")
        Me.il_chat_Images.Images.SetKeyName(1, "user-invisible.ico")
        '
        'lbl_chat_connections_count
        '
        Me.lbl_chat_connections_count.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_chat_connections_count.Location = New System.Drawing.Point(6, 8)
        Me.lbl_chat_connections_count.Name = "lbl_chat_connections_count"
        Me.lbl_chat_connections_count.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_chat_connections_count.Size = New System.Drawing.Size(36, 14)
        Me.lbl_chat_connections_count.TabIndex = 12
        '
        'btn_chat_send_to_All
        '
        Me.btn_chat_send_to_All.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_chat_send_to_All.Enabled = False
        Me.btn_chat_send_to_All.Location = New System.Drawing.Point(235, 383)
        Me.btn_chat_send_to_All.Name = "btn_chat_send_to_All"
        Me.btn_chat_send_to_All.Size = New System.Drawing.Size(110, 23)
        Me.btn_chat_send_to_All.TabIndex = 8
        Me.btn_chat_send_to_All.Text = "Send to All"
        Me.btn_chat_send_to_All.UseVisualStyleBackColor = True
        '
        'txt_chat_message
        '
        Me.txt_chat_message.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_chat_message.Location = New System.Drawing.Point(6, 354)
        Me.txt_chat_message.Name = "txt_chat_message"
        Me.txt_chat_message.Size = New System.Drawing.Size(339, 20)
        Me.txt_chat_message.TabIndex = 5
        '
        'logTextBox
        '
        Me.logTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.logTextBox.Location = New System.Drawing.Point(3, 0)
        Me.logTextBox.Multiline = True
        Me.logTextBox.Name = "logTextBox"
        Me.logTextBox.ReadOnly = True
        Me.logTextBox.Size = New System.Drawing.Size(345, 348)
        Me.logTextBox.TabIndex = 4
        Me.logTextBox.TabStop = False
        '
        'tp_appsetting
        '
        Me.tp_appsetting.Controls.Add(Me.btn_app_settings_save)
        Me.tp_appsetting.Controls.Add(Me.lb_default_language)
        Me.tp_appsetting.Controls.Add(Me.lbl_default_lang)
        Me.tp_appsetting.Location = New System.Drawing.Point(4, 22)
        Me.tp_appsetting.Name = "tp_appsetting"
        Me.tp_appsetting.Size = New System.Drawing.Size(348, 406)
        Me.tp_appsetting.TabIndex = 2
        Me.tp_appsetting.Text = "AppSetting"
        Me.tp_appsetting.UseVisualStyleBackColor = True
        '
        'btn_app_settings_save
        '
        Me.btn_app_settings_save.Location = New System.Drawing.Point(270, 383)
        Me.btn_app_settings_save.Name = "btn_app_settings_save"
        Me.btn_app_settings_save.Size = New System.Drawing.Size(75, 23)
        Me.btn_app_settings_save.TabIndex = 47
        Me.btn_app_settings_save.Text = "Save"
        Me.btn_app_settings_save.UseVisualStyleBackColor = True
        '
        'lb_default_language
        '
        Me.lb_default_language.FormattingEnabled = True
        Me.lb_default_language.Location = New System.Drawing.Point(150, 3)
        Me.lb_default_language.Name = "lb_default_language"
        Me.lb_default_language.Size = New System.Drawing.Size(195, 17)
        Me.lb_default_language.TabIndex = 1
        '
        'lbl_default_lang
        '
        Me.lbl_default_lang.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_default_lang.Location = New System.Drawing.Point(3, 3)
        Me.lbl_default_lang.Name = "lbl_default_lang"
        Me.lbl_default_lang.Size = New System.Drawing.Size(141, 17)
        Me.lbl_default_lang.TabIndex = 0
        Me.lbl_default_lang.Text = "default_language"
        '
        'tp_dictionary
        '
        Me.tp_dictionary.Controls.Add(Me.cb_word)
        Me.tp_dictionary.Controls.Add(Me.cb_languages)
        Me.tp_dictionary.Controls.Add(Me.btn_clear_form)
        Me.tp_dictionary.Controls.Add(Me.btn_dictionary_save)
        Me.tp_dictionary.Controls.Add(Me.txt_translate)
        Me.tp_dictionary.Controls.Add(Me.lbl_translate)
        Me.tp_dictionary.Controls.Add(Me.lbl_word)
        Me.tp_dictionary.Controls.Add(Me.lbl_language)
        Me.tp_dictionary.Location = New System.Drawing.Point(4, 22)
        Me.tp_dictionary.Name = "tp_dictionary"
        Me.tp_dictionary.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_dictionary.Size = New System.Drawing.Size(348, 406)
        Me.tp_dictionary.TabIndex = 0
        Me.tp_dictionary.Text = "dictionary"
        Me.tp_dictionary.UseVisualStyleBackColor = True
        '
        'cb_word
        '
        Me.cb_word.BackColor = System.Drawing.Color.Moccasin
        Me.cb_word.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cb_word.FormattingEnabled = True
        Me.cb_word.Location = New System.Drawing.Point(163, 6)
        Me.cb_word.Name = "cb_word"
        Me.cb_word.Size = New System.Drawing.Size(182, 98)
        Me.cb_word.TabIndex = 9
        '
        'cb_languages
        '
        Me.cb_languages.BackColor = System.Drawing.Color.Moccasin
        Me.cb_languages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cb_languages.FormattingEnabled = True
        Me.cb_languages.Location = New System.Drawing.Point(163, 110)
        Me.cb_languages.Name = "cb_languages"
        Me.cb_languages.Size = New System.Drawing.Size(182, 98)
        Me.cb_languages.TabIndex = 8
        '
        'btn_clear_form
        '
        Me.btn_clear_form.Location = New System.Drawing.Point(0, 383)
        Me.btn_clear_form.Name = "btn_clear_form"
        Me.btn_clear_form.Size = New System.Drawing.Size(75, 23)
        Me.btn_clear_form.TabIndex = 7
        Me.btn_clear_form.Text = "Clear Form"
        Me.btn_clear_form.UseVisualStyleBackColor = True
        '
        'btn_dictionary_save
        '
        Me.btn_dictionary_save.Location = New System.Drawing.Point(270, 383)
        Me.btn_dictionary_save.Name = "btn_dictionary_save"
        Me.btn_dictionary_save.Size = New System.Drawing.Size(75, 23)
        Me.btn_dictionary_save.TabIndex = 6
        Me.btn_dictionary_save.Text = "Save"
        Me.btn_dictionary_save.UseVisualStyleBackColor = True
        '
        'txt_translate
        '
        Me.txt_translate.BackColor = System.Drawing.Color.Moccasin
        Me.txt_translate.Location = New System.Drawing.Point(163, 229)
        Me.txt_translate.Name = "txt_translate"
        Me.txt_translate.Size = New System.Drawing.Size(182, 20)
        Me.txt_translate.TabIndex = 5
        '
        'lbl_translate
        '
        Me.lbl_translate.AutoSize = True
        Me.lbl_translate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_translate.Location = New System.Drawing.Point(6, 230)
        Me.lbl_translate.Name = "lbl_translate"
        Me.lbl_translate.Size = New System.Drawing.Size(65, 16)
        Me.lbl_translate.TabIndex = 4
        Me.lbl_translate.Text = "Translate"
        '
        'lbl_word
        '
        Me.lbl_word.AutoSize = True
        Me.lbl_word.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_word.Location = New System.Drawing.Point(6, 9)
        Me.lbl_word.Name = "lbl_word"
        Me.lbl_word.Size = New System.Drawing.Size(41, 16)
        Me.lbl_word.TabIndex = 2
        Me.lbl_word.Text = "Word"
        '
        'lbl_language
        '
        Me.lbl_language.AutoSize = True
        Me.lbl_language.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_language.Location = New System.Drawing.Point(6, 110)
        Me.lbl_language.Name = "lbl_language"
        Me.lbl_language.Size = New System.Drawing.Size(69, 16)
        Me.lbl_language.TabIndex = 1
        Me.lbl_language.Text = "Language"
        '
        'tp_chat_settings
        '
        Me.tp_chat_settings.Controls.Add(Me.txt_delay_notification)
        Me.tp_chat_settings.Controls.Add(Me.lbl_delay_notification)
        Me.tp_chat_settings.Controls.Add(Me.btn_show_files)
        Me.tp_chat_settings.Controls.Add(Me.btn_open_log_file)
        Me.tp_chat_settings.Controls.Add(Me.txt_chat_log_line_limit)
        Me.tp_chat_settings.Controls.Add(Me.lbl_lbl_log_line_limit)
        Me.tp_chat_settings.Controls.Add(Me.chb_logging_chat_messages)
        Me.tp_chat_settings.Controls.Add(Me.btn_chat_save)
        Me.tp_chat_settings.Controls.Add(Me.txt_chat_port)
        Me.tp_chat_settings.Controls.Add(Me.lbl_chat_port)
        Me.tp_chat_settings.Controls.Add(Me.txt_pc_name)
        Me.tp_chat_settings.Controls.Add(Me.lbl_pc_name)
        Me.tp_chat_settings.Controls.Add(Me.txt_ipaddresses)
        Me.tp_chat_settings.Controls.Add(Me.lbl_ip_address)
        Me.tp_chat_settings.Controls.Add(Me.txt_server_user)
        Me.tp_chat_settings.Controls.Add(Me.lbl_server_user)
        Me.tp_chat_settings.Controls.Add(Me.txt_domain)
        Me.tp_chat_settings.Controls.Add(Me.lbl_domain)
        Me.tp_chat_settings.Location = New System.Drawing.Point(4, 22)
        Me.tp_chat_settings.Name = "tp_chat_settings"
        Me.tp_chat_settings.Size = New System.Drawing.Size(348, 406)
        Me.tp_chat_settings.TabIndex = 3
        Me.tp_chat_settings.Text = "chat_settings"
        Me.tp_chat_settings.UseVisualStyleBackColor = True
        '
        'btn_show_files
        '
        Me.btn_show_files.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_show_files.Location = New System.Drawing.Point(6, 202)
        Me.btn_show_files.Name = "btn_show_files"
        Me.btn_show_files.Size = New System.Drawing.Size(103, 26)
        Me.btn_show_files.TabIndex = 51
        Me.btn_show_files.Text = "Show Files"
        Me.btn_show_files.UseVisualStyleBackColor = True
        '
        'btn_open_log_file
        '
        Me.btn_open_log_file.Location = New System.Drawing.Point(6, 234)
        Me.btn_open_log_file.Name = "btn_open_log_file"
        Me.btn_open_log_file.Size = New System.Drawing.Size(103, 26)
        Me.btn_open_log_file.TabIndex = 50
        Me.btn_open_log_file.Text = "Open Log File"
        Me.btn_open_log_file.UseVisualStyleBackColor = True
        '
        'txt_chat_log_line_limit
        '
        Me.txt_chat_log_line_limit.Location = New System.Drawing.Point(150, 174)
        Me.txt_chat_log_line_limit.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_chat_log_line_limit.Name = "txt_chat_log_line_limit"
        Me.txt_chat_log_line_limit.Size = New System.Drawing.Size(195, 20)
        Me.txt_chat_log_line_limit.TabIndex = 49
        '
        'lbl_lbl_log_line_limit
        '
        Me.lbl_lbl_log_line_limit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_lbl_log_line_limit.Location = New System.Drawing.Point(3, 147)
        Me.lbl_lbl_log_line_limit.Name = "lbl_lbl_log_line_limit"
        Me.lbl_lbl_log_line_limit.Size = New System.Drawing.Size(141, 47)
        Me.lbl_lbl_log_line_limit.TabIndex = 48
        Me.lbl_lbl_log_line_limit.Text = "log_size_limit"
        Me.lbl_lbl_log_line_limit.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'chb_logging_chat_messages
        '
        Me.chb_logging_chat_messages.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_logging_chat_messages.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_logging_chat_messages.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_logging_chat_messages.Location = New System.Drawing.Point(110, 147)
        Me.chb_logging_chat_messages.Name = "chb_logging_chat_messages"
        Me.chb_logging_chat_messages.Size = New System.Drawing.Size(235, 24)
        Me.chb_logging_chat_messages.TabIndex = 47
        Me.chb_logging_chat_messages.Text = "logging_chat_messages"
        Me.chb_logging_chat_messages.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_logging_chat_messages.UseVisualStyleBackColor = True
        '
        'btn_chat_save
        '
        Me.btn_chat_save.Location = New System.Drawing.Point(270, 383)
        Me.btn_chat_save.Name = "btn_chat_save"
        Me.btn_chat_save.Size = New System.Drawing.Size(75, 23)
        Me.btn_chat_save.TabIndex = 46
        Me.btn_chat_save.Text = "Save"
        Me.btn_chat_save.UseVisualStyleBackColor = True
        '
        'txt_chat_port
        '
        Me.txt_chat_port.Location = New System.Drawing.Point(150, 101)
        Me.txt_chat_port.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_chat_port.Name = "txt_chat_port"
        Me.txt_chat_port.Size = New System.Drawing.Size(195, 20)
        Me.txt_chat_port.TabIndex = 42
        '
        'lbl_chat_port
        '
        Me.lbl_chat_port.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_chat_port.Location = New System.Drawing.Point(3, 101)
        Me.lbl_chat_port.Name = "lbl_chat_port"
        Me.lbl_chat_port.Size = New System.Drawing.Size(141, 20)
        Me.lbl_chat_port.TabIndex = 41
        Me.lbl_chat_port.Text = "chat_port"
        '
        'txt_pc_name
        '
        Me.txt_pc_name.Enabled = False
        Me.txt_pc_name.Location = New System.Drawing.Point(150, 78)
        Me.txt_pc_name.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_pc_name.Name = "txt_pc_name"
        Me.txt_pc_name.Size = New System.Drawing.Size(195, 20)
        Me.txt_pc_name.TabIndex = 40
        '
        'lbl_pc_name
        '
        Me.lbl_pc_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_pc_name.Location = New System.Drawing.Point(3, 78)
        Me.lbl_pc_name.Name = "lbl_pc_name"
        Me.lbl_pc_name.Size = New System.Drawing.Size(141, 20)
        Me.lbl_pc_name.TabIndex = 39
        Me.lbl_pc_name.Text = "pc_name"
        '
        'txt_ipaddresses
        '
        Me.txt_ipaddresses.Enabled = False
        Me.txt_ipaddresses.Location = New System.Drawing.Point(150, 55)
        Me.txt_ipaddresses.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_ipaddresses.Name = "txt_ipaddresses"
        Me.txt_ipaddresses.Size = New System.Drawing.Size(195, 20)
        Me.txt_ipaddresses.TabIndex = 38
        '
        'lbl_ip_address
        '
        Me.lbl_ip_address.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_ip_address.Location = New System.Drawing.Point(3, 55)
        Me.lbl_ip_address.Name = "lbl_ip_address"
        Me.lbl_ip_address.Size = New System.Drawing.Size(141, 20)
        Me.lbl_ip_address.TabIndex = 37
        Me.lbl_ip_address.Text = "ip_address"
        '
        'txt_server_user
        '
        Me.txt_server_user.Enabled = False
        Me.txt_server_user.Location = New System.Drawing.Point(150, 32)
        Me.txt_server_user.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_server_user.Name = "txt_server_user"
        Me.txt_server_user.Size = New System.Drawing.Size(195, 20)
        Me.txt_server_user.TabIndex = 36
        '
        'lbl_server_user
        '
        Me.lbl_server_user.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_server_user.Location = New System.Drawing.Point(3, 35)
        Me.lbl_server_user.Name = "lbl_server_user"
        Me.lbl_server_user.Size = New System.Drawing.Size(141, 20)
        Me.lbl_server_user.TabIndex = 35
        Me.lbl_server_user.Text = "Server User"
        '
        'txt_domain
        '
        Me.txt_domain.Enabled = False
        Me.txt_domain.Location = New System.Drawing.Point(150, 9)
        Me.txt_domain.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_domain.Name = "txt_domain"
        Me.txt_domain.Size = New System.Drawing.Size(195, 20)
        Me.txt_domain.TabIndex = 34
        '
        'lbl_domain
        '
        Me.lbl_domain.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_domain.Location = New System.Drawing.Point(3, 10)
        Me.lbl_domain.Name = "lbl_domain"
        Me.lbl_domain.Size = New System.Drawing.Size(141, 19)
        Me.lbl_domain.TabIndex = 33
        Me.lbl_domain.Text = "domain"
        '
        'st_status_app_bar
        '
        Me.st_status_app_bar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tss_chat_server_status, Me.tss_search_clients})
        Me.st_status_app_bar.Location = New System.Drawing.Point(0, 446)
        Me.st_status_app_bar.Name = "st_status_app_bar"
        Me.st_status_app_bar.Size = New System.Drawing.Size(496, 22)
        Me.st_status_app_bar.TabIndex = 2
        Me.st_status_app_bar.Text = "StatusStrip1"
        '
        'tss_chat_server_status
        '
        Me.tss_chat_server_status.BackColor = System.Drawing.Color.Red
        Me.tss_chat_server_status.DoubleClickEnabled = True
        Me.tss_chat_server_status.IsLink = True
        Me.tss_chat_server_status.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.tss_chat_server_status.Name = "tss_chat_server_status"
        Me.tss_chat_server_status.Size = New System.Drawing.Size(72, 17)
        Me.tss_chat_server_status.Text = "Chat Service"
        '
        'tss_search_clients
        '
        Me.tss_search_clients.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tss_search_clients.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tss_search_clients.DoubleClickEnabled = True
        Me.tss_search_clients.ForeColor = System.Drawing.Color.ForestGreen
        Me.tss_search_clients.IsLink = True
        Me.tss_search_clients.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.tss_search_clients.Margin = New System.Windows.Forms.Padding(5, 3, 0, 2)
        Me.tss_search_clients.Name = "tss_search_clients"
        Me.tss_search_clients.Size = New System.Drawing.Size(81, 17)
        Me.tss_search_clients.Text = "Search Clients"
        '
        'lbl_modules
        '
        Me.lbl_modules.AutoSize = True
        Me.lbl_modules.Location = New System.Drawing.Point(0, -1)
        Me.lbl_modules.Name = "lbl_modules"
        Me.lbl_modules.Size = New System.Drawing.Size(36, 13)
        Me.lbl_modules.TabIndex = 3
        Me.lbl_modules.Text = "Modul"
        '
        'chat_ofdSendFileToClient
        '
        Me.chat_ofdSendFileToClient.FileName = "OpenFileDialog1"
        '
        'ni_tray_icon
        '
        Me.ni_tray_icon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ni_tray_icon.ContextMenuStrip = Me.cms_tray_menu
        Me.ni_tray_icon.Icon = CType(resources.GetObject("ni_tray_icon.Icon"), System.Drawing.Icon)
        Me.ni_tray_icon.Text = "Swebex"
        Me.ni_tray_icon.Visible = True
        '
        'cms_tray_menu
        '
        Me.cms_tray_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cms_tray_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_show_hidden, Me.ts_about_app, Me.ts_exit_app})
        Me.cms_tray_menu.Name = "cms_tray_menu"
        Me.cms_tray_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.cms_tray_menu.ShowImageMargin = False
        Me.cms_tray_menu.Size = New System.Drawing.Size(150, 70)
        Me.cms_tray_menu.Text = "Swebex"
        '
        'ts_show_hidden
        '
        Me.ts_show_hidden.Name = "ts_show_hidden"
        Me.ts_show_hidden.Size = New System.Drawing.Size(149, 22)
        Me.ts_show_hidden.Text = "Show / Hidden"
        '
        'ts_about_app
        '
        Me.ts_about_app.Name = "ts_about_app"
        Me.ts_about_app.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.ts_about_app.Size = New System.Drawing.Size(149, 22)
        Me.ts_about_app.Text = "About App"
        Me.ts_about_app.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ts_exit_app
        '
        Me.ts_exit_app.Name = "ts_exit_app"
        Me.ts_exit_app.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ts_exit_app.Size = New System.Drawing.Size(149, 22)
        Me.ts_exit_app.Text = "Exit"
        Me.ts_exit_app.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_delay_notification
        '
        Me.txt_delay_notification.Location = New System.Drawing.Point(150, 124)
        Me.txt_delay_notification.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txt_delay_notification.Name = "txt_delay_notification"
        Me.txt_delay_notification.Size = New System.Drawing.Size(195, 20)
        Me.txt_delay_notification.TabIndex = 53
        '
        'lbl_delay_notification
        '
        Me.lbl_delay_notification.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_delay_notification.Location = New System.Drawing.Point(3, 124)
        Me.lbl_delay_notification.Name = "lbl_delay_notification"
        Me.lbl_delay_notification.Size = New System.Drawing.Size(141, 20)
        Me.lbl_delay_notification.TabIndex = 52
        Me.lbl_delay_notification.Text = "Delay Notification"
        '
        'main_form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 468)
        Me.Controls.Add(Me.lbl_modules)
        Me.Controls.Add(Me.st_status_app_bar)
        Me.Controls.Add(Me.tb_datalist)
        Me.Controls.Add(Me.lb_modules)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(512, 506)
        Me.Name = "main_form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Swebex"
        Me.tb_datalist.ResumeLayout(False)
        Me.tp_chat_application.ResumeLayout(False)
        Me.tp_chat_application.PerformLayout()
        Me.cms_chat_RightClickMenu.ResumeLayout(False)
        Me.tp_appsetting.ResumeLayout(False)
        Me.tp_dictionary.ResumeLayout(False)
        Me.tp_dictionary.PerformLayout()
        Me.tp_chat_settings.ResumeLayout(False)
        Me.tp_chat_settings.PerformLayout()
        Me.st_status_app_bar.ResumeLayout(False)
        Me.st_status_app_bar.PerformLayout()
        Me.cms_tray_menu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents t_system_timer As System.Windows.Forms.Timer
    Friend WithEvents lb_modules As System.Windows.Forms.ListBox
    Friend WithEvents tb_datalist As System.Windows.Forms.TabControl
    Friend WithEvents tp_dictionary As System.Windows.Forms.TabPage
    Friend WithEvents tp_chat_application As System.Windows.Forms.TabPage
    Friend WithEvents st_status_app_bar As System.Windows.Forms.StatusStrip
    Friend WithEvents lbl_modules As System.Windows.Forms.Label
    Friend WithEvents btn_clear_form As System.Windows.Forms.Button
    Friend WithEvents btn_dictionary_save As System.Windows.Forms.Button
    Friend WithEvents txt_translate As System.Windows.Forms.TextBox
    Friend WithEvents lbl_translate As System.Windows.Forms.Label
    Friend WithEvents lbl_word As System.Windows.Forms.Label
    Friend WithEvents lbl_language As System.Windows.Forms.Label
    Friend WithEvents cb_word As System.Windows.Forms.ComboBox
    Friend WithEvents cb_languages As System.Windows.Forms.ComboBox
    Friend WithEvents tp_appsetting As System.Windows.Forms.TabPage
    Friend WithEvents lb_default_language As System.Windows.Forms.ListBox
    Friend WithEvents lbl_default_lang As System.Windows.Forms.Label
    Private WithEvents logTextBox As System.Windows.Forms.TextBox
    Private WithEvents txt_chat_message As System.Windows.Forms.TextBox
    Friend WithEvents lbl_chat_connections_count As System.Windows.Forms.Label
    Friend WithEvents lv_chat_Client_list As System.Windows.Forms.ListView
    Friend WithEvents lv_chat_Client_list_Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents lv_chat_Client_list_sessionid As System.Windows.Forms.ColumnHeader
    Friend WithEvents lv_chat_client_list_machineid As System.Windows.Forms.ColumnHeader
    Friend WithEvents cms_chat_RightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cms_send_text As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cms_send_file As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TestLargeArrayTransferHelperToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cms_disconnect_client As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents il_chat_Images As System.Windows.Forms.ImageList
    Friend WithEvents chat_ofdSendFileToClient As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btn_chat_clear_history As System.Windows.Forms.Button
    Friend WithEvents tss_chat_server_status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tp_chat_settings As System.Windows.Forms.TabPage
    Friend WithEvents txt_chat_port As System.Windows.Forms.TextBox
    Friend WithEvents lbl_chat_port As System.Windows.Forms.Label
    Friend WithEvents txt_pc_name As System.Windows.Forms.TextBox
    Friend WithEvents lbl_pc_name As System.Windows.Forms.Label
    Friend WithEvents txt_ipaddresses As System.Windows.Forms.TextBox
    Friend WithEvents lbl_ip_address As System.Windows.Forms.Label
    Friend WithEvents txt_server_user As System.Windows.Forms.TextBox
    Friend WithEvents lbl_server_user As System.Windows.Forms.Label
    Friend WithEvents txt_domain As System.Windows.Forms.TextBox
    Friend WithEvents lbl_domain As System.Windows.Forms.Label
    Public WithEvents btn_chat_send_to_All As System.Windows.Forms.Button
    Friend WithEvents btn_chat_save As System.Windows.Forms.Button
    Public WithEvents btn_chat_disconnect_all As System.Windows.Forms.Button
    Friend WithEvents btn_app_settings_save As System.Windows.Forms.Button
    Friend WithEvents txt_chat_log_line_limit As System.Windows.Forms.TextBox
    Friend WithEvents lbl_lbl_log_line_limit As System.Windows.Forms.Label
    Friend WithEvents chb_logging_chat_messages As System.Windows.Forms.CheckBox
    Friend WithEvents btn_open_log_file As System.Windows.Forms.Button
    Friend WithEvents ni_tray_icon As System.Windows.Forms.NotifyIcon
    Friend WithEvents tss_search_clients As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cms_tray_menu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ts_about_app As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ts_exit_app As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rtb_chat_history As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_show_files As System.Windows.Forms.Button
    Friend WithEvents ts_show_hidden As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txt_delay_notification As System.Windows.Forms.TextBox
    Friend WithEvents lbl_delay_notification As System.Windows.Forms.Label

End Class
