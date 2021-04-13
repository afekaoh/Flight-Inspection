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
using Flight_Inspection.controls.Video;
using Flight_Inspection.controls.Joystick;
using Flight_Inspection.Pages.Settings;
using System.ComponentModel;
using Flight_Inspection.controls.DataWindow;

namespace Flight_Inspection
{
    /// <summary>
    /// Interaction logic for FlightData.xaml
    /// </summary>
    public partial class FlightData : Window
    {
        private readonly FlightDataViewModel flight;
        private readonly List<IControlView> views;
        private readonly List<Frame> frames;

        public FlightData()
        {
            this.DataContext = new FlightDataViewModel();
            flight = DataContext as FlightDataViewModel;
            flight.PropertyChanged += OnReady;
            InitializeComponent();
            views = new List<IControlView>
            {
                new FlightCharts(),
                new VideoPanelView(),
               new JoystickView(),
               new DataWindow() 
               
            };

            views.ForEach(v => flight.AddViewModel(v.GetViewModel()));
            flight.addEvent();
            frames = new List<Frame>
            {
                Charts,
                VideoPanel,
                Joystick,
                MoreInfo
            };
        }

        public void OnReady(object sender, PropertyChangedEventArgs e)
        {
            flight.UpdateSettings(new SettingsArgs {Ts = (e as OnReadyEventArgs).TS });
            frames.ForEach(f => f.Navigate(views.Find(v => v.Name == f.Name)));
        }
    }
}
