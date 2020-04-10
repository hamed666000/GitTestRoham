using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutSKernel;
using Newtonsoft.Json;


namespace DataSevice
{
    class Program
    {
        static int Main(string[] args)
        {

            string sWPID = "";
            if (args.Length > 0)
            {
                sWPID = args[0]; // Die .cutsproj wird in den TwinMode-Funktionen übergeben
            }
            
                

           String sExeName  = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase); // Namen der exe. lesen
           sExeName = sExeName.Replace(".exe", "");
           Console.Title = sExeName; // Fenstername der Konsole ändern zum Applikationsnamen
           Console.WriteLine("Start Action");


           string sJSon = "";
           string sConfigPath = sExeName + ".json";

            // Deklarieren der Config Variablen
           string sValueSource = "";
           string sValueTarget = "";
           string sValueMode = "";
           string sValueDebug = "";
           string sValueCutSFolder = "";
           string sValueFile = "";
           string sValueNode = "";
           string sbReturnValue = "";
           string sReturnValueVariable = "";
           string sGetMetaData = "";

            bool bConfigErr = false;
            int iReturnValue = 1;

            // Config einlesen 
            try
            {   
              using (StreamReader sr = new StreamReader(sConfigPath))
              {
                        sJSon  = sr.ReadToEnd();
              }
            }    
            catch (IOException e)
            {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    iReturnValue = 2;
                
            }

            if(iReturnValue != 2)
            { 
            try
            {   //Einlesen der Config Datei 
                var dicConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(sJSon);

            // Übergabe der Config Parameter an die Optionen
            if (dicConfig.TryGetValue("Source", out sValueSource) == false)
                bConfigErr = true; // Fehler 

            if (dicConfig.TryGetValue("Target", out sValueTarget) == false)
                bConfigErr = true; // Fehler 

            if (dicConfig.TryGetValue("File", out sValueFile) == false)
                bConfigErr = true; // Fehler 

            if (dicConfig.TryGetValue("Mode", out sValueMode) == false)
                bConfigErr = true; // Fehler 

            if (dicConfig.TryGetValue("Debug", out sValueDebug) == false)
                bConfigErr = true; // Fehler 

            if (dicConfig.TryGetValue("cutSFolder", out sValueCutSFolder) == false)
                bConfigErr = true; // Fehler 

            if (dicConfig.TryGetValue("Node", out sValueNode) == false)
                bConfigErr = true; // Fehler

            if (dicConfig.TryGetValue("bReturnValue", out sbReturnValue) == false)
                    bConfigErr = true; // Fehler

            if (dicConfig.TryGetValue("ReturnValueVariable", out sReturnValueVariable) == false)
                    bConfigErr = true; // Fehler

            if (dicConfig.TryGetValue("GetMetaData", out sGetMetaData) == false)
                   bConfigErr = true; // Fehler
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                    iReturnValue = 2;
                
            }
            }
            if (bConfigErr)
            {
                Console.WriteLine("Config file is not valid");
                Console.WriteLine("Press any key to exit Application");
                Console.ReadKey();
                    iReturnValue = 2;
                
            }


            String sName = "MAIN.fbCell" + sExeName;

            if (sValueMode == "Data")// Einfacher Modus zum Kopieren von Dateien
            {
                c_DataMode cDataMode = new c_DataMode();

                if(cDataMode.DataMode(sValueSource, sValueTarget, sValueFile))
                {
                    iReturnValue = 2;
                }

            }

            if (sValueMode == "TwinGet") // Aus dem Zwilling wird ein Pfad geholt und eine Kopie in das Ziel-Verzeichniss geschrieben
            {
                c_TwinMode cTwinMode = new c_TwinMode();

                if(!cTwinMode.TwinModeGetFromTwin(sValueCutSFolder, sValueSource, sValueNode, sValueTarget, sValueFile, sGetMetaData, sWPID))
                {
                    iReturnValue = 2;
                }


                sName = sName.Replace("_Get", "");
                sName = sName + ".fbDataServiceGet.m_iReturnValue";
                Console.WriteLine(sName);
               // Console.ReadKey();
            }




            if (sValueMode == "TwinPut") // Datei wird in den Zwilling geschrieben und in das Zwillings-Verzeichniss kopiert
            {
                c_TwinMode cTwinMode = new c_TwinMode();

                if(!cTwinMode.TwinModePutToTwin(sValueCutSFolder, sValueSource, sValueNode, sValueTarget, sValueFile, sWPID))
                {
                    iReturnValue = 2;
                }
                    

                sName = sName.Replace("_Put", "");
                sName = sName + ".fbDataServicePut.m_iReturnValue";

            }


           if (sValueMode == "TwinNew") // Aus dem Zwilling wird ein Pfad geholt und eine Kopie in das Ziel-Verzeichniss geschrieben
           {
               c_TwinMode cTwinMode = new c_TwinMode();

               if (!cTwinMode.TwinModeNewTwin(sValueCutSFolder, sValueSource,sValueTarget, sWPID))
               {
                   iReturnValue = 2;
               }


               sName = sName.Replace("_Get", "");
               sName = sName + ".fbDataServiceGet.m_iReturnValue";

           }

            ADSWriteInterface AdsWriteReturnValue = new ADSWriteInterface();

            
            if(sbReturnValue == "true")
            {
                if (sReturnValueVariable == "") // Wenn dieser String leer ist in die erzeugte Variable schreiben
                    AdsWriteReturnValue.writeToPLCInt(sName, iReturnValue);
                else // Wenn in dem String etwas steht, dann in die angegebene Variable
                    AdsWriteReturnValue.writeToPLCInt(sReturnValueVariable, iReturnValue);
                Console.WriteLine(iReturnValue);
            }

            if (sValueDebug == "true" || iReturnValue == 2) // Damit der Fehler gelesen werden kann wird dieser hier mit in der Bedingung abgefragt
                Console.ReadKey();
            return 0x00000000;
        }
    }
}
