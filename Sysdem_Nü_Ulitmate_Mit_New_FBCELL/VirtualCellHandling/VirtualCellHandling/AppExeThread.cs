using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace VirtualCellHandling
{
    class c_AppExeThread
    {
        private String m_sPath;
        private object m_oConfig;
        private List<c_CellConfig> m_ListOfConfigobj;
        public c_AppExeThread(String sPath, List<c_CellConfig> ListOfConfigobj)
        {
         
            m_sPath = sPath;
            m_ListOfConfigobj = ListOfConfigobj;
           
        }

        public void Method()
        {
            // Lesen der Aktion
            
            Thread.Sleep(500); // Wartezeit um nicht zu früh zu lesen
            // ReadAczion()
            string sJSon = "";
            try
            {
                using (StreamReader sr = new StreamReader(m_sPath))
                {
                    sJSon = sr.ReadToEnd();
                    
                    Console.WriteLine(sJSon);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read: sPath in c_AppExeThread");
                Console.WriteLine(e.Message);
            }
            // Parsen der Aktion in ein Dictionary
            Dictionary<String, String> dicAction = JsonConvert.DeserializeObject<Dictionary<String, String>>(sJSon);
            //dicAction Dictionary<String, String>  = JsonConvert.DeserializeObject<Dictionary<String, String>>(sJSon);
            //var dicAction = JsonConvert.DeserializeObject<Dictionary<string, string>>(sJSon);

            // Auswahl der Applikation und der
            String sCellName;
            String sWPID;

          

            if (dicAction.TryGetValue("Name", out sCellName) == false)
                { Console.WriteLine("Name of Action could not be read"); }
            if (dicAction.TryGetValue("WPID", out sWPID) == false)
               { Console.WriteLine("WPID of Action could not be read"); }
 
            // Zelle Suchen 
            foreach (c_CellConfig CellConfig in m_ListOfConfigobj)
            {

                if(CellConfig.m_sName == sCellName)
                {
                    //StartModul()
                    // Aktion für Zelle auswählen Matlab oder exe
                    switch (CellConfig.m_sAppType)
                    {
                        case "exe":
                            ExeAction(sWPID, CellConfig.m_sAppPath);
                            break;
                        case "Matlab":
                            MatlabAction(sWPID, CellConfig.m_sAppPath);
                            
                            break;
                        default:
                            Console.WriteLine("Error: Default in switch (sActionType)");
                            break;
                    }
                }


            }
            // DeleteActionFile()
            System.IO.File.Delete(m_sPath);

         
            return;

        }


        private void MatlabAction(string sWPID, string sPathOfApp)
        {
            // Folgenden Zeilen basieren auf einem Beispiel von Matlab
            // Create the MATLAB instance
            
            String sFileName = System.IO.Path.GetFileName(sPathOfApp);
            String sFilePath = sPathOfApp.Replace("\\" + sFileName, "");

          

            Type MatlabType = Type.GetTypeFromProgID("Matlab.Desktop.Application");
            MLApp.MLApp matlab = (MLApp.MLApp)Activator.CreateInstance(MatlabType);

            // Change to the directory where the function is located 
            String sExecutePath = "cd " + sFilePath;

            matlab.Execute(sExecutePath);

            // Define the output 
            object result = null;

            // Call the MATLAB function myfunc
            matlab.Feval(sFileName.Replace(".m",""), 1, out result, sWPID);

            // Display result  
            object[] res = result as object[];
            // Hier könnte noch eine Abfrage folgen ob das Result ein Fehler ist !!! Ist noch nicht definiert wie das aussieht.

        }
        private void ExeAction(string sWPID, string sPathOfApp)
        {

            String sFileName = System.IO.Path.GetFileName(sPathOfApp);
            String sFilePath = sPathOfApp.Replace("\\" + sFileName, "");
            String sFilefullPath = sFilePath + "\\" + sFileName;
            String sFiletoEXEcute = sFilefullPath.Replace("\\", "\\\\");
            String sFillsampel = sFiletoEXEcute.Replace(".exe", "");
            sFillsampel += "_" + sWPID + ".exe"  ;
            //Process.Start()
            

            Console.WriteLine(sFileName);
            Console.WriteLine(sFilePath);
            Console.WriteLine(sFilefullPath);
            Console.WriteLine(sFiletoEXEcute);
            Console.WriteLine(sFillsampel);
           

            try
            {
                Process.Start(sFillsampel);
            }
            catch (Exception e) {

                Console.WriteLine( e.Message +  "   : \n " +  sFillsampel);
                
            }
            
            Console.ReadLine();
            // hier kann eine exe gestartet werden 
            // weitere wie Python oder ähnliches können zusätzlich mit weiteren Abfragen eingefügt werden
        }
    }
}
