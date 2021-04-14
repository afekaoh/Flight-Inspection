using Flight_Inspection.controls;
using Flight_Inspection.Windows.FligthData;
using System;
using System.ComponentModel;
using System.Linq;

namespace Flight_Inspection.Pages.FlightGear
{
    class FlightGearViewModel : IPagesViewModel
    {
        private FlightGearModel flightGearModel;
        private TimeSeries ts;
        public event EventHandler Start;
        private bool ready;

        FlightDataViewModel dataViewModel;
        SettingsArgs settings;
        public FlightDataViewModel DataViewModel
        {
            get => dataViewModel;
            set
            {
                if (dataViewModel == value)
                {
                    return;
                }

                dataViewModel = value;
                OnPropertyChanged();
            }
        }

        public void OnStart()
        {
            Start?.Invoke(this, EventArgs.Empty);
        }
        public TimeSeries Ts { get => ts; set => ts = value; }

        public FlightGearViewModel()
        {
            this.ready = false;
            this.flightGearModel = new FlightGearModel();
            this.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "DataViewModel")
                {
                    DataViewModel.SetTimeEvent += SetTime;
                    DataViewModel.SetStopEvent += SetStop;
                }
            };
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
                flightGearModel.StartPlay();
                OnStart();
            }
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
            flightGearModel.SetSettings(settingsArgs.Ts, settingsArgs.ProcPath);
            this.settings = settingsArgs;
            this.Ts = settingsArgs.Ts;
            this.ready = true;
        }

        public override void UpdateSettings()
        {
            if (!(dataViewModel is null))
                dataViewModel.SetSettings(settings);
        }

        void SetTime(object sender, SetTimeEventArgs e)
        {
            flightGearModel.CurrentTime = e.Time;
        }

        void SetStop(object sender, SetStopEventArgs e)
        {
            flightGearModel.Play = !e.Stop;
        }
    }
}
