using Flight_Inspection.controls;
using Flight_Inspection.Pages;
using Flight_Inspection.Pages.FlightGear;
using Flight_Inspection.Pages.Settings;
using Flight_Inspection.Windows.MainWindow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly List<IViewPages> pages;
        private readonly MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {

            this.DataContext = new MainWindowViewModel();
            mainWindowViewModel = DataContext as MainWindowViewModel;
            this.pages = new List<IViewPages>();
            pages.Add(new SettingsView());
            pages.Add(new FlightGearView());
            pages.ForEach(p =>
            {
                p.OnReady += Settings_OnReady;
                p.NewWindow += OnNewWindow;
                p.Closed += Flight_Closed;
                mainWindowViewModel.AddViewModel(p.GetViewModel());
            });
            this.Initialized += OnInitializedMain;

            InitializeComponent();
        }

        public void OnInitializedMain(object sender, EventArgs e)
        {
            mainWindowViewModel.UpdateSettings();
        }

        private void Settings_OnReady(object sender, OnReadyEventArgs e)
        {
            var csv = e.GetSetting("CSV_Test").Checked ? e.GetSetting("CSV_Test").Content : e.GetSetting("CSV_Normal").Content;
            var xml = e.GetSetting("XML").Content;
            var Ts = new TimeSeries(csv, xml);
            var ProcPath = e.GetSetting("Proc_PATH").Content;
            mainWindowViewModel.SetSettings(new SettingsArgs { Ts = Ts, ProcPath = ProcPath });
        }

        private void Move_Page(object sender, RoutedEventArgs e)
        {
            frame.Navigate(pages.Find(p => p.Name == (sender as Button).Name));
        }

        public void OnNewWindow(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Flight_Closed(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
