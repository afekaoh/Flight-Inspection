// oz rigler 316291897 15/04/2021
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Joystick
    // a model class
    // that is responsible for holding all the relevant data for the datas for the currnt control 
{
    class JoystickModel : INotifyPropertyChanged
    {
        // event that will be activated when a property gets a new value
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries ts;
        private float aileron;
        private float rudder;
        private float elevator;
        private float throttle;
        private int currentTime;
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
        public float Aileron
        {
            get => aileron;
            private set
            {
                aileron = value;
                OnPropertyChanged();
            }
        }

        public float Rudder
        {
            get => rudder;
            private set
            {
                rudder = value;
                OnPropertyChanged();
            }

        }

        public float Elevator
        {
            get => elevator;
            private set
            {
                elevator = value;
                OnPropertyChanged();
            }

        }

        public float Throttle
        {
            get => throttle;
            private set
            {
                throttle = value;
                OnPropertyChanged();
            }

        }
        // activate the PropertyChanged event when a property get a new value
        // sending the name of the property
        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        // a method thats responsible for updating the properties according the currect time
        public void sendData()
        {
            Aileron = ts.GetFeatureData("aileron").ElementAt(CurrentTime);
            Rudder = ts.GetFeatureData("rudder").ElementAt(CurrentTime);
            Elevator = ts.GetFeatureData("elevator").ElementAt(CurrentTime);
            Throttle = ts.GetFeatureData("throttle").ElementAt(CurrentTime);
        }
        // needed for nomalization of the view so the max value will be possitioned at the maxium view range
        public float maxVal(String feature)
        {
            return (float)ts.GetFeatureData(feature).Max();
        }
        public float minVal(String feature)
        {
            return (float)ts.GetFeatureData(feature).Min();
        }
    }
}
