using Flight_Inspection.controls;
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
        public MainWindow()
        {
            InitializeComponent();
            settings = new SettingsView();
            FlightGear = new FlightGearView(settings.getSettings());
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(settings);
        }

        private void FlightGear_Click(object sender, RoutedEventArgs e)
        {
            FlightGear.setSettings(settings.getSettings());
            frame.Navigate(FlightGear);

        }
    }
}
