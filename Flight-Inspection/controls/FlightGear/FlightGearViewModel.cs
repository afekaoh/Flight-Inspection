using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.FlightGear
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private FlightGearModel flightGearModel;
        private TimeSeries ts;
        private bool ready;

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

        public void StartPlay()
        {
            if (ready)
                flightGearModel.StartPlay();

        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void setSettings(TimeSeries ts, string procPath)
        {
            flightGearModel.setSettings(ts, procPath);
            this.ready = true;
        }
    }
}
