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
            model.CurrentTime++;
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
            model.TimeSeries = settingsArgs.ts;
            model.MaxSlider = settingsArgs.ts.Rows.Count;
        }

        internal override void setTime(int time)
        {
            throw new NotImplementedException();
        }

        private int currentTime;

        public int CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                currentTime = value;
                OnPropertyChanged();
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
                this.CurrentTime = model.CurrentTime;
        }


    }

}
