using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace restClient_0
{

    class cBaseAction
    {
        public String getDataSet()
        {
            return (JsonConvert.SerializeObject(this));
        }

    }

    class cPost : cBaseAction
    {
        public string title { get; set; }
        public string body { get; set; }
        public string userId { get; set; }



    }
    class cMirRequestGetPLC : cBaseAction
    {
        public string ID { get; set; }

    }

    class cMirRequestPutPLC : cBaseAction
    {
        public string value { get; set; }
        public string label { get; set; }
 
    }

    class cMirRegister : cBaseAction
    {
        public string id { get; set; }
        public string label { get; set; }
        public string url { get; set; }
        public string value { get; set; }

    }
}
