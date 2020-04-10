using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace SystemDemonstrator
{
    public sealed class c_Config
    {
        private static c_Config instance = null;
        private static readonly object padlock = new object();

        public static String m_sSysDemStatusFile { get; set; }
        public static String m_sStorageFile { get; set; }
        public static String m_sRegenarationPathFile { get; set; }
        public static String m_sRegenarationPathApp { get; set; }
        public static String m_sProtocolPath { get; set; }
        public static bool bConfigErr { get; set; }

        c_Config()
        {


        }

        public static c_Config Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new c_Config();
                    }
                    return instance;
                }
            }
        }

    }
}