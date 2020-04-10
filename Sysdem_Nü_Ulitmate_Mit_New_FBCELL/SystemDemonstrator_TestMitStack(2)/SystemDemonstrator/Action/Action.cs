using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Newtonsoft.Json;



namespace SystemDemonstrator
{
    class c_Action
    {
        public c_Action()
        {
            m_sActionID = "";
            m_sWPID = "Empty";
            m_iWPPlace = 0;
            m_sCell = "";
            m_eActionType = 0;
            m_iActionState = 0;
        }
        public string m_sActionID { get; set; }
        public string m_sWPID { get; set; }
        public int  m_iWPPlace { get; set; }
        public string  m_sCell { get; set; }
        public eAction m_eActionType { get; set; }
        // 0 : nichts
        // 1 : Get WP from Machine
        // 2 : Put WP to Machine
        public int m_iActionState { get; set; }
        // 0 : IDLE
        // 1 : Pending
        // 2 : Running
        // 3 : Finished

        public bool isIDLE (){if (m_iActionState == 0) return true;else return false;}
        public bool isPending (){if (m_iActionState == 1) return true;else return false;}
        public bool isRunning (){if (m_iActionState == 2) return true;else return false;}
        public bool isFinished (){if (m_iActionState == 3) return true;else return false;}

        public String ToString ()
        {
            return JsonConvert.SerializeObject(this);
            
        }
    }
}
