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

namespace Flight_Inspection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var procPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\FlightGear 2020.3.6";
            Process proc = new Process();
            proc.StartInfo.FileName = "C:\\Program Files\\FlightGear 2020.3.6\\bin\\fgfs.exe";
            proc.StartInfo.WorkingDirectory = "C:\\Program Files\\FlightGear 2020.3.6\\bin";
            proc.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            proc.Start();
        }

        private void b1_KeyUp(object sender, KeyEventArgs e)
        {
            b1.
        }
    }
}
