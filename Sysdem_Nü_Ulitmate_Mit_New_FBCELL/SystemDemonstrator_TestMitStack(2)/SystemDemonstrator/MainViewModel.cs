using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SystemDemonstrator;
using System.Windows.Input;
using SystemDemonstrator.Commands;
using System.Xml.Serialization;
using Newtonsoft;
using System.Xml.Linq;
using Newtonsoft.Json;

using System.Collections.ObjectModel;



namespace SystemDemonstrator
{
    public enum eAction { Nothing, GetFromCell, PutToCell }
    class c_MainViewModel : INotifyPropertyChanged
    {
        // Model / Fabric
        private c_Fabric m_cFabric;

        // ViewModel
        public List<c_StoragePlace> m_Storage { get; set; }
        public List<c_StoragePlace> m_GUIStorage { get; set; }
        public List<c_Cell> m_GUIListCells { get; set; }

        public List<c_WorkingStep> m_GUIListOfWorkingSteps { get; set; }
        public String m_sGUIXMLWorkingSteps { get; set; }

        

        public bool m_bAutoModeActive { get; set; }
        public bool m_bManualModeActive { get; set; }
        public String m_sAutoModeBtnText { get; set; }
        public String m_sAutoModeText { get; set; }
        
        public c_Action m_cGuiAction{ get; set; }

        public String m_sMessageText { get; set; }
        String m_sAddOld;

        // Commands
        public ICommand UpdateListsCommand
        { get; private set;}
        public ICommand CreateActionCommand
        { get; private set; }

        public ICommand AutoModeCommand
        { get; private set; }

        public static DateTime Now { get; }

        public Collection<c_TreeObj> ProjectList { get; set; }

        //konstruktor
        public c_MainViewModel()
        {
            // Config

            c_Config.m_sProtocolPath = "ysdfgb";

            CommonFunctions Reader = new CommonFunctions();

            string sJSon = "";
            string sPathOfConfig;
            string sRef = "";
   
            // Einlesen der Konfigurierten Pfade aus der confoig.json
            sPathOfConfig = "Config/Config.json";

            Reader.ReadJSonFile(sPathOfConfig, ref sJSon);
            Dictionary<string, string> dicConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(sJSon);
            

            if (dicConfig.TryGetValue("m_sSysDemStatusFile", out sRef) == false)
                c_Config.bConfigErr = true; // Fehler 
            c_Config.m_sSysDemStatusFile = sRef;

            if (dicConfig.TryGetValue("m_sStorageFile", out sRef) == false)
                c_Config.bConfigErr = true; // Fehler 
            c_Config.m_sStorageFile = sRef;

            if (dicConfig.TryGetValue("m_sRegenarationPathFile", out sRef) == false)
                c_Config.bConfigErr = true; // Fehler 
            c_Config.m_sRegenarationPathFile = sRef;

            if (dicConfig.TryGetValue("m_sRegenarationPathApp", out sRef) == false)
                c_Config.bConfigErr = true; // Fehler 
            c_Config.m_sRegenarationPathApp = sRef;

            if (dicConfig.TryGetValue("m_sProtocolPath", out sRef) == false)
                c_Config.bConfigErr = true; // Fehler 
            c_Config.m_sProtocolPath = sRef;





            // FabrikTeileReal
            m_cFabric = new c_Fabric(this);
            

             //GUI Elemente
            m_cGuiAction = new c_Action();
            m_cGuiAction.m_eActionType = eAction.PutToCell;
            m_cGuiAction.m_sCell = "A2";
            m_cGuiAction.m_iWPPlace = 3;
            m_cGuiAction.m_sWPID = "Muster1";
            m_GUIListCells = new List<c_Cell> ();
            m_GUIStorage = new List<c_StoragePlace>();
            List<c_WorkingStep> m_GUIListOfWorkingSteps = new List<c_WorkingStep>();

            m_bAutoModeActive = false;
            m_bManualModeActive = true;
            m_sAutoModeBtnText = "Start";
            m_sMessageText = "";
            m_sMessageText = "Messages:";
            m_sAutoModeText = "Automode: OFF";

            if (c_Config.bConfigErr) // Wenn die Confoig Datei einen Fehler hat eine Message anzeigen
                AddToMessages("Error: Check Config!!!");
            //Commands
            UpdateListsCommand = new ICUpdateListsCommand(this);
            CreateActionCommand = new ICCreateAction(this);
            AutoModeCommand = new ICAutoMode(this);

        }


       


        public void AddToMessages(String sAdd)
        {
            if(m_sAddOld != sAdd)// Damit nicht der gleiche String mehrmals geschrieben wird
            { 
                m_sMessageText += Environment.NewLine;
                m_sMessageText += "[";
                m_sMessageText += DateTime.Now.ToString();
                m_sMessageText += "]";
                m_sMessageText += sAdd;
                m_sAddOld = sAdd;
                OnPropertyChange("m_sMessageText");
            }
        }


        public void CreateAction()
        {
            
            if (m_cFabric.m_cAktionHandler.IsActionFree())
            {

                switch (m_cGuiAction.m_eActionType)
                {
                    case eAction.GetFromCell:
                        m_cGuiAction.m_iWPPlace = m_cFabric.m_cMHS.GetStorage().GetPlaceOfID(m_cGuiAction.m_sWPID);
                        break;
                    case eAction.PutToCell:
                        m_cGuiAction.m_iWPPlace = m_cFabric.m_cMHS.GetStorage().GetPlaceOfID(m_cGuiAction.m_sWPID);
                        break;
                    default:
                        return;
                }

                m_cFabric.m_cAktionHandler.makeAndRequestAction(m_cGuiAction, true);
                m_cFabric.updateActionString();
            }
        }

        public void SetAutomode ()
        {
            if (m_bAutoModeActive)
            {
                m_bAutoModeActive = false;
                m_bManualModeActive = true;

                m_sAutoModeBtnText = "Start";
                m_sAutoModeText = "Automode: OFF";

            }
            else
            {
                m_bAutoModeActive = true;
                m_bManualModeActive = false;
                m_sAutoModeBtnText = "Stop";
                m_sAutoModeText = "Automode: ON";

            }

            OnPropertyChange("m_bAutoModeActive");
            OnPropertyChange("m_bManualModeActive");
            OnPropertyChange("m_sAutoModeBtnText");
            OnPropertyChange("m_sAutoModeText");

            m_cFabric.m_bAutoModeActive = m_bAutoModeActive;
            m_cFabric.m_bFirstActionInAutoMode = m_bAutoModeActive;
                
            m_cFabric.m_bFirstActionInAutoMode = m_bAutoModeActive;
            m_cFabric.update();

        }
        // Funktionen
        public void UpdateGUILists()
        {

            OnPropertyChange("m_sGUIXMLWorkingSteps");

            m_GUIListCells = null;
            m_GUIListCells = new List<c_Cell>();
            foreach(c_Cell Cell in m_cFabric.m_cCellArea.GetCellList())
            {
                m_GUIListCells.Add(Cell);

            }
            ChangeCellStateString();
            OnPropertyChange("m_GUIListCells");

            m_GUIStorage = null;
            m_GUIStorage = new List<c_StoragePlace>();
            foreach (var Place in m_cFabric.m_cMHS.m_cStorage.GetStorageLocationList())
            {
                m_GUIStorage.Add(Place);
            }
            OnPropertyChange("m_GUIStorage");

            OnPropertyChange("m_sMessageText");

            // TreeViewData
            // Werkstücke finden


            ProjectList = new Collection<c_TreeObj> /*{ new c_TreeObj { Id = 0, Name = "first" }, new c_TreeObj { Id = 99, Name = "makka" } }*/();
            //ProjectList[0].Children.Add(new c_TreeObj { Id = 1, Name = "second" });
            //ProjectList[0].Children.Add(new c_TreeObj { Id = 2, Name = "third" });
            //ProjectList[0].Children[0].Children.Add(new c_TreeObj { Id = 3, Name = "fourth" });
            ConsoleColor[] consoleColors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

              
            int i = -1;
            double dCurreentCount = m_cFabric.m_cWorkingSchedule.GetStepCount();
            ProjectList.Clear();
            //OnPropertyChange("ProjectList");
            try
            {
                foreach (var Place in m_cFabric.m_cMHS.m_cStorage.GetStorageLocationList())
                {

                    if (Place.m_iWorkpieceID != "Empty")
                    {
                        i++;
                        ProjectList.Add(new c_TreeObj { m_sName = Place.m_iWorkpieceID, m_sColour = "Black" }); // Wird bearbeit

                        foreach (var Step in m_cFabric.m_cWorkingSchedule.m_ListOfWorkingSteps)
                        {
                            if (Step.m_sAction == "put" && Step.m_iWorkpiece == Place.m_iNumber)
                            {

                                if (dCurreentCount >= Step.m_dWorkingStep) // Welchen Zustand hat der Schritt?
                                {

                                    foreach (var FutureStep in m_cFabric.m_cWorkingSchedule.m_ListOfWorkingSteps)
                                    {
                                        if (FutureStep.m_sCell == Step.m_sCell && FutureStep.m_sAction == "pickup" && FutureStep.m_dWorkingStep > Step.m_dWorkingStep)
                                        {
                                            if (FutureStep.m_dWorkingStep > dCurreentCount)
                                            {
                                                foreach (var Cell in m_GUIListCells)
                                                {
                                                    if (Cell.Name == Step.m_sCell)
                                                    {
                                                        if (Cell.Idle)
                                                            ProjectList[i].Children.Add(new c_TreeObj { m_sName = Step.m_sCell, m_sColour = "Blue" }); // Wartend in Zelle
                                                        else
                                                            ProjectList[i].Children.Add(new c_TreeObj { m_sName = Step.m_sCell, m_sColour = "Orange" }); // Wird bearbeit
                                                    }


                                                }

                                                // ProjectList[i].Children.Add(new c_TreeObj { m_sName = Step.m_sCell, m_sColour = "Black" }); // Schwarz noch nicht bearbeitet



                                            }
                                            else
                                            {
                                                ProjectList[i].Children.Add(new c_TreeObj { m_sName = Step.m_sCell, m_sColour = "Green" }); // Fertig und abgeholt
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                    ProjectList[i].Children.Add(new c_TreeObj { m_sName = Step.m_sCell, m_sColour = "Black" }); // Schwarz noch nicht bearbeitet
                            }
                        }
                    }
                }
                OnPropertyChange("ProjectList");
            }
            catch (IOException e)
            {
                String str;
                str = e.Message;
            }
        }



        // Event 
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var Handler = PropertyChanged;
                Handler (this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ChangeCellStateString()
        {
            foreach (var Cell in m_GUIListCells)
            {
                if(Cell.IsVirtualCell)
                {
                    Cell.State = "Virtual";
                }
                else
                { 
                switch (Cell.State)
                {
                    case "0":
                        Cell.State = "ToStandby";
                        break;
                    case "1":
                        Cell.State = "Standby";
                        break;
                    case "2":
                        Cell.State = "ToLoadPos";
                        break;
                    case "3":
                        Cell.State = "LoadPos";
                        break;
                    case "4":
                        Cell.State = "ToStartable";
                        break;
                    case "5":
                        Cell.State = "Startable";
                        break;
                    case "6":
                        Cell.State = "ToRunning";
                        break;
                    case "7":
                        Cell.State = "Running";
                        break;
                    case "8":
                        Cell.State = "ToUnloadPos";
                        break;
                    case "9":
                        Cell.State = "UnloadPos";
                        break;
                    case "10":
                        Cell.State = "Finished";
                        break;
                    case "11":
                        Cell.State = "ToEmeStop";
                        break;
                    case "12":
                        Cell.State = "EmeStop";
                        break;
                    case "13":
                        Cell.State = "ToEmeOff";
                        break;
                    case "14":
                        Cell.State = "EmeOff";
                        break;
                    case "15":
                        Cell.State = "Error";
                        break;
                }
                }
            }
            

        }

    }


}
