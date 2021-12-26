using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.IO;
using PdfToImage;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Barcode
{




    public partial class Form1 : Form
    {
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        string pdfPath = string.Empty;
        bool form_status = false;
        string config_file = "\\config.ini";
        string template_xml_file = "\\template.xml";
        string xml_source = "";
        string log_file = "\\result.log";
        string proccess_directory = "\\TEMP";
        DateTime load_time = DateTime.Now;
        bool automat_proccess = false;
        string processed_filename_full_path = "";
        string processed_filename = "";
        string[] unique_identification;
        string[] unique_endpage;
        string Sourcedirectory = "";
        List<String> delete_File_list = new List<String>();
        //string.Format("{0:HH:mm:ss tt}", DateTime.Now);

        public Form1()
        {
            InitializeComponent();
        }



        


        // <summary>
        /// Show the reults of the barcode scan in a message box.
        /// </summary>
        /// <param name="dtStart">Start time of scan</param>
        /// <param name="iScans">Number of scans done</param>
        /// <param name="barcodes">List of barcodes found</param>
        private void ShowResults(DateTime dtStart, int iScans, ref System.Collections.ArrayList barcodes)
        {
            string Message = string.Empty;
            if (barcodes.Count > 0)
            {
                Message += "Found barcodes:\n";

                foreach (object bc in barcodes)
                {
                    Message += bc + "\n";
                }
            }
            else
            {
                Message += "Failed to find a barcode.";
            }
            //rtbResult.Text = Message;
            //MessageBox.Show(Message);
            this.Refresh();
        }                    





        private void btnSave_Click(object sender, EventArgs e)
        {

        }




        protected override void OnVisibleChanged(EventArgs e)
        {
            //base.OnVisibleChanged(e);
            //MessageBox.Show(Convert.ToString(form_status));

            if (form_status == false) {
                tstb_config_form.Text = "Zobrazit OCRBAR";
                this.Visible = false;
            }
            else {
                tstb_config_form.Text = "Skrýt OCRBAR";
                this.Visible = true;
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            //cboEncoding.SelectedIndex = 2;
            //cboVersion.SelectedIndex = 6;
            //cboCorrectionLevel.SelectedIndex = 1;

            Bitmap temp = new Bitmap(1, 1);
            temp.SetPixel(0, 0, this.BackColor);
            //barcode.Image = (System.Drawing.Image)temp;

            string line = "";
            try    
            {
                global_functions.app_functions.fn_create_directory(Application.StartupPath + proccess_directory);
            line = global_functions.app_functions.fn_load_config_file(Application.StartupPath + config_file);
            string[] parts = line.Split('#');
            for (int i = 0; i < parts.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        txt_unique_identification.Text = parts[0];
                        break;
                    case 1:
                        txt_in_folder.Text = parts[1];
                        break;
                    case 2:
                        txt_out_folder.Text = parts[2];
                        break;
                    case 3:
                        if (parts[3] == "ON") { chb_delete_source_file.Checked = true; }
                        break;
                    case 4:
                        txt_time_interval.Text = parts[4];
                        break;
                    case 5:
                        txt_limit_log_size.Text = parts[5];
                        break;
                    case 6:
                        if (parts[6] == "ON") { chb_automatic_start_service.Checked = true; }
                        break;
                    case 7:
                        if (parts[7] == "ON") { chb_directory_only.Checked = true; }
                        break;
                    case 8:
                        txt_pdf_splitter.Text = parts[8];
                        break;
                    case 9:
                        if (parts[9] == "ON") { chb_pdf_split.Checked = true; }
                        break;
                }
            }

            //global_functions.app_functions.fn_copy_directory_files(txt_in_folder.Text, Application.StartupPath + proccess_directory);
            }
                catch
            {
                form_status = true;
                MessageBox.Show("Nastavení Se Nepodařilo Načíst. \n Proveďte konfiguraci.");
            }

            xml_source = global_functions.app_functions.fn_load_file_to_variable(Application.StartupPath + template_xml_file);
            if (chb_automatic_start_service.Checked == true) { fn_proccess_barcode_files(Application.StartupPath + proccess_directory, txt_out_folder.Text); }
        }





        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (MessageBox.Show("Chcete skutečně aplikaci ukončit?", "BARCODE OCR",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                base.OnFormClosing(e);
            }
            else {
                e.Cancel = true;
            }

          
        } 



        /// <summary>
        /// Open EAN-13 image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// EAN-13 Decode Scan button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// EAN-13 Decode Clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// EAN -13 code Encode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
 
  


        private void btnRandom_Click(object sender, EventArgs e)
        {
            //if (RandomEAN13Code() != string.Empty)
            //    txtData.Text = RandomEAN13Code();           
        }




        private string RandomEAN13Code()
        {
            Random Rnd = new Random();
            string number = string.Empty;
            for (int i = 1; i < 14; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }

            if (CheckCode(number))
                return number;
            else
                RandomEAN13Code();
            
            return number;
        }





        private bool CheckCode(string code)
        {
            if (code == null || code.Length != 13)
                return false;

            int res;
            foreach (char c in code)
                if (!int.TryParse(c.ToString(), out res))
                    return false;

            char check = (char)('0' + CalculateChecksum(code.Substring(0, 12)));
            return code[12] == check;
        }





        public static int CalculateChecksum(string code)
        {
            if (code == null || code.Length != 12)
                throw new ArgumentException("Code length should be 12, i.e. excluding the checksum digit");

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int v;
                if (!int.TryParse(code[i].ToString(), out v))
                    throw new ArgumentException("Invalid character encountered in specified code.");
                sum += (i % 2 == 0 ? v : v * 3);
            }
            int check = 10 - (sum % 10);
            return check % 10;
        }




        public void btnPDFFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PDF File|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                tbPDFname.Text = fileName;
                PDFConvert converter = new PDFConvert();
                //Setup the converter
                converter.RenderingThreads = -1;
                converter.TextAlphaBit = -1;
                converter.TextAlphaBit = -1;
                converter.OutputToMultipleFile = true;
                converter.FirstPageToConvert = -1;
                converter.LastPageToConvert = -1;
                converter.FitPage = false;
                converter.JPEGQuality = 100;
                converter.OutputFormat = "png16m";
                converter.ResolutionX = 500; //500x390 good 500x450
                converter.ResolutionY = 390;
                System.IO.FileInfo input = new FileInfo(fileName);
                string directoryPath = System.IO.Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(input.Name);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                pdfPath = directoryPath;
                string output = string.Format("{0}\\{1}{2}", directoryPath, input.Name, ".png");                
                converter.Convert(input.FullName, output);
            }
        }




        private void ScanEANPdf(string imagePath)
        {
            System.Collections.ArrayList barcodes = new System.Collections.ArrayList();
            DateTime dtStart = DateTime.Now;
            int iScans = 100;
            Cursor.Current = Cursors.WaitCursor; 
            foreach (var item in Directory.GetFiles(imagePath))
            {

                if (Path.GetExtension(item).ToLower() == ".png")
                {
                    Bitmap b = new Bitmap(item);
                    BarcodeImaging.FullScanPage(ref barcodes, b, iScans);
                }
            }
            // Show the results in a message box
            ShowPDFResults(dtStart, iScans, ref barcodes);
            Cursor.Current = Cursors.Default;
        }




        private void ScanQRPdf(string imagePath)
        {
            System.Collections.ArrayList barcodes = new System.Collections.ArrayList();
            DateTime dtStart = DateTime.Now;
            int iScans = 100;
            Cursor.Current = Cursors.WaitCursor; 
            foreach (var item in Directory.GetFiles(imagePath))
            {
                if (Path.GetExtension(item).ToLower() == ".png")
                {
                    Bitmap b = new Bitmap(item);
                    try
                    {
                        QRCodeDecoder decoder = new QRCodeDecoder();
                        //QRCodeDecoder.Canvas = new ConsoleCanvas();
                        String decodedString = decoder.decode(new QRCodeBitmapImage(b));
                        
                        if (automat_proccess == true){
                            if (decodedString.Contains(txt_unique_identification.Text))
                            {
                                MessageBox.Show(decodedString);
                            }
                        }
                        else
                        {
                            rtbpdfData.Text += decodedString + "\n";
                        }

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Scan PDF result
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="iScans"></param>
        /// <param name="barcodes"></param>
        /// 

        private void ShowPDFResults(DateTime dtStart, int iScans, ref System.Collections.ArrayList barcodes)
        {
            string Message = string.Empty;
            string scanned_barcode = "";
            string scanned_barcode_result = "";
            bool fouded_correct_code = false;
            
            if (barcodes.Count > 0)
            {
                if (automat_proccess == false) {
                    Message += "Nalezené Čárové Kódy:\n"; 
                }

                foreach (object bc in barcodes)
                {
                    scanned_barcode = "";
                    scanned_barcode_result = "";
                    scanned_barcode += bc;
                    scanned_barcode = new string(scanned_barcode.Where(c => c <= sbyte.MaxValue).ToArray());
                    scanned_barcode_result += bc;
                    scanned_barcode_result = new string(scanned_barcode_result.Where(c => c <= sbyte.MaxValue).ToArray());

                    if (automat_proccess == true)
                    {

                        //if (scanned_barcode.Contains(txt_unique_identification.Text))




                        if (unique_identification.Any(scanned_barcode.Contains))
                        {
                            fouded_correct_code = true;
                            FileInfo file_extension = new FileInfo(processed_filename_full_path);

                            if (chb_directory_only.Checked == false){
                                scanned_barcode = global_functions.app_functions.fn_check_filename_part_in_directory(txt_out_folder.Text, scanned_barcode);
                            } else {
                                scanned_barcode = global_functions.app_functions.fn_check_filename_part_in_directory(Path.Combine(txt_out_folder.Text, Sourcedirectory), scanned_barcode);
                            }

                            
                            string final_xml = xml_source.Replace("xxXXXXXXxx", scanned_barcode_result);
                            final_xml = final_xml.Replace("yyYYYYYYyy", "Automaticky vlozeny a prirazeny dokument");

                            if (chb_directory_only.Checked == false) 
                            { 
                                global_functions.app_functions.fn_copy_file(processed_filename_full_path, txt_out_folder.Text + scanned_barcode + file_extension.Extension);
                                global_functions.app_functions.fn_save_variable_as_file(txt_out_folder.Text + scanned_barcode + file_extension.Extension +  ".dms_info.xml", final_xml);
                            }
                            else
                            {
                                // subdirectory zde
                                global_functions.app_functions.fn_copy_file(processed_filename_full_path, Path.Combine(txt_out_folder.Text, Sourcedirectory) + "\\" + scanned_barcode + file_extension.Extension);
                                global_functions.app_functions.fn_save_variable_as_file(Path.Combine(txt_out_folder.Text, Sourcedirectory) + "\\" + scanned_barcode + file_extension.Extension + ".dms_info.xml", final_xml);
                            }

                            global_functions.app_functions.fn_save_log_file(Application.StartupPath + log_file, "Inserted file: " + scanned_barcode + ";"+ file_extension.Extension + ";" + DateTime.Now);

                            //MessageBox.Show(processed_filename_full_path+ txt_out_folder.Text + scanned_barcode + file_extension.Extension);
                        }

                    }
                    else
                    {



                        Message += bc + "\n";
                    }

                }

                if (fouded_correct_code == false && automat_proccess == true)
                {
                        string final_xml = xml_source.Replace("xxXXXXXXxx", processed_filename);
                        final_xml = final_xml.Replace("yyYYYYYYyy", "Automaticky vlozeny Chybny dokument");

                        if (chb_directory_only.Checked == false)
                        {
                            global_functions.app_functions.fn_copy_file(processed_filename_full_path, txt_out_folder.Text + processed_filename);
                            global_functions.app_functions.fn_save_variable_as_file(txt_out_folder.Text + processed_filename + ".dms_info.xml", final_xml);
                        }
                        else
                        {
                            // subdirectory zde
                            global_functions.app_functions.fn_copy_file(processed_filename_full_path, Path.Combine(txt_out_folder.Text, Sourcedirectory) + "\\" + processed_filename);
                            global_functions.app_functions.fn_save_variable_as_file(Path.Combine(txt_out_folder.Text, Sourcedirectory) + "\\" + processed_filename + ".dms_info.xml", final_xml);
                        }

                        global_functions.app_functions.fn_save_log_file(Application.StartupPath + log_file, "Inserted BAD file: " +";"+ processed_filename + ";" + DateTime.Now);
                }


                if (automat_proccess == true)
                {
                    if (chb_delete_source_file.Checked == true)
                    {
                        if (chb_directory_only.Checked == false)
                        {
                            global_functions.app_functions.fn_delete_file(txt_in_folder.Text + processed_filename);
                        }
                        else
                        {
                            // subdirectory zde
                            global_functions.app_functions.fn_delete_file(Path.Combine(txt_in_folder.Text, Sourcedirectory) + "\\" + processed_filename);
                        }
                    }
                }

            }
            else
            {
                if (automat_proccess == true)
                {
                    string final_xml = xml_source.Replace("xxXXXXXXxx", processed_filename);
                    final_xml = final_xml.Replace("yyYYYYYYyy", "Automaticky vlozeny Chybny dokument");

                    if (chb_directory_only.Checked == false)
                    {
                        global_functions.app_functions.fn_copy_file(processed_filename_full_path, txt_out_folder.Text + processed_filename);
                        global_functions.app_functions.fn_save_variable_as_file(txt_out_folder.Text + processed_filename + ".dms_info.xml", final_xml);
                    }
                    else
                    {
                        // subdirecory zde
                        global_functions.app_functions.fn_copy_file(processed_filename_full_path, Path.Combine(txt_out_folder.Text, Sourcedirectory) + "\\" + processed_filename);
                        global_functions.app_functions.fn_save_variable_as_file(Path.Combine(txt_out_folder.Text, Sourcedirectory) + "\\" + processed_filename + ".dms_info.xml", final_xml);
                    }

                    global_functions.app_functions.fn_save_log_file(Application.StartupPath + log_file, "Inserted BAD file: " +";"+ processed_filename + ";" + DateTime.Now);

                        if (chb_delete_source_file.Checked == true)
                        {
                            if (chb_directory_only.Checked == false)
                            {
                                global_functions.app_functions.fn_delete_file(txt_in_folder.Text + processed_filename);
                            }
                            else
                            {
                                // subdirecory zde
                                global_functions.app_functions.fn_delete_file(Path.Combine(txt_in_folder.Text, Sourcedirectory) + "\\" + processed_filename);
                            }
                        }
                }
                else
                {
                    Message += "Žádné Čárové Kódy Nenalezeny.";
                }
            }


            if (automat_proccess == false)
            {
                rtbpdfData.Text = Message;
                lblWaiting.ForeColor = System.Drawing.Color.Black;
                lblWaiting.Text = "Skenování Dokončeno";
            }
            this.Refresh();

        }      
       



        private void btnFileScan_Click(object sender, EventArgs e)
        {
            rtbpdfData.Text = string.Empty;
            //lblWaiting.ForeColor = System.Drawing.Color.Red;
            lblWaiting.Text = "Čekejte Prosím";
            if(pdfPath != string.Empty)
                ScanEANPdf(pdfPath);
        }




        private void btnPDFClear_Click(object sender, EventArgs e)
        {
            tbPDFname.Text = string.Empty;
            rtbpdfData.Text = string.Empty;
        }




        private void btnQRPDFScan_Click(object sender, EventArgs e)
        {
            rtbpdfData.Text = string.Empty;
            if (pdfPath != string.Empty)
                ScanQRPdf(pdfPath);
        }


        
        private void mi_ocrdriver_Opening(object sender, CancelEventArgs e)
        {

        }




        private void tstb_config_form_Click(object sender, EventArgs e)
        {
            //base.OnVisibleChanged(e);
            if (this.Visible == true) {
                form_status = false;
                base.OnVisibleChanged(e);
                //this.Hide();
                this.Visible = false;
            }
            else {
                form_status = true;
                base.OnVisibleChanged(e);
                //this.Show(); 
                this.Visible = true;
            }
        }







        private void tstb_close_app_Click(object sender, EventArgs e)
        {
                     System.Windows.Forms.Application.Exit();
        }

        private void btn_config_save_Click(object sender, EventArgs e)
        {
            string temp ;
            if (chb_delete_source_file.Checked == true ){
                temp = "ON";
            } else {temp = "OFF";}
            string param = txt_unique_identification.Text + "#" + txt_in_folder.Text + "#" + txt_out_folder.Text +"#"+ temp + "#" + txt_time_interval.Text + "#" +txt_limit_log_size.Text + "#"; 
            if (chb_automatic_start_service.Checked == true ){
                temp = "ON";
            } else {temp = "OFF";}
            param += temp+ "#";
            if (chb_directory_only.Checked == true)
            {
                temp = "ON";
            } else {temp = "OFF";}

            param += temp + "#" + txt_pdf_splitter.Text + "#";
            if (chb_pdf_split.Checked == true)
            {
                temp = "ON";
            }
            else { temp = "OFF"; }
            param += temp + "#";
            
            global_functions.app_functions.fn_save_config_file(Application.StartupPath + config_file, param);
        }


        private void label1_Click(object sender, EventArgs e)
        {
        
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (chb_automatic_start_service.Checked == true)
                {
                    txt_time_interval.BackColor = System.Drawing.Color.Orange;
                }
                else
                {
                    txt_time_interval.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void lbl_out_folder_Click(object sender, EventArgs e)
        {
        
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void txt_pdf_splitter_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        
        }

        private void label2_Click(object sender, EventArgs e)
        {
        
        }

        private void label3_Click(object sender, EventArgs e)
        {
        
        }

        private void txt_filename_format_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chb_pdf_split.Checked == true)
            {
                txt_pdf_splitter.BackColor = System.Drawing.Color.Orange;
            }
            else
            {
                txt_pdf_splitter.BackColor = System.Drawing.Color.White;
            }
        }


        private void btn_input_dialog_Click(object sender, EventArgs e)
        {
            fb_input_dialog.Description = "Vyberte Zdrojovou Složku";
            try
                {
                    if (fb_input_dialog.ShowDialog() == DialogResult.OK) 
                    {
                        txt_in_folder.Text = string.Format(fb_input_dialog.SelectedPath + "\\");
                    }
                }
            catch{
        
            }
        }

        private void btn_output_dialog_Click(object sender, EventArgs e)
        {
            fb_output_dialog.Description = "Vyberte Cílovou Složku";
            try
                {
                    if (fb_output_dialog.ShowDialog() == DialogResult.OK) 
                    {
                        txt_out_folder.Text = string.Format(fb_output_dialog.SelectedPath + "\\");
                    }
                }
            catch{
        
            }

        }

        private void btn_start_service_Click(object sender, EventArgs e)
        {
            if (automat_proccess == false)
            {
                fn_proccess_barcode_files(Application.StartupPath + proccess_directory, txt_out_folder.Text);
            }
        }


        private void btn_open_log_file_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + log_file);

        }


//----------------------------------------------------------------------------------------------

        private void fn_load_barcode_files(string sourcePath, string SourceFileName)
        {
//            openFileDialog1.Filter = "PDF File|*.pdf|All files (*.*)|*.*";
//            openFileDialog1.FilterIndex = 1;
//           openFileDialog1.RestoreDirectory = true;
//            openFileDialog1.FileName = string.Empty;

//                String fileName = SourceFileName;
                processed_filename_full_path = SourceFileName;
//                tbPDFname.Text = fileName;
                PDFConvert converter = new PDFConvert();
                //Setup the converter
                converter.RenderingThreads = -1;
                converter.TextAlphaBit = -1;
                converter.TextAlphaBit = -1;
                converter.OutputToMultipleFile = true;
                converter.FirstPageToConvert = -1;
                converter.LastPageToConvert = -1;
                converter.FitPage = false;
                converter.JPEGQuality = 100;
                converter.OutputFormat = "png16m"; //png256,png16m
                converter.ResolutionX = 500;  //500x390 good 500x450 for 600dpi  600x500
                converter.ResolutionY = 390;
                System.IO.FileInfo input = new FileInfo(SourceFileName);
                string directoryPath = System.IO.Path.GetDirectoryName(SourceFileName) + "\\" + Path.GetFileNameWithoutExtension(input.Name);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                pdfPath = directoryPath;
                string output = string.Format("{0}\\{1}{2}", directoryPath, input.Name, ".png");
                converter.Convert(input.FullName, output);
            
        }



        private void fn_proccess_barcode_files(string sourcePath, string targetPath)
        {
            string SourceFileName = "";
            FileInfo check_file ;
            //int temp_cycle = 1 ;
            global_functions.app_functions.fn_check_file_size(Application.StartupPath + log_file, txt_limit_log_size.Text);




            if (chb_directory_only.Checked == false)
            {
                //PDF DECOMPOSITION START
                if (chb_pdf_split.Checked == true) 
                {
                    if (global_functions.app_functions.fn_check_directory(txt_in_folder.Text))
                    {
                        string[] files = System.IO.Directory.GetFiles(txt_in_folder.Text);
                        foreach (string s in files)
                        {

                            SourceFileName = System.IO.Path.GetFileName(s);


                            check_file = new FileInfo (Path.Combine(txt_in_folder.Text, SourceFileName));

                            if (global_functions.app_functions.fn_IsFileLocked(check_file) == false)
                            {
                                if (SourceFileName != "Thumbs.db" && global_functions.app_functions.fn_check_used_file(txt_in_folder.Text + SourceFileName)==false)
                                {
                                    if (fn_GetTextFromPDF(Path.Combine(txt_in_folder.Text, SourceFileName), txt_in_folder.Text) == true)
                                    {
                                        //DIRECT DELETE
                                        if (global_functions.app_functions.fn_delete_file(txt_in_folder.Text + SourceFileName) == false)
                                        {
                                            MessageBox.Show("SOUBOR NELZE SMAZAT: " + txt_in_folder.Text + SourceFileName);
                                        }
                                    }
                                }
                            }
                            else { //MessageBox.Show(" file cannot be open"); 
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Některá ze Složek Neexistuje nebo nelze zkopírovat");
                    }

                    // USING ARRAY 
                    //for (temp_cycle = 0; temp_cycle < delete_File_list.Count; temp_cycle++)
                    //{
                    //    if (global_functions.app_functions.fn_delete_file(delete_File_list[temp_cycle]) == false)
                    //    {
                    //        MessageBox.Show("SOUBOR NELZE SMAZAT: " + delete_File_list[temp_cycle]);
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("DELETED");
                    //    }
                    //}
                
                }
                //PDF DECOMPOSITION END 
                global_functions.app_functions.fn_copy_directory_files(txt_in_folder.Text, Application.StartupPath + proccess_directory);
            }
            else
            {
                string[] directories = System.IO.Directory.GetDirectories(txt_in_folder.Text);
                    foreach (string t in directories)
                    {
                        Sourcedirectory = System.IO.Path.GetFileNameWithoutExtension(t);

                        //PDF DECOMPOSITION START
                        if (chb_pdf_split.Checked == true)
                        {
                            if (global_functions.app_functions.fn_check_directory(Path.Combine(txt_in_folder.Text, Sourcedirectory)))
                            {
                                string[] files = System.IO.Directory.GetFiles(Path.Combine(txt_in_folder.Text, Sourcedirectory));
                                foreach (string s in files)
                                {
                                    SourceFileName = System.IO.Path.GetFileName(s);

                                    check_file = new FileInfo(Path.Combine(Path.Combine(txt_in_folder.Text, Sourcedirectory), SourceFileName));
                                    if (global_functions.app_functions.fn_IsFileLocked(check_file) == false)
                                    {

                                        if (SourceFileName != "Thumbs.db" && global_functions.app_functions.fn_check_used_file(Path.Combine(Path.Combine(txt_in_folder.Text, Sourcedirectory), SourceFileName)) == false)
                                        {

                                            if (fn_GetTextFromPDF(Path.Combine(Path.Combine(txt_in_folder.Text, Sourcedirectory), SourceFileName), Path.Combine(txt_in_folder.Text, Sourcedirectory)) == true)
                                            {
                                                if (global_functions.app_functions.fn_delete_file(Path.Combine(Path.Combine(txt_in_folder.Text, Sourcedirectory), SourceFileName)) == false)
                                                {
                                                    MessageBox.Show("SOUBOR NELZE SMAZAT: " + Path.Combine(Path.Combine(txt_in_folder.Text, Sourcedirectory), SourceFileName));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Některá ze Složek Neexistuje nebo nelze zkopírovat");
                            }
                        }
                        //PDF DECOMPOSITION END 


                        global_functions.app_functions.fn_create_directory(Path.Combine(Application.StartupPath + proccess_directory, Sourcedirectory));
                        global_functions.app_functions.fn_copy_directory_files(Path.Combine(txt_in_folder.Text, Sourcedirectory), Path.Combine(Application.StartupPath + proccess_directory, Sourcedirectory));
                    }

            }
            

            //string DestinationFileName = "";
            automat_proccess = true;
            check_proccess_status();

            unique_identification = txt_unique_identification.Text.Split(';');
            unique_identification = unique_identification.Where(s => !String.IsNullOrEmpty(s)).ToArray();

            if (global_functions.app_functions.fn_check_directory(sourcePath))
            {

                if (chb_directory_only.Checked == false) 
                { 

                    string[] files = System.IO.Directory.GetFiles(sourcePath);
                    foreach (string s in files)
                    {

                        SourceFileName = System.IO.Path.GetFileName(s);
                        processed_filename = SourceFileName;
                        if (processed_filename != "Thumbs.db") {

                            fn_load_barcode_files(sourcePath,Path.Combine(sourcePath, SourceFileName));
                            ScanEANPdf(pdfPath);
                        }
                    }
                }
                else
                {
                 //   MessageBox.Show(SourceFileName);                    

                    string[] directories = System.IO.Directory.GetDirectories(sourcePath);
                    foreach (string t in directories)
                    {

                        Sourcedirectory = System.IO.Path.GetFileNameWithoutExtension(t);
                   //     MessageBox.Show(Sourcedirectory);

                        string[] files = System.IO.Directory.GetFiles(Path.Combine(sourcePath, Sourcedirectory));
                        foreach (string s in files)
                        {
                            SourceFileName = System.IO.Path.GetFileName(s);
                            processed_filename = SourceFileName;
                            if (processed_filename != "Thumbs.db")
                            {

                                fn_load_barcode_files(sourcePath, Path.Combine(Path.Combine(sourcePath, Sourcedirectory), SourceFileName));
                                ScanEANPdf(pdfPath);
                            }
                        }

                    }

                }


            }
            else
            {
                MessageBox.Show("Některá ze Složek Neexistuje");
            }

            global_functions.app_functions.fn_clearFolder(Application.StartupPath + proccess_directory);
            load_time = DateTime.Now.AddSeconds(Convert.ToInt32(txt_time_interval.Text));
            automat_proccess = false;
            check_proccess_status();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (load_time <= DateTime.Now && automat_proccess == false && chb_automatic_start_service.Checked == true)
            {
                fn_proccess_barcode_files(Application.StartupPath + proccess_directory, txt_out_folder.Text);
            }
        }


        private void tstb_start_ocr_process_Click(object sender, EventArgs e)
        {
            if (automat_proccess == false)
            {
                fn_proccess_barcode_files(Application.StartupPath + proccess_directory, txt_out_folder.Text);
            }
        }



        private void check_proccess_status()
        {
            if (automat_proccess == true)
            {
                btn_stop_service.Enabled = true;
                btn_start_service.Enabled = false;
                tstb_start_ocr_process.Enabled = false;
                tstb_stop_ocr_process.Enabled = true;

            }
            else
            {
                btn_stop_service.Enabled = false;
                btn_start_service.Enabled = true;
                tstb_start_ocr_process.Enabled = true;
                tstb_stop_ocr_process.Enabled = false;
            }
        }



        private void ni_traymenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //base.OnVisibleChanged(e);
            if (this.Visible == true)
            {
                form_status = false;
                base.OnVisibleChanged(e);
                //this.Hide();
                this.Visible = false;
            }
            else
            {
                form_status = true;
                base.OnVisibleChanged(e);
                //this.Show(); 
                this.Visible = true;
            }

        }




        public void fn_ExtractPage(string sourcePdfPath, string outputPdfPath, int pageNumber, string password = "")
        {
            PdfReader reader = null;
            Document document = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            try
            {
                // Intialize a new PdfReader instance with the contents of the source Pdf file:
                reader = new PdfReader(sourcePdfPath);

                // Capture the correct size and orientation for the page:
                document = new Document(reader.GetPageSizeWithRotation(pageNumber));

                // Initialize an instance of the PdfCopyClass with the source 
                // document and an output file stream:
                pdfCopyProvider = new PdfCopy(document,
                    new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                document.Open();

                // Extract the desired page number:
                importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
                pdfCopyProvider.AddPage(importedPage);
                document.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool fn_GetTextFromPDF(string PdfFileName,string source_split_path)
        {
            PdfReader oReader = new iTextSharp.text.pdf.PdfReader(PdfFileName);
            string sOut = "";
            int from_page = 1;
            int to_page = 1;
            string[] filenamepart;
            bool status = false;
            bool fn_status = false;

            unique_endpage = txt_pdf_splitter.Text.Split(';');
            unique_endpage = unique_endpage.Where(s => !String.IsNullOrEmpty(s)).ToArray();

            for (int for_cycle = 1; for_cycle <= oReader.NumberOfPages; for_cycle++)
            {
                status = false;
                to_page = for_cycle;
                iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                sOut = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(oReader, for_cycle, its); //load full page

                //if (sOut.Contains(txt_pdf_splitter.Text) == true)
                if (unique_endpage.Any(sOut.Contains))
                {
                    fn_status = true;
                    FileInfo file_extension = new FileInfo(Path.Combine(source_split_path, PdfFileName));
                    filenamepart = Convert.ToString(file_extension.Name).Split('.');
                    fn_ExtractPages(Path.Combine(source_split_path, PdfFileName), Path.Combine(source_split_path, filenamepart[0] + "_" + Convert.ToString(for_cycle)) + file_extension.Extension, from_page, (to_page-1));
                    from_page = for_cycle +2;
                    status = true;
                }
            }
            oReader.Close();

            if (status == false && fn_status == true && from_page <= to_page)
            {
                FileInfo file_extension = new FileInfo(Path.Combine(source_split_path, PdfFileName));
                filenamepart = Convert.ToString(file_extension.Name).Split('.');
                fn_ExtractPages(Path.Combine(source_split_path, PdfFileName), Path.Combine(source_split_path, filenamepart[0] + "_" + Convert.ToString(to_page)) + file_extension.Extension, from_page, to_page);
            }

            return fn_status;
        }




        public static void fn_ExtractPages(string sourcePdfPath, string outputPdfPath, int startPage, int endPage)
        {
            if (endPage > 0 && startPage <= endPage)
            {
                PdfReader reader = null;
                Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage = null;

                try
                {
                    // Intialize a new PdfReader instance with the contents of the source Pdf file:
                    reader = new PdfReader(sourcePdfPath);

                    // For simplicity, I am assuming all the pages share the same size
                    // and rotation as the first page:
                    sourceDocument = new Document(reader.GetPageSizeWithRotation(startPage));

                    // Initialize an instance of the PdfCopyClass with the source 
                    // document and an output file stream:
                    pdfCopyProvider = new PdfCopy(sourceDocument,
                        new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                    sourceDocument.Open();
                    // Walk the specified range and add the page copies to the output file:
                    for (int i = startPage; i <= endPage; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }
                    pdfCopyProvider.Close();
                    sourceDocument.Close();
                    reader.Close();
                    importedPage.ClosePath();
                }
                catch (Exception ex)
                {
                      throw ex;
                }
            }
        }



        public static void fn_ExtractFolowPages(string sourcePdfPath, string outputPdfPath, int[] extractThesePages)
        {
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage = null;

            try
            {
                // Intialize a new PdfReader instance with the 
                // contents of the source Pdf file:
                reader = new PdfReader(sourcePdfPath);

                // For simplicity, I am assuming all the pages share the same size
                // and rotation as the first page:
                sourceDocument = new Document(reader.GetPageSizeWithRotation(extractThesePages[0]));

                // Initialize an instance of the PdfCopyClass with the source 
                // document and an output file stream:
                pdfCopyProvider = new PdfCopy(sourceDocument,
                    new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                sourceDocument.Open();

                // Walk the array and add the page copies to the output file:
                foreach (int pageNumber in extractThesePages)
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
                    pdfCopyProvider.AddPage(importedPage);
                }
                sourceDocument.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void chb_directory_only_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (chb_directory_only.Checked == true)
                {
                    txt_in_folder.BackColor = System.Drawing.Color.Orange;
                    txt_out_folder.BackColor = System.Drawing.Color.Orange;
                }
                else
                {
                    txt_in_folder.BackColor = System.Drawing.Color.White;
                    txt_out_folder.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private void txt_unique_identification_TextChanged(object sender, EventArgs e)
        {

        }



        

    }
}



namespace iTextTools
{
    public class PdfExtractorUtility
    {
    }
}
