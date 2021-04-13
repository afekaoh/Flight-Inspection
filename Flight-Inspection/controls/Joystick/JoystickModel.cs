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
            int size = ts.GetFeatureData("aileron").Count;
            int counter = currentTime;
            while (counter < size)
            {
                Aileron = ts.GetFeatureData("aileron").ElementAt(counter);
                Rudder = ts.GetFeatureData("rudder").ElementAt(counter);
                Elevator = ts.GetFeatureData("elevator").ElementAt(counter);
                Throttle = ts.GetFeatureData("throttle").ElementAt(counter);
                Console.WriteLine($"{aileron} {rudder}  {elevator} {throttle}");
                Thread.Sleep(10);
                counter++;
            }
        }
        public float maxAbs (String feature){

            float minVal = (float) Math.Abs(ts.GetFeatureData(feature).Min());
            float maxVal = Math.Abs(ts.GetFeatureData(feature).Max());
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
