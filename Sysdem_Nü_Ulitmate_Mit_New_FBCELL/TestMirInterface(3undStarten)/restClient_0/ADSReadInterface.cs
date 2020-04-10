using System;
using System.IO;
using TwinCAT.Ads;
using System.Windows.Forms;


// Diese Klasse Erzeugt ein ADS-"Interface" für eine Integer Variable. Der Name der Varibalen muss in sVariableName angegeben werden. BSP MAIN.iTest

// Es ist darauf zu achten, dass die Varibalen Typen von PLC und Windows unterschiedlich definiert sind PLC:DINT = Win:int




namespace restClient_0
{
    class ADSReadInterface
    {
        private TcAdsClient tcClient;
        private int[] hConnect;
        private AdsStream dataStream;
        private BinaryReader binRead;
        private int[] aiData;
        private string m_sVariableNumber { get; set; } // Variablen Name
        private string m_sVariabelName { get; set; } // Variablen Name

        public ADSReadInterface(string sVariableNumber )
        {
            m_sVariableNumber = sVariableNumber;

            m_sVariabelName = "MAIN.fbMHS.aMHS[xx]";

            m_sVariabelName = m_sVariabelName.Replace("xx", sVariableNumber);

            InitializeInterface();

        }



        public void InitializeInterface()
        {

            dataStream = new AdsStream(31);
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
                hConnect[0] = tcClient.AddDeviceNotification(m_sVariabelName, dataStream, 0, 2, AdsTransMode.OnChange, 10, 0, aiData);
                //hConnect[1] = tcClient.AddDeviceNotification("MAIN.intVal", dataStream, 1, 2, AdsTransMode.OnChange, 100, 0, tbInt);
            }
            catch (Exception err)
            {
                bool b = true;
                while (b)
                {
                    string message = "Connection to PLC-Variables failed. Please Run TwinCat or check declared variables and retry";
                    string caption = "Connection failed!";
                    MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    if (result != DialogResult.Retry)
                    {
                        b = false;
                    }
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

            int f = 0;
            string sID;
            if (e.NotificationHandle == hConnect[0])
                f = binRead.ReadInt32();

            RESTClient rClient = new RESTClient();

            sID = "http://www.mir.com/api/v2.0.0/registers/xx";
            sID = sID.Replace("xx", m_sVariableNumber);
            rClient.endPoint = sID;
            
            rClient.makeRequestMir( f);

        }

    }
}
