using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Video
{
    class VideoPanelModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int maxSlider=1;


        public int MaxSlider
        {
            get
            {
                return maxSlider;
            }
            set
            {
                maxSlider = value;
                INotifyPropertyChanged("MaxSlider");
            }
        }

        private void INotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
    }
}
