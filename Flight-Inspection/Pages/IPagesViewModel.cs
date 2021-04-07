using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    public abstract class IPagesViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual public void OnPropertyChanged([CallerMemberName] string name = null, PropertyChangedEventArgs e = null)
        {
            var args = (e is null) ? new PropertyChangedEventArgs(name) : e;
            PropertyChanged?.Invoke(this, args);
        }

        public abstract void SetSettings(SettingsArgs settingsArgs);
        public abstract void UpdateSettings();

    }
}
