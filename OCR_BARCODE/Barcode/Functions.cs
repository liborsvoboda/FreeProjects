using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;


namespace global_functions
{
    static class app_functions
    {
  //      #region Variables
//            public bool fn_check_file = false;
        //static
//        #endregion


        public static bool fn_check_directory(string directory)
    {     
        return Directory.Exists(directory);
    }




        public static bool fn_create_directory(string directory)
        {
            if (fn_check_directory(directory) == false)
            {
        try
        {
            Directory.CreateDirectory(directory);
        }
        catch
        {
            return false;
        }
        return true;
            }
    return true;
    }



        public static bool fn_IsFileLocked(FileInfo file)
        {

            try
            {
                string fpath = file.FullName;
                FileStream fs = File.OpenWrite(fpath);
                fs.Close();
                //MessageBox.Show("ok" + file.FullName);
                return false;

            }

            catch (Exception) {
                //MessageBox.Show("chyba" + file.FullName);
                return true; }      
            
            
            //FileStream stream = null;
            //cannot run on UNC path

            //try
            //{
            //    stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            //}
            //catch //(IOException)
            //{
            //    return true;
            //}
            //finally
            //{
            //    if (stream != null)
            //        stream.Close();
            //}

            //return false;
        }



        public static bool fn_check_file(string file)
    {     
        return File.Exists(file);
    }




        public static bool fn_create_file(string file)
    {
        if (File.Exists(file) == false) {
            System.IO.File.Create(file).Close();
        }
        if (fn_check_file(file) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


        public static bool fn_copy_file(string file1,string file2)
        {
            try{
                File.Copy(file1, file2,true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
                    
        }



        
        public static string fn_load_file_to_variable (string file)
        {
            try
            {
                return System.IO.File.ReadAllText(file);
            }
            catch (Exception e)
            {
                MessageBox.Show("Šablona " + file + " chybí");
                return null;
            }
        }



        public static bool fn_save_variable_as_file (string file,string value)
        {
         try
         {
             File.WriteAllText(file, value);
             return true;
         }
            catch(IOException e)
            {
                return false;
            }
        }



        public static bool fn_delete_directory(string path)
        { 
            try
            { 
                System.IO.Directory.Delete(path, true);
                return true;
           }
            catch
            {
                return false;
            }
        }




        public static void fn_clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                try
                {
                        fi.Delete();
                }
            catch
                {

                }
            }
            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                try
                {
                    fn_clearFolder(di.FullName);
                    di.Delete();
                }
                catch {
                }
            }
            

        }


        public static bool fn_check_used_file(string filename)
        {
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                }
                 return false;
            }
            catch
            {
                //MessageBox.Show("Soubor je Používán");
                return true;
            }
        }




        public static bool fn_copy_directory_files(string sourcePath, string targetPath)
        {
            bool fn_result = true;
            string SourceFileName = "";
            string DestinationFileName = "";
            FileInfo tmp_check_file;
            if (fn_check_directory(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                foreach (string s in files)
                {
                    tmp_check_file = new FileInfo(Path.Combine(sourcePath, s));
                    if (global_functions.app_functions.fn_IsFileLocked(tmp_check_file) == false)
                    { 
                        SourceFileName = System.IO.Path.GetFileName(s);
                        if (fn_check_used_file(s) == false)
                        {
                            DestinationFileName = System.IO.Path.Combine(targetPath, SourceFileName);
                    
                            if (fn_copy_file(s, DestinationFileName) == false)
                            {
                                fn_result =  false;
                            }
                        }
                    }
                }
            }
            else
            {
                fn_result = false;
                MessageBox.Show("Některá ze Složek Neexistuje nebo nelze zkopírovat");
            }
            return fn_result;
        }





        public static bool fn_delete_file(string file)
        {  try 
            {
                File.Delete(file);
                    if (fn_check_file(file) == true)
                        {return false;} else {return true;}
            }
            catch (Exception e)
                {return false;}
        }







        public static bool fn_save_config_file(string file,string setting)
        {
                if (fn_delete_file(file)==true) 
                    {
                        fn_create_file(file);
                
                using (StreamWriter sw = new StreamWriter(file))
            {
               sw.WriteLine(setting);
               sw.Close();
            }

                using (StreamReader sr = new StreamReader(file))
                {

                    if (sr.ReadLine() == setting) { MessageBox.Show("Nastavení Bylo Uloženo."); return true; }
                    else { MessageBox.Show("Nastavení se nepodařilo uložit.\n Zkontrolujte Práva Zápisu Instalační složky."); return false; }

                }
                    }
                else { MessageBox.Show("Nastavení se nepodařilo uložit.\n Zkontrolujte Práva Zápisu Instalační složky."); return false; } 
        }




        public static string fn_load_config_file(string file)
        {
            string line = "";
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string temp = ""; 
                        while ((temp = sr.ReadLine()) != null )
                        {
                            line = temp;
                        }
                    }
          return  line;
        }





        public static bool fn_save_log_file(string file, string proccess_result)
        {
            fn_create_file(file);

                FileStream aFile = new FileStream(file, FileMode.Append, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(aFile))
                {
                    sw.WriteLine(proccess_result);
                    sw.Close();
                    return true;
                }
                if (fn_check_file(file) == false) {
                    MessageBox.Show("Výsledek Zpracování se nepodařilo uložit do Log souboru. \n Prosím zkontrolujte složku aplikace."); return false; 
                }
        }




        public static void fn_check_file_size(string file, string max_size)
        {
            FileInfo f = new FileInfo(file);
            if (f.Length >= (Convert.ToDouble(max_size)*1048576))
            {
                fn_resize_files(file);
            }
        }




        public static void fn_resize_files(string file)
        {
            int counter = 0;
            int remove_lines_count = 1000;
            string line;
            System.IO.StreamReader load_file = new System.IO.StreamReader(file);
            string newstr = null;
            while ((line = load_file.ReadLine()) != null)
            {
                if (counter > remove_lines_count)
                {
                    newstr += line + Environment.NewLine;
                }
                counter++;
            }

            load_file.Close();

            if (System.IO.File.Exists(file))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(file);
                sw.WriteLine(newstr);
                sw.Close();
            }

        }



        public static string fn_check_filename_part_in_directory(string path,string filename)
        {
            string fn_result = "";
            string SourceFileName = "";
            string[] new_filename;
            int max_count = 1;


            if (fn_check_directory(path))
            {
                string[] files = System.IO.Directory.GetFiles(path);

                foreach (string s in files)
                {
                    SourceFileName = System.IO.Path.GetFileName(s);
                    new_filename = SourceFileName.Split('_');

                    if (SourceFileName.Contains(filename))
                    {
                        new_filename = new_filename[1].Split('.');
                        if (Convert.ToInt32(new_filename[0]) >= max_count )
                        {
                            max_count = Convert.ToInt32(new_filename[0]) + 1;
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Cílová složka neexistuje.");
            }

            if (fn_result == "") { fn_result = filename + "_1"; }
            return filename + "_" + Convert.ToString(max_count);
        }


    }


}