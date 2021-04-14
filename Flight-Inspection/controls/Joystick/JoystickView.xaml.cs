using System;
using System.Windows;
using System.Windows.Controls;

namespace Flight_Inspection.controls.Joystick
{
    /// <summary>
    /// Interaction logic for JoystickView.xaml
    /// </summary>
    public partial class JoystickView : UserControl, IControlView
    {
        JoystickViewModel JoystickViewModel;
        public JoystickView()
        {
            InitializeComponent();
            this.DataContext = new JoystickViewModel();
            this.JoystickViewModel = this.DataContext as JoystickViewModel;
            JoystickViewModel.Ready += addFeatures;
        }

        private void addFeatures(object sender, EventArgs e)
        {
            JoystickViewModel.addData("aileron", (int)InnerCanvas.Width);
            JoystickViewModel.addData("elevator", (int)InnerCanvas.Width);
            GridT.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            GridT.Arrange(new Rect(0, 0, GridT.DesiredSize.Width, GridT.DesiredSize.Height));
            RudderCanvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            RudderCanvas.Arrange(new Rect(0, 0, GridT.DesiredSize.Width, GridT.DesiredSize.Height));
            JoystickViewModel.addData("throttle", (int)GridT.ActualHeight - 40);
            JoystickViewModel.addData("rudder", (int)RudderCanvas.ActualWidth - 40);
            Console.WriteLine($"Hello {RudderCanvas.ActualWidth}");
            Console.WriteLine($"Hello {GridT.ActualHeight}");
        }

        public IControlViewModel GetViewModel()
        {
            return this.JoystickViewModel;
        }

        private void GridT_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            JoystickViewModel.findData("throttle").setCanvasDim((int)GridT.ActualHeight - 40);
        }
    }
}
