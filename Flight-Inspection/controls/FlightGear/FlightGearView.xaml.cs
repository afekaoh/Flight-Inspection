using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
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
using Flight_Inspection.controls.FlightGear;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Flight_Inspection.controls
{
    /// <summary>
    /// Interaction logic for FlightGearView.xaml
    /// </summary>
    public partial class FlightGearView : UserControl
    {
        private FlightGearViewModel fg;
        public FlightGearView()
        {
            InitializeComponent();
            DataContext = new FlightGearViewModel();
            fg = DataContext as FlightGearViewModel;

        }

        private void Start_FG_Click(object sender, RoutedEventArgs e)
        {
            fg.StartFG();
        }

        private void CSV_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FileDialog fbd = new System.Windows.Forms.OpenFileDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fg.CsvFileName.Content = fbd.InitialDirectory + fbd.FileName;
            }
        }

        private void XML_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FileDialog fbd = new System.Windows.Forms.OpenFileDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fg.XMLPath.Content = fbd.InitialDirectory + fbd.FileName;
            }
        }
        private void Path_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fg.ProcPath.Content = fbd.SelectedPath;
            }
        }

        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            fg.setReady();
        }

        private void Start_Simulation_Click(object sender, RoutedEventArgs e)
        {
            fg.StartPlay();

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            fg.SaveData();
        }

    }
}

