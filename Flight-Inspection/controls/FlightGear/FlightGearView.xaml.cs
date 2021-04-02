using System;
using System.Runtime.InteropServices;
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
using Flight_Inspection.controls.FlightGear;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Flight_Inspection.Pages.Settings;

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for FlightGearView.xaml
    /// </summary>
    public partial class FlightGearView : UserControl
    {
        private readonly FlightGearViewModel fg;
        public event EventHandler NewWindow;
        public FlightGearView(SettingPacket settings = null)
        {
            InitializeComponent();
            DataContext = new FlightGearViewModel(settings);
            fg = DataContext as FlightGearViewModel;
        }

        private void Start_FG_Click(object sender, RoutedEventArgs e)
        {
            fg.StartFG();
        }

        internal void setSettings(SettingPacket settingPacket)
        {
            fg.setSettings(settingPacket);
        }

        private void Start_Simulation_Click(object sender, RoutedEventArgs e)
        {
            //fg.StartPlay();
            NewWindow?.Invoke(this, e);
        }


    }
}

