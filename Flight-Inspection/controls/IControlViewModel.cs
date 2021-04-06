using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    public abstract class IControlViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public abstract void SetSettings(SettingsArgs settingsArgs);
    }
}
