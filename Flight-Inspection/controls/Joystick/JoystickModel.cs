using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Joystick
{

    class JoystickModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries ts;
        private float aileron;
        private float rudder;
        private float elevator;
        private float throttle;
        private int currentTime=0;

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
                OnPropertyChanged("aileron");
            }
        }

        public float Rudder
        {
            get => rudder;
            private set
            {
                rudder = value;
                OnPropertyChanged("rudder");
            }

        }

        public float Elevator
        {
            get => elevator;
            private set
            {
                elevator = value;
                OnPropertyChanged("elevator");
            }

        }

        public float Throttle
        {
            get => throttle;
            private set
            {
                throttle = value;
                OnPropertyChanged("throttle");
            }

        }

        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void sendData()
        {
            int size = ts.getFeatureData("aileron").Count;
            int counter = currentTime;
            while (counter < size)
            {
                Aileron = ts.getFeatureData("aileron").ElementAt(counter);
                Rudder = ts.getFeatureData("rudder").ElementAt(counter);
                Elevator = ts.getFeatureData("elevator").ElementAt(counter);
                Throttle = ts.getFeatureData("throttle").ElementAt(counter);
                Thread.Sleep(10);
                counter++;
            }
        }
        public float maxAbs (String feature){

            float minVal = (float) Math.Abs(ts.getFeatureData(feature).Min());
            float maxVal = Math.Abs(ts.getFeatureData(feature).Max());
            if(minVal >= maxVal)
            {
                return minVal;
            }
            if (minVal < maxVal)
            {
                return maxVal;
            }
            return 0;
        } 
    }
}
