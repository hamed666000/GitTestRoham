using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SystemDemonstrator
{
    class c_WorkingStep
    {


        [JsonProperty("t")] public double m_dWorkingStep { get; set; } 
        [JsonProperty("cell")] public String m_sCell { get; set; } 
        [JsonProperty("action")] public String m_sAction { get; set; } 
        [JsonProperty("workpiece")] public int m_iWorkpiece { get; set; } 



    }
}
