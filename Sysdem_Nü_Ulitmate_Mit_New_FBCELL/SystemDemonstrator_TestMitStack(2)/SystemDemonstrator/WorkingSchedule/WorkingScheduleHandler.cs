using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDemonstrator
{
    class c_WorkingScheduleHandler
    {
        public c_WorkingSchedule m_cWorkingSchedules { get; set; }

        private Stack<c_Action> StackOfActions;

        c_Fabric m_cFabric;
        public c_WorkingScheduleHandler(c_Fabric cFabric)
        {
            m_cWorkingSchedules = new c_WorkingSchedule();
            m_cFabric = cFabric;
            StackOfActions = new Stack<c_Action>();
        }
        public bool ActionStackEmpty()
        {
            if (StackOfActions.Count == 0)
                return true;
            else
                return false;

        }
        public void PushAction(c_Action cAction)
        {
            // nur wenn die gleiche Aktion nicht schon vorhande ist 

            StackOfActions.Push(cAction);
        }
        public c_Action PopNexAction()
        {
            return StackOfActions.Pop();
        }
        public void ClearStackOfActions()
        {
             StackOfActions.Clear();
        }


        public bool IsEmpty()
        {
            if (StackOfActions.Count() == 0)
                return true;
            else
                return false;

        }

    }
}
