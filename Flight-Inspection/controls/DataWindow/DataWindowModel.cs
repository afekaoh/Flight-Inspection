// oz rigler 316291897 15/04/2021
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
    // a model class
    // that is responsible for holding all the relevant data for the datas for the currnt control 
    class DataWindowModel : INotifyPropertyChanged
    {
        // event that will be activated when a property gets a new value
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries ts;
        private float airspeed;
        private float altimeter;
        private float direction;
        private int currentTime = 0;
        private float yaw;
        private float pitch;
        private float roll;
        // recive a time from the controlView that sets the properties to the currect line in the timeSeries 
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
        // when a property get a new value, PropertyChanged event will notify the VM that the property change and will inform which one

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
        //  responsible for updating the datas according the current time
        public void sendData()
        {     
                Yaw = ts.GetFeatureData("side-slip-deg").ElementAt(CurrentTime);
                Pitch = ts.GetFeatureData("pitch-deg").ElementAt(CurrentTime);
                Roll = ts.GetFeatureData("roll-deg").ElementAt(CurrentTime);
                AirSpeed = ts.GetFeatureData("airspeed-kt").ElementAt(CurrentTime);
                Altimeter = ts.GetFeatureData("altitude-ft").ElementAt(CurrentTime);
                Direction = ts.GetFeatureData("magnetic-compass_indicated-heading-deg").ElementAt(CurrentTime);
        }
        // needed for nomalization of the view so the max value will be possitioned at the maxium view range
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