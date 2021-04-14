using Flight_Inspection.controls;
using Flight_Inspection.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Pages.Settings
{
    public class OnReadyEventArgs : PropertyChangedEventArgs
    {

        public OnReadyEventArgs(string propertyName, bool ready, params SettingItem[] settingItems) : base(propertyName)
        {
            settings = new List<SettingItem>();
            settings.AddRange(settingItems);
            this.ready = ready;
        }

        public OnReadyEventArgs(string propertyName, TimeSeries ts) : base(propertyName) { TS = ts; }


        public List<SettingItem> settings;
        public bool ready { get; set; }
        public TimeSeries TS { get; }


        public SettingItem GetSetting(string str)
        {
            return settings.Find(s => s.Name == str);
        }
    }
}
