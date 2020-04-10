using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Permissions;

namespace VirtualCellHandling
{
    class Program
    {

      
        static void  Main(string[] args)
        {




            c_FileWatcher cFileWatcher = new c_FileWatcher();
            String sJSon = "";
            String sConfigPath;
            
         
            try
            {
                using (StreamReader sr = new StreamReader("config.json"))
                {
                    sJSon = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        

            var dicConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(sJSon);
           
            
            String sPathOfActions = "";
            if (dicConfig.TryGetValue("PathOfActions", out sPathOfActions) == false)
            { Console.WriteLine("PathOfActions-Config could not be read"); }

            cFileWatcher.Run(sPathOfActions, sJSon);
        }
    }
}

