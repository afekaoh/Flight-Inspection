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
        private float aileron=0;
        private float rudder;
        private float elevator;
        private float throttle;

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
        public void start()
        {
            int size = ts.getFeatureData("aileron").Count;
            for (int i = 0; i < size; i++)
            {
                Aileron = ts.getFeatureData("aileron").ElementAt(i);
                Rudder = ts.getFeatureData("rudder").ElementAt(i);
                Elevator = ts.getFeatureData("elevator").ElementAt(i);
                Throttle = ts.getFeatureData("throttle").ElementAt(i);
                Console.WriteLine($"{aileron} {rudder}  {elevator} {throttle}");
                Thread.Sleep(10);
            }
        }
    }
}
