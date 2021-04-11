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
                findData("aileron").Data = value * 300 + 300;
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
                findData("rudder").Data = 330 + value * 400;
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
                findData("elevator").Data = value * 100 + 150;
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
                findData("throttle").Data = 230 - value * 100;
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

        public void start()
        {
            Thread t = new Thread(model.sendData)
            {
                IsBackground = true
            };
            t.Start();
        }

        public void TheModlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "aileron":
                    VM_Aileron = model.Aileron;
                    break;
                case "rudder":
                    VM_Rudder = model.Rudder;
                    break;
                case "throttle":
                    VM_Throttle = model.Throttle;
                    break;
                case "elevator":
                    VM_Elevator = model.Elevator;
                    break;
            }

        }

        internal override void setTime(int time)
        {

        }
    }
}
