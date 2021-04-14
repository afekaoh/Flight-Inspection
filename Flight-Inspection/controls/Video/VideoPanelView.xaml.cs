
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

namespace Flight_Inspection.controls.Video
{
    /// <summary>
    /// Interaction logic for VideoPanelView.xaml
    /// </summary>
    public partial class VideoPanelView : UserControl, IControlView
    {
        private bool isPlaying;
        VideoPanelViewModel videoPanelViewModel;


        //video panel view constructor
        public VideoPanelView()
        {
            InitializeComponent();
            videoPanelViewModel = new VideoPanelViewModel();
            DataContext = videoPanelViewModel;
            isPlaying = false;
        }

        public IControlViewModel GetViewModel()
        {
            return videoPanelViewModel;
        }

        //start the flight inspection with the play button 
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            //check if the button isn't pressed / if the video isn't working 
            if (!isPlaying)
            {
                Play.Content = FindResource("Stop");
                videoPanelViewModel.StartPlay();
                isPlaying = true;
            }
            else
            {
                Play.Content = FindResource("Play");
                videoPanelViewModel.Pause();
                isPlaying = false;
            }
        }
        //pause the flight inspection with the pause button 
        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            videoPanelViewModel.Pause();
        }

        //stop the flight inspection with the stop button 
        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            videoPanelViewModel.Pause();
        }

        private void speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void speed_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
