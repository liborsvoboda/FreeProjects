<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class warehouse_detector
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
        Dim lbl_printer_processor As System.Windows.Forms.Label
        Dim lbl_paper_cut As System.Windows.Forms.Label
        Dim lbl_for_lbl_printers As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(warehouse_detector))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.st_product_finder = New System.Windows.Forms.TabPage
        Me.btn_open_packing_list = New System.Windows.Forms.Button
        Me.btn_open_draw = New System.Windows.Forms.Button
        Me.txt_atr1 = New System.Windows.Forms.TextBox
        Me.txt_atr2 = New System.Windows.Forms.TextBox
        Me.btn_print = New System.Windows.Forms.Button
        Me.btn_print_to = New System.Windows.Forms.Button
        Me.btn_clear = New System.Windows.Forms.Button
        Me.btn_semiproduct = New System.Windows.Forms.Button
        Me.btn_minus = New System.Windows.Forms.Button
        Me.lbl_last_load = New System.Windows.Forms.Label
        Me.psx_waiting = New System.Windows.Forms.PictureBox
        Me.lb_query_result = New System.Windows.Forms.ListBox
        Me.btn_status = New System.Windows.Forms.Button
        Me.lbl_atr2 = New System.Windows.Forms.Label
        Me.lbl_atr1 = New System.Windows.Forms.Label
        Me.sb_statusbar = New System.Windows.Forms.StatusBar
        Me.btn_search = New System.Windows.Forms.Button
        Me.lbl_found_location = New System.Windows.Forms.Label
        Me.lb_product = New System.Windows.Forms.Label
        Me.txt_product = New System.Windows.Forms.TextBox
        Me.st_location_content = New System.Windows.Forms.TabPage
        Me.lbl_last_load1 = New System.Windows.Forms.Label
        Me.btn_status1 = New System.Windows.Forms.Button
        Me.btn_loc_search = New System.Windows.Forms.Button
        Me.lb_found_items = New System.Windows.Forms.ListBox
        Me.lbl_location = New System.Windows.Forms.Label
        Me.txt_location = New System.Windows.Forms.TextBox
        Me.st_configuration = New System.Windows.Forms.TabPage
        Me.chb_nnv = New System.Windows.Forms.CheckBox
        Me.chb_sql_debugger = New System.Windows.Forms.CheckBox
        Me.cb_no_location = New System.Windows.Forms.CheckBox
        Me.chb_negative_count = New System.Windows.Forms.CheckBox
        Me.btn_off_line_import = New System.Windows.Forms.Button
        Me.lb_warehouse = New System.Windows.Forms.ListBox
        Me.sb_last_load = New System.Windows.Forms.StatusBar
        Me.chb_zero_value = New System.Windows.Forms.CheckBox
        Me.lbl_selected_warehouse = New System.Windows.Forms.Label
        Me.lb_selected_warehouse = New System.Windows.Forms.ListBox
        Me.lb_status = New System.Windows.Forms.Label
        Me.txt_inout_note = New System.Windows.Forms.Label
        Me.pb_data_status = New System.Windows.Forms.ProgressBar
        Me.st_printer = New System.Windows.Forms.TabPage
        Me.btn_clean_packing_list_path = New System.Windows.Forms.Button
        Me.btn_set_packing_list_path = New System.Windows.Forms.Button
        Me.txt_packing_list_path = New System.Windows.Forms.TextBox
        Me.lbl_packing_list_path = New System.Windows.Forms.Label
        Me.btn_clean_path = New System.Windows.Forms.Button
        Me.btn_set_epdm_path = New System.Windows.Forms.Button
        Me.txt_epdm_drawing_path = New System.Windows.Forms.TextBox
        Me.lbl_epdm_drawing_path = New System.Windows.Forms.Label
        Me.nud_min_row_count = New System.Windows.Forms.NumericUpDown
        Me.chb_paper_cut = New System.Windows.Forms.CheckBox
        Me.cb_printer_processor = New System.Windows.Forms.ComboBox
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.fbd_folder_list = New System.Windows.Forms.FolderBrowserDialog
        lbl_printer_processor = New System.Windows.Forms.Label
        lbl_paper_cut = New System.Windows.Forms.Label
        lbl_for_lbl_printers = New System.Windows.Forms.Label
        Label1 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.st_product_finder.SuspendLayout()
        CType(Me.psx_waiting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.st_location_content.SuspendLayout()
        Me.st_configuration.SuspendLayout()
        Me.st_printer.SuspendLayout()
        CType(Me.nud_min_row_count, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbl_printer_processor
        '
        lbl_printer_processor.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        lbl_printer_processor.Location = New System.Drawing.Point(8, 13)
        lbl_printer_processor.Name = "lbl_printer_processor"
        lbl_printer_processor.Size = New System.Drawing.Size(163, 18)
        lbl_printer_processor.TabIndex = 93
        lbl_printer_processor.Text = "Tiskový procesor"
        '
        'lbl_paper_cut
        '
        lbl_paper_cut.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        lbl_paper_cut.Location = New System.Drawing.Point(8, 46)
        lbl_paper_cut.Name = "lbl_paper_cut"
        lbl_paper_cut.Size = New System.Drawing.Size(75, 18)
        lbl_paper_cut.TabIndex = 95
        lbl_paper_cut.Text = "Zaøezávat"
        '
        'lbl_for_lbl_printers
        '
        lbl_for_lbl_printers.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        lbl_for_lbl_printers.Location = New System.Drawing.Point(8, 64)
        lbl_for_lbl_printers.Name = "lbl_for_lbl_printers"
        lbl_for_lbl_printers.Size = New System.Drawing.Size(129, 18)
        lbl_for_lbl_printers.TabIndex = 97
        lbl_for_lbl_printers.Text = "(pouze pro Lbl.Tiskárny)"
        '
        'Label1
        '
        Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Label1.Location = New System.Drawing.Point(8, 82)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(137, 18)
        Label1.TabIndex = 98
        Label1.Text = "Vert.Kalibrace"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.st_product_finder)
        Me.TabControl1.Controls.Add(Me.st_location_content)
        Me.TabControl1.Controls.Add(Me.st_configuration)
        Me.TabControl1.Controls.Add(Me.st_printer)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(314, 266)
        Me.TabControl1.TabIndex = 101
        Me.TabControl1.TabStop = False
        '
        'st_product_finder
        '
        Me.st_product_finder.Controls.Add(Me.btn_open_packing_list)
        Me.st_product_finder.Controls.Add(Me.btn_open_draw)
        Me.st_product_finder.Controls.Add(Me.txt_atr1)
        Me.st_product_finder.Controls.Add(Me.txt_atr2)
        Me.st_product_finder.Controls.Add(Me.btn_print)
        Me.st_product_finder.Controls.Add(Me.btn_print_to)
        Me.st_product_finder.Controls.Add(Me.btn_clear)
        Me.st_product_finder.Controls.Add(Me.btn_semiproduct)
        Me.st_product_finder.Controls.Add(Me.btn_minus)
        Me.st_product_finder.Controls.Add(Me.lbl_last_load)
        Me.st_product_finder.Controls.Add(Me.psx_waiting)
        Me.st_product_finder.Controls.Add(Me.lb_query_result)
        Me.st_product_finder.Controls.Add(Me.btn_status)
        Me.st_product_finder.Controls.Add(Me.lbl_atr2)
        Me.st_product_finder.Controls.Add(Me.lbl_atr1)
        Me.st_product_finder.Controls.Add(Me.sb_statusbar)
        Me.st_product_finder.Controls.Add(Me.btn_search)
        Me.st_product_finder.Controls.Add(Me.lbl_found_location)
        Me.st_product_finder.Controls.Add(Me.lb_product)
        Me.st_product_finder.Controls.Add(Me.txt_product)
        Me.st_product_finder.Location = New System.Drawing.Point(4, 23)
        Me.st_product_finder.Name = "st_product_finder"
        Me.st_product_finder.Size = New System.Drawing.Size(306, 239)
        Me.st_product_finder.TabIndex = 0
        Me.st_product_finder.Text = "VYHLEDÁVAÈ"
        Me.st_product_finder.UseVisualStyleBackColor = True
        '
        'btn_open_packing_list
        '
        Me.btn_open_packing_list.BackgroundImage = Global.Warehouse_Detector.My.Resources.Resources.packing_list
        Me.btn_open_packing_list.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_open_packing_list.Enabled = False
        Me.btn_open_packing_list.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_open_packing_list.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.btn_open_packing_list.Location = New System.Drawing.Point(265, 217)
        Me.btn_open_packing_list.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_open_packing_list.Name = "btn_open_packing_list"
        Me.btn_open_packing_list.Size = New System.Drawing.Size(41, 26)
        Me.btn_open_packing_list.TabIndex = 96
        Me.btn_open_packing_list.Text = "F3"
        Me.btn_open_packing_list.UseVisualStyleBackColor = True
        '
        'btn_open_draw
        '
        Me.btn_open_draw.BackgroundImage = Global.Warehouse_Detector.My.Resources.Resources.draw
        Me.btn_open_draw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_open_draw.Enabled = False
        Me.btn_open_draw.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_open_draw.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.btn_open_draw.Location = New System.Drawing.Point(228, 217)
        Me.btn_open_draw.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_open_draw.Name = "btn_open_draw"
        Me.btn_open_draw.Size = New System.Drawing.Size(37, 26)
        Me.btn_open_draw.TabIndex = 95
        Me.btn_open_draw.Text = "F2"
        '
        'txt_atr1
        '
        Me.txt_atr1.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.txt_atr1.Location = New System.Drawing.Point(64, 57)
        Me.txt_atr1.Name = "txt_atr1"
        Me.txt_atr1.Size = New System.Drawing.Size(92, 33)
        Me.txt_atr1.TabIndex = 20
        '
        'txt_atr2
        '
        Me.txt_atr2.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.txt_atr2.Location = New System.Drawing.Point(64, 90)
        Me.txt_atr2.Name = "txt_atr2"
        Me.txt_atr2.Size = New System.Drawing.Size(92, 33)
        Me.txt_atr2.TabIndex = 30
        '
        'btn_print
        '
        Me.btn_print.Location = New System.Drawing.Point(5, 188)
        Me.btn_print.Name = "btn_print"
        Me.btn_print.Size = New System.Drawing.Size(71, 26)
        Me.btn_print.TabIndex = 87
        Me.btn_print.Text = "TISK"
        '
        'btn_print_to
        '
        Me.btn_print_to.Location = New System.Drawing.Point(82, 188)
        Me.btn_print_to.Name = "btn_print_to"
        Me.btn_print_to.Size = New System.Drawing.Size(74, 26)
        Me.btn_print_to.TabIndex = 86
        Me.btn_print_to.Text = "TISK na.."
        '
        'btn_clear
        '
        Me.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_clear.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_clear.Location = New System.Drawing.Point(136, 2)
        Me.btn_clear.Name = "btn_clear"
        Me.btn_clear.Size = New System.Drawing.Size(20, 18)
        Me.btn_clear.TabIndex = 78
        Me.btn_clear.TabStop = False
        Me.btn_clear.Text = "C"
        '
        'btn_semiproduct
        '
        Me.btn_semiproduct.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.btn_semiproduct.Location = New System.Drawing.Point(96, 156)
        Me.btn_semiproduct.Name = "btn_semiproduct"
        Me.btn_semiproduct.Size = New System.Drawing.Size(60, 26)
        Me.btn_semiproduct.TabIndex = 70
        Me.btn_semiproduct.TabStop = False
        Me.btn_semiproduct.Text = "POL.ZAK."
        '
        'btn_minus
        '
        Me.btn_minus.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_minus.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_minus.Location = New System.Drawing.Point(110, 2)
        Me.btn_minus.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_minus.Name = "btn_minus"
        Me.btn_minus.Size = New System.Drawing.Size(23, 19)
        Me.btn_minus.TabIndex = 62
        Me.btn_minus.TabStop = False
        Me.btn_minus.Text = "-"
        '
        'lbl_last_load
        '
        Me.lbl_last_load.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.lbl_last_load.Font = New System.Drawing.Font("Arial", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_last_load.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_last_load.Location = New System.Drawing.Point(58, 223)
        Me.lbl_last_load.Name = "lbl_last_load"
        Me.lbl_last_load.Size = New System.Drawing.Size(167, 16)
        Me.lbl_last_load.TabIndex = 88
        Me.lbl_last_load.Text = "Poslední Naètení:"
        Me.lbl_last_load.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'psx_waiting
        '
        Me.psx_waiting.Image = CType(resources.GetObject("psx_waiting.Image"), System.Drawing.Image)
        Me.psx_waiting.Location = New System.Drawing.Point(5, 124)
        Me.psx_waiting.Name = "psx_waiting"
        Me.psx_waiting.Size = New System.Drawing.Size(86, 58)
        Me.psx_waiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.psx_waiting.TabIndex = 89
        Me.psx_waiting.TabStop = False
        '
        'lb_query_result
        '
        Me.lb_query_result.HorizontalScrollbar = True
        Me.lb_query_result.ItemHeight = 14
        Me.lb_query_result.Location = New System.Drawing.Point(162, 23)
        Me.lb_query_result.Name = "lb_query_result"
        Me.lb_query_result.Size = New System.Drawing.Size(141, 186)
        Me.lb_query_result.TabIndex = 56
        Me.lb_query_result.TabStop = False
        '
        'btn_status
        '
        Me.btn_status.Enabled = False
        Me.btn_status.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_status.Font = New System.Drawing.Font("Trebuchet MS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_status.ForeColor = System.Drawing.Color.ForestGreen
        Me.btn_status.Location = New System.Drawing.Point(0, 218)
        Me.btn_status.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_status.Name = "btn_status"
        Me.btn_status.Size = New System.Drawing.Size(55, 21)
        Me.btn_status.TabIndex = 50
        Me.btn_status.TabStop = False
        Me.btn_status.Text = "ONLINE"
        '
        'lbl_atr2
        '
        Me.lbl_atr2.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_atr2.Location = New System.Drawing.Point(4, 94)
        Me.lbl_atr2.Name = "lbl_atr2"
        Me.lbl_atr2.Size = New System.Drawing.Size(72, 20)
        Me.lbl_atr2.TabIndex = 90
        Me.lbl_atr2.Text = "ATR2"
        '
        'lbl_atr1
        '
        Me.lbl_atr1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_atr1.Location = New System.Drawing.Point(5, 62)
        Me.lbl_atr1.Name = "lbl_atr1"
        Me.lbl_atr1.Size = New System.Drawing.Size(71, 23)
        Me.lbl_atr1.TabIndex = 91
        Me.lbl_atr1.Text = "ATR1"
        '
        'sb_statusbar
        '
        Me.sb_statusbar.Location = New System.Drawing.Point(0, 215)
        Me.sb_statusbar.Name = "sb_statusbar"
        Me.sb_statusbar.Size = New System.Drawing.Size(306, 24)
        Me.sb_statusbar.TabIndex = 92
        '
        'btn_search
        '
        Me.btn_search.Location = New System.Drawing.Point(96, 124)
        Me.btn_search.Name = "btn_search"
        Me.btn_search.Size = New System.Drawing.Size(60, 26)
        Me.btn_search.TabIndex = 40
        Me.btn_search.Text = "HLEDAT"
        '
        'lbl_found_location
        '
        Me.lbl_found_location.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_found_location.Location = New System.Drawing.Point(160, 0)
        Me.lbl_found_location.Name = "lbl_found_location"
        Me.lbl_found_location.Size = New System.Drawing.Size(146, 20)
        Me.lbl_found_location.TabIndex = 93
        Me.lbl_found_location.Text = "LOKACE | KS"
        '
        'lb_product
        '
        Me.lb_product.Location = New System.Drawing.Point(4, 4)
        Me.lb_product.Name = "lb_product"
        Me.lb_product.Size = New System.Drawing.Size(133, 17)
        Me.lb_product.TabIndex = 94
        Me.lb_product.Text = "ZAK/NOM/IDPŽ."
        '
        'txt_product
        '
        Me.txt_product.AcceptsReturn = True
        Me.txt_product.AcceptsTab = True
        Me.txt_product.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.txt_product.Location = New System.Drawing.Point(5, 23)
        Me.txt_product.Name = "txt_product"
        Me.txt_product.Size = New System.Drawing.Size(151, 33)
        Me.txt_product.TabIndex = 10
        '
        'st_location_content
        '
        Me.st_location_content.Controls.Add(Me.lbl_last_load1)
        Me.st_location_content.Controls.Add(Me.btn_status1)
        Me.st_location_content.Controls.Add(Me.btn_loc_search)
        Me.st_location_content.Controls.Add(Me.lb_found_items)
        Me.st_location_content.Controls.Add(Me.lbl_location)
        Me.st_location_content.Controls.Add(Me.txt_location)
        Me.st_location_content.Location = New System.Drawing.Point(4, 23)
        Me.st_location_content.Name = "st_location_content"
        Me.st_location_content.Size = New System.Drawing.Size(306, 239)
        Me.st_location_content.TabIndex = 1
        Me.st_location_content.Text = "OBSAH LOKACE"
        Me.st_location_content.UseVisualStyleBackColor = True
        '
        'lbl_last_load1
        '
        Me.lbl_last_load1.Location = New System.Drawing.Point(107, 219)
        Me.lbl_last_load1.Name = "lbl_last_load1"
        Me.lbl_last_load1.Size = New System.Drawing.Size(198, 16)
        Me.lbl_last_load1.TabIndex = 0
        Me.lbl_last_load1.Text = "Poslední Naètení:"
        '
        'btn_status1
        '
        Me.btn_status1.Enabled = False
        Me.btn_status1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btn_status1.Font = New System.Drawing.Font("Trebuchet MS", 14.0!, System.Drawing.FontStyle.Bold)
        Me.btn_status1.ForeColor = System.Drawing.Color.ForestGreen
        Me.btn_status1.Location = New System.Drawing.Point(0, 219)
        Me.btn_status1.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_status1.Name = "btn_status1"
        Me.btn_status1.Size = New System.Drawing.Size(104, 20)
        Me.btn_status1.TabIndex = 52
        Me.btn_status1.TabStop = False
        Me.btn_status1.Text = "ONLINE"
        '
        'btn_loc_search
        '
        Me.btn_loc_search.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btn_loc_search.Location = New System.Drawing.Point(223, 3)
        Me.btn_loc_search.Name = "btn_loc_search"
        Me.btn_loc_search.Size = New System.Drawing.Size(84, 32)
        Me.btn_loc_search.TabIndex = 41
        Me.btn_loc_search.Text = "HLEDAT"
        '
        'lb_found_items
        '
        Me.lb_found_items.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.lb_found_items.HorizontalScrollbar = True
        Me.lb_found_items.ItemHeight = 24
        Me.lb_found_items.Location = New System.Drawing.Point(3, 41)
        Me.lb_found_items.Name = "lb_found_items"
        Me.lb_found_items.Size = New System.Drawing.Size(300, 172)
        Me.lb_found_items.TabIndex = 13
        '
        'lbl_location
        '
        Me.lbl_location.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_location.Location = New System.Drawing.Point(0, 9)
        Me.lbl_location.Name = "lbl_location"
        Me.lbl_location.Size = New System.Drawing.Size(76, 20)
        Me.lbl_location.TabIndex = 53
        Me.lbl_location.Text = "LOKACE"
        Me.lbl_location.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txt_location
        '
        Me.txt_location.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.txt_location.Location = New System.Drawing.Point(76, 3)
        Me.txt_location.Name = "txt_location"
        Me.txt_location.Size = New System.Drawing.Size(145, 33)
        Me.txt_location.TabIndex = 12
        '
        'st_configuration
        '
        Me.st_configuration.Controls.Add(Me.chb_nnv)
        Me.st_configuration.Controls.Add(Me.chb_sql_debugger)
        Me.st_configuration.Controls.Add(Me.cb_no_location)
        Me.st_configuration.Controls.Add(Me.chb_negative_count)
        Me.st_configuration.Controls.Add(Me.btn_off_line_import)
        Me.st_configuration.Controls.Add(Me.lb_warehouse)
        Me.st_configuration.Controls.Add(Me.sb_last_load)
        Me.st_configuration.Controls.Add(Me.chb_zero_value)
        Me.st_configuration.Controls.Add(Me.lbl_selected_warehouse)
        Me.st_configuration.Controls.Add(Me.lb_selected_warehouse)
        Me.st_configuration.Controls.Add(Me.lb_status)
        Me.st_configuration.Controls.Add(Me.txt_inout_note)
        Me.st_configuration.Controls.Add(Me.pb_data_status)
        Me.st_configuration.Location = New System.Drawing.Point(4, 23)
        Me.st_configuration.Name = "st_configuration"
        Me.st_configuration.Size = New System.Drawing.Size(306, 239)
        Me.st_configuration.TabIndex = 2
        Me.st_configuration.Text = "NAST.SQL"
        Me.st_configuration.UseVisualStyleBackColor = True
        '
        'chb_nnv
        '
        Me.chb_nnv.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chb_nnv.Location = New System.Drawing.Point(3, 168)
        Me.chb_nnv.Name = "chb_nnv"
        Me.chb_nnv.Size = New System.Drawing.Size(173, 20)
        Me.chb_nnv.TabIndex = 48
        Me.chb_nnv.Text = "Vèetnì položek NV (NNV)"
        '
        'chb_sql_debugger
        '
        Me.chb_sql_debugger.AutoSize = True
        Me.chb_sql_debugger.Location = New System.Drawing.Point(221, 5)
        Me.chb_sql_debugger.Name = "chb_sql_debugger"
        Me.chb_sql_debugger.Size = New System.Drawing.Size(81, 18)
        Me.chb_sql_debugger.TabIndex = 47
        Me.chb_sql_debugger.Text = "SQL Debug"
        Me.chb_sql_debugger.UseVisualStyleBackColor = True
        '
        'cb_no_location
        '
        Me.cb_no_location.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cb_no_location.Location = New System.Drawing.Point(3, 35)
        Me.cb_no_location.Name = "cb_no_location"
        Me.cb_no_location.Size = New System.Drawing.Size(145, 20)
        Me.cb_no_location.TabIndex = 41
        Me.cb_no_location.Text = "BEZ LOKACE"
        '
        'chb_negative_count
        '
        Me.chb_negative_count.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chb_negative_count.Location = New System.Drawing.Point(3, 19)
        Me.chb_negative_count.Name = "chb_negative_count"
        Me.chb_negative_count.Size = New System.Drawing.Size(174, 20)
        Me.chb_negative_count.TabIndex = 15
        Me.chb_negative_count.Text = "ZÁPORNÉ MNOŽSTVÍ"
        '
        'btn_off_line_import
        '
        Me.btn_off_line_import.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btn_off_line_import.Location = New System.Drawing.Point(153, 215)
        Me.btn_off_line_import.Name = "btn_off_line_import"
        Me.btn_off_line_import.Size = New System.Drawing.Size(153, 24)
        Me.btn_off_line_import.TabIndex = 40
        Me.btn_off_line_import.Text = "OFFLINE IMPORT "
        '
        'lb_warehouse
        '
        Me.lb_warehouse.ItemHeight = 14
        Me.lb_warehouse.Location = New System.Drawing.Point(4, 58)
        Me.lb_warehouse.Name = "lb_warehouse"
        Me.lb_warehouse.Size = New System.Drawing.Size(140, 102)
        Me.lb_warehouse.TabIndex = 20
        Me.lb_warehouse.Tag = ""
        '
        'sb_last_load
        '
        Me.sb_last_load.Location = New System.Drawing.Point(0, 215)
        Me.sb_last_load.Name = "sb_last_load"
        Me.sb_last_load.Size = New System.Drawing.Size(306, 24)
        Me.sb_last_load.TabIndex = 42
        Me.sb_last_load.Text = "Poslední Naètení:"
        '
        'chb_zero_value
        '
        Me.chb_zero_value.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chb_zero_value.Location = New System.Drawing.Point(3, 3)
        Me.chb_zero_value.Name = "chb_zero_value"
        Me.chb_zero_value.Size = New System.Drawing.Size(145, 20)
        Me.chb_zero_value.TabIndex = 10
        Me.chb_zero_value.Text = "0 MNOŽSTVÍ"
        '
        'lbl_selected_warehouse
        '
        Me.lbl_selected_warehouse.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lbl_selected_warehouse.Location = New System.Drawing.Point(151, 35)
        Me.lbl_selected_warehouse.Name = "lbl_selected_warehouse"
        Me.lbl_selected_warehouse.Size = New System.Drawing.Size(157, 20)
        Me.lbl_selected_warehouse.TabIndex = 43
        Me.lbl_selected_warehouse.Text = "VYBRANÉ SKLADY"
        Me.lbl_selected_warehouse.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lb_selected_warehouse
        '
        Me.lb_selected_warehouse.ItemHeight = 14
        Me.lb_selected_warehouse.Location = New System.Drawing.Point(150, 58)
        Me.lb_selected_warehouse.Name = "lb_selected_warehouse"
        Me.lb_selected_warehouse.Size = New System.Drawing.Size(151, 102)
        Me.lb_selected_warehouse.TabIndex = 30
        Me.lb_selected_warehouse.Tag = ""
        '
        'lb_status
        '
        Me.lb_status.Font = New System.Drawing.Font("Tahoma", 16.0!)
        Me.lb_status.Location = New System.Drawing.Point(213, 161)
        Me.lb_status.Name = "lb_status"
        Me.lb_status.Size = New System.Drawing.Size(90, 24)
        Me.lb_status.TabIndex = 44
        Me.lb_status.Text = "STATUS:"
        '
        'txt_inout_note
        '
        Me.txt_inout_note.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txt_inout_note.Location = New System.Drawing.Point(4, 121)
        Me.txt_inout_note.Name = "txt_inout_note"
        Me.txt_inout_note.Size = New System.Drawing.Size(297, 20)
        Me.txt_inout_note.TabIndex = 45
        Me.txt_inout_note.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pb_data_status
        '
        Me.pb_data_status.Location = New System.Drawing.Point(213, 188)
        Me.pb_data_status.Name = "pb_data_status"
        Me.pb_data_status.Size = New System.Drawing.Size(88, 21)
        Me.pb_data_status.TabIndex = 46
        '
        'st_printer
        '
        Me.st_printer.Controls.Add(Me.btn_clean_packing_list_path)
        Me.st_printer.Controls.Add(Me.btn_set_packing_list_path)
        Me.st_printer.Controls.Add(Me.txt_packing_list_path)
        Me.st_printer.Controls.Add(Me.lbl_packing_list_path)
        Me.st_printer.Controls.Add(Me.btn_clean_path)
        Me.st_printer.Controls.Add(Me.btn_set_epdm_path)
        Me.st_printer.Controls.Add(Me.txt_epdm_drawing_path)
        Me.st_printer.Controls.Add(Me.lbl_epdm_drawing_path)
        Me.st_printer.Controls.Add(Me.nud_min_row_count)
        Me.st_printer.Controls.Add(Label1)
        Me.st_printer.Controls.Add(lbl_for_lbl_printers)
        Me.st_printer.Controls.Add(Me.chb_paper_cut)
        Me.st_printer.Controls.Add(lbl_paper_cut)
        Me.st_printer.Controls.Add(Me.cb_printer_processor)
        Me.st_printer.Controls.Add(lbl_printer_processor)
        Me.st_printer.Location = New System.Drawing.Point(4, 23)
        Me.st_printer.Name = "st_printer"
        Me.st_printer.Size = New System.Drawing.Size(306, 239)
        Me.st_printer.TabIndex = 3
        Me.st_printer.Text = "TISK"
        Me.st_printer.UseVisualStyleBackColor = True
        '
        'btn_clean_packing_list_path
        '
        Me.btn_clean_packing_list_path.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_clean_packing_list_path.Location = New System.Drawing.Point(273, 178)
        Me.btn_clean_packing_list_path.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_clean_packing_list_path.Name = "btn_clean_packing_list_path"
        Me.btn_clean_packing_list_path.Size = New System.Drawing.Size(25, 23)
        Me.btn_clean_packing_list_path.TabIndex = 107
        Me.btn_clean_packing_list_path.Text = "X"
        Me.btn_clean_packing_list_path.UseVisualStyleBackColor = True
        '
        'btn_set_packing_list_path
        '
        Me.btn_set_packing_list_path.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_set_packing_list_path.Location = New System.Drawing.Point(234, 178)
        Me.btn_set_packing_list_path.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_set_packing_list_path.Name = "btn_set_packing_list_path"
        Me.btn_set_packing_list_path.Size = New System.Drawing.Size(39, 23)
        Me.btn_set_packing_list_path.TabIndex = 106
        Me.btn_set_packing_list_path.Text = "Adresáø"
        Me.btn_set_packing_list_path.UseVisualStyleBackColor = True
        '
        'txt_packing_list_path
        '
        Me.txt_packing_list_path.Enabled = False
        Me.txt_packing_list_path.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_packing_list_path.Location = New System.Drawing.Point(8, 204)
        Me.txt_packing_list_path.Name = "txt_packing_list_path"
        Me.txt_packing_list_path.Size = New System.Drawing.Size(290, 22)
        Me.txt_packing_list_path.TabIndex = 105
        '
        'lbl_packing_list_path
        '
        Me.lbl_packing_list_path.AutoSize = True
        Me.lbl_packing_list_path.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_packing_list_path.Location = New System.Drawing.Point(8, 182)
        Me.lbl_packing_list_path.Name = "lbl_packing_list_path"
        Me.lbl_packing_list_path.Size = New System.Drawing.Size(207, 16)
        Me.lbl_packing_list_path.TabIndex = 104
        Me.lbl_packing_list_path.Text = "Adresáø s Výkresy EPDM (PDF)"
        '
        'btn_clean_path
        '
        Me.btn_clean_path.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_clean_path.Location = New System.Drawing.Point(273, 119)
        Me.btn_clean_path.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_clean_path.Name = "btn_clean_path"
        Me.btn_clean_path.Size = New System.Drawing.Size(25, 23)
        Me.btn_clean_path.TabIndex = 103
        Me.btn_clean_path.Text = "X"
        Me.btn_clean_path.UseVisualStyleBackColor = True
        '
        'btn_set_epdm_path
        '
        Me.btn_set_epdm_path.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_set_epdm_path.Location = New System.Drawing.Point(234, 119)
        Me.btn_set_epdm_path.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_set_epdm_path.Name = "btn_set_epdm_path"
        Me.btn_set_epdm_path.Size = New System.Drawing.Size(39, 23)
        Me.btn_set_epdm_path.TabIndex = 102
        Me.btn_set_epdm_path.Text = "Adresáø"
        Me.btn_set_epdm_path.UseVisualStyleBackColor = True
        '
        'txt_epdm_drawing_path
        '
        Me.txt_epdm_drawing_path.Enabled = False
        Me.txt_epdm_drawing_path.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txt_epdm_drawing_path.Location = New System.Drawing.Point(8, 145)
        Me.txt_epdm_drawing_path.Name = "txt_epdm_drawing_path"
        Me.txt_epdm_drawing_path.Size = New System.Drawing.Size(290, 22)
        Me.txt_epdm_drawing_path.TabIndex = 101
        '
        'lbl_epdm_drawing_path
        '
        Me.lbl_epdm_drawing_path.AutoSize = True
        Me.lbl_epdm_drawing_path.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_epdm_drawing_path.Location = New System.Drawing.Point(8, 123)
        Me.lbl_epdm_drawing_path.Name = "lbl_epdm_drawing_path"
        Me.lbl_epdm_drawing_path.Size = New System.Drawing.Size(207, 16)
        Me.lbl_epdm_drawing_path.TabIndex = 100
        Me.lbl_epdm_drawing_path.Text = "Adresáø s Výkresy EPDM (PDF)"
        '
        'nud_min_row_count
        '
        Me.nud_min_row_count.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.nud_min_row_count.Location = New System.Drawing.Point(211, 78)
        Me.nud_min_row_count.Name = "nud_min_row_count"
        Me.nud_min_row_count.Size = New System.Drawing.Size(87, 22)
        Me.nud_min_row_count.TabIndex = 99
        Me.nud_min_row_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chb_paper_cut
        '
        Me.chb_paper_cut.AutoSize = True
        Me.chb_paper_cut.Location = New System.Drawing.Point(283, 50)
        Me.chb_paper_cut.Name = "chb_paper_cut"
        Me.chb_paper_cut.Size = New System.Drawing.Size(15, 14)
        Me.chb_paper_cut.TabIndex = 96
        Me.chb_paper_cut.UseVisualStyleBackColor = True
        '
        'cb_printer_processor
        '
        Me.cb_printer_processor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_printer_processor.FormattingEnabled = True
        Me.cb_printer_processor.Items.AddRange(New Object() {"Automatic", "RAW", "RAW [FF appended]", "RAW [FF auto]", "Text"})
        Me.cb_printer_processor.Location = New System.Drawing.Point(177, 13)
        Me.cb_printer_processor.Name = "cb_printer_processor"
        Me.cb_printer_processor.Size = New System.Drawing.Size(121, 22)
        Me.cb_printer_processor.TabIndex = 94
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'warehouse_detector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 266)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "warehouse_detector"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WAREHOUSE DETECTOR 1.19"
        Me.TabControl1.ResumeLayout(False)
        Me.st_product_finder.ResumeLayout(False)
        Me.st_product_finder.PerformLayout()
        CType(Me.psx_waiting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.st_location_content.ResumeLayout(False)
        Me.st_location_content.PerformLayout()
        Me.st_configuration.ResumeLayout(False)
        Me.st_configuration.PerformLayout()
        Me.st_printer.ResumeLayout(False)
        Me.st_printer.PerformLayout()
        CType(Me.nud_min_row_count, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents st_product_finder As System.Windows.Forms.TabPage
    Friend WithEvents btn_print As System.Windows.Forms.Button
    Friend WithEvents btn_print_to As System.Windows.Forms.Button
    Friend WithEvents btn_clear As System.Windows.Forms.Button
    Friend WithEvents btn_semiproduct As System.Windows.Forms.Button
    Friend WithEvents btn_minus As System.Windows.Forms.Button
    Friend WithEvents lbl_last_load As System.Windows.Forms.Label
    Friend WithEvents psx_waiting As System.Windows.Forms.PictureBox
    Friend WithEvents lb_query_result As System.Windows.Forms.ListBox
    Friend WithEvents btn_status As System.Windows.Forms.Button
    Friend WithEvents lbl_atr2 As System.Windows.Forms.Label
    Friend WithEvents txt_atr2 As System.Windows.Forms.TextBox
    Friend WithEvents lbl_atr1 As System.Windows.Forms.Label
    Friend WithEvents txt_atr1 As System.Windows.Forms.TextBox
    Friend WithEvents sb_statusbar As System.Windows.Forms.StatusBar
    Friend WithEvents btn_search As System.Windows.Forms.Button
    Friend WithEvents lbl_found_location As System.Windows.Forms.Label
    Friend WithEvents lb_product As System.Windows.Forms.Label
    Friend WithEvents txt_product As System.Windows.Forms.TextBox
    Friend WithEvents st_location_content As System.Windows.Forms.TabPage
    Friend WithEvents lbl_last_load1 As System.Windows.Forms.Label
    Friend WithEvents btn_status1 As System.Windows.Forms.Button
    Friend WithEvents btn_loc_search As System.Windows.Forms.Button
    Friend WithEvents lb_found_items As System.Windows.Forms.ListBox
    Friend WithEvents lbl_location As System.Windows.Forms.Label
    Friend WithEvents txt_location As System.Windows.Forms.TextBox
    Friend WithEvents st_configuration As System.Windows.Forms.TabPage
    Friend WithEvents cb_no_location As System.Windows.Forms.CheckBox
    Friend WithEvents chb_negative_count As System.Windows.Forms.CheckBox
    Friend WithEvents btn_off_line_import As System.Windows.Forms.Button
    Friend WithEvents lb_warehouse As System.Windows.Forms.ListBox
    Friend WithEvents sb_last_load As System.Windows.Forms.StatusBar
    Friend WithEvents chb_zero_value As System.Windows.Forms.CheckBox
    Friend WithEvents lbl_selected_warehouse As System.Windows.Forms.Label
    Friend WithEvents lb_selected_warehouse As System.Windows.Forms.ListBox
    Friend WithEvents lb_status As System.Windows.Forms.Label
    Friend WithEvents txt_inout_note As System.Windows.Forms.Label
    Friend WithEvents pb_data_status As System.Windows.Forms.ProgressBar
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents chb_sql_debugger As System.Windows.Forms.CheckBox
    Friend WithEvents chb_nnv As System.Windows.Forms.CheckBox
    Friend WithEvents st_printer As System.Windows.Forms.TabPage
    Friend WithEvents cb_printer_processor As System.Windows.Forms.ComboBox
    Friend WithEvents chb_paper_cut As System.Windows.Forms.CheckBox
    Friend WithEvents nud_min_row_count As System.Windows.Forms.NumericUpDown
    Friend WithEvents btn_open_draw As System.Windows.Forms.Button
    Friend WithEvents fbd_folder_list As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btn_set_epdm_path As System.Windows.Forms.Button
    Friend WithEvents txt_epdm_drawing_path As System.Windows.Forms.TextBox
    Friend WithEvents lbl_epdm_drawing_path As System.Windows.Forms.Label
    Friend WithEvents btn_clean_path As System.Windows.Forms.Button
    Friend WithEvents btn_open_packing_list As System.Windows.Forms.Button
    Friend WithEvents btn_clean_packing_list_path As System.Windows.Forms.Button
    Friend WithEvents btn_set_packing_list_path As System.Windows.Forms.Button
    Friend WithEvents txt_packing_list_path As System.Windows.Forms.TextBox
    Friend WithEvents lbl_packing_list_path As System.Windows.Forms.Label

End Class
