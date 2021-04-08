using Flight_Inspection.Pages.FlightGear;
using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Flight_Inspection.controls;

namespace Flight_Inspection.Pages.FlightGear
{
    class FlightGearViewModel : IPagesViewModel
    {
        private FlightGearModel flightGearModel;
        private TimeSeries ts;
        public event EventHandler Start;
        private bool ready;


        public void OnStart()
        {
            Start?.Invoke(this, EventArgs.Empty);
        }
        public TimeSeries Ts { get => ts; set => ts = value; }

        public FlightGearViewModel()
        {
            this.ready = false;
            this.flightGearModel = new FlightGearModel();
        }

        public void StartFG()
        {
            if (ready)
                flightGearModel.StartFG();

        }

        public void StartPlay(System.Windows.RoutedEventArgs e)
        {
            if (ready)
            {
                /*flightGearModel.StartPlay();*/
                OnStart();
            }
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
            flightGearModel.setSettings(settingsArgs.ts, settingsArgs.procPath);
            this.Ts = settingsArgs.ts;
            this.ready = true;
        }

        public override void UpdateSettings()
        {
        }
    }
}
