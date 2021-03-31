using Flight_Inspection.controls.FlightGear;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for FlightGearControl.xaml
    /// </summary>
    public partial class FlightGearControl : UserControl
    {

        public String ProcPath { get; set; }
        private const string InputUri = "C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\controls\\FlightGear\\playback_small.xml";
        private const string CsvFileName = "C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\controls\\FlightGear\\reg_flight.csv";
        private readonly TimeSeries TS;
        private readonly Process FG;
        private readonly Thread t;
        public FlightGearControl()
        {
            InitializeComponent();
            this.TS = new TimeSeries(CsvFileName, InputUri);
            this.ProcPath = "C:\\Program Files\\FlightGear 2020.3.6\\bin";
            FG = new Process();
            FG.StartInfo.FileName = ProcPath + "\\fgfs.exe";
            FG.StartInfo.WorkingDirectory = ProcPath;
            FG.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
            t = new Thread(Send_Data);
        }

        private void Start_FlightGear(object sender, RoutedEventArgs e)
        {
            FG.Start();
        }

        private void Start_Simulation(object sender, RoutedEventArgs e)
        {
            if (!t.IsAlive)
                t.Start();
        }

        private void Send_Data()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[1];
            var remoteEP = new IPEndPoint(ipAddress, 5400);
            using (var soc = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                soc.Connect(remoteEP);
                var rows = TS.Rows;
                if (soc.Connected)
                {

                    foreach (var buffer in from string r in rows
                                           let buffer = Encoding.ASCII.GetBytes(r + "\n")
                                           select buffer)
                    {
                        soc.Send(buffer);
                        Thread.Sleep(100);
                    }

                }
            }
        }

        ~FlightGearControl()
        {
            if (t.IsAlive)
            {
                t.Abort();
            }
            if (IsProccesRunning(FG))
            {
                FG.CloseMainWindow();
                FG.Close();
            }
        }

        private static bool IsProccesRunning(Process proc)
        {

            try
            {
                Process.GetProcessById(proc.Id);
            }
            // the procces hasn't started yet
            catch (InvalidOperationException) { return false; }
            // the procces hasn't been initialized 
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException) { return false; }
            return true;
        }
    }

}

