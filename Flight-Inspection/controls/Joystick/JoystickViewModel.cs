using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Flight_Inspection.controls.Joystick
{
    class JoystickViewModel : IControlViewModel
    {
        List<NormelaizedData> datas;
        private JoystickModel model;
        public event EventHandler Ready; 

        private void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }
        public float VM_Aileron
        {
            get
            {
                var data = findData("aileron");
                if (data != null)
                    return data.Data;
                else
                    return 0;
            }
            set
            {
                findData("aileron").Data = value*100; //* findData("aileron").Normalize;
                OnPropertyChanged();
            }
        }

        public float VM_Rudder
        {
            get
            {
                var data = findData("rudder");
                if (data != null)
                    return data.Data;
                else
                    return 0;
            }
            set
            {
                findData("rudder").Data = value*100; //* findData("rudder").Normalize/2;
                OnPropertyChanged();
            }
        }
        public float VM_Elevator
        {
            get
            {
                var data = findData("elevator");
                if (data != null)
                    return data.Data;
                else
                    return 0;
            }
            set
            {
                findData("elevator").Data = value*100;// * findData("elevator").Normalize ;
                OnPropertyChanged();
            }
        }
        public float VM_Throttle
        {
            get
            {
                var data = findData("throttle");
                if (data != null)
                    return data.Data;
                else
                    return 0;
            }
            set
            {
                findData("throttle").Data = 230 - value*100;// * findData("throttle").Normalize;
                OnPropertyChanged();

            }
        }
        public JoystickViewModel()
        {
            model = new JoystickModel();
            datas = new List<NormelaizedData>();
            model.PropertyChanged += TheModlePropertyChanged;
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
            this.OnReady();
        }

        public NormelaizedData findData (string name) { return (NormelaizedData)datas.Find(JoyStickData => JoyStickData.Name == name); }

        public void addData(string name, float CanvasDim)
        {
            datas.Add(new NormelaizedData(name, CanvasDim, model.maxAbs(name)));
        }

        public void TheModlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Aileron":
                    VM_Aileron = model.Aileron;
                    break;
                case "Rudder":
                    VM_Rudder = model.Rudder;
                    break;
                case "Throttle":
                    VM_Throttle = model.Throttle;
                    break;
                case "Elevator":
                    VM_Elevator = model.Elevator;
                    break;
            }
        }
        internal override void setTime(int time)
        {
            model.CurrentTime = time;
        }
    }
}
