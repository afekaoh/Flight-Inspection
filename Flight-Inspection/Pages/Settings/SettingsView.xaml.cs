using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flight_Inspection
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsView : Page
    {
        SettingsViewModel settings;
        public SettingsView()
        {
            this.DataContext = new SettingsViewModel();
            settings = DataContext as SettingsViewModel;
            InitializeComponent();
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.CommonDialog fbd;
            string name = (sender as Button).Name;
            name = (e.Source as Button).Content as string;
            Console.WriteLine(name);
            switch (name)
            {
                case "PATH":
                    fbd = new System.Windows.Forms.FolderBrowserDialog();
                    break;
                default:
                    fbd = new System.Windows.Forms.OpenFileDialog();
                    break;
            }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var s = (fbd.GetType() is System.Windows.Forms.FolderBrowserDialog) ?
                    (fbd as System.Windows.Forms.FolderBrowserDialog).SelectedPath :
                    (fbd as System.Windows.Forms.FileDialog).InitialDirectory + (fbd as System.Windows.Forms.FileDialog).FileName;
                settings.getSettingItem(name).Content = s;
            }

        }

        internal SettingPacket getSettings()
        {
            return settings.GetSettings();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            settings.SaveData();
        }
    }
}
