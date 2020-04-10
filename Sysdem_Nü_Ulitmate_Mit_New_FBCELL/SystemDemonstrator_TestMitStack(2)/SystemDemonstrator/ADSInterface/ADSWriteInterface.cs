using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace SystemDemonstrator
{
    class ADSWriteInterface
    {
        //TcAdsClient tcClient = new TcAdsClient();
        //AdsStream dataStream = new AdsStream(4);
        //AdsBinaryReader binReader = new AdsBinaryReader(dataStream);

        public bool writeToPLCInt(string sName, int iValue)
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
                
            }
            finally
            {
                tcClient.DeleteVariableHandle(iHandle);
                tcClient.Dispose();
            }

            return true;
        }

        public bool writeToPLCString(string sName, string sValue)
        {
            bool bret = true;
            TcAdsClient tcClient = new TcAdsClient();
            AdsStream dataStream = new AdsStream(100);
            AdsBinaryReader binReader = new AdsBinaryReader(dataStream);

            int iHandle = 0;
            tcClient.Connect(851);
            iHandle = tcClient.CreateVariableHandle(sName);

            try
            {
                tcClient.WriteAnyString(iHandle, sValue, sValue.Length,Encoding.Default);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                bret = false;
            }
            finally
            {
                tcClient.DeleteVariableHandle(iHandle);
                tcClient.Dispose();
            }

            return bret;
        }
    }
}
