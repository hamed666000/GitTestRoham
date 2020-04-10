using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDemonstrator
{
    class c_TreeObj
    {
        public c_TreeObj()
        {
            Children = new List<c_TreeObj>();
        }
        public int m_iId { get; set; }
        public string m_sName { get; set; }
        public double m_dStep { get; set; }
        public string m_sColour { get; set; }

        public IList<c_TreeObj> Children { get; set; }

    }
}
