using System;
using System.IO;
using TwinCAT.Ads;
using System.Windows;
using SystemDemonstrator;


// Diese Klasse Erzeugt ein ADS-"Interface" für eine Integer Variable. Der Name der Varibalen muss in sVariableName angegeben werden. BSP MAIN.iTest

// Es ist darauf zu achten, dass die Varibalen Typen von PLC und Windows unterschiedlich definiert sind PLC:DINT = Win:int




namespace SystemDemonstrator.ADSInterface
{
    class ADSReadInterface
    {
        private TcAdsClient tcClient;
        private int[] hConnect;
        private AdsStream dataStream;
        private BinaryReader binRead;
        private int[] aiData;
        private string m_String;
        private string m_sVariableNumber { get; set; } // Variablen Name
        private string m_sVariabelName { get; set; } // Variablen Name

        public delegate void EventDelegate(string str);

        private event EventDelegate eventHandler;


        public void Registrate(c_Base_ADSReadInterface m)
        {
            eventHandler += m.Update;
            m_String = "TestRegistr";
        }


        public ADSReadInterface(string sVariableName )
        {

            m_sVariabelName = sVariableName;


            InitializeInterface();

        }



        public void InitializeInterface()
        {

            dataStream = new AdsStream(255);
            //Encoding wird auf ASCII gesetzt, um Strings lesen zu können
            binRead = new BinaryReader(dataStream, System.Text.Encoding.ASCII);
            // Instanz der Klasse TcAdsClient erzeugen
            tcClient = new TcAdsClient();
            // Verbindung mit Port 851 auf dem lokalen Computer herstellen
            tcClient.Connect(851);

            hConnect = new int[7];
            aiData = new int[7];

            try
            {
                hConnect[0] = tcClient.AddDeviceNotification(m_sVariabelName, dataStream, 0, 255, AdsTransMode.OnChange, 500, 0, aiData);
                //hConnect[1] = tcClient.AddDeviceNotification("MAIN.intVal", dataStream, 1, 2, AdsTransMode.OnChange, 100, 0, tbInt);
            }
            catch (Exception err)
            {
                bool b = true;
                while (b)
                {
                    string message = "Connection to PLC-Variables failed. Please Run TwinCat or check declared variables and retry";
                    string caption = "Connection failed!";
                }

            }

            tcClient.AdsNotification += new AdsNotificationEventHandler(OnNotification);
        }

        public void delete ()
        {
            binRead.Close();
            tcClient.DeleteDeviceNotification(hConnect[0]);
            tcClient.Disconnect();

            hConnect[0] = 0;


        }
        private void OnNotification(object sender, AdsNotificationEventArgs e)
        {


            if (e.NotificationHandle == hConnect[0])
            {
                try
                {
                    
                    m_String = new String(binRead.ReadChars(255));
                 
                    m_String = m_String.Substring(0, m_String.IndexOf('\0'));// Auf nutzbare Länge kürzen (bis endof-file)
                  //  m_String.

                }
                catch (Exception err)
                {
                    bool b = true;
                    while (b)
                    {
                        string message = "Connection to PLC-Variables failed. Please Run TwinCat or check declared variables and retry";
                        string caption = "Connection failed!";
                    }

                }

                eventHandler(m_String);



               

            }

        }

    }
}
