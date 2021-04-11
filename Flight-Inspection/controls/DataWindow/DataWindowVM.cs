using Flight_Inspection.controls.Joystick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.DataWindow
{
    class DataWindowVM : IControlViewModel
    {
        DataWindowModel model;
        List<NormelaizedData> datas;
        public event EventHandler Ready;


        DataWindowVM()
        {
            model = new DataWindowModel();
            model.PropertyChanged += TheModlePropertyChanged;
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
            throw new NotImplementedException();
        }

        internal override void setTime(int time)
        {
            model.CurrentTime = time;
            throw new NotImplementedException();
        }
        private void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }
        public float VM_Yaw
        {
            get { return findData("yaw").Data; }
            set
            {
                findData("yaw").Data = value * 300 + 300;
                OnPropertyChanged();
            }
        }

        public float VM_Pitch
        {
            get { return findData("pitch").Data; }
            set
            {
                findData("pitch").Data = 330 + value * 400;
                OnPropertyChanged();
            }
        }
        public float VM_Roll
        {
            get { return findData("roll").Data; }
            set
            {
                findData("roll").Data = value * 100 + 150;
                OnPropertyChanged();
            }
        }
        public float VM_Airspeed
        {
            get { return findData("airspeed").Data; }
            set
            {
                findData("airspeed").Data = 230 - value * 100;
                OnPropertyChanged();

            }
        }

        public float VM_Altimeter
        {
            get { return findData("altimeter").Data; }
            set
            {
                findData("altimeter").Data = 230 - value * 100;
                OnPropertyChanged();
            }
        }
        public float VM_Direction
        {
            get { return findData("direction").Data; }
            set
            {
                findData("direction").Data = 230 - value * 100;
                OnPropertyChanged();
            }
        }

        public NormelaizedData findData(string name) { return (NormelaizedData)datas.Find(JoyStickData => JoyStickData.Name == name); }

        public void addData(string name, float CanvasDim)
        {
            datas.Add(new NormelaizedData(name, CanvasDim, model.maxAbs(name)));
        }
        public void TheModlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "yaw")
            {
                VM_Yaw = model.Yaw;
            }
            else if (e.PropertyName == "pitch")
            {
                VM_Pitch = model.Pitch;
            }
            else if (e.PropertyName == "roll")
            {
                VM_Roll = model.Roll;
            }
            else if (e.PropertyName == "altimeter")
            {
                VM_Altimeter = model.Altimeter;
            }
            else if (e.PropertyName == "airSpeed")
            {
                VM_Airspeed = model.AirSpeed;
            }
            else if (e.PropertyName == "direction")
            {
                VM_Direction = model.Direction;
            }

        }
    }
}
