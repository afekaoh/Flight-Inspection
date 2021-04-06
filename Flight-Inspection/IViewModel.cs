﻿using Flight_Inspection.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection
{
    interface IViewModel : INotifyPropertyChanged
    {
        void SetSettings(SettingsArgs settingsArgs);
    }
}
