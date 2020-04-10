using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using SystemDemonstrator.ADSInterface;

namespace SystemDemonstrator
{
    class c_Fabric : c_Base_ADSReadInterface
    {
        private c_MainViewModel m_cMainView;

        public c_MHS m_cMHS { get; set; }
        public c_ActionHandler m_cAktionHandler { get; set; }
        public c_CellArea m_cCellArea { get; set; }
        public c_WorkingSchedule m_cWorkingSchedule { get; set; }

        public c_WorkingScheduleHandler m_cWorkingScheduleHandler { get; set; }

        public bool m_bAutoModeActive { get; set; }
        public bool m_bFirstAktion { get; set; }
        public bool m_bFirstActionInAutoMode { get; set; }

        private bool m_bGetNextActionEntered { get; set; }

        // Config-Variablen

        private static readonly Object Lock = new Object();
        public c_Fabric(c_MainViewModel cMainView)
        {
            m_cMainView = cMainView;

            ADSReadJSon = new ADSReadInterface("MAIN.m_sJsonTrigger");


            ADSReadJSon.Registrate((c_Base_ADSReadInterface)this);




            
            m_cMHS = new c_MHS(this);
            m_cAktionHandler = new c_ActionHandler(this);
            m_cCellArea = new c_CellArea(this);
            m_cWorkingSchedule = new c_WorkingSchedule();

            m_cWorkingScheduleHandler = new c_WorkingScheduleHandler(this);
            m_bFirstActionInAutoMode = false; // Wichtig, weil im ersten der Vergleich zur vorherigen Aktion Fehlt

        }

        private bool AutoActionUpdate()
        {

            c_WorkingStep cNextStep = new c_WorkingStep();
            Queue<c_WorkingStep> QueueOfNextSteps = new Queue<c_WorkingStep>();
            Stack<c_Action> ReverseStackOfActions = new Stack<c_Action>();

            if (m_cWorkingScheduleHandler.IsEmpty())
            {
                if (m_cWorkingSchedule.NextSteps(ref QueueOfNextSteps))
                {
                    c_Cell cCell = new c_Cell();
                    foreach (c_WorkingStep Step in QueueOfNextSteps)
                    {
                        if (Step.m_sAction == "pickup")
                        {
                            if (!m_cCellArea.SearchWPInCells(Step.m_iWorkpiece, ref cCell))
                            {

                                m_cWorkingSchedule.SetStepCountBack();
                                m_cWorkingScheduleHandler.ClearStackOfActions();
                                return false;
                            }
                            if (!cCell.Idle)
                            {

                                m_cWorkingSchedule.SetStepCountBack();
                                m_cWorkingScheduleHandler.ClearStackOfActions();
                                return false;
                            }
                            c_Action cNewAction = new c_Action();
                            cNewAction.m_sWPID = m_cMHS.GetStorage().GetStorageIDOfPlace(Step.m_iWorkpiece);
                            cNewAction.m_iWPPlace = Step.m_iWorkpiece;
                            cNewAction.m_sCell = Step.m_sCell;
                            cNewAction.m_eActionType = eAction.GetFromCell;
                            // Aktion in den Stack zur Abarbeitung pushen
                            ReverseStackOfActions.Push(cNewAction);
                            //m_cWorkingScheduleHandler.PushAction(cNewAction);
                        }
                        if (Step.m_sAction == "put")
                        {

                            if (!m_cCellArea.GetCellOfName(Step.m_sCell, ref cCell))
                            {

                                m_cWorkingSchedule.SetStepCountBack();
                                m_cWorkingScheduleHandler.ClearStackOfActions();
                                return false;
                            }
                            if (!cCell.Idle)
                            {

                                m_cWorkingSchedule.SetStepCountBack();
                                m_cWorkingScheduleHandler.ClearStackOfActions();
                                return false;
                            }


                            c_Action cNewAction = new c_Action();
                            cNewAction.m_sWPID = m_cMHS.GetStorage().GetStorageIDOfPlace(Step.m_iWorkpiece);
                            cNewAction.m_iWPPlace = Step.m_iWorkpiece;
                            cNewAction.m_sCell = Step.m_sCell;
                            cNewAction.m_eActionType = eAction.PutToCell;
                            // Aktion in den Stack zur Abarbeitung pushen
                            ReverseStackOfActions.Push(cNewAction);

                           // m_cWorkingScheduleHandler.PushAction(cNewAction);

                        }
                
                    }
                }

            }

           
            foreach(c_Action Action in ReverseStackOfActions)
            {
                m_cWorkingScheduleHandler.PushAction(Action);
            }

            return true;
        }

        private void CheckAndPushVirtualActions(Queue<c_WorkingStep>  QueueOfNextSteps)
        {
            // Virtuelle Aktionen im Schritt finden
            foreach (c_WorkingStep Step in QueueOfNextSteps)
            {
                if (m_cCellArea.IsCellVirtualByName(Step.m_sCell))
                {
                    // neue Aktion erstellen
                    c_Action cVirtualAction = new c_Action();
                    cVirtualAction.m_sWPID = "0";
                    cVirtualAction.m_iWPPlace = Step.m_iWorkpiece;
                    cVirtualAction.m_sCell = Step.m_sCell;
                    cVirtualAction.m_eActionType = eAction.PutToCell;
                    // Aktion in den Stack zur Abarbeitung pushen
                    m_cWorkingScheduleHandler.PushAction(cVirtualAction);

                }

            }

        }
        private void EndOfAction () // Hier AKtionen die nach der Ausführung einer Aktion Notwendig sind zb. GUI Update etc.
        {

            c_Action cCurrentAction = m_cAktionHandler.GetCurrentAction();
            c_Action cLastAction = m_cAktionHandler.GetLastAction();
            if(cCurrentAction == null || m_bFirstActionInAutoMode)
            {
                if ( m_bFirstActionInAutoMode)
                {
                    AutoActionUpdate();
                    if(!m_cWorkingScheduleHandler.IsEmpty())
                    {
                        
                        m_cAktionHandler.makeAndRequestAction(m_cWorkingScheduleHandler.PopNexAction(), m_bFirstActionInAutoMode);
                        m_bFirstActionInAutoMode = false;
                    }
                }
                    
            }

            if (cCurrentAction.isFinished())
            {
                
                if (cLastAction.m_sActionID != cCurrentAction.m_sActionID )// Nur wenn die Aktion nicht die alte ist
                {
                    //Storage-Update
                    if (!m_cCellArea.IsCellVirtualByName(cCurrentAction.m_sCell))// Platzsituation bei Virtuellen Zellen nicht ändern
                    { 
                        if (cCurrentAction.m_eActionType == eAction.PutToCell ) 
                        {
                            m_cMHS.m_cStorage.SetPlaceEmpty(cCurrentAction.m_iWPPlace);
                        }
                        else
                        {
                            m_cMHS.m_cStorage.SetPlaceOccupied(cCurrentAction.m_iWPPlace);
                        }
                    }


                }
               
                if(m_cWorkingScheduleHandler.IsEmpty() && m_bAutoModeActive && m_cAktionHandler.IsActionFree())
                {
                    AutoActionUpdate();
                    m_bFirstActionInAutoMode = false;
                }
                    

                if (!m_cWorkingScheduleHandler.IsEmpty() && m_cAktionHandler.IsActionFree())
                {
                    m_cAktionHandler.makeAndRequestAction(m_cWorkingScheduleHandler.PopNexAction(), false);
                }

            }


        }
        private void updateActionFinished()
        {
            // Aufruf zur Abfrage ob eine Aktion beendet wurde und durchführen der Entsprechenden Anweisungen

                EndOfAction();
            // Nächste Aktion wenn im AutoMode




        }
        public void updateActionString()
        {
            m_cMainView.AddToMessages(m_cAktionHandler.GetCurrentAction().ToString());

        }
        public void update()
        {
        lock (Lock)
        {
            // Überprüfen und updaten der Aktionen 
            updateActionFinished();

            // GUI-Listen Updaten
            m_cMainView.UpdateGUILists();

            // SystemZustand updaten
            writeSystemState();

         }


        }
        public override void Update(string sJSon)
        {
            update();
        }
  

        private bool writeSystemState()
        {

            String sPath = c_Config.m_sSysDemStatusFile;

            
            c_SystemState cSystemState = new c_SystemState();
            cSystemState.m_dStepCount = m_cWorkingSchedule.GetStepCount();
            cSystemState.m_ListOfCells = m_cCellArea.GetCellList();
            String sJson;

            sJson = JsonConvert.SerializeObject(cSystemState);
            try
            {
                using (StreamWriter sr = new StreamWriter(sPath))
                {
                    sr.Write(sJson);
                }
            }
            catch (IOException e)
            {
                return (false);
            }

            return true;
        }



    }
}
