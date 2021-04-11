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
        List<JoyStickData> datas;
        private JoystickModel model;
        public event EventHandler Ready; 

        private void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }
        public float VM_Aileron
        {
            get { return findData("aileron").Data; }
            set {
                findData("aileron").Data = value*300+ 300;
                OnPropertyChanged();
            }
        }

        public float VM_Rudder
        {
            get { return findData("rudder").Data; }
            set
            {
                findData("rudder").Data =330+ value*400;
                OnPropertyChanged();
            }
        }
        public float VM_Elevator
        {
            get { return findData("elevator").Data; }
            set
            {
                findData("elevator").Data = value * 100 + 150;
                OnPropertyChanged();
            }
        }
        public float VM_Throttle
        {
            get { return findData("throttle").Data; }
            set
            {
                findData("throttle").Data = 230 - value * 100;
                OnPropertyChanged();

            }
        }
        public JoystickViewModel()
        {
            model = new JoystickModel();
            datas = new List<JoyStickData>();
            model.PropertyChanged += TheModlePropertyChanged;
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
            this.OnReady();
        }

        public JoyStickData findData (string name) { return (JoyStickData)datas.Find(JoyStickData => JoyStickData.Name == name); }

        public void addData (string name,float CanvasDim)
        {
            datas.Add(new JoyStickData(name, CanvasDim, model.maxAbs(name)));
        }

        public void start()
        {
            Thread t = new Thread(model.sendData)
            {
                IsBackground = true
            };
            t.Start();
        }

        public void TheModlePropertyChanged(object sender , PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "aileron")
            {
                VM_Aileron = model.Aileron;
            }
            if (e.PropertyName == "rudder")
            {
                VM_Rudder = model.Rudder;
            }
            if (e.PropertyName == "throttle")
            {
                VM_Throttle = model.Throttle;
            }
            if (e.PropertyName == "elevator")
            {
                VM_Elevator = model.Elevator;
            }
        }

        internal override void setTime(int time)
        {
        }
    }
}
