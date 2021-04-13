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

namespace Flight_Inspection.controls.DataWindow
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : UserControl, IControlView
    {
        DataWindowVM Vm;
        public DataWindow()
        {
            DataContext = new DataWindowVM();
            this.Vm = this.DataContext as DataWindowVM;
/*            Vm.Ready += addFeatures;
*/            InitializeComponent();
        }


        public IControlViewModel GetViewModel()
        {
            return this.Vm;
        }

/*        private void addFeatures(object sender, EventArgs e)
        {
            JoystickViewModel.addData("aileron", (float)JoyStickCanvas.Width);
            JoystickViewModel.addData("elevator", (float)JoyStickCanvas.Height);
            JoystickViewModel.addData("throttle", (float)ThrotteleRange.Height);
            JoystickViewModel.addData("rudder", (float)RudderRange.Width);
            Console.WriteLine($"Hello {JoyStickCanvas.Width}");
            Console.WriteLine($"Hello {JoyStickCanvas.Height}");

        }
*/    }
}
