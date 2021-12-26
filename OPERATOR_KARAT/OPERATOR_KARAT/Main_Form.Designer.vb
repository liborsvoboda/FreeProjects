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
        Me.tb_control = New System.Windows.Forms.TabControl()
        Me.tp_datalist = New System.Windows.Forms.TabPage()
        Me.dgw_summary_view = New System.Windows.Forms.DataGridView()
        Me.dgw_query_view = New System.Windows.Forms.DataGridView()
        Me.tp_settings = New System.Windows.Forms.TabPage()
        Me.chb_own_def_loc = New System.Windows.Forms.CheckBox()
        Me.btn_own_def_folder = New System.Windows.Forms.Button()
        Me.txt_own_def_location = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chb_dgview_multiselect = New System.Windows.Forms.CheckBox()
        Me.chb_extensible_rows = New System.Windows.Forms.CheckBox()
        Me.chb_clean_form_bad_sql = New System.Windows.Forms.CheckBox()
        Me.btn_sql_login_test = New System.Windows.Forms.Button()
        Me.lbl_debuger_fn = New System.Windows.Forms.Label()
        Me.chb_select_first_row = New System.Windows.Forms.CheckBox()
        Me.txt_font_size = New System.Windows.Forms.TextBox()
        Me.lbl_font_size = New System.Windows.Forms.Label()
        Me.chb_clean_form_god_sql = New System.Windows.Forms.CheckBox()
        Me.chb_must_field_message = New System.Windows.Forms.CheckBox()
        Me.cb_default_keyboard = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chb_format_debbuger = New System.Windows.Forms.CheckBox()
        Me.txt_mssql_server = New System.Windows.Forms.TextBox()
        Me.lbl_server = New System.Windows.Forms.Label()
        Me.btn_save_setting = New System.Windows.Forms.Button()
        Me.txt_mssql_password = New System.Windows.Forms.TextBox()
        Me.lbl_mssql_password = New System.Windows.Forms.Label()
        Me.txt_mssql_name = New System.Windows.Forms.TextBox()
        Me.lbl_mssql_name = New System.Windows.Forms.Label()
        Me.lbl_mssql_section = New System.Windows.Forms.Label()
        Me.chb_sql_debug = New System.Windows.Forms.CheckBox()
        Me.tp_help = New System.Windows.Forms.TabPage()
        Me.txt_help = New System.Windows.Forms.TextBox()
        Me.lbl_available_reports = New System.Windows.Forms.Label()
        Me.lb_available_reports = New System.Windows.Forms.ListBox()
        Me.p_user_inputs = New System.Windows.Forms.Panel()
        Me.lbl_user_inputs = New System.Windows.Forms.Label()
        Me.t_system_time = New System.Windows.Forms.Timer(Me.components)
        Me.p_sub_buttons = New System.Windows.Forms.Panel()
        Me.lbl_subfunction = New System.Windows.Forms.Label()
        Me.btn_back = New System.Windows.Forms.Button()
        Me.cb_reports = New System.Windows.Forms.ComboBox()
        Me.fbd_own_definitions_folder = New System.Windows.Forms.FolderBrowserDialog()
        Me.tb_control.SuspendLayout()
        Me.tp_datalist.SuspendLayout()
        CType(Me.dgw_summary_view, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgw_query_view, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_settings.SuspendLayout()
        Me.tp_help.SuspendLayout()
        Me.SuspendLayout()
        '
        'tb_control
        '
        Me.tb_control.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tb_control.Controls.Add(Me.tp_datalist)
        Me.tb_control.Controls.Add(Me.tp_settings)
        Me.tb_control.Controls.Add(Me.tp_help)
        Me.tb_control.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tb_control.Location = New System.Drawing.Point(251, 0)
        Me.tb_control.Margin = New System.Windows.Forms.Padding(0)
        Me.tb_control.Name = "tb_control"
        Me.tb_control.Padding = New System.Drawing.Point(0, 0)
        Me.tb_control.SelectedIndex = 0
        Me.tb_control.Size = New System.Drawing.Size(601, 646)
        Me.tb_control.TabIndex = 0
        '
        'tp_datalist
        '
        Me.tp_datalist.Controls.Add(Me.dgw_summary_view)
        Me.tp_datalist.Controls.Add(Me.dgw_query_view)
        Me.tp_datalist.Location = New System.Drawing.Point(4, 25)
        Me.tp_datalist.Name = "tp_datalist"
        Me.tp_datalist.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_datalist.Size = New System.Drawing.Size(593, 617)
        Me.tp_datalist.TabIndex = 0
        Me.tp_datalist.Text = "DATALIST"
        Me.tp_datalist.UseVisualStyleBackColor = True
        '
        'dgw_summary_view
        '
        Me.dgw_summary_view.AllowUserToAddRows = False
        Me.dgw_summary_view.AllowUserToDeleteRows = False
        Me.dgw_summary_view.AllowUserToResizeRows = False
        Me.dgw_summary_view.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgw_summary_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgw_summary_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgw_summary_view.ColumnHeadersVisible = False
        Me.dgw_summary_view.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgw_summary_view.EnableHeadersVisualStyles = False
        Me.dgw_summary_view.Location = New System.Drawing.Point(0, 568)
        Me.dgw_summary_view.MultiSelect = False
        Me.dgw_summary_view.Name = "dgw_summary_view"
        Me.dgw_summary_view.ReadOnly = True
        Me.dgw_summary_view.RowHeadersVisible = False
        Me.dgw_summary_view.Size = New System.Drawing.Size(597, 49)
        Me.dgw_summary_view.TabIndex = 2
        '
        'dgw_query_view
        '
        Me.dgw_query_view.AllowUserToAddRows = False
        Me.dgw_query_view.AllowUserToDeleteRows = False
        Me.dgw_query_view.AllowUserToOrderColumns = True
        Me.dgw_query_view.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgw_query_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgw_query_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgw_query_view.Location = New System.Drawing.Point(0, 0)
        Me.dgw_query_view.MultiSelect = False
        Me.dgw_query_view.Name = "dgw_query_view"
        Me.dgw_query_view.ReadOnly = True
        Me.dgw_query_view.RowHeadersVisible = False
        Me.dgw_query_view.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgw_query_view.Size = New System.Drawing.Size(597, 562)
        Me.dgw_query_view.StandardTab = True
        Me.dgw_query_view.TabIndex = 0
        '
        'tp_settings
        '
        Me.tp_settings.Controls.Add(Me.chb_own_def_loc)
        Me.tp_settings.Controls.Add(Me.btn_own_def_folder)
        Me.tp_settings.Controls.Add(Me.txt_own_def_location)
        Me.tp_settings.Controls.Add(Me.Label2)
        Me.tp_settings.Controls.Add(Me.chb_dgview_multiselect)
        Me.tp_settings.Controls.Add(Me.chb_extensible_rows)
        Me.tp_settings.Controls.Add(Me.chb_clean_form_bad_sql)
        Me.tp_settings.Controls.Add(Me.btn_sql_login_test)
        Me.tp_settings.Controls.Add(Me.lbl_debuger_fn)
        Me.tp_settings.Controls.Add(Me.chb_select_first_row)
        Me.tp_settings.Controls.Add(Me.txt_font_size)
        Me.tp_settings.Controls.Add(Me.lbl_font_size)
        Me.tp_settings.Controls.Add(Me.chb_clean_form_god_sql)
        Me.tp_settings.Controls.Add(Me.chb_must_field_message)
        Me.tp_settings.Controls.Add(Me.cb_default_keyboard)
        Me.tp_settings.Controls.Add(Me.Label1)
        Me.tp_settings.Controls.Add(Me.chb_format_debbuger)
        Me.tp_settings.Controls.Add(Me.txt_mssql_server)
        Me.tp_settings.Controls.Add(Me.lbl_server)
        Me.tp_settings.Controls.Add(Me.btn_save_setting)
        Me.tp_settings.Controls.Add(Me.txt_mssql_password)
        Me.tp_settings.Controls.Add(Me.lbl_mssql_password)
        Me.tp_settings.Controls.Add(Me.txt_mssql_name)
        Me.tp_settings.Controls.Add(Me.lbl_mssql_name)
        Me.tp_settings.Controls.Add(Me.lbl_mssql_section)
        Me.tp_settings.Controls.Add(Me.chb_sql_debug)
        Me.tp_settings.Location = New System.Drawing.Point(4, 25)
        Me.tp_settings.Margin = New System.Windows.Forms.Padding(0)
        Me.tp_settings.Name = "tp_settings"
        Me.tp_settings.Size = New System.Drawing.Size(593, 617)
        Me.tp_settings.TabIndex = 1
        Me.tp_settings.Text = "Nastavení"
        Me.tp_settings.UseVisualStyleBackColor = True
        '
        'chb_own_def_loc
        '
        Me.chb_own_def_loc.AutoSize = True
        Me.chb_own_def_loc.Location = New System.Drawing.Point(9, 370)
        Me.chb_own_def_loc.Name = "chb_own_def_loc"
        Me.chb_own_def_loc.Size = New System.Drawing.Size(15, 14)
        Me.chb_own_def_loc.TabIndex = 1154
        Me.chb_own_def_loc.UseVisualStyleBackColor = True
        '
        'btn_own_def_folder
        '
        Me.btn_own_def_folder.Enabled = False
        Me.btn_own_def_folder.Location = New System.Drawing.Point(277, 365)
        Me.btn_own_def_folder.Name = "btn_own_def_folder"
        Me.btn_own_def_folder.Size = New System.Drawing.Size(70, 22)
        Me.btn_own_def_folder.TabIndex = 200
        Me.btn_own_def_folder.Text = "Adresář"
        Me.btn_own_def_folder.UseVisualStyleBackColor = True
        '
        'txt_own_def_location
        '
        Me.txt_own_def_location.AcceptsReturn = True
        Me.txt_own_def_location.Enabled = False
        Me.txt_own_def_location.Location = New System.Drawing.Point(39, 365)
        Me.txt_own_def_location.Name = "txt_own_def_location"
        Me.txt_own_def_location.Size = New System.Drawing.Size(232, 22)
        Me.txt_own_def_location.TabIndex = 190
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 345)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 19)
        Me.Label2.TabIndex = 1153
        Me.Label2.Text = "Vlastní ůložiště Definic"
        '
        'chb_dgview_multiselect
        '
        Me.chb_dgview_multiselect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_dgview_multiselect.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chb_dgview_multiselect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_dgview_multiselect.Location = New System.Drawing.Point(118, 291)
        Me.chb_dgview_multiselect.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_dgview_multiselect.Name = "chb_dgview_multiselect"
        Me.chb_dgview_multiselect.Size = New System.Drawing.Size(227, 16)
        Me.chb_dgview_multiselect.TabIndex = 170
        Me.chb_dgview_multiselect.Text = "Označení Více Řádků"
        Me.chb_dgview_multiselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_dgview_multiselect.UseVisualStyleBackColor = True
        '
        'chb_extensible_rows
        '
        Me.chb_extensible_rows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_extensible_rows.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chb_extensible_rows.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_extensible_rows.Location = New System.Drawing.Point(160, 268)
        Me.chb_extensible_rows.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_extensible_rows.Name = "chb_extensible_rows"
        Me.chb_extensible_rows.Size = New System.Drawing.Size(185, 16)
        Me.chb_extensible_rows.TabIndex = 160
        Me.chb_extensible_rows.Text = "Roztažitelné řádky"
        Me.chb_extensible_rows.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_extensible_rows.UseVisualStyleBackColor = True
        '
        'chb_clean_form_bad_sql
        '
        Me.chb_clean_form_bad_sql.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_clean_form_bad_sql.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chb_clean_form_bad_sql.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_clean_form_bad_sql.Location = New System.Drawing.Point(26, 224)
        Me.chb_clean_form_bad_sql.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_clean_form_bad_sql.Name = "chb_clean_form_bad_sql"
        Me.chb_clean_form_bad_sql.Size = New System.Drawing.Size(319, 16)
        Me.chb_clean_form_bad_sql.TabIndex = 145
        Me.chb_clean_form_bad_sql.Text = "Vymazat Formulář po neúspěšném provedení dotazu"
        Me.chb_clean_form_bad_sql.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_clean_form_bad_sql.UseVisualStyleBackColor = True
        '
        'btn_sql_login_test
        '
        Me.btn_sql_login_test.Location = New System.Drawing.Point(243, 111)
        Me.btn_sql_login_test.Name = "btn_sql_login_test"
        Me.btn_sql_login_test.Size = New System.Drawing.Size(103, 23)
        Me.btn_sql_login_test.TabIndex = 125
        Me.btn_sql_login_test.Text = "Test Spojení"
        Me.btn_sql_login_test.UseVisualStyleBackColor = True
        '
        'lbl_debuger_fn
        '
        Me.lbl_debuger_fn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_debuger_fn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_debuger_fn.Location = New System.Drawing.Point(364, 30)
        Me.lbl_debuger_fn.Name = "lbl_debuger_fn"
        Me.lbl_debuger_fn.Size = New System.Drawing.Size(223, 23)
        Me.lbl_debuger_fn.TabIndex = 1003
        Me.lbl_debuger_fn.Text = "DEBUGER FUNCTIONS"
        Me.lbl_debuger_fn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chb_select_first_row
        '
        Me.chb_select_first_row.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_select_first_row.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chb_select_first_row.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_select_first_row.Location = New System.Drawing.Point(87, 246)
        Me.chb_select_first_row.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_select_first_row.Name = "chb_select_first_row"
        Me.chb_select_first_row.Size = New System.Drawing.Size(257, 16)
        Me.chb_select_first_row.TabIndex = 150
        Me.chb_select_first_row.Text = "Označit První Záznam"
        Me.chb_select_first_row.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_select_first_row.UseVisualStyleBackColor = True
        '
        'txt_font_size
        '
        Me.txt_font_size.Location = New System.Drawing.Point(169, 312)
        Me.txt_font_size.Name = "txt_font_size"
        Me.txt_font_size.Size = New System.Drawing.Size(177, 22)
        Me.txt_font_size.TabIndex = 180
        '
        'lbl_font_size
        '
        Me.lbl_font_size.Location = New System.Drawing.Point(6, 317)
        Me.lbl_font_size.Name = "lbl_font_size"
        Me.lbl_font_size.Size = New System.Drawing.Size(136, 19)
        Me.lbl_font_size.TabIndex = 1002
        Me.lbl_font_size.Text = "Velikost Textu"
        '
        'chb_clean_form_god_sql
        '
        Me.chb_clean_form_god_sql.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_clean_form_god_sql.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chb_clean_form_god_sql.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_clean_form_god_sql.Location = New System.Drawing.Point(9, 202)
        Me.chb_clean_form_god_sql.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_clean_form_god_sql.Name = "chb_clean_form_god_sql"
        Me.chb_clean_form_god_sql.Size = New System.Drawing.Size(337, 16)
        Me.chb_clean_form_god_sql.TabIndex = 140
        Me.chb_clean_form_god_sql.Text = "Vymazat Formulář po úspěšném provedení dotazu"
        Me.chb_clean_form_god_sql.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_clean_form_god_sql.UseVisualStyleBackColor = True
        '
        'chb_must_field_message
        '
        Me.chb_must_field_message.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_must_field_message.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chb_must_field_message.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_must_field_message.Location = New System.Drawing.Point(46, 180)
        Me.chb_must_field_message.Margin = New System.Windows.Forms.Padding(0)
        Me.chb_must_field_message.Name = "chb_must_field_message"
        Me.chb_must_field_message.Size = New System.Drawing.Size(299, 16)
        Me.chb_must_field_message.TabIndex = 135
        Me.chb_must_field_message.Text = "Zobrazovat Zprávu Povinného pole"
        Me.chb_must_field_message.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chb_must_field_message.UseVisualStyleBackColor = True
        '
        'cb_default_keyboard
        '
        Me.cb_default_keyboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_default_keyboard.FormattingEnabled = True
        Me.cb_default_keyboard.ItemHeight = 16
        Me.cb_default_keyboard.Items.AddRange(New Object() {"", "LANG_CZECH", "LANG_ENGLISH", "LANG_FRENCH", "LANG_GERMAN", "LANG_ITALIAN", "LANG_NORWEGIAN", "LANG_PORTUGUESE", "LANG_RUSSIAN", "LANG_SPANISH", "LANG_UKRAINE"})
        Me.cb_default_keyboard.Location = New System.Drawing.Point(169, 155)
        Me.cb_default_keyboard.Name = "cb_default_keyboard"
        Me.cb_default_keyboard.Size = New System.Drawing.Size(177, 24)
        Me.cb_default_keyboard.TabIndex = 130
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 23)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Def.Klávesnice:"
        '
        'chb_format_debbuger
        '
        Me.chb_format_debbuger.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chb_format_debbuger.AutoSize = True
        Me.chb_format_debbuger.Location = New System.Drawing.Point(451, 56)
        Me.chb_format_debbuger.Name = "chb_format_debbuger"
        Me.chb_format_debbuger.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_format_debbuger.Size = New System.Drawing.Size(133, 20)
        Me.chb_format_debbuger.TabIndex = 1100
        Me.chb_format_debbuger.Text = "Format Debugger"
        Me.chb_format_debbuger.UseVisualStyleBackColor = True
        '
        'txt_mssql_server
        '
        Me.txt_mssql_server.AcceptsReturn = True
        Me.txt_mssql_server.Location = New System.Drawing.Point(169, 31)
        Me.txt_mssql_server.Name = "txt_mssql_server"
        Me.txt_mssql_server.Size = New System.Drawing.Size(177, 22)
        Me.txt_mssql_server.TabIndex = 100
        '
        'lbl_server
        '
        Me.lbl_server.Location = New System.Drawing.Point(6, 36)
        Me.lbl_server.Name = "lbl_server"
        Me.lbl_server.Size = New System.Drawing.Size(139, 19)
        Me.lbl_server.TabIndex = 7
        Me.lbl_server.Text = "MSSQL Server:"
        '
        'btn_save_setting
        '
        Me.btn_save_setting.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_save_setting.Location = New System.Drawing.Point(252, 416)
        Me.btn_save_setting.Name = "btn_save_setting"
        Me.btn_save_setting.Size = New System.Drawing.Size(95, 23)
        Me.btn_save_setting.TabIndex = 250
        Me.btn_save_setting.Text = "Uložit"
        Me.btn_save_setting.UseVisualStyleBackColor = True
        '
        'txt_mssql_password
        '
        Me.txt_mssql_password.Location = New System.Drawing.Point(169, 83)
        Me.txt_mssql_password.Name = "txt_mssql_password"
        Me.txt_mssql_password.Size = New System.Drawing.Size(177, 22)
        Me.txt_mssql_password.TabIndex = 120
        Me.txt_mssql_password.UseSystemPasswordChar = True
        '
        'lbl_mssql_password
        '
        Me.lbl_mssql_password.Location = New System.Drawing.Point(6, 85)
        Me.lbl_mssql_password.Name = "lbl_mssql_password"
        Me.lbl_mssql_password.Size = New System.Drawing.Size(136, 16)
        Me.lbl_mssql_password.TabIndex = 4
        Me.lbl_mssql_password.Text = "Heslo:"
        '
        'txt_mssql_name
        '
        Me.txt_mssql_name.Location = New System.Drawing.Point(169, 57)
        Me.txt_mssql_name.Name = "txt_mssql_name"
        Me.txt_mssql_name.Size = New System.Drawing.Size(177, 22)
        Me.txt_mssql_name.TabIndex = 110
        '
        'lbl_mssql_name
        '
        Me.lbl_mssql_name.Location = New System.Drawing.Point(6, 62)
        Me.lbl_mssql_name.Name = "lbl_mssql_name"
        Me.lbl_mssql_name.Size = New System.Drawing.Size(136, 19)
        Me.lbl_mssql_name.TabIndex = 2
        Me.lbl_mssql_name.Text = "Jméno:"
        '
        'lbl_mssql_section
        '
        Me.lbl_mssql_section.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_mssql_section.Location = New System.Drawing.Point(6, 10)
        Me.lbl_mssql_section.Name = "lbl_mssql_section"
        Me.lbl_mssql_section.Size = New System.Drawing.Size(223, 23)
        Me.lbl_mssql_section.TabIndex = 1
        Me.lbl_mssql_section.Text = "MSSQL SERVER LOGIN"
        Me.lbl_mssql_section.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chb_sql_debug
        '
        Me.chb_sql_debug.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chb_sql_debug.AutoSize = True
        Me.chb_sql_debug.Location = New System.Drawing.Point(466, 79)
        Me.chb_sql_debug.Name = "chb_sql_debug"
        Me.chb_sql_debug.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chb_sql_debug.Size = New System.Drawing.Size(117, 20)
        Me.chb_sql_debug.TabIndex = 1150
        Me.chb_sql_debug.Text = "SQL Debugger"
        Me.chb_sql_debug.UseVisualStyleBackColor = True
        '
        'tp_help
        '
        Me.tp_help.AutoScroll = True
        Me.tp_help.Controls.Add(Me.txt_help)
        Me.tp_help.Location = New System.Drawing.Point(4, 25)
        Me.tp_help.Name = "tp_help"
        Me.tp_help.Size = New System.Drawing.Size(593, 617)
        Me.tp_help.TabIndex = 2
        Me.tp_help.Text = "Nápověda"
        Me.tp_help.UseVisualStyleBackColor = True
        '
        'txt_help
        '
        Me.txt_help.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txt_help.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_help.Location = New System.Drawing.Point(0, 0)
        Me.txt_help.Multiline = True
        Me.txt_help.Name = "txt_help"
        Me.txt_help.ReadOnly = True
        Me.txt_help.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_help.Size = New System.Drawing.Size(593, 617)
        Me.txt_help.TabIndex = 2
        Me.txt_help.Text = resources.GetString("txt_help.Text")
        '
        'lbl_available_reports
        '
        Me.lbl_available_reports.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_available_reports.Location = New System.Drawing.Point(3, 9)
        Me.lbl_available_reports.Name = "lbl_available_reports"
        Me.lbl_available_reports.Size = New System.Drawing.Size(243, 13)
        Me.lbl_available_reports.TabIndex = 0
        Me.lbl_available_reports.Text = "DOSTUPNÉ PŘEHLEDY"
        Me.lbl_available_reports.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lb_available_reports
        '
        Me.lb_available_reports.FormattingEnabled = True
        Me.lb_available_reports.Location = New System.Drawing.Point(3, 22)
        Me.lb_available_reports.Margin = New System.Windows.Forms.Padding(0)
        Me.lb_available_reports.Name = "lb_available_reports"
        Me.lb_available_reports.Size = New System.Drawing.Size(245, 173)
        Me.lb_available_reports.TabIndex = 3
        '
        'p_user_inputs
        '
        Me.p_user_inputs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.p_user_inputs.AutoScroll = True
        Me.p_user_inputs.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.p_user_inputs.Location = New System.Drawing.Point(3, 225)
        Me.p_user_inputs.Name = "p_user_inputs"
        Me.p_user_inputs.Size = New System.Drawing.Size(246, 417)
        Me.p_user_inputs.TabIndex = 4
        '
        'lbl_user_inputs
        '
        Me.lbl_user_inputs.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lbl_user_inputs.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_user_inputs.Location = New System.Drawing.Point(3, 207)
        Me.lbl_user_inputs.Name = "lbl_user_inputs"
        Me.lbl_user_inputs.Size = New System.Drawing.Size(246, 18)
        Me.lbl_user_inputs.TabIndex = 0
        Me.lbl_user_inputs.Text = "UŽIVATELSKÁ SEKCE"
        Me.lbl_user_inputs.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        't_system_time
        '
        Me.t_system_time.Enabled = True
        Me.t_system_time.Interval = 1000
        '
        'p_sub_buttons
        '
        Me.p_sub_buttons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.p_sub_buttons.AutoScroll = True
        Me.p_sub_buttons.BackColor = System.Drawing.SystemColors.Info
        Me.p_sub_buttons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_sub_buttons.Location = New System.Drawing.Point(111, 638)
        Me.p_sub_buttons.Name = "p_sub_buttons"
        Me.p_sub_buttons.Size = New System.Drawing.Size(661, 29)
        Me.p_sub_buttons.TabIndex = 5
        '
        'lbl_subfunction
        '
        Me.lbl_subfunction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_subfunction.BackColor = System.Drawing.SystemColors.Info
        Me.lbl_subfunction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_subfunction.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_subfunction.Location = New System.Drawing.Point(3, 624)
        Me.lbl_subfunction.Name = "lbl_subfunction"
        Me.lbl_subfunction.Size = New System.Drawing.Size(115, 45)
        Me.lbl_subfunction.TabIndex = 0
        Me.lbl_subfunction.Text = "NAVÁZANÉ:"
        '
        'btn_back
        '
        Me.btn_back.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_back.Enabled = False
        Me.btn_back.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_back.Location = New System.Drawing.Point(771, 638)
        Me.btn_back.Name = "btn_back"
        Me.btn_back.Size = New System.Drawing.Size(71, 30)
        Me.btn_back.TabIndex = 6
        Me.btn_back.Text = "ODHLÁSIT"
        Me.btn_back.UseVisualStyleBackColor = True
        '
        'cb_reports
        '
        Me.cb_reports.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cb_reports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_reports.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cb_reports.FormattingEnabled = True
        Me.cb_reports.Location = New System.Drawing.Point(3, 645)
        Me.cb_reports.Name = "cb_reports"
        Me.cb_reports.Size = New System.Drawing.Size(115, 23)
        Me.cb_reports.TabIndex = 3
        '
        'fbd_own_definitions_folder
        '
        Me.fbd_own_definitions_folder.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 669)
        Me.Controls.Add(Me.cb_reports)
        Me.Controls.Add(Me.btn_back)
        Me.Controls.Add(Me.lbl_subfunction)
        Me.Controls.Add(Me.p_sub_buttons)
        Me.Controls.Add(Me.lbl_user_inputs)
        Me.Controls.Add(Me.p_user_inputs)
        Me.Controls.Add(Me.lb_available_reports)
        Me.Controls.Add(Me.lbl_available_reports)
        Me.Controls.Add(Me.tb_control)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "Main_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TERMINAL STUDIO"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tb_control.ResumeLayout(False)
        Me.tp_datalist.ResumeLayout(False)
        CType(Me.dgw_summary_view, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgw_query_view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_settings.ResumeLayout(False)
        Me.tp_settings.PerformLayout()
        Me.tp_help.ResumeLayout(False)
        Me.tp_help.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tb_control As System.Windows.Forms.TabControl
    Friend WithEvents tp_datalist As System.Windows.Forms.TabPage
    Friend WithEvents tp_settings As System.Windows.Forms.TabPage
    Friend WithEvents dgw_query_view As System.Windows.Forms.DataGridView
    Friend WithEvents chb_sql_debug As System.Windows.Forms.CheckBox
    Friend WithEvents txt_mssql_password As System.Windows.Forms.TextBox
    Friend WithEvents lbl_mssql_password As System.Windows.Forms.Label
    Friend WithEvents txt_mssql_name As System.Windows.Forms.TextBox
    Friend WithEvents lbl_mssql_name As System.Windows.Forms.Label
    Friend WithEvents lbl_mssql_section As System.Windows.Forms.Label
    Friend WithEvents btn_save_setting As System.Windows.Forms.Button
    Friend WithEvents txt_mssql_server As System.Windows.Forms.TextBox
    Friend WithEvents lbl_server As System.Windows.Forms.Label
    Friend WithEvents lbl_available_reports As System.Windows.Forms.Label
    Friend WithEvents lb_available_reports As System.Windows.Forms.ListBox
    Friend WithEvents lbl_user_inputs As System.Windows.Forms.Label
    Friend WithEvents chb_format_debbuger As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cb_default_keyboard As System.Windows.Forms.ComboBox
    Friend WithEvents chb_must_field_message As System.Windows.Forms.CheckBox
    Friend WithEvents chb_clean_form_god_sql As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_font_size As System.Windows.Forms.Label
    Friend WithEvents txt_font_size As System.Windows.Forms.TextBox
    Friend WithEvents chb_select_first_row As System.Windows.Forms.CheckBox
    Friend WithEvents t_system_time As System.Windows.Forms.Timer
    Friend WithEvents p_sub_buttons As System.Windows.Forms.Panel
    Friend WithEvents lbl_subfunction As System.Windows.Forms.Label
    Friend WithEvents btn_back As System.Windows.Forms.Button
    Friend WithEvents tp_help As System.Windows.Forms.TabPage
    Friend WithEvents txt_help As System.Windows.Forms.TextBox
    Friend WithEvents lbl_debuger_fn As System.Windows.Forms.Label
    Friend WithEvents btn_sql_login_test As System.Windows.Forms.Button
    Friend WithEvents chb_clean_form_bad_sql As System.Windows.Forms.CheckBox
    Friend WithEvents dgw_summary_view As System.Windows.Forms.DataGridView
    Friend WithEvents chb_dgview_multiselect As System.Windows.Forms.CheckBox
    Friend WithEvents chb_extensible_rows As System.Windows.Forms.CheckBox
    Public WithEvents p_user_inputs As System.Windows.Forms.Panel
    Friend WithEvents cb_reports As System.Windows.Forms.ComboBox
    Friend WithEvents btn_own_def_folder As System.Windows.Forms.Button
    Friend WithEvents txt_own_def_location As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents fbd_own_definitions_folder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents chb_own_def_loc As System.Windows.Forms.CheckBox

End Class
