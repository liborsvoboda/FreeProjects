using CheckPublicIPOwner.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CheckPublicIPOwner
{
    class Functions
    {
        public static Encoding fn_file_detect_encoding(string FileName)
        {
            string enc = "";
            if (File.Exists(FileName))
            {
                FileStream filein = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                if ((filein.CanSeek))
                {
                    byte[] bom = new byte[5];
                    filein.Read(bom, 0, 4);
                    // EF BB BF       = utf-8
                    // FF FE          = ucs-2le, ucs-4le, and ucs-16le
                    // FE FF          = utf-16 and ucs-2
                    // 00 00 FE FF    = ucs-4
                    if ((((bom[0] == 0xEF) && (bom[1] == 0xBB) && (bom[2] == 0xBF)) || ((bom[0] == 0xFF) && (bom[1] == 0xFE)) || ((bom[0] == 0xFE) && (bom[1] == 0xFF)) || ((bom[0] == 0x0) && (bom[1] == 0x0) && (bom[2] == 0xFE) && (bom[3] == 0xFF))))
                        enc = "Unicode";
                    else
                        enc = "ASCII";
                    // Position the file cursor back to the start of the file
                    filein.Seek(0, SeekOrigin.Begin);
                }
                filein.Close();
            }
            if (enc == "Unicode")
                return Encoding.UTF8;
            else
                return Encoding.Default;
        }

        public static T LoadDefinedFormatFile<T>(string iPListFile, bool createFileIfNotExist, string newFileContent = null)
        {
            try
            {
                if (File.Exists(iPListFile) || File.Exists(Path.Combine(AppContext.BaseDirectory, iPListFile)))
                {
                    using (StreamReader sr = new StreamReader((File.Exists(iPListFile) ? iPListFile : Path.Combine(AppContext.BaseDirectory, iPListFile)), fn_file_detect_encoding(iPListFile)))
                    {
                        return System.Text.Json.JsonSerializer.Deserialize<T>(sr.ReadToEnd());
                        sr.Close();
                    }
                }
                else if (createFileIfNotExist)
                {
                    using (StreamWriter sw = File.CreateText(iPListFile))
                    {
                        sw.Write(newFileContent);
                        sw.Flush();
                        sw.Close();
                    }
                    return (T)new object();
                }
                else
                {
                    return (T)new object();
                }
            }
            catch (Exception Ex) {
                Console.WriteLine(Ex.Message);
                return (T)new object();
            }
        }


        public static T GetApiRequest<T>(string apiUrl, string key = null)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient()) {
                    try
                    {
                        string json = httpClient.GetStringAsync(apiUrl + (!string.IsNullOrWhiteSpace(key) ? "/" + key : "")).GetAwaiter().GetResult();
                        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
                    }
                    catch (Exception ex)
                    {
                        return (T)new object();
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return (T) new object();
            }
        }

        public static bool SaveSettings(string savePathParam,string apiData)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(savePathParam))
                {
                    sw.Write(apiData);
                    sw.Flush();
                    sw.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
        }

        public static void ApplicationClose()
        {
            Environment.Exit(0);
        }
    }
}
