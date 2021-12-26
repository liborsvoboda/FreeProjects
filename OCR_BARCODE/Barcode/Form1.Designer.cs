namespace Barcode
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tb_filescan = new System.Windows.Forms.TabPage();
            this.btnQRPDFScan = new System.Windows.Forms.Button();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.btnPDFClear = new System.Windows.Forms.Button();
            this.rtbpdfData = new System.Windows.Forms.RichTextBox();
            this.tbPDFname = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnFileScan = new System.Windows.Forms.Button();
            this.btnPDFFile = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.tp_config = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pdf_splitter = new System.Windows.Forms.TextBox();
            this.chb_pdf_split = new System.Windows.Forms.CheckBox();
            this.chb_directory_only = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_output_dialog = new System.Windows.Forms.Button();
            this.btn_input_dialog = new System.Windows.Forms.Button();
            this.txt_unique_identification = new System.Windows.Forms.TextBox();
            this.lbl_unique_identification = new System.Windows.Forms.Label();
            this.txt_limit_log_size = new System.Windows.Forms.TextBox();
            this.lbl_limit_log_size = new System.Windows.Forms.Label();
            this.txt_time_interval = new System.Windows.Forms.TextBox();
            this.lbl_time_interval = new System.Windows.Forms.Label();
            this.chb_delete_source_file = new System.Windows.Forms.CheckBox();
            this.txt_out_folder = new System.Windows.Forms.TextBox();
            this.txt_in_folder = new System.Windows.Forms.TextBox();
            this.lbl_out_folder = new System.Windows.Forms.Label();
            this.lbl_in_folder = new System.Windows.Forms.Label();
            this.chb_automatic_start_service = new System.Windows.Forms.CheckBox();
            this.btn_config_save = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_uninstall_service = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_install_service = new System.Windows.Forms.Button();
            this.btn_start_service = new System.Windows.Forms.Button();
            this.btn_open_log_file = new System.Windows.Forms.Button();
            this.btn_stop_service = new System.Windows.Forms.Button();
            this.tb_control1 = new System.Windows.Forms.TabControl();
            this.ni_traymenu = new System.Windows.Forms.NotifyIcon(this.components);
            this.mi_ocrdriver = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tstb_start_ocr_process = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_stop_ocr_process = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_config_form = new System.Windows.Forms.ToolStripMenuItem();
            this.tstb_close_app = new System.Windows.Forms.ToolStripMenuItem();
            this.fb_input_dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.fb_output_dialog = new System.Windows.Forms.FolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tb_filescan.SuspendLayout();
            this.tp_config.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tb_control1.SuspendLayout();
            this.mi_ocrdriver.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tb_filescan
            // 
            this.tb_filescan.Controls.Add(this.btnQRPDFScan);
            this.tb_filescan.Controls.Add(this.lblWaiting);
            this.tb_filescan.Controls.Add(this.btnPDFClear);
            this.tb_filescan.Controls.Add(this.rtbpdfData);
            this.tb_filescan.Controls.Add(this.tbPDFname);
            this.tb_filescan.Controls.Add(this.label16);
            this.tb_filescan.Controls.Add(this.btnFileScan);
            this.tb_filescan.Controls.Add(this.btnPDFFile);
            this.tb_filescan.Controls.Add(this.label15);
            this.tb_filescan.Location = new System.Drawing.Point(4, 22);
            this.tb_filescan.Name = "tb_filescan";
            this.tb_filescan.Size = new System.Drawing.Size(362, 365);
            this.tb_filescan.TabIndex = 2;
            this.tb_filescan.Text = "Scan Souboru (BARCODE)";
            this.tb_filescan.UseVisualStyleBackColor = true;
            // 
            // btnQRPDFScan
            // 
            this.btnQRPDFScan.Location = new System.Drawing.Point(94, 66);
            this.btnQRPDFScan.Name = "btnQRPDFScan";
            this.btnQRPDFScan.Size = new System.Drawing.Size(78, 60);
            this.btnQRPDFScan.TabIndex = 50;
            this.btnQRPDFScan.Text = "Skenovat QR Code";
            this.btnQRPDFScan.UseVisualStyleBackColor = true;
            this.btnQRPDFScan.Click += new System.EventHandler(this.btnQRPDFScan_Click);
            // 
            // lblWaiting
            // 
            this.lblWaiting.Location = new System.Drawing.Point(178, 129);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(179, 20);
            this.lblWaiting.TabIndex = 18;
            // 
            // btnPDFClear
            // 
            this.btnPDFClear.Location = new System.Drawing.Point(296, 33);
            this.btnPDFClear.Name = "btnPDFClear";
            this.btnPDFClear.Size = new System.Drawing.Size(61, 29);
            this.btnPDFClear.TabIndex = 30;
            this.btnPDFClear.Text = "Vymazat";
            this.btnPDFClear.UseVisualStyleBackColor = true;
            this.btnPDFClear.Click += new System.EventHandler(this.btnPDFClear_Click);
            // 
            // rtbpdfData
            // 
            this.rtbpdfData.Location = new System.Drawing.Point(178, 65);
            this.rtbpdfData.Name = "rtbpdfData";
            this.rtbpdfData.Size = new System.Drawing.Size(179, 61);
            this.rtbpdfData.TabIndex = 60;
            this.rtbpdfData.Text = "";
            // 
            // tbPDFname
            // 
            this.tbPDFname.Location = new System.Drawing.Point(95, 6);
            this.tbPDFname.Name = "tbPDFname";
            this.tbPDFname.Size = new System.Drawing.Size(181, 20);
            this.tbPDFname.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(178, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 20);
            this.label16.TabIndex = 15;
            this.label16.Text = "Nalezené Čárové Kódy";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnFileScan
            // 
            this.btnFileScan.Location = new System.Drawing.Point(10, 66);
            this.btnFileScan.Name = "btnFileScan";
            this.btnFileScan.Size = new System.Drawing.Size(78, 60);
            this.btnFileScan.TabIndex = 40;
            this.btnFileScan.Text = "Skenovat EAN128";
            this.btnFileScan.UseVisualStyleBackColor = true;
            this.btnFileScan.Click += new System.EventHandler(this.btnFileScan_Click);
            // 
            // btnPDFFile
            // 
            this.btnPDFFile.Location = new System.Drawing.Point(282, 4);
            this.btnPDFFile.Name = "btnPDFFile";
            this.btnPDFFile.Size = new System.Drawing.Size(75, 23);
            this.btnPDFFile.TabIndex = 20;
            this.btnPDFFile.Text = "Soubor";
            this.btnPDFFile.UseVisualStyleBackColor = true;
            this.btnPDFFile.Click += new System.EventHandler(this.btnPDFFile_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Vyber PDF:";
            // 
            // tp_config
            // 
            this.tp_config.Controls.Add(this.chb_delete_source_file);
            this.tp_config.Controls.Add(this.lbl_limit_log_size);
            this.tp_config.Controls.Add(this.label2);
            this.tp_config.Controls.Add(this.txt_pdf_splitter);
            this.tp_config.Controls.Add(this.chb_pdf_split);
            this.tp_config.Controls.Add(this.chb_directory_only);
            this.tp_config.Controls.Add(this.label1);
            this.tp_config.Controls.Add(this.btn_output_dialog);
            this.tp_config.Controls.Add(this.btn_input_dialog);
            this.tp_config.Controls.Add(this.txt_unique_identification);
            this.tp_config.Controls.Add(this.lbl_unique_identification);
            this.tp_config.Controls.Add(this.txt_limit_log_size);
            this.tp_config.Controls.Add(this.txt_time_interval);
            this.tp_config.Controls.Add(this.lbl_time_interval);
            this.tp_config.Controls.Add(this.txt_out_folder);
            this.tp_config.Controls.Add(this.txt_in_folder);
            this.tp_config.Controls.Add(this.lbl_out_folder);
            this.tp_config.Controls.Add(this.lbl_in_folder);
            this.tp_config.Controls.Add(this.chb_automatic_start_service);
            this.tp_config.Controls.Add(this.btn_config_save);
            this.tp_config.Controls.Add(this.panel1);
            this.tp_config.Location = new System.Drawing.Point(4, 22);
            this.tp_config.Name = "tp_config";
            this.tp_config.Padding = new System.Windows.Forms.Padding(3);
            this.tp_config.Size = new System.Drawing.Size(362, 365);
            this.tp_config.TabIndex = 0;
            this.tp_config.Text = "Konfigurace";
            this.tp_config.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 158);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 29);
            this.label2.TabIndex = 149;
            this.label2.Text = "Oddělovač PDF:           ( samostatná stránka)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_pdf_splitter
            // 
            this.txt_pdf_splitter.Location = new System.Drawing.Point(136, 158);
            this.txt_pdf_splitter.Name = "txt_pdf_splitter";
            this.txt_pdf_splitter.Size = new System.Drawing.Size(223, 20);
            this.txt_pdf_splitter.TabIndex = 83;
            this.txt_pdf_splitter.TextChanged += new System.EventHandler(this.txt_pdf_splitter_TextChanged);
            // 
            // chb_pdf_split
            // 
            this.chb_pdf_split.AutoSize = true;
            this.chb_pdf_split.Location = new System.Drawing.Point(248, 190);
            this.chb_pdf_split.Name = "chb_pdf_split";
            this.chb_pdf_split.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chb_pdf_split.Size = new System.Drawing.Size(111, 17);
            this.chb_pdf_split.TabIndex = 95;
            this.chb_pdf_split.Text = "Dělit PDF soubory";
            this.chb_pdf_split.UseVisualStyleBackColor = true;
            this.chb_pdf_split.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // chb_directory_only
            // 
            this.chb_directory_only.Location = new System.Drawing.Point(134, 83);
            this.chb_directory_only.Name = "chb_directory_only";
            this.chb_directory_only.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chb_directory_only.Size = new System.Drawing.Size(173, 17);
            this.chb_directory_only.TabIndex = 50;
            this.chb_directory_only.Text = "Pracovat jen s podadresáři 1:1";
            this.chb_directory_only.UseVisualStyleBackColor = true;
            this.chb_directory_only.CheckedChanged += new System.EventHandler(this.chb_directory_only_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(216, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 27);
            this.label1.TabIndex = 146;
            this.label1.Text = "\";\" - Oddělovač";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_output_dialog
            // 
            this.btn_output_dialog.Location = new System.Drawing.Point(293, 58);
            this.btn_output_dialog.Name = "btn_output_dialog";
            this.btn_output_dialog.Size = new System.Drawing.Size(64, 23);
            this.btn_output_dialog.TabIndex = 45;
            this.btn_output_dialog.Text = "Vyber";
            this.btn_output_dialog.UseVisualStyleBackColor = true;
            this.btn_output_dialog.Click += new System.EventHandler(this.btn_output_dialog_Click);
            // 
            // btn_input_dialog
            // 
            this.btn_input_dialog.Location = new System.Drawing.Point(293, 33);
            this.btn_input_dialog.Name = "btn_input_dialog";
            this.btn_input_dialog.Size = new System.Drawing.Size(64, 23);
            this.btn_input_dialog.TabIndex = 35;
            this.btn_input_dialog.Text = "Vyber";
            this.btn_input_dialog.UseVisualStyleBackColor = true;
            this.btn_input_dialog.Click += new System.EventHandler(this.btn_input_dialog_Click);
            // 
            // txt_unique_identification
            // 
            this.txt_unique_identification.BackColor = System.Drawing.Color.Orange;
            this.txt_unique_identification.Location = new System.Drawing.Point(134, 10);
            this.txt_unique_identification.Name = "txt_unique_identification";
            this.txt_unique_identification.Size = new System.Drawing.Size(223, 20);
            this.txt_unique_identification.TabIndex = 20;
            this.txt_unique_identification.TextChanged += new System.EventHandler(this.txt_unique_identification_TextChanged);
            // 
            // lbl_unique_identification
            // 
            this.lbl_unique_identification.Location = new System.Drawing.Point(8, 13);
            this.lbl_unique_identification.Name = "lbl_unique_identification";
            this.lbl_unique_identification.Size = new System.Drawing.Size(120, 19);
            this.lbl_unique_identification.TabIndex = 21;
            this.lbl_unique_identification.Text = "Unikátní Identifikátory (;):";
            // 
            // txt_limit_log_size
            // 
            this.txt_limit_log_size.Location = new System.Drawing.Point(136, 132);
            this.txt_limit_log_size.Name = "txt_limit_log_size";
            this.txt_limit_log_size.Size = new System.Drawing.Size(223, 20);
            this.txt_limit_log_size.TabIndex = 80;
            this.txt_limit_log_size.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // lbl_limit_log_size
            // 
            this.lbl_limit_log_size.Location = new System.Drawing.Point(9, 128);
            this.lbl_limit_log_size.Name = "lbl_limit_log_size";
            this.lbl_limit_log_size.Size = new System.Drawing.Size(100, 30);
            this.lbl_limit_log_size.TabIndex = 18;
            this.lbl_limit_log_size.Text = "Limit Log Souboru (MB):";
            this.lbl_limit_log_size.Click += new System.EventHandler(this.label3_Click);
            // 
            // txt_time_interval
            // 
            this.txt_time_interval.Location = new System.Drawing.Point(136, 102);
            this.txt_time_interval.Name = "txt_time_interval";
            this.txt_time_interval.Size = new System.Drawing.Size(223, 20);
            this.txt_time_interval.TabIndex = 70;
            this.txt_time_interval.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // lbl_time_interval
            // 
            this.lbl_time_interval.Location = new System.Drawing.Point(9, 98);
            this.lbl_time_interval.Name = "lbl_time_interval";
            this.lbl_time_interval.Size = new System.Drawing.Size(100, 30);
            this.lbl_time_interval.TabIndex = 16;
            this.lbl_time_interval.Text = "Časový Invernal pro AUTO (sec):";
            this.lbl_time_interval.Click += new System.EventHandler(this.label2_Click);
            // 
            // chb_delete_source_file
            // 
            this.chb_delete_source_file.AutoSize = true;
            this.chb_delete_source_file.Location = new System.Drawing.Point(102, 190);
            this.chb_delete_source_file.Name = "chb_delete_source_file";
            this.chb_delete_source_file.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chb_delete_source_file.Size = new System.Drawing.Size(140, 17);
            this.chb_delete_source_file.TabIndex = 85;
            this.chb_delete_source_file.Text = "Smazat zdrojový Soubor";
            this.chb_delete_source_file.UseVisualStyleBackColor = true;
            this.chb_delete_source_file.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // txt_out_folder
            // 
            this.txt_out_folder.Location = new System.Drawing.Point(134, 60);
            this.txt_out_folder.Name = "txt_out_folder";
            this.txt_out_folder.Size = new System.Drawing.Size(153, 20);
            this.txt_out_folder.TabIndex = 40;
            this.txt_out_folder.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txt_in_folder
            // 
            this.txt_in_folder.Location = new System.Drawing.Point(134, 35);
            this.txt_in_folder.Name = "txt_in_folder";
            this.txt_in_folder.Size = new System.Drawing.Size(153, 20);
            this.txt_in_folder.TabIndex = 30;
            this.txt_in_folder.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // lbl_out_folder
            // 
            this.lbl_out_folder.Location = new System.Drawing.Point(8, 63);
            this.lbl_out_folder.Name = "lbl_out_folder";
            this.lbl_out_folder.Size = new System.Drawing.Size(100, 17);
            this.lbl_out_folder.TabIndex = 9;
            this.lbl_out_folder.Text = "Výstupní Složka:";
            this.lbl_out_folder.Click += new System.EventHandler(this.lbl_out_folder_Click);
            // 
            // lbl_in_folder
            // 
            this.lbl_in_folder.Location = new System.Drawing.Point(8, 39);
            this.lbl_in_folder.Name = "lbl_in_folder";
            this.lbl_in_folder.Size = new System.Drawing.Size(100, 19);
            this.lbl_in_folder.TabIndex = 8;
            this.lbl_in_folder.Text = "Vstupní Složka:";
            this.lbl_in_folder.Click += new System.EventHandler(this.label1_Click);
            // 
            // chb_automatic_start_service
            // 
            this.chb_automatic_start_service.AutoSize = true;
            this.chb_automatic_start_service.Location = new System.Drawing.Point(7, 213);
            this.chb_automatic_start_service.Name = "chb_automatic_start_service";
            this.chb_automatic_start_service.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chb_automatic_start_service.Size = new System.Drawing.Size(235, 17);
            this.chb_automatic_start_service.TabIndex = 90;
            this.chb_automatic_start_service.Text = "Spustit Službu BARCODE OCR Automaticky";
            this.chb_automatic_start_service.UseVisualStyleBackColor = true;
            this.chb_automatic_start_service.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_config_save
            // 
            this.btn_config_save.Location = new System.Drawing.Point(284, 222);
            this.btn_config_save.Name = "btn_config_save";
            this.btn_config_save.Size = new System.Drawing.Size(75, 40);
            this.btn_config_save.TabIndex = 100;
            this.btn_config_save.Text = "Uložit Nastavení";
            this.btn_config_save.UseVisualStyleBackColor = true;
            this.btn_config_save.Click += new System.EventHandler(this.btn_config_save_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.btn_uninstall_service);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btn_install_service);
            this.panel1.Controls.Add(this.btn_start_service);
            this.panel1.Controls.Add(this.btn_open_log_file);
            this.panel1.Controls.Add(this.btn_stop_service);
            this.panel1.Location = new System.Drawing.Point(0, 270);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 95);
            this.panel1.TabIndex = 143;
            // 
            // btn_uninstall_service
            // 
            this.btn_uninstall_service.Enabled = false;
            this.btn_uninstall_service.Location = new System.Drawing.Point(4, 68);
            this.btn_uninstall_service.Name = "btn_uninstall_service";
            this.btn_uninstall_service.Size = new System.Drawing.Size(104, 23);
            this.btn_uninstall_service.TabIndex = 120;
            this.btn_uninstall_service.Text = "ODINSTALOVAT";
            this.btn_uninstall_service.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(7, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "SLUŽBA";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_install_service
            // 
            this.btn_install_service.Enabled = false;
            this.btn_install_service.Location = new System.Drawing.Point(4, 39);
            this.btn_install_service.Name = "btn_install_service";
            this.btn_install_service.Size = new System.Drawing.Size(104, 23);
            this.btn_install_service.TabIndex = 110;
            this.btn_install_service.Text = "INSTALOVAT";
            this.btn_install_service.UseVisualStyleBackColor = true;
            // 
            // btn_start_service
            // 
            this.btn_start_service.Location = new System.Drawing.Point(111, 10);
            this.btn_start_service.Name = "btn_start_service";
            this.btn_start_service.Size = new System.Drawing.Size(75, 23);
            this.btn_start_service.TabIndex = 130;
            this.btn_start_service.Text = "START";
            this.btn_start_service.UseVisualStyleBackColor = true;
            this.btn_start_service.Click += new System.EventHandler(this.btn_start_service_Click);
            // 
            // btn_open_log_file
            // 
            this.btn_open_log_file.Location = new System.Drawing.Point(111, 68);
            this.btn_open_log_file.Name = "btn_open_log_file";
            this.btn_open_log_file.Size = new System.Drawing.Size(75, 23);
            this.btn_open_log_file.TabIndex = 150;
            this.btn_open_log_file.Text = "Otevřít Log";
            this.btn_open_log_file.UseVisualStyleBackColor = true;
            this.btn_open_log_file.Click += new System.EventHandler(this.btn_open_log_file_Click);
            // 
            // btn_stop_service
            // 
            this.btn_stop_service.Enabled = false;
            this.btn_stop_service.Location = new System.Drawing.Point(111, 39);
            this.btn_stop_service.Name = "btn_stop_service";
            this.btn_stop_service.Size = new System.Drawing.Size(75, 23);
            this.btn_stop_service.TabIndex = 140;
            this.btn_stop_service.Text = "STOP";
            this.btn_stop_service.UseVisualStyleBackColor = true;
            // 
            // tb_control1
            // 
            this.tb_control1.Controls.Add(this.tb_filescan);
            this.tb_control1.Controls.Add(this.tp_config);
            this.tb_control1.Location = new System.Drawing.Point(1, 1);
            this.tb_control1.Name = "tb_control1";
            this.tb_control1.SelectedIndex = 0;
            this.tb_control1.Size = new System.Drawing.Size(370, 391);
            this.tb_control1.TabIndex = 0;
            // 
            // ni_traymenu
            // 
            this.ni_traymenu.ContextMenuStrip = this.mi_ocrdriver;
            this.ni_traymenu.Icon = ((System.Drawing.Icon)(resources.GetObject("ni_traymenu.Icon")));
            this.ni_traymenu.Text = "Služba BARCODE OCR";
            this.ni_traymenu.Visible = true;
            this.ni_traymenu.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ni_traymenu_MouseDoubleClick);
            // 
            // mi_ocrdriver
            // 
            this.mi_ocrdriver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstb_start_ocr_process,
            this.tstb_stop_ocr_process,
            this.tstb_config_form,
            this.tstb_close_app});
            this.mi_ocrdriver.Name = "Konfigurace";
            this.mi_ocrdriver.Size = new System.Drawing.Size(219, 92);
            this.mi_ocrdriver.TabStop = true;
            this.mi_ocrdriver.Opening += new System.ComponentModel.CancelEventHandler(this.mi_ocrdriver_Opening);
            // 
            // tstb_start_ocr_process
            // 
            this.tstb_start_ocr_process.DoubleClickEnabled = true;
            this.tstb_start_ocr_process.Name = "tstb_start_ocr_process";
            this.tstb_start_ocr_process.Size = new System.Drawing.Size(218, 22);
            this.tstb_start_ocr_process.Text = "Start Služby BARCODE OCR";
            this.tstb_start_ocr_process.Click += new System.EventHandler(this.tstb_start_ocr_process_Click);
            // 
            // tstb_stop_ocr_process
            // 
            this.tstb_stop_ocr_process.DoubleClickEnabled = true;
            this.tstb_stop_ocr_process.Enabled = false;
            this.tstb_stop_ocr_process.Name = "tstb_stop_ocr_process";
            this.tstb_stop_ocr_process.Size = new System.Drawing.Size(218, 22);
            this.tstb_stop_ocr_process.Text = "Stop Služby BARCODE OCR";
            // 
            // tstb_config_form
            // 
            this.tstb_config_form.Name = "tstb_config_form";
            this.tstb_config_form.Size = new System.Drawing.Size(218, 22);
            this.tstb_config_form.Text = "Zobrazit OCRBAR";
            this.tstb_config_form.Click += new System.EventHandler(this.tstb_config_form_Click);
            // 
            // tstb_close_app
            // 
            this.tstb_close_app.DoubleClickEnabled = true;
            this.tstb_close_app.Name = "tstb_close_app";
            this.tstb_close_app.Size = new System.Drawing.Size(218, 22);
            this.tstb_close_app.Text = "Ukončit Aplikaci";
            this.tstb_close_app.Click += new System.EventHandler(this.tstb_close_app_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 393);
            this.Controls.Add(this.tb_control1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BARCODE OCR";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tb_filescan.ResumeLayout(false);
            this.tb_filescan.PerformLayout();
            this.tp_config.ResumeLayout(false);
            this.tp_config.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tb_control1.ResumeLayout(false);
            this.mi_ocrdriver.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tb_filescan;
        private System.Windows.Forms.Button btnQRPDFScan;
        private System.Windows.Forms.Label lblWaiting;
        private System.Windows.Forms.Button btnPDFClear;
        private System.Windows.Forms.RichTextBox rtbpdfData;
        private System.Windows.Forms.TextBox tbPDFname;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnFileScan;
        private System.Windows.Forms.Button btnPDFFile;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tp_config;
        private System.Windows.Forms.TabControl tb_control1;
        private System.Windows.Forms.Button btn_config_save;
        private System.Windows.Forms.NotifyIcon ni_traymenu;
        private System.Windows.Forms.ContextMenuStrip mi_ocrdriver;
        private System.Windows.Forms.ToolStripMenuItem tstb_start_ocr_process;
        private System.Windows.Forms.ToolStripMenuItem tstb_stop_ocr_process;
        private System.Windows.Forms.ToolStripMenuItem tstb_config_form;
        private System.Windows.Forms.ToolStripMenuItem tstb_close_app;
        private System.Windows.Forms.Button btn_stop_service;
        private System.Windows.Forms.Button btn_start_service;
        private System.Windows.Forms.CheckBox chb_automatic_start_service;
        private System.Windows.Forms.Label lbl_in_folder;
        private System.Windows.Forms.TextBox txt_out_folder;
        private System.Windows.Forms.TextBox txt_in_folder;
        private System.Windows.Forms.Label lbl_out_folder;
        private System.Windows.Forms.TextBox txt_limit_log_size;
        private System.Windows.Forms.Label lbl_limit_log_size;
        private System.Windows.Forms.TextBox txt_time_interval;
        private System.Windows.Forms.Label lbl_time_interval;
        private System.Windows.Forms.CheckBox chb_delete_source_file;
        private System.Windows.Forms.TextBox txt_unique_identification;
        private System.Windows.Forms.Label lbl_unique_identification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_open_log_file;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_uninstall_service;
        private System.Windows.Forms.Button btn_install_service;
        private System.Windows.Forms.Button btn_input_dialog;
        private System.Windows.Forms.FolderBrowserDialog fb_input_dialog;
        private System.Windows.Forms.Button btn_output_dialog;
        private System.Windows.Forms.FolderBrowserDialog fb_output_dialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chb_directory_only;
        private System.Windows.Forms.CheckBox chb_pdf_split;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_pdf_splitter;
    }
}

