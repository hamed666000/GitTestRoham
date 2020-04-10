using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace restClient_0
{
    public partial class Form1 : Form
    {

        private ObserverThreadMHS Observer;
        private bool bBtnStarted;
        private List<ADSReadInterface> AdsInterfaceList;
        private Thread InstanceCaller;
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdGO_Click(object sender, EventArgs e)
        {



            string strJSON = string.Empty;
            if (!CBMirConnection.Checked)
            {
                RESTClient rClient = new RESTClient();
                rClient.endPoint = txtRequestURI.Text;
                debugOutput("RESTClient Object created.");

                strJSON = rClient.makeRequest();

                

            }
            else
            {      

                RESTClient rClient = new RESTClient();

                rClient.endPoint = "http://www.mir.com/api/v2.0.0/registers/1";

                debugOutput("RESTClient Object created.");
                int iA, iB;
                if (int.TryParse(tbID.Text, out iA) && int.TryParse(tbValue.Text, out iB))
                {
                    strJSON = rClient.makeRequestMir( iB);
                }
                else
                {
                    // Initializes the variables to pass to the MessageBox.Show method.
                    string message = "Please insert both Numbers";
                    string caption = "Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    //if (result == System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    // Closes the parent form.
                    //    this.Close();
                    //}


                }
            }


            debugOutput(strJSON);
        }

        private void debugOutput(string strDebugText)
        {
            try
            {
                System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);
                txtResponse.Text = txtResponse.Text + strDebugText + Environment.NewLine;
                txtResponse.SelectionStart = txtResponse.TextLength;
                txtResponse.ScrollToCaret();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message, ToString() + Environment.NewLine);
            }
        }

        private httpVerb ParseCombo()
        {
            httpVerb Parsed;
            Object selectedItem = comBoxAction.SelectedItem;
            ;
            switch (selectedItem.ToString())
            {
                case "GET":
                    Parsed = httpVerb.GET;
                    break;
                case "POST":
                    Parsed = httpVerb.POST;
                    break;
                case "PUT":
                    Parsed = httpVerb.PUT;
                    break;
                case "DELETE":
                    Parsed = httpVerb.DELETE;
                    break;
                default:
                    Parsed = httpVerb.GET;
                    break;

            }



            return Parsed;
        }

        private void RbTwinCat_CheckedChanged(object sender, EventArgs e)
        {
            if(rbTwinCat.Checked)
            {
                rbGUI.Checked = false;
            }
            else
            {
                rbGUI.Checked = true;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //Observer = new ObserverThreadMHS() ;// Startet und beendet einen Thread zur Zyklischen Abfrage der MHS Register-Variablen

            AdsInterfaceList = new List<ADSReadInterface>();

            if (bBtnStarted == false)
            {

                for (int i = 1; i <= 10; i++)
                {


                    ADSReadInterface Inter = new ADSReadInterface(i.ToString());
                    AdsInterfaceList.Add(Inter);
                }

                //Observer.MethodTest0(sTest);MethodTest0
                InstanceCaller = new Thread(ObserverThreadMHS.Method);

                InstanceCaller.Start(2);
                bBtnStarted = true;
                btnStart.Text = "Stop";
            }
            else
            {

                InstanceCaller.Abort();


                while(AdsInterfaceList.Count >0)
                {

                    AdsInterfaceList[0].delete();

                }
                AdsInterfaceList.Clear();


                btnStart.Text = "Start";

                bBtnStarted = false;

            }


        }

        private void txtRequestURI_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
