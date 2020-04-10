using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDemonstrator
{
    class c_ErrorHandler
    {
        c_Fabric m_cFabric;


        public c_ErrorHandler(c_Fabric cFabric)
        {
            m_cFabric = cFabric;

        }

        public void Update()
        {



            m_cFabric.update(); 
        }

    }
}
