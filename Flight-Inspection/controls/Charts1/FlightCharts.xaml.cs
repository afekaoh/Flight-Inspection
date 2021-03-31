using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class FlightCharts : UserControl
    {
        VMCharts vm;
        public FlightCharts()
        {
            InitializeComponent();
            vm = new VMCharts();
            DataContext = vm;
            lbTodoList.ItemsSource = vm.GetNames();
        }

        private void choosenOption(object sender, MouseButtonEventArgs e)
        {
            string content = (sender as ContentControl).Content.ToString();
            List<float> vs = vm.getData(content);

        }


    }
}
