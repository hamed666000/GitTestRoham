using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace VirtualCellHandling
{
    class c_FileWatcher
    {
        bool m_bSemaphore;
        private List<c_CellConfig> m_ListOfConfigobj;

        public c_FileWatcher()
        {
            string sJSon = "";
            try
            {
                using (StreamReader sr = new StreamReader("configCell.json"))
                {
                    sJSon = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read: configCell.json");
                Console.WriteLine(e.Message);
            }

            m_ListOfConfigobj = JsonConvert.DeserializeObject<List<c_CellConfig>>(sJSon);
            
        }

        public void Run(String sPathOfActions, String sJSon)
        {
            
            if (!Directory.Exists(sPathOfActions))
            {
                Console.WriteLine("The File does not exist");
                return;
            }


            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = sPathOfActions;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                //watcher.NotifyFilter = NotifyFilters.CreationTime;//NotifyFilters.LastWrite;

                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
|                                   NotifyFilters.FileName;

                // Only watch text files.
                watcher.Filter = "*.json";

                // Add event handlers.
                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;


                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the connection.");
                while (Console.Read() != 'q') ;
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {

            
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

                c_AppExeThread cAET = new c_AppExeThread(e.FullPath, m_ListOfConfigobj);

                Thread thread = new Thread(new ThreadStart(cAET.Method));

                thread.Start();
            
        }

    }
}
