using Flight_Inspection.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Windows.MainWindow
{
    class MainWindowViewModel : IViewModel
    {
        readonly List<IPagesViewModel> PagesViewModels;

        public MainWindowViewModel()
        {
            PagesViewModels = new List<IPagesViewModel>();
        }


        public void AddViewModel(IPagesViewModel controlViewModel)
        {
            PagesViewModels.Add(controlViewModel);
        }

        public void UpdateSettings()
        {
            PagesViewModels.ForEach(vm => vm.UpdateSettings());
        }

        public void SetSettings(SettingsArgs settingsArgs)
        {
            PagesViewModels.ForEach(vm => vm.SetSettings(settingsArgs));
        }

        private SettingsArgs settings;

        public event PropertyChangedEventHandler PropertyChanged;
        virtual public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public SettingsArgs Settings
        {
            get => settings;
            set
            {
                settings = value;
                OnPropertyChanged();
            }
        }

    }
}
