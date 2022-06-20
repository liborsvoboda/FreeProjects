using CheckPublicIPOwner.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CheckPublicIPOwner
{
    class Program
    {
        public static List<IPList> IPList = new();
        private readonly static string configFileName = "config.json";
        private static readonly Config Config = new() { checkOwnerIpUrl = Functions.LoadDefinedFormatFile<Config>(configFileName, true, System.Text.Json.JsonSerializer.Serialize<Config>(new())).checkOwnerIpUrl };

        static void Main(string[] args) 
        {
            //Console.Clear(); foreach (String arg in Environment.GetCommandLineArgs()) { Console.Write(arg);
            if (args.Length == 0)
            {
                Console.WriteLine("For more info use indispensable param /?");
                Console.ReadKey();
                Functions.ApplicationClose();
            }
            if (args[0] != "/?")
            {
                IPList = Functions.LoadDefinedFormatFile<List<IPList>>(args[0], false);
            } else {
                //generate example file for faster using
                Functions.LoadDefinedFormatFile<List<IPList>>("ExampleIpListFile.json", true, "[{\"IP\":\"10.10.10.10\"},{\"IP\":\"10.10.10.20\"}]");

                Console.WriteLine("This is utility for read and Owners via Url from config.json.");
                Console.WriteLine("Tool check all IP addresses from json file, which is inserted as first parameter.");
                Console.WriteLine("Tool write the returned results from API responses to file, which is set as second parameter or will be created the 'ResponseJsonFile.json' automatically.");
                Console.WriteLine("Example of config.json file with free APIUrl for load OwnerInfo of each IP address in the list {'checkIP': 'http://ip-api.com/json/'}");
                Console.WriteLine("Example of jsonFile with IP addresses List for Owners check: [{\"IP\":\"10.10.10.10\"},{\"IP\":\"10.10.10.20\"}]");
                Console.WriteLine("Example of use utility: CheckPublicIPOwner.exe 'c:\aha\\IPListJsonFile.json' 'c:\aha\\ResponseJsonFile.json' ");
                Console.WriteLine("Simple using. Bye from https://GroupWare-Solution.eU");
                Console.ReadKey();
                Functions.ApplicationClose();
            }

            if (IPList.Count == 0) {
                Console.WriteLine("Count of IP Addresses is zero. Please insert valid JSON file with param 'IP'. Example [{\"IP\":\"10.10.10.10\"}]. ");
                Console.WriteLine("For more info use indispensable param /?");
                Console.ReadKey();
                Functions.ApplicationClose();
            }
            else
            {
                List<string> returnedApiInfo = new();
                IPList.ForEach(ip =>
                {
                    returnedApiInfo.Add(Functions.GetApiRequest<Object>(Config.checkOwnerIpUrl, ip.ip).ToString());
                });

                string responseFile = (args.Length == 2) ? args[1] : "ResponseJsonFile.json";
                Functions.SaveSettings(responseFile, "[" + string.Join(",", returnedApiInfo) + "]" );
            }

           
        }
    }
}
