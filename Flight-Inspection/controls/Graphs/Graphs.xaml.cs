using Flight_Inspection.controls.Graphs;
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

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {
        public class Names
        {
            public string Title { get; set; }
        }
        controls.Graphs.VMGraphs vm;
        public Graphs()
        {
            InitializeComponent();
            vm = new controls.Graphs.VMGraphs();
            DataContext = vm;
            lbTodoList.ItemsSource = vm.GetNames();
        }

        private void choosenOption(object sender, MouseButtonEventArgs e)
        {
            string content = (sender as ContentControl).Content.ToString();
            switch (content)
            {
                case "A":
                    graph1.Background = new SolidColorBrush(Color.FromRgb(0, 100, 100));
                    graph2.Background = new SolidColorBrush(Color.FromRgb(0, 100, 100));
                    graph3.Background = new SolidColorBrush(Color.FromRgb(0, 100, 100));
                    break;
                case "B":
                    graph1.Background = new SolidColorBrush(Color.FromRgb(0, 200, 200));
                    graph2.Background = new SolidColorBrush(Color.FromRgb(0, 200, 200));
                    graph3.Background = new SolidColorBrush(Color.FromRgb(0, 200, 200));
                    break;
                case "C":
                    graph1.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    graph2.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    graph3.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    break;
                case "D":
                    graph1.Background = new SolidColorBrush(Color.FromRgb(250, 100, 100));
                    graph2.Background = new SolidColorBrush(Color.FromRgb(250, 100, 100));
                    graph3.Background = new SolidColorBrush(Color.FromRgb(250, 100, 100));
                    break;

            }
        }

        
    }
}
