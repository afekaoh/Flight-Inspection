using Flight_Inspection.controls;
using Flight_Inspection.controls.FlightGear;
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
        readonly FlightDataViewModel flight;
        readonly List<IControlView> views;

        public FlightData()
        {
            InitializeComponent();
            views = new List<IControlView>();
            views.Add(new FlightCharts());
            views.ForEach(v => flight.AddViewModel(v.GetViewModel()));
            Charts.Navigate(views.Find(v => v.Name == "Charts"));
        }

        public void OnReady(object sender, EventArgs e)
        {
            flight.UpdateSettings(new SettingsArgs { ts = TS });
        }

        internal TimeSeries TS
        {
            get { return ts; }
            set
            {
                ts = value;
                fc.setTimeSeries(TS);
            }
        }
    }
}
