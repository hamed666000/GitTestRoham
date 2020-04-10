using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace restClient_0
{
    class ObserverThreadMHS
    {

        

        public int iTest { get; set; }

        

        public ObserverThreadMHS()
        {

        }

        public static void Method(object obj)
        {
            ADSWriteInterface Interface = new ADSWriteInterface();

            while (true)
            {
                List<cMirRegister> RegisterList;

                string sRequest;

                RESTClient rClient = new RESTClient();

                rClient.endPoint = "http://www.mir.com/api/v2.0.0/registers";


                sRequest = rClient.makeRequestMiRReg();
                try
                {
                    RegisterList = JsonConvert.DeserializeObject<List<cMirRegister>>(sRequest);


                    for (int i = 10; i < 20; i++) // Nur die Register ab 11-20 werden von dem MHS and die PLC geschickt (1-10 von PLC zu MHS)
                    {


                        int iValue;
                        int.TryParse(RegisterList[i].value, out iValue);
                        string sName = "MAIN.fbMHS.aMHS[x]";
                        sName = sName.Replace("x", RegisterList[i].id);


                        Interface.writeToPLC(sName, iValue);

                    }
                }
                catch(Exception e)
                {
                    // ThFe

                }
                // ADS Übertragung der Register


                Thread.Sleep(1000);

            }
        }


        public static void MethodTest0(object obj)
        {
            int value = (int)obj;
            ADSWriteInterface Interface = new ADSWriteInterface();

            while (true)
            {
                List<cMirRegister> RegisterList;

                string sTest = "[{\"id\":1,\"label\":\"\",\"url\":\"/v2.0.0/registers/1\",\"value\":77},{\"id\":2,\"label\":\"\",\"url\":\"/v2.0.0/registers/2\",\"value\":0},{\"id\":3,\"label\":\"\",\"url\":\"/v2.0.0/registers/3\",\"value\":0},{\"id\":4,\"label\":\"\",\"url\":\"/v2.0.0/registers/4\",\"value\":0},{\"id\":5,\"label\":\"\",\"url\":\"/v2.0.0/registers/5\",\"value\":0}]";

                RegisterList = JsonConvert.DeserializeObject<List<cMirRegister>>(sTest);


                for (int i = 11; i < 20; i++)
                {


                    int iValue;
                    int.TryParse(RegisterList[i].value, out iValue);
                    string sName = "MAIN.fbMHS.aMHS[x]";
                    sName = sName.Replace("x", RegisterList[i].id);


                    Interface.writeToPLC(sName, iValue);

                }
   

                Thread.Sleep(1000);
            }


        }

    }
}
