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
        private float VM_aileron;
        private float VM_rudder;
        private float VM_elevator;
        private float VM_throttle;
        public float VM_Aileron
        {
            get { return VM_aileron; }
            set { 
                VM_aileron = value*1000+ 300 ;
                OnPropertyChanged();
            }
        }

        public float VM_Rudder
        {
            get { return VM_rudder; }
            set
            {
                VM_rudder = value;
                OnPropertyChanged();
            }
        }
        public float VM_Elevator
        {
            get { return VM_elevator; }
            set
            {
                VM_elevator = value;
                OnPropertyChanged();
            }
        }
        public float VM_Throttle
        {
            get { return VM_throttle; }
            set
            {
                VM_throttle = value;
                OnPropertyChanged("VM_throttle");
            }
        }
        private JoystickModel model;
        public JoystickViewModel()
        {
            model = new JoystickModel();
            model.PropertyChanged += TheModlePropertyChanged;
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
        }

        public void start()
        {
            Thread t = new Thread(model.start)
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
