using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDemonstrator.ADSInterface;
using Newtonsoft.Json;

namespace SystemDemonstrator
{
    class c_ActionHandler : c_Base_ADSReadInterface
    {
        private c_Action m_cLastAction;
        private c_Action m_cCurrentAction;
        private ADSWriteInterface m_cAdsWriteInterface;
        c_Fabric m_cFabric;


        public c_ActionHandler(c_Fabric cFabric)
        {
            m_cFabric = cFabric;

            m_cCurrentAction = new c_Action();
            m_cLastAction = new c_Action();

            m_cAdsWriteInterface = new ADSWriteInterface();
            ADSReadJSon = new ADSReadInterface("MAIN.sJsonDocLastAction");
            ADSReadJSon.Registrate((c_Base_ADSReadInterface)this);
        }


        public bool IsActionFree()
        {
            if (/*m_cCurrentAction.isIDLE() ||*/ m_cCurrentAction.isFinished())
                return true;
            else
                return false;

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public c_Action GetCurrentAction()
        {
            return m_cCurrentAction;
        }

        public c_Action GetLastAction()
        {
            return m_cLastAction;
        }

        public bool makeAndRequestAction(c_Action cRequestAction, bool bFirstActionInAutoMode)
        {
            bool bret = false;
            if (IsActionFree() || bFirstActionInAutoMode)
            {
                {
                    cRequestAction.m_sActionID = RandomString(6);

                } while (cRequestAction.m_sActionID == m_cCurrentAction.m_sActionID) // Falls zufällig eine gleiche ID erstellt wurde erneut durchführen

                m_cLastAction = m_cCurrentAction;
              

                m_cCurrentAction = cRequestAction;
            }
            else
                return false;
            

            // Senden der Aktion an PLC
            string sJSon;
            sJSon = JsonConvert.SerializeObject(m_cCurrentAction);
            bret = m_cAdsWriteInterface.writeToPLCString("MAIN.sJsonDocAction", sJSon);
            m_cCurrentAction.m_iActionState = 1;
            m_cFabric.updateActionString(); // Message
            return bret;
        }


        public override void Update(string sJSon)
        {
            c_Action cPlcAction = new c_Action();
            cPlcAction = JsonConvert.DeserializeObject<c_Action>(sJSon);
            if (m_cCurrentAction.m_sActionID == cPlcAction.m_sActionID)
                m_cCurrentAction.m_iActionState = cPlcAction.m_iActionState;


            m_cFabric.update();
        }


    }
}
