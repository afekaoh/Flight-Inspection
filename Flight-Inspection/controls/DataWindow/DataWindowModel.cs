using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.DataWindow
{
    class DataWindowModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries ts;
        private float airspeed;
        private float altimeter;
        private float direction;
        private int currentTime = 0;
        private float yaw;
        private float pitch;
        private float roll;

        public int CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                sendData();
            }
        }
        public void SetSettings(SettingsArgs settingsArgs)
        {
            ts = settingsArgs.Ts;
        }
        public float Yaw
        {
            get => yaw;
            private set
            {
                yaw = value;
                OnPropertyChanged("yaw");
            }
        }

        public float Pitch
        {
            get => pitch;
            private set
            {
                pitch = value;
                OnPropertyChanged("pitch");
            }

        }

        public float Roll
        {
            get => roll;
            private set
            {
                roll = value;
                OnPropertyChanged("roll");
            }

        }

        public float AirSpeed
        {
            get => airspeed;
            private set
            {
                airspeed = value;
                OnPropertyChanged("airSpeed");
            }

        }

        public float Altimeter
        {
            get => altimeter;
            private set
            {
                altimeter = value;
                OnPropertyChanged("altimeter");
            }

        }
        public float Direction
        {
            get => direction;
            private set
            {
                direction = value;
                OnPropertyChanged("direction");
            }
        }
        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void sendData()
        {     
                Yaw = ts.GetFeatureData("side-slip-deg").ElementAt(CurrentTime);
                Pitch = ts.GetFeatureData("pitch-deg").ElementAt(CurrentTime);
                Roll = ts.GetFeatureData("roll-deg").ElementAt(CurrentTime);
                AirSpeed = ts.GetFeatureData("airspeed-kt").ElementAt(CurrentTime);
                Altimeter = ts.GetFeatureData("altitude-ft").ElementAt(CurrentTime);
                Direction = ts.GetFeatureData("magnetic-compass_indicated-heading-deg").ElementAt(CurrentTime);
        }

        public float maxVal(String feature)
        {
            if (ts == null)
                return 0;
            return (float)ts.GetFeatureData(feature).Max();
        }
        public float minVal(String feature)
        {
            if (ts == null)
                return 0;
            return (float)ts.GetFeatureData(feature).Min();
        }
    } 
}