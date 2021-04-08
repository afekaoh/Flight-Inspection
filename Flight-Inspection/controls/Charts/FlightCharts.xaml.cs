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
    public partial class FlightCharts : UserControl, IControlView
    {
        VMCharts vm;
        public FlightCharts()
        {
            InitializeComponent();
            vm = new VMCharts();
            DataContext = vm;
            vm.Ready += OnReady;
        }

        public IControlViewModel GetViewModel()
        {
            return vm;
        }

        public void OnReady(object sender, EventArgs e)
        {
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
            vm.Current = (sender as ListBox).SelectedItem as Property;
            chart1.DataSource = vm.getDataContent(vm.Current.Name);
            chart1.Series["series"].XValueMember = "Key";
            chart1.Series["series"].YValueMembers = "Value";
            chart1.DataBind();
            chart2.DataSource = vm.getDataContent(vm.Current.Attach);
            chart2.Series["seriesSecond"].XValueMember = "Key";
            chart2.Series["seriesSecond"].YValueMembers = "Value";
            chart2.DataBind();
            var list = vm.getDataContentAttach(vm.Current.Name);
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
