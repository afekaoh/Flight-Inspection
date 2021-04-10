using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Controls;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media;
using System;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;

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
            vm.Current = vm.GetNames()[0];
            vm.updateSeries();
        }
        private void choosenOption(object sender, MouseButtonEventArgs e)
        {
            vm.Current = (sender as ListBox).SelectedItem as Property;
        }

        private void lbTodoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
                choosenOption(lbTodoList, null);
        }

        private void onChoosePoint(object sender, ChartPoint chartPoint)
        {
            Console.WriteLine(chartPoint.X + " " + chartPoint.Y);
        }
    }
}
