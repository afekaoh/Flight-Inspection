using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;

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
            Dictionary<int, double> value = new Dictionary<int, double>();
            List<float> vs = vm.getData("elevator");
            for (int i = 0; i < vs.Count; i++)
                value.Add(i, vs[i]);

            Chart chart = this.FindName("chart1") as Chart;
            chart.DataSource = value;
            chart.Series["series"].XValueMember = "Key";
            chart.Series["series"].YValueMembers = "Value";
            chart.ChartAreas["chartArea"].AxisX.MajorGrid.LineWidth = 0;
            chart.ChartAreas["chartArea"].AxisY.MajorGrid.LineWidth = 0;
        }

        private void choosenOption(object sender, MouseButtonEventArgs e)
        {
            string content = (sender as ContentControl).Content.ToString();
            List<float> vs = vm.getData(content);
            if (content == "elevator"){
                graph2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 100, 100));
                graph3.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 100, 100));
            }
            Dictionary<int, float> value = new Dictionary<int, float>();
            for (int i = 0; i < 10; i++)
            {
                value.Add(i, vs[i]);
            }
            chart1.DataSource = value;
            chart1.Series["series"].XValueMember = "Key";
            chart1.Series["series"].YValueMembers = "Value";
        }


    }
}
