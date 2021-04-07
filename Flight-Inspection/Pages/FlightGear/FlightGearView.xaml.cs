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
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Flight_Inspection.Pages.Settings;
using Flight_Inspection.controls;

namespace Flight_Inspection.Pages.FlightGear
{
    /// <summary>
    /// Interaction logic for FlightGearView.xaml
    /// </summary>
    public partial class FlightGearView : Page, IViewPages
    {
        private FlightGearViewModel flightGearViewModel;
        public FlightData flight;


        public FlightGearView()
        {
            this.DataContext = new FlightGearViewModel();
            flightGearViewModel = DataContext as FlightGearViewModel;
            flightGearViewModel.Start += OnStart;
            this.Name = "FlightGear";
            InitializeComponent();
        }

        public event EventHandler NewWindow;
        public event EventHandler Closed;
        public event EventHandler<OnReadyEventArgs> OnReady;


        protected virtual void OnOnReady(object sender, OnReadyEventArgs e)
        {
            OnReady?.Invoke(sender, e);
        }

        private void Start_FG_Click(object sender, RoutedEventArgs e)
        {
            flightGearViewModel.StartFG();
        }

        void Flight_Closed(object sender, EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        private void Start_Simulation_Click(object sender, RoutedEventArgs e)
        {
            flightGearViewModel.StartPlay(e);
        }

        private void OnStart(object sender, EventArgs e)
        {
            flight = new FlightData() { TS = flightGearViewModel.Ts };
            flight.Closed += Flight_Closed;
            OnNewWindow(e);
            flight.Show();
        }

        public void OnNewWindow(EventArgs e)
        {
            NewWindow?.Invoke(this, e);
        }

        public IPagesViewModel GetViewModel()
        {
            return flightGearViewModel;
        }
        public bool Ready { get { return !(flightGearViewModel.Ts is null); } }
    }
}
