using Flight_Inspection.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Windows.FligthData
{
    public class FlightDataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<IControlViewModel> viewModels;
        private TimeSeries ts;

        public FlightDataViewModel()
        {
            this.viewModels = new List<IControlViewModel>();
        }

        public TimeSeries Ts
        {
            get => ts;
            set
            {
                ts = value;
                OnPropertyChanged();
            }
        }

        public void AddViewModel(IControlViewModel viewModel)
        {
            viewModels.Add(viewModel);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void UpdateSettings(SettingsArgs settingsArgs)
        {
            viewModels.ForEach(vm => vm.SetSettings(settingsArgs));
        }
    }
}
