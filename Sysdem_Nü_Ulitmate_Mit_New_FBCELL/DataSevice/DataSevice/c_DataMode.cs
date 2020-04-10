using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSevice
{
    class c_DataMode
    {
        // In diesem Modus wird eine einfache Kopier-Aktion durchgeführt
        public bool DataMode(string sSource, string sTarget, string sValueFile)
        {
            try { 
            string sourceFile = System.IO.Path.Combine(sSource, sValueFile);
            string destFile = System.IO.Path.Combine(sTarget, sValueFile);

            System.IO.Directory.CreateDirectory(sTarget);

            System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
            return true;
        }


    }
}
