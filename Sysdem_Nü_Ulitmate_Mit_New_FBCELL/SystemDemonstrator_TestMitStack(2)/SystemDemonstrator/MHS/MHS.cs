using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDemonstrator
{
    class c_MHS
    {
        public c_Storage m_cStorage;
        c_Fabric m_cFabric;

        public c_MHS(c_Fabric cFabric)
        {
            m_cFabric = cFabric;
            m_cStorage = new c_Storage(m_cFabric);
            
        }

        public c_Storage GetStorage()
        {
            return m_cStorage;
        }

    }
}
