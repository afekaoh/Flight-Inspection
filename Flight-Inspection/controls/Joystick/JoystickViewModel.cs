// oz rigler 316291897 15/04/2021
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
// a view- model class
// prossesing the raw data of the model with caculations and adaptation in order to view at the desired way for spesipic view.
namespace Flight_Inspection.controls.Joystick
{
    class JoystickViewModel : IControlViewModel
    {
        // a stl that holds the data
        List<NormelaizedData> datas;
        private JoystickModel model;
        // an event thats notify the view class that the VM is ready for use will happend after setting the datas
        public event EventHandler Ready; 
        // notify that the VM is ready
        private void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }
        // when a propertiy change ,its activated OnPropertyChanged event thats notify the binded item in the view to change accordingly
        public float VM_Aileron
        {
            get
            {
                var data = findData("aileron");
                if (data != null)
                    return data.Normalize /2 ;
                else
                    return 0;
            }
            set
            {
                findData("aileron").Data = value ;
                OnPropertyChanged();
            }
        }
        public Thickness VM_Rudder
        {
            get
            {
                if (rudder != null)
                {
                    return rudder;
                }
                else
                    return new Thickness(0, 5, 5, 10);
            }
            set
            {
                findData("rudder").Data = (float)value.Top;
                if(!Double.IsNaN((double)findData("rudder").Normalize))
                    rudder = new Thickness(findData("rudder").Normalize * (-1), 5, 5, 10);
                else
                    rudder = new Thickness(0, 5, 5, 10);
                OnPropertyChanged();
            }
        }
        private Thickness rudder;

        public float VM_Elevator
        {
            get
            {
                var data = findData("elevator");
                if (data != null)
                    return data.Normalize /2;
                else
                    return 0;
            }
            set
            {
                findData("elevator").Data = value   ;
                OnPropertyChanged();
            }
        }
        public Thickness VM_Throttle
        {
            get
            {
                if (thicknessT != null)
                {
                    return thicknessT;
                }
                else
                    return new Thickness(10, 0, 5, 5);
            }
            set
            {
                findData("throttle").Data = (float)value.Top;
                thicknessT = new Thickness(10, findData("throttle").Normalize * (-1), 5, 5);
                OnPropertyChanged();
            }
        }
        private Thickness thicknessT;

        public JoystickViewModel()
        {
            model = new JoystickModel();
            datas = new List<NormelaizedData>();
            // adding a function to the model event, that will change vm properties whenever model properties are changed.
            model.PropertyChanged += TheModlePropertyChanged;
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
            this.OnReady();
        }
        // required for searching in the list field datas.
        public NormelaizedData findData (string name) { return (NormelaizedData)datas.Find(JoyStickData => JoyStickData.Name == name); } 
        public void addData(string name, int CanvasDim)
        {
            datas.Add(new NormelaizedData(name, CanvasDim,model.maxVal(name),model.minVal(name)));
        }
        // when the model properties getting a new value this function will be activated
        // its get the property name that has changed and changing the appropriate VM property
        public void TheModlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Aileron":
                    VM_Aileron = model.Aileron;
                    break;
                case "Rudder":
                    VM_Rudder = new Thickness(model.Rudder, 0, 0, 0);
                    break;
                case "Throttle":
                    VM_Throttle = new Thickness(0, model.Throttle,0,0);
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
