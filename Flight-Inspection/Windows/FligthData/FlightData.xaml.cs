using Flight_Inspection.controls;
using Flight_Inspection.Windows.FligthData;
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
    /// Interaction logic for FlightData.xaml
    /// </summary>
    public partial class FlightData : Window
    {
        private FlightCharts fc;
        readonly FlightDataViewModel flight;
        public FlightData()
        {
            this.DataContext = new FlightDataViewModel();
            flight = DataContext as FlightDataViewModel;
            flight.PropertyChanged += OnReady;
            InitializeComponent();
            fc = new FlightCharts();
            frame1.Navigate(fc);
        }

        public void OnReady(object sender, EventArgs e)
        {
            fc.setTimeSeries(TS);
        }

        internal TimeSeries TS
        {
            get { return flight.Ts; }
            set
            {
                flight.Ts = value;
            }
        }
    }
}
