﻿using Flight_Inspection.controls.Joystick;
using LiveCharts;
using LiveCharts.Wpf;
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
        List<NormelaizedData> dataList;

        public DataWindowVM()
        {
            model = new DataWindowModel();
            datas = new Dictionary<string, float>();
            dataList = new List<NormelaizedData>();
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
                var data = findData("side-slip-deg");
                if (data != null)
                    data.Data = value;
                datas["yaw"] = data.Normalize;
                OnPropertyChanged();
            }
        }

        public float VM_Pitch
        {
            get { return datas["pitch"]; }
            set
            {
                var data = findData("pitch-deg");
                if (data != null)
                    data.Data = value;
                datas["pitch"] = data.Normalize;
                OnPropertyChanged();
            }
        }
        public float VM_Roll
        {
            get { return datas["roll"]; }
            set
            {
                var data = findData("roll-deg");
                if (data != null)
                    data.Data = value;
                datas["roll"] = data.Normalize;
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
                var data = findData("altitude-ft");
                if (data != null)
                    data.Data = value;
                datas["altimeter"] = data.Normalize;
                OnPropertyChanged();
            }
        }
        public float VM_Direction
        {
            get { return datas["direction"]; }
            set
            {
                if (180 + value < 360)
                    datas["direction"] = value + 180;
                else
                    datas["direction"] = value - 180;
                OnPropertyChanged();
            }
        }

        public NormelaizedData findData(string name) { return (NormelaizedData)dataList.Find(JoyStickData => JoyStickData.Name == name); }

        public void addData(string name, int CanvasDim)
        {
            dataList.Add(new NormelaizedData(name, CanvasDim, model.maxVal(name), model.minVal(name)));
        }

        public void TheModlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "yaw")
            {
                VM_Yaw = model.Yaw - model.minVal("side-slip-deg");
            }
            else if (e.PropertyName == "pitch")
            {
                VM_Pitch = model.Pitch - model.minVal("pitch-deg");
            }
            else if (e.PropertyName == "roll")
            {
                VM_Roll = model.Roll - model.minVal("roll-deg");
            }
            else if (e.PropertyName == "altimeter")
            {
                VM_Altimeter = model.Altimeter - model.minVal("altitude-ft");
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
