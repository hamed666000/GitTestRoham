using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace restClient_0
{
    class ADSWriteInterface
    {
        //TcAdsClient tcClient = new TcAdsClient();
        //AdsStream dataStream = new AdsStream(4);
        //AdsBinaryReader binReader = new AdsBinaryReader(dataStream);

        public bool writeToPLC (string sName, int iValue)
        {
            TcAdsClient tcClient = new TcAdsClient();
            AdsStream dataStream = new AdsStream(4);
            AdsBinaryReader binReader = new AdsBinaryReader(dataStream);
            int iHandle = 0;
            tcClient.Connect(851);
            iHandle = tcClient.CreateVariableHandle(sName);

            try
            {
                tcClient.WriteAny(iHandle, iValue);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Console.ReadKey();
            }
            finally
            {
                //Delete variable handle
                tcClient.DeleteVariableHandle(iHandle);
                tcClient.Dispose();
            }

            return true;
        }

    }
}
