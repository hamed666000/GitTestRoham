using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CutSKernel;
using VirtualTwin;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;


namespace DataSevice
{

    class c_TwinMode
    {

        public Action<List<CutSKernel.PlugIn.PluginInfo>> Log { get; set; }
        public bool TwinModeGetFromTwin(string cutSFolder, string sSource, string sValueNode, string sTarget, string sValueFile,string sGetMetaData, string sWPID )
        {
            try {
                string sNameOfData;
            if (sWPID == "")
                {
                    sWPID = "Muster.cutsproj";
                    sNameOfData = "VirtualTwin";
                }
                else
                {
                    sNameOfData = sWPID;
                    sWPID = sWPID + ".cutsproj";
                }
                


            CutSKernel.Controller.CutSController cutSController = CutSKernel.Controller.CutSController.GetController();
            cutSController.ScanKernelTypes();
            cutSController.ClearMachineModel();
            CutSKernel.Controller.ExporterController.Instance.ActiveExporter.Clear();
            

            CutSKernel.PlugIn.CutSPluginHost pluginHost = new CutSKernel.PlugIn.CutSPluginHost();
            pluginHost.FindPlugins(cutSFolder);
            
            //string sProjectFile = sSource; // ".\\Muster.cutsproj";

            string sProjectFile = System.IO.Path.Combine(sSource, sNameOfData);
            sProjectFile = System.IO.Path.Combine(sProjectFile, sWPID);

            CutSKernel.Serialization.ProjectSerializer projectSerializer = new CutSKernel.Serialization.ProjectSerializer(pluginHost, sProjectFile);



            var savedPlugins = projectSerializer.GetSavedPlugins();
            //newModel.GetCopy().;
            foreach (var PlugInName in savedPlugins)
            {
                var pluginInfo = pluginHost.GetPluginInfoForTypeName(PlugInName);
                if (pluginInfo == null)
                {
                    string exep = "Plug in not found";
                    return false;
                }
                else
                { 
                    if (!pluginInfo.Plugin.IsLoaded)
                        pluginHost.LoadPlugin(pluginInfo);
                    projectSerializer.DeserializePlugin(PlugInName);
                }
                
              

            }

            List<string> list = projectSerializer.GetSavedPlugins();

            CutSKernel.Kinematic.AKinBaseObject newModel = projectSerializer.DeserializeModel();

            CutSKernel.Kinematic.AKinBaseObject nextModel;

            cutSController.AddChildsToModel(newModel, true);

                // Schleife
                string[] asNodes = sValueNode.Split('\\');
                int i = 0;
                // Kontrollschleife ob Nodes Vorhanden sind, wenn nicht erstellen
                foreach (var Node in asNodes)
                {
                    i++;
                    if(i <= asNodes.Length)
                    {
                        newModel = newModel.GetChildByName(Node);

                        if(newModel == null)
                        {
                            Console.WriteLine("Node not found");
                            return false;
                        }
                    }
                }
                
                
            VirtualTwin.FileNode VtFile = (VirtualTwin.FileNode)newModel.GetChildByName(sValueFile);
                if (VtFile == null)
                {
                    Console.WriteLine("File-Node not found");
                    return false;
                }

            string sDestinationFileName = VtFile.DestinationFileName;

                if(sGetMetaData == "true" )
                {
                    Dictionary<String, String> dicOfMetaData = VtFile.MetaData;

                    String sJson = "";

                    sJson = JsonConvert.SerializeObject(dicOfMetaData);
                    try
                    {
                        using (StreamWriter sr = new StreamWriter(System.IO.Path.Combine(sTarget, "MetaData.json")))
                        {
                            sr.Write(sJson);
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Exception:" + e.Message);
                        return (false);
                    }

                }
                


            string sourceFile = System.IO.Path.Combine(sSource, sNameOfData);

            sourceFile = System.IO.Path.Combine(sourceFile, "VirtualTwin");

            sourceFile = System.IO.Path.Combine(sourceFile, sDestinationFileName);

            string destFile = System.IO.Path.Combine(sTarget, sValueFile);

            System.IO.Directory.CreateDirectory(sTarget);

            System.IO.File.Copy(sourceFile, destFile, true);

            pluginHost.UnloadPlugins();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
                return false;
            }
            return true;


        }

        public bool TwinModePutToTwin(string cutSFolder, string sSource, string sValueNode, string sTarget, string sValueFile, string sWPID )
        {
            try
            {
                string sNameOfData;
                if (sWPID == "")
                {
                    sWPID = "Muster.cutsproj";
                    sNameOfData = "VirtualTwin";
                }
                else
                {
                    sNameOfData = sWPID;
                    sWPID = sWPID + ".cutsproj";
                }


                CutSKernel.Controller.CutSController cutSController = CutSKernel.Controller.CutSController.GetController();
            cutSController.ScanKernelTypes();
            cutSController.ClearMachineModel();
            CutSKernel.Controller.ExporterController.Instance.ActiveExporter.Clear();

            cutSController.PluginHost.FindPlugins(cutSFolder);
                string sProjectFile = System.IO.Path.Combine(sTarget, sNameOfData);
                sProjectFile = System.IO.Path.Combine(sProjectFile, sWPID);

            CutSKernel.Serialization.ProjectSerializer projectSerializer = new CutSKernel.Serialization. ProjectSerializer(cutSController.PluginHost, sProjectFile);


            var savedPlugins = projectSerializer.GetSavedPlugins();
           
            foreach (var PlugInName in savedPlugins)
            {
                var pluginInfo = cutSController.PluginHost.GetPluginInfoForTypeName(PlugInName);
                if (pluginInfo == null)
                {
                    string exep = "Plug in not found";
                    return false;
                }
                else
                {
                    if (!pluginInfo.Plugin.IsLoaded)
                    {
                        cutSController.PluginHost.LoadPlugin(pluginInfo);

                    }
                    projectSerializer.DeserializePlugin(PlugInName);
                }



            }

            cutSController.LoadProject(sProjectFile, Log, false);

            CutSKernel.Kinematic.AKinBaseObject newModel = projectSerializer.DeserializeModel();
            

            cutSController.AddChildsToModel(newModel, true);

                
            // Schleife
                string[] asNodes = sValueNode.Split('\\');
            
            // Kontrollschleife ob Nodes Vorhanden sind, wenn nicht erstellen

            int i = 0;
            //  Rekursives hinein Itterieren in den Pfad (Falls ein Knoten nicht vorhanden ist wird dieser erstellt)
            if(NodeCheckAndCreate(i, newModel, asNodes))
            {
                Console.WriteLine("ErrorFileNoder Creation NodeCheckAndCreate");
                return false;
            }



            i = 0;
            VirtualTwin.DataNode iterNode = (VirtualTwin.DataNode)newModel.GetChildByName(asNodes[0]);

            // Den Letzten DatenKnoten vor dem FileObjekt finden
            foreach (string sNode in asNodes)
            {

                if((i > 0) && i < (asNodes.Length ))
                    iterNode = (VirtualTwin.DataNode)newModel.GetChildByName(sNode);
                i++;

            }
            // File Knoten holen und die Datei Importieren
            VirtualTwin.FileNode newFileNode = new VirtualTwin.FileNode(/*System.IO.Path.Combine(sSource, sValueFile)*/);
            if(!System.IO.File.Exists(System.IO.Path.Combine(sSource, sValueFile)))
            {
                Console.WriteLine("File not found");
                return false;
            }
            newFileNode.ImportFile = System.IO.Path.Combine(sSource, sValueFile);
            newFileNode.Name = sValueFile;
            DateTime dt = new DateTime();
            dt = DateTime.Now;   
                

            newFileNode.MetaData.Add("lastmodified", dt.ToString("yyyy/MM/dd HH:mm:ss"));
            // Meta-Daten schreiben wenn meta.json vorhanden
            if(System.IO.File.Exists(System.IO.Path.Combine(sSource, "meta.json")))
            {
                    string sJSon = "";
                    // Meta einlesen 
                    try
                    {
                        using (StreamReader sr = new StreamReader(System.IO.Path.Combine(sSource, "meta.json")))
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

                    foreach (var Pair in dicConfig)
                    {
                        newFileNode.MetaData.Add(Pair.Key, Pair.Value);
                    }
                    
                         
                }


            iterNode.AddChild(newFileNode);

            projectSerializer.Serialize(sProjectFile);


            cutSController.PluginHost.UnloadPlugins();

            }
            catch(Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
                return false;
            }
            return true;


        }
        private bool NodeCheckAndCreate(int i, CutSKernel.Kinematic.AKinBaseObject UpperNode, string[]  asNodes)
        {
            if (i > asNodes.Length - 1)
                return false;


            VirtualTwin.DataNode currentNode = (VirtualTwin.DataNode)UpperNode.GetChildByName(asNodes[i]);
            if (currentNode == null)
            {

                VirtualTwin.DataNode NewNode = new VirtualTwin.DataNode();
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                NewNode.Name = asNodes[i];
                NewNode.MetaData.Add("lastmodified", dt.ToString("yyyy/MM/dd HH:mm:ss"));
                UpperNode.AddChild(NewNode);

            }
            
            bool berr = false;
            berr = NodeCheckAndCreate(i + 1, (CutSKernel.Kinematic.AKinBaseObject)UpperNode.GetChildByName(asNodes[i]), asNodes);
            
            return berr;
        }
        public bool TwinModeNewTwin(string cutSFolder, string sSource, string sTarget, string sWPID)
        {

            try
            {
                string sNameOfData;
                if (sWPID == "")
                {
                    sWPID = "Muster.cutsproj";
                    sNameOfData = "VirtualTwin";
                }
                else
                {
                    sNameOfData = sWPID;
                    sWPID = sWPID + ".cutsproj";
                }



                CutSKernel.Controller.CutSController cutSController = CutSKernel.Controller.CutSController.GetController();
                cutSController.ScanKernelTypes();
                
                cutSController.ClearMachineModel();
                CutSKernel.Controller.ExporterController.Instance.ActiveExporter.Clear();

                
                CutSKernel.PlugIn.CutSPluginHost pluginHost = new CutSKernel.PlugIn.CutSPluginHost();
                cutSController.PluginHost.FindPlugins(cutSFolder);

                //string sProjectFile = sSource; // ".\\Muster.cutsproj";

                string sProjectFile = System.IO.Path.Combine(sSource, "TwinBase");
                sProjectFile = System.IO.Path.Combine(sProjectFile, "Muster.cutsproj");

                CutSKernel.Serialization.ProjectSerializer projectSerializer = new CutSKernel.Serialization.ProjectSerializer(pluginHost, sProjectFile);
                
                var savedPlugins = projectSerializer.GetSavedPlugins();
                //newModel.GetCopy().;
                foreach (var PlugInName in savedPlugins)
                {
                    var pluginInfo = cutSController.PluginHost.GetPluginInfoForTypeName(PlugInName);
                    if (pluginInfo == null)
                    {
                        string exep = "Plug in not found";
                        return false;
                    }
                    else
                    {
                        if (!pluginInfo.Plugin.IsLoaded)
                        {
                            cutSController.PluginHost.LoadPlugin(pluginInfo);

                        }
                        projectSerializer.DeserializePlugin(PlugInName);
                    }

                }

                List<string> list = projectSerializer.GetSavedPlugins();

                string sDataPath = System.IO.Path.Combine(sSource, sNameOfData);

                System.IO.Directory.CreateDirectory(sDataPath);

                string sProjectFilePath = System.IO.Path.Combine(sSource, sWPID);


                cutSController.SaveProject(sProjectFilePath, true);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
                return false;
            }


            return true;
        }

    }
}
