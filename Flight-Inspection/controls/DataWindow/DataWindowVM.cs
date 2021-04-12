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
        Dictionary<string, float> datas;
        public event EventHandler Ready;


        public DataWindowVM()
        {
            model = new DataWindowModel();
            datas = new Dictionary<string, float>();
            datas.Add("yaw", model.Yaw);
            datas.Add("pitch", model.Pitch);
            datas.Add("roll", model.Roll);
            datas.Add("altimeter", model.Altimeter);
            datas.Add("airSpeed", model.AirSpeed);
            datas.Add("direction", model.Direction);
            model.PropertyChanged += TheModlePropertyChanged;
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
            this.OnReady();
        }

        internal override void setTime(int time)
        {
            model.CurrentTime = time;
        }
        private void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }
        public float VM_Yaw
        {
            get { return datas["yaw"]; }
            set
            {
                datas["yaw"] = value;
                OnPropertyChanged();
            }
        }

        public float VM_Pitch
        {
            get { return datas["pitch"]; }
            set
            {
                datas["pitch"] =  value;
                OnPropertyChanged();
            }
        }
        public float VM_Roll
        {
            get { return datas["roll"]; }
            set
            {
                datas["roll"] = value;
                OnPropertyChanged();
            }
        }
        public float VM_Airspeed
        {
            get { return datas["airSpeed"]; }
            set
            {
                datas["airSpeed"] = value;
                OnPropertyChanged();

            }
        }

        public float VM_Altimeter
        {
            get { return datas["altimeter"]; }
            set
            {
                datas["altimeter"] =  value;
                OnPropertyChanged();
            }
        }
        public float VM_Direction
        {
            get { return datas["direction"]; }
            set
            {
                datas["direction"] = value;
                OnPropertyChanged();
            }
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
