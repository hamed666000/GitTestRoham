using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDemonstrator
{
    class c_Cell
    {
        public string Name { get; set; }
        public string State { get; set; }
        public bool Idle { get; set; }
        public int Workpiece { get; set; }
        public bool IsDisassemblyCell { get; set; }

        public bool IsVirtualCell { get; set; }
        public c_Cell() {; }
    }
}
