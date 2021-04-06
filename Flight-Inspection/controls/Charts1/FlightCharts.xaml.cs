using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System;
using Flight_Inspection.controls.FlightGear;

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
            var chosen = (sender as ListBox).SelectedItem as Property;
            string content = chosen.Name;
            var rand = new Random();
            List<Property> ls = vm.GetNames();
            string next = ls[rand.Next(0, ls.Count)].Name;
            chart1.DataSource = vm.getDataContent(content);
            chart1.Series["series"].XValueMember = "Key";
            chart1.Series["series"].YValueMembers = "Value";
            chart1.DataBind();
            chart2.DataSource = vm.getDataContent(next);
            chart2.Series["seriesSecond"].XValueMember = "Key";
            chart2.Series["seriesSecond"].YValueMembers = "Value";
            chart2.DataBind();
            var list = vm.getDataContent(content, next);
            chart3.Series["seriesThird"].Points.Clear();
            foreach (var item in list)
            {
                chart3.Series["seriesThird"].Points.AddXY(item.Item1, item.Item2);
            }
            chart3.Series["seriesThird"].XValueMember = "Key";
            chart3.Series["seriesThird"].YValueMembers = "Value";
            chart3.DataBind();
        }

        private void lbTodoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
                choosenOption(lbTodoList, null);
        }
    }
}
