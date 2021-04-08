using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Joystick
{
    class JoystickViewModel : IControlViewModel
    {
        private float VM_aileron;

        public float VM_Aileron
        {
            get { return VM_aileron; }
            set { 
                VM_aileron = value;
                OnPropertyChanged("VM_Aileron");
            }
        }

        public float VM_rudder
        {
            get => model.Rudder;
        }
        public float VM_elevator
        {
            get => model.Elevator;
        }
        public float VM_throttle
        {
            get => model.Throttle;
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
            model.start();
        }

        public void TheModlePropertyChanged(object sender , PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "aileron")
            {
                VM_Aileron = model.Aileron;
            }
        }

    }
}
