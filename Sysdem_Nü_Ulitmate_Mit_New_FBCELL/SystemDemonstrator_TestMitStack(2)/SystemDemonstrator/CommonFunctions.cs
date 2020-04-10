using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SystemDemonstrator
{
    class CommonFunctions
    {

        public bool ReadJSonFile(string sPath, ref string srefJSon )
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(sPath))
                {
                    // Read the stream to a string, and write the string to the console.
                    srefJSon = sr.ReadToEnd();
                    //Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

    }
}
