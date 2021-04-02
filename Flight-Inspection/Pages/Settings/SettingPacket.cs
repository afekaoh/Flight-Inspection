using Flight_Inspection.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Pages.Settings
{
    public class SettingPacket
    {
        public SettingItem CSV { get; set; }
        public SettingItem XML { get; set; }
        public SettingItem PATH { get; set; }
        public bool ready { get; set; }
    }
}
