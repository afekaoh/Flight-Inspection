using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.FlightGear
{
    class FlightGearModel
    {
        private TimeSeries TS;
        private readonly Process FG;
        private readonly Thread t;

        public FlightGearModel()
        {
            FG = new Process();
            t = new Thread(Send_Data);
        }


        public void StartFG()
        {
            FG.Start();
        }

        public void StartPlay()
        {
            t.IsBackground = true;
            t.Start();
        }


        private void Send_Data()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[1];
            var remoteEP = new IPEndPoint(ipAddress, 5400);
            var stopwatch = new Stopwatch();
            using (var soc = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                soc.Connect(remoteEP);
                var rows = TS.Rows;
                if (soc.Connected)
                {
                    Console.WriteLine("yay");
                    foreach (var buffer in from string r in rows
                                           let buffer = Encoding.ASCII.GetBytes(r + "\n")
                                           select buffer)
                    {
                        stopwatch.Start();
                        soc.Send(buffer);
                        stopwatch.Stop();
                        int sleepTime = (int)Math.Max(0, 100 - stopwatch.ElapsedMilliseconds);
                        Thread.Sleep(sleepTime);
                    }

                    /*for (; speed < rows.Count; speed++)
                    {
                        var buffer = Encoding.ASCII.GetBytes(rows[speed] + "\n");
                        soc.Send(buffer);
                        Thread.Sleep(100);
                    }*/
                }
            }
        }

        internal void setSettings(TimeSeries ts, string procPath)
        {
            this.TS = ts;
            FG.StartInfo.FileName = procPath + "\\fgfs.exe";
            FG.StartInfo.WorkingDirectory = procPath;
            FG.StartInfo.Arguments = "--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small --fdm=null";
        }

        ~FlightGearModel()
        {
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
