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
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                MaxSlider = model.MaxSlider;
            };
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
                OnPropertyChanged("MaxSlider");
            }
        }

        private void INotifyPropertyChanged(string v)
        {
            if (v == "MaxSlider")
            {
                maxSlider = model.MaxSlider;
            }
        }

        public void MaxSliderUpdate(int u)
        {
            model.MaxSlider = u;
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {

        }

        private int currentTime;

        public int CurrentTime
        {
            get
            {
                return currentTime;
            }
            set { currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        private int timeSeries;

        public int TimeSeries
        {
            get { return timeSeries; }
            set { timeSeries = value; }
        }




    }

}


