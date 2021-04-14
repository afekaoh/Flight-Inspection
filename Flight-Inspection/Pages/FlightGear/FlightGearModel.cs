using Flight_Inspection.controls;
using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection.Pages.FlightGear
{
    class FlightGearModel : INotifyPropertyChanged
    {
        private readonly Process FlightGearProcess;

        private bool play;
        private readonly Thread sendFlightDataThread;

        int time;
        private TimeSeries TS;

        public FlightGearModel()
        {
            FlightGearProcess = new Process();
            sendFlightDataThread = new Thread(Send_Data);
        }

        ~FlightGearModel()
        {
            // making sure that the process is closing when the user closing the application 
            if (IsProccesRunning(FlightGearProcess))
            {
                FlightGearProcess.CloseMainWindow();
                FlightGearProcess.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static bool IsProccesRunning(Process proc)
        {
            try
            {
                Process.GetProcessById(proc.Id);
            }
            // the process hasn't started yet
            catch (InvalidOperationException)
            {
                return false;
            }
            // the process hasn'sendFlightData been initialized 
            catch (Exception e) when (e is ArgumentException || e is ArgumentNullException)
            {
                return false;
            }
            return true;
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
                while (soc.Connected)
                {
                    if (play)
                    {
                        var line = rows[CurrentTime] + "\n";
                        var buffer = Encoding.ASCII.GetBytes(line);
                        soc.Send(buffer);
                        Thread.Sleep(10);
                    }
                }
            }
        }

        internal void SetSettings(TimeSeries ts, string procPath)
        {
            // setting some process related settings
            this.TS = ts;
            FlightGearProcess.StartInfo.FileName = procPath + "\\fgfs.exe";
            FlightGearProcess.StartInfo.WorkingDirectory = procPath;
            FlightGearProcess.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
        }

        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }


        public void StartFG()
        {
            // open the flight fear process
            FlightGearProcess.Start();
        }

        public void StartPlay()
        {
            if (sendFlightDataThread.ThreadState is System.Threading.ThreadState.Unstarted)
            {
                // making sure sure that closing the window will close the thread
                sendFlightDataThread.IsBackground = true;
                sendFlightDataThread.Start();
            }
            {
                // if the thread hasn'sendFlightData already been started
            }
        }

        // Does the App is playing now
        public bool Play { get { return play; } set { play = value; } }

        // What is the Currnt Time to play right now
        public int CurrentTime { get => time; set { time = value; } }
    }
}
