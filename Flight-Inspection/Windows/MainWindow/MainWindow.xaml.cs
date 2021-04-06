using Flight_Inspection.controls;
using Flight_Inspection.controls.FlightGear;
using Flight_Inspection.Pages.Settings;
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
        SettingsView settings;
        FlightGearView FlightGear;
        TimeSeries ts;
        /*
         * 
                [DllImport("C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\Windows\\MainWindow\\anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
                public static extern float pearson([MarshalAs(UnmanagedType.LPArray)] float[] x, [MarshalAs(UnmanagedType.LPArray)] float[] y, int sizeX, int sizeY);
                    float[] x = { 1, 2, 5, 7, 9, 10 };
                    float[] y = { 5, 6, 8, 10, 12, 14 };
                    Console.WriteLine(pearson(x, y, x.Length, y.Length));
         */
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
