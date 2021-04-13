using Flight_Inspection.controls;
using Flight_Inspection.Pages.Settings;
using Flight_Inspection.Windows.FligthData;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        public event EventHandler Closed;

        public event EventHandler NewWindow;

        public event EventHandler<OnReadyEventArgs> OnReady;

        void Flight_Closed(object sender, EventArgs e) { Closed?.Invoke(this, e); }

        private void OnStart(object sender, EventArgs e)
        {
            // Setting up the FlightData window where all the Controls seats
            flight = new FlightData();
            flightGearViewModel.DataViewModel = flight.DataContext as FlightDataViewModel;
            flightGearViewModel.UpdateSettings();
            flight.Closed += Flight_Closed;
            OnNewWindow(e);
            flight.Show();
        }

        private void Start_FG_Click(object sender, RoutedEventArgs e) { flightGearViewModel.StartFG(); }

        private void Start_Simulation_Click(object sender, RoutedEventArgs e) { flightGearViewModel.StartPlay(e); }


        protected virtual void OnOnReady(object sender, OnReadyEventArgs e) { OnReady?.Invoke(sender, e); }

        public IPagesViewModel GetViewModel() { return flightGearViewModel; }

        public void OnNewWindow(EventArgs e) { NewWindow?.Invoke(this, e); }

        // If the App is ready to start play the simulation
        public bool Ready { get { return !(flightGearViewModel.Ts is null); } }
    }
}
