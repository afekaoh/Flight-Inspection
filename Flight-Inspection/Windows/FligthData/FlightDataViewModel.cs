﻿using Flight_Inspection.controls;
using Flight_Inspection.Pages.Settings;
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
        public event EventHandler<SetTimeEventArgs> SetTimeEvent;
        private readonly List<IControlViewModel> viewModels;
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

        public void SetTime(object sender, SetTimeEventArgs e)
        {
            viewModels.ForEach(vm => vm.setTime(e.Time));
            SetTimeEvent?.Invoke(this, e);
        }

        public void AddViewModel(IControlViewModel viewModel)
        {
            viewModels.Add(viewModel);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            var e = new OnReadyEventArgs(name, Ts);
            PropertyChanged?.Invoke(this, e);
        }

        internal void UpdateSettings(SettingsArgs settingsArgs)
        {
            viewModels.ForEach(vm => vm.SetSettings(settingsArgs));
        }
    }
}
