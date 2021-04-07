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
            this.PropertyChanged += OnSettingsChanged;
        }


        public void AddViewModel(IPagesViewModel controlViewModel)
        {
            PagesViewModels.Add(controlViewModel);
        }

        internal void SetSettings()
        {
            PagesViewModels.ForEach(vm => vm.SetSettings(settings));
        }
        public void UpdateSettings()
        {
            PagesViewModels.ForEach(vm => vm.UpdateSettings());
        }

        void IViewModel.SetSettings(SettingsArgs settingsArgs)
        {
        }

        public void OnSettingsChanged(object Sender, PropertyChangedEventArgs e)
        {
            this.SetSettings();
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
