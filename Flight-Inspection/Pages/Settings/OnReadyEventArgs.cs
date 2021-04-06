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

        public OnReadyEventArgs(string propertyName, SettingItem cSV, SettingItem xML, SettingItem pATH, bool ready) : base(propertyName)
        {
            CSV = cSV;
            XML = xML;
            PATH = pATH;
            this.ready = ready;
        }

        public SettingItem CSV { get; set; }
        public SettingItem XML { get; set; }
        public SettingItem PATH { get; set; }
        public bool ready { get; set; }
    }
}
