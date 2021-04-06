using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System;

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
        }
        public void setTimeSeries(TimeSeries ts)
        {
            vm.setTimeSeries(ts);
            lbTodoList.ItemsSource = vm.GetNames();
            chart1.ChartAreas["chartArea"].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas["chartArea"].AxisY.MajorGrid.LineWidth = 0;
            chart3.ChartAreas["chartAreaThird"].AxisY.MajorGrid.LineWidth = 0;
            chart3.ChartAreas["chartAreaThird"].AxisX.MajorGrid.LineWidth = 0;
            chart2.ChartAreas["chartAreaSecond"].AxisY.MajorGrid.LineWidth = 0;
            chart2.ChartAreas["chartAreaSecond"].AxisX.MajorGrid.LineWidth = 0;
        }

        private void choosenOption(object sender, MouseButtonEventArgs e)
        {
            string content = (sender as ContentControl).Content.ToString();
            List<float> vs = vm.getData(content);

        }


    }
}
