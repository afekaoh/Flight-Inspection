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

        public float Direction
        {
            get => direction;
            private set
            {
                direction = value;
                OnPropertyChanged("direction");
            }
        }
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

        public float Airspeed
        {
            get => airspeed;
            private set
            {
                airspeed = value;
                OnPropertyChanged("airspeed");
            }

        }

        public float Altimeter
        {
            get => altimeter;
            private set
            {
                airspeed = value;
                OnPropertyChanged("altimeter");
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
                Yaw = ts.getFeatureData("heading-deg").ElementAt(counter);
                Pitch = ts.getFeatureData("pitch-deg").ElementAt(counter);
                Roll = ts.getFeatureData("roll-deg").ElementAt(counter);
                Airspeed = ts.getFeatureData("airspeed-kt").ElementAt(counter);
                Altimeter = ts.getFeatureData("altitude-ft").ElementAt(counter);
                Direction = ts.getFeatureData("magnetic-compass_indicated-heading-deg").ElementAt(counter);

                Console.WriteLine($"{yaw} {pitch}  {roll} {airspeed} {altimeter} {Direction}");
                Thread.Sleep(10);
                counter++;
            }
        }
    }
}