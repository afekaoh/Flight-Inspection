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
            InitializeComponent();
        }

        public IControlViewModel GetViewModel()
        {
            return this.JoystickViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JoystickViewModel.start();
        }
    }
}
