using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SystemDemonstrator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        

        public MainWindow()
        {
            


            InitializeComponent();
            //
            // The DataContext serves as the starting point of Binding Paths
            DataContext = new c_MainViewModel();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            int iA, iB;
            if (int.TryParse(tbWPID.Text, out iA) && int.TryParse(tbCell.Text, out iB))
            {
                ADSWriteInterface Interface = new ADSWriteInterface();

                //Interface.writeToPLCInt("MAIN.m_iWPRequest", int.Parse(tbAction.Text));
                //Interface.writeToPLCInt("MAIN.m_iCellRequest", int.Parse(tbCell.Text));
                //Interface.writeToPLCInt("MAIN.m_iActionRequest", int.Parse(tbWPID.Text));

            }

        }

    }
}
