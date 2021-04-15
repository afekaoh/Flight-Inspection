// oz rigler 316291897 15/04/2021
using System;
using System.Windows;
using System.Windows.Controls;

namespace Flight_Inspection.controls.Joystick
{
    /// <summary>
    /// Interaction logic for JoystickView.xaml
    /// responsible for showing the datas at the GUI
    /// holding a viewModel field that is represendet as data context
    /// </summary>
    public partial class JoystickView : UserControl, IControlView
    {
        JoystickViewModel JoystickViewModel;
        public JoystickView()
        {
            InitializeComponent();
            this.DataContext = new JoystickViewModel();
            this.JoystickViewModel = this.DataContext as JoystickViewModel;
            // when the viewModel is ready , it will create the features datas at the view modle class
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
        }

        public IControlViewModel GetViewModel()
        {
            return this.JoystickViewModel;
        }
        // activate when the grid is changed and will set a new grid size in viewmodel fields
        private void GridT_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            JoystickViewModel.findData("throttle").setCanvasDim((int)GridT.ActualHeight - 40);
        }
    }
}
