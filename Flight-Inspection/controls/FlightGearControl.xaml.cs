using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for FlightGearControl.xaml
    /// </summary>
    public partial class FlightGearControl : UserControl
    {
       public String procPath { get; set; }
        public FlightGearControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            procPath = "C:\\Program Files\\FlightGear 2020.3.6\\bin";
            Process proc = new Process();
            proc.StartInfo.FileName = procPath + "\\fgfs.exe";
            proc.StartInfo.WorkingDirectory = procPath;
            proc.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            proc.Start();
        }
    }
}
