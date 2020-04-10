using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SystemDemonstrator
{
    class c_WorkingSchedule
    {
        public List<c_WorkingStep> m_ListOfWorkingSteps { get; set; }
        private double m_dStep;
        private double m_dHighestStep; // Hier wird immer die höchste Schrittnummer eingetragen nach einem Update der WorkingSteps
        private string m_sPathOfRegengerationPath;
        public c_WorkingSchedule()
        {
            
            m_sPathOfRegengerationPath = c_Config.m_sRegenarationPathFile;

            m_ListOfWorkingSteps = new List<c_WorkingStep>();
            m_dStep = -1; // Weil der erste Schritt bereits Schritt 0 und bei der Abfrage des ersten bereits hochgezählt wird
            UpdateListOfWorkingSchedules();
        }

        private void UpdateListOfWorkingSchedules()
        {
            CommonFunctions Reader = new CommonFunctions();
            string sJSon = "";

            Reader.ReadJSonFile(m_sPathOfRegengerationPath, ref sJSon);

            m_ListOfWorkingSteps = JsonConvert.DeserializeObject<List<c_WorkingStep>>(sJSon);
            
            // Bei jedem Update den höchsten Schritt suchen
            foreach (c_WorkingStep Step in m_ListOfWorkingSteps)
            {
                if (m_dHighestStep < Step.m_dWorkingStep )
                {
                    m_dHighestStep = Step.m_dWorkingStep;

                }

            }


        }
        public bool SetStepCount(double dStep)
        {
            m_dStep = dStep;
            return true;
        }

        public double GetStepCount()
        {
            return m_dStep;
        }


        public bool SetStepCountBack()
        {
            m_dStep = m_dStep-1;
            return true;
        }
        public bool NextSteps(ref Queue<c_WorkingStep> QueueOfNextSteps)
        {
            bool bFound = false;
            UpdateListOfWorkingSchedules(); // hier wird der Regenerationspfad aktualisiert

            while (m_dStep < m_dHighestStep) // Sollte es keine Arbeitsschritte mehr geben wird die Schleife abgebrochen
            {
                m_dStep = m_dStep + 1;
                foreach (c_WorkingStep Step in m_ListOfWorkingSteps)
                {

                    if (Step.m_dWorkingStep == m_dStep)
                    {
                        QueueOfNextSteps.Enqueue(Step);
                        bFound = true;
                    }

                }
                if (bFound) // Wenn Aktionen für den Schritt gefunden wurden wird die letzte gefundene in cNextStep geschrieben
                    break;

            }
            return bFound; // keine Aktion gefunden
        }


        public bool NextStep(ref c_WorkingStep cNextStep, ref Queue<c_WorkingStep> QueueOfNextSteps)
        {
            bool bFound = false;
            UpdateListOfWorkingSchedules(); // hier wird der Regenerationspfad aktualisiert

            while (m_dStep < m_dHighestStep) // Sollte es keine Arbeitsschritte mehr geben wird die Schleife abgebrochen
            {
                m_dStep = m_dStep + 1;
                foreach (c_WorkingStep Step in m_ListOfWorkingSteps)
                {

                    if(Step.m_dWorkingStep == m_dStep)
                    {
                        QueueOfNextSteps.Enqueue(Step);
                        cNextStep = Step;           
                        bFound = true;
                    }

                }
                if (bFound) // Wenn Aktionen für den Schritt gefunden wurden wird die letzte gefundene in cNextStep geschrieben
                    break;

            }
            return bFound; // keine Aktion gefunden
        }
        public bool CheckForVirtActionInStep(ref c_WorkingStep cNextStep)// 
        {


            return true;
        }


    }
}
