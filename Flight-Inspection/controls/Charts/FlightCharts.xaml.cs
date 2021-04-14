using System.Windows.Controls;
using System.Windows.Input;
using System;
using LiveCharts;

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    ///  In the charts we used the library of LiveCharts - > https://lvcharts.net/
    /// </summary>
    public partial class FlightCharts : UserControl, IControlView
    {
        VMCharts vm;
        public FlightCharts()
        {
            InitializeComponent();
            //mvvm
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
            //set all the options for the graphs and dispaly the first one as default
            lbTodoList.ItemsSource = vm.GetNames();
            chartXAnomaly.Separator.StrokeThickness = 0;
            chartYAnomaly.Separator.StrokeThickness = 0;
            vm.Current = vm.GetNames()[0];
            vm.updateSeries();
        }
        private void choosenOption(object sender, MouseButtonEventArgs e)
        {
            //choose an option and update the graphs
            vm.Current = (sender as ListBox).SelectedItem as Property;
        }

        //to able to go through the list faster
        private void lbTodoList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
                choosenOption(lbTodoList, null);
        }

        //go to the wanted time according to the point
        private void onChoosePoint(object sender, ChartPoint chartPoint)
        {
            vm.Time = (int)chartPoint.X;
        }
    }
}
