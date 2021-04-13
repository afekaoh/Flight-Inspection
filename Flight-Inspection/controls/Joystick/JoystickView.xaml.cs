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
            JoystickViewModel.addData("aileron", (float)InnerCanvas.ActualWidth);
            JoystickViewModel.addData("elevator", (float)InnerCanvas.ActualWidth);
            Console.WriteLine(InnerCanvas.ActualWidth);
            ThrotteleCanvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            ThrotteleCanvas.Arrange(new Rect(0, 0, ThrotteleCanvas.DesiredSize.Width, ThrotteleCanvas.DesiredSize.Height));
            JoystickViewModel.addData("throttle", 200);
            JoystickViewModel.addData("rudder", 200);
            Console.WriteLine($"Hello {JoyStickCanvas.Width}");
            Console.WriteLine($"Hello {GridT.ActualHeight}");
        }

        public IControlViewModel GetViewModel()
        {
            return this.JoystickViewModel;
        }
    }
}
