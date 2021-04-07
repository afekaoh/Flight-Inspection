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
using Flight_Inspection.controls.Video;

namespace Flight_Inspection
{
    /// <summary>
    /// Interaction logic for FlightData.xaml
    /// </summary>
    public partial class FlightData : Window
    {
        private VideoPanelView fc;
        private TimeSeries ts;
        public FlightData()
        {
            InitializeComponent();
            fc = new VideoPanelView();
            frame1.Navigate(fc);
        }

        internal TimeSeries TS
        {
            get { return ts; }
            set
            {
                ts = value;
                //fc.setTimeSeries(TS);
            }
        }
    }
}
