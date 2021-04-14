using Flight_Inspection.controls;
using Flight_Inspection.Pages.Settings;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Flight_Inspection
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsView : Page, IViewPages
    {
        private readonly SettingsViewModel settings;

        public event EventHandler<OnReadyEventArgs> OnReady;
        public event EventHandler NewWindow;
        public event EventHandler Closed;


        public SettingsView()
        {
            settings = new SettingsViewModel();
            this.DataContext = settings;
            settings.PropertyChanged += onReadyChanged;
            this.Name = "Settings";
            InitializeComponent();
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.CommonDialog fbd;
            string name = (sender as Button).Name;
            name = (e.Source as Button).Content as string;
            switch (name)
            {
                case "Proc_PATH":
                    fbd = new System.Windows.Forms.FolderBrowserDialog();
                    break;
                default:
                    fbd = new System.Windows.Forms.OpenFileDialog();
                    break;
            }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var s = (fbd is System.Windows.Forms.FolderBrowserDialog) ?
                    (fbd as System.Windows.Forms.FolderBrowserDialog).SelectedPath :
                    (fbd as System.Windows.Forms.FileDialog).InitialDirectory + (fbd as System.Windows.Forms.FileDialog).FileName;
                settings.getSettingItem(name).Content = s;
            }
            updateSettings();
        }

        internal void updateSettings()
        {
            settings.UpdateSettings();
        }

        public void onReadyChanged(object sender, PropertyChangedEventArgs e)
        {
            var settingsViewModel = (sender as SettingsViewModel);
            if ((e.PropertyName is "Ready" || e.PropertyName is "settings") && settingsViewModel.Ready)
            {
                OnReadyEvent(e as OnReadyEventArgs);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            settings.SaveData();
        }

        public void OnReadyEvent(OnReadyEventArgs e)
        {
            OnReady?.Invoke(this, e);
        }

        public IPagesViewModel GetViewModel()
        {
            return settings;
        }
    }
}
