using Flight_Inspection.controls;
using Flight_Inspection.controls.FlightGear;
using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingsView settings;
        FlightGearView FlightGear;
        TimeSeries ts;
        public MainWindow()
        {
            InitializeComponent();
            settings = new SettingsView();
            settings.OnReady += Settings_OnReady;
            FlightGear = new FlightGearView();
            FlightGear.NewWindow += OnNewWindow;
            settings.updateSettings();
        }

        private void Settings_OnReady(object sender, EventArgs e)
        {
            var ea = e as OnReadyEventArgs;
            ts = new TimeSeries(ea.CSV.Content, ea.XML.Content);
            FlightGear.updateSettings(ts, ea.PATH.Content);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(settings);
        }

        private void FlightGear_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(FlightGear);
        }
        public void OnNewWindow(object sender, EventArgs e)
        {
            FlightData flight = new FlightData();
            flight.TS = ts;
            flight.Closed += Flight_Closed;
            flight.Show();
            this.Hide();
        }

        private void Flight_Closed(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
