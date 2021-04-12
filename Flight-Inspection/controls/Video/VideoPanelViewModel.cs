using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Video
{
    class VideoPanelViewModel : IControlViewModel
    {

        private int maxSlider;
        private VideoPanelModel model;


        public VideoPanelViewModel()
        {
            this.model = new VideoPanelModel();
            model.PropertyChanged += UpdateCurrentTime;
            model.PropertyChanged += MaxSliderUpdate;
            model.PropertyChanged += UpdateStop;
        }

        public int MaxSlider
        {
            get
            {
                return maxSlider;
            }
            set
            {
                maxSlider = value;
                OnPropertyChanged();
            }
        }

        public void MaxSliderUpdate(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "MaxSlider")
                this.MaxSlider = model.MaxSlider;
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.TimeSeries = settingsArgs.Ts;
            model.MaxSlider = settingsArgs.Ts.Rows.Count;
        }

        internal override void setTime(int time)
        {
            Time = time;
        }

        private volatile int currentTime;

        public int Time
        {
            get
            {
                return currentTime;
            }
            set
            {
                if (currentTime != value && value<maxSlider)
                {
                    currentTime = value;
                    model.CurrentTime = value;
                    OnPropertyChanged(value);
                }
            }
        }


        internal void StartPlay()
        {
            model.StartPlay();
        }

        internal void Pause()
        {
            model.Pause();
        }

        private int timeSeries;

        public int TimeSeries
        {
            get { return timeSeries; }
            set { timeSeries = value; }
        }

        public void UpdateCurrentTime(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "CurrentTime")
                this.Time = model.CurrentTime;
        }

        public void UpdateStop(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "Stop")
                this.Stop = model.Stop;
        }

        private bool stop;

        public bool Stop
        {
            get { return stop; }
            set
            {
                stop = value;
                OnPropertyChanged(value);
            }
        }

    }

}
