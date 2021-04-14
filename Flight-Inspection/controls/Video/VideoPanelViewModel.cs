using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //Video panel view model constructor.
        public VideoPanelViewModel()
        {
            this.model = new VideoPanelModel();
            model.PropertyChanged += UpdateCurrentTime;
            model.PropertyChanged += MaxSliderUpdate;
            model.PropertyChanged += UpdateStop;
        }
        //The maximum value on the slider.
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

        private double speed=0;

        public double Speed
        {
            get
            {
                if (speed==0)
                {
                    speed = 1;
                    OnPropertyChanged();
                }
                return speed;
            }
            set
            {
                if (speed != value)
                {
                    //Get the speed valuew from the model.
                    speed = value;
                    model.Speed = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<double> SpeedOptions { get => new ObservableCollection<double>() { 0.5,1,1.25,1.5,2}; }

        //This method is updating the maximum value of the slider.

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

        //The property of the slider's time. 
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

        //get the changed from the model and update the play video panel in the view
        internal void StartPlay()
        {
            model.StartPlay();
        }

        //get the changed from the model and update the pause button in the view
        internal void Pause()
        {
            model.Pause(); 
        }

        private int timeSeries;

        //Time series property. Getting the time series value. 
        public int TimeSeries
        {
            get { return timeSeries; }
            set { timeSeries = value; }
        }

        //Updating the curent time of the video panel
        public void UpdateCurrentTime(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "CurrentTime")
                this.Time = model.CurrentTime;
        }

        //Update the stop property -> and the view by getting the value from the model
        public void UpdateStop(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "Stop")
                this.Stop = model.Stop;
        }


        //The stop property
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
