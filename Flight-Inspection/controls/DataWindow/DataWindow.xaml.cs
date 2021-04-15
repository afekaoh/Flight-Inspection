// oz rigler 316291897 15/04/2021
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
    /// responsible for showing the datas at the GUI
    /// holding a viewModel field that is represendet as data context
    /// </summary>
    public partial class DataWindow : UserControl, IControlView
    {
        DataWindowVM Vm;
        public DataWindow()
        {
            DataContext = new DataWindowVM();
            this.Vm = this.DataContext as DataWindowVM;
            Vm.Ready += addFeatures;
          InitializeComponent();
            
        }


        public IControlViewModel GetViewModel()
        {
            return this.Vm;
        }

       private void addFeatures(object sender, EventArgs e)
        {
            //sets the features to start point with the list of the normalized properties.
            GridPitch.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            GridPitch.Arrange(new Rect(0, 0, GridPitch.DesiredSize.Width, GridPitch.DesiredSize.Height));
            GridAltimeter.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            GridAltimeter.Arrange(new Rect(0, 0, GridAltimeter.DesiredSize.Width, GridAltimeter.DesiredSize.Height));
            Vm.addData("pitch-deg", (int)GridPitch.ActualHeight);
            Vm.addData("roll-deg", (int)GridPitch.ActualHeight);
            Vm.addData("side-slip-deg", (int)GridPitch.ActualHeight);
            Vm.addData("altitude-ft", (int)GridAltimeter.ActualHeight);
        }
        // 
        private void AngularGauge_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
