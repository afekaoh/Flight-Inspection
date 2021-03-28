using System;
using System.Runtime.InteropServices;
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
using Flight_Inspection.controls.FlightGear;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for FlightGearControl.xaml
    /// </summary>
    public partial class FlightGearControl : UserControl
    {

        public String ProcPath { get; set; }
        private readonly TimeSeries TS;
        private readonly Process FG;
        Socket soc;
        private readonly IPEndPoint remoteEP;
        public FlightGearControl()
        {
            InitializeComponent();
            this.TS = new TimeSeries("C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\controls\\FlightGear\\reg_flight.csv");
            this.ProcPath = ProcPath = "C:\\Program Files\\FlightGear 2020.3.6\\bin";
            FG = new Process();
            FG.StartInfo.FileName = ProcPath + "\\fgfs.exe";
            FG.StartInfo.WorkingDirectory = ProcPath;
            FG.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[1];
            remoteEP = new IPEndPoint(ipAddress, 5400);
            soc = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FG.Start();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            soc.Connect(remoteEP);
            var rows = TS.Rows;
            if (soc.Connected)
            {
                Console.WriteLine("yay");
                using (NetworkStream networkStream = new NetworkStream(soc))
                {
                    rows.ForEach(r =>
                    {
                        byte[] vs = Encoding.ASCII.GetBytes(r + "\n");
                        networkStream.Write(vs, 0, vs.Length);
                    });
                }
            }
        }
    }
}
