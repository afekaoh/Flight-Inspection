using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.DataContext = new JoystickViewModel();
            this.JoystickViewModel = this.DataContext as JoystickViewModel;
            JoystickViewModel.Ready += addFeatures;
            InitializeComponent();

        }

        private void JoyStickCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void addFeatures(object sender, EventArgs e)
        {
            JoystickViewModel.addData("aileron", (float)JoyStickCanvas.Width);
            JoystickViewModel.addData("elevator", (float)JoyStickCanvas.Height);
            JoystickViewModel.addData("throttle", (float)ThrotteleRange.Height);
            JoystickViewModel.addData("rudder", (float)RudderRange.Width);
            Console.WriteLine($"Hello {JoyStickCanvas.Width}");
            Console.WriteLine($"Hello {JoyStickCanvas.Height}");

        }

        public IControlViewModel GetViewModel()
        {
            return this.JoystickViewModel;
        }
    }
}
