using Flight_Inspection.Pages.FlightGear;
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
        virtual public void OnPropertyChanged(int time, [CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new SetTimeEventArgs(time, name));
        }
        virtual public void OnPropertyChanged(bool stop, [CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new SetStopEventArgs(stop, name));
        }
        public abstract void SetSettings(SettingsArgs settingsArgs);
        internal abstract void setTime(int time);
    }
}
