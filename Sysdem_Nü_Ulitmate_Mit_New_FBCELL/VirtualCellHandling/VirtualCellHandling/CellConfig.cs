using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VirtualCellHandling
{

    class c_CellConfig
    {
        [JsonProperty("Name")] public String m_sName { get; set; }
        [JsonProperty("AppType")] public String m_sAppType { get; set; }
        [JsonProperty("AppPath")] public String m_sAppPath { get; set; }
        
    }

    class c_CellProperty

    {
        [JsonProperty("Name")] public String m_Name { get; set; }
        [JsonProperty("WPID")] public String m_WPID { get; set; }
    }
}


