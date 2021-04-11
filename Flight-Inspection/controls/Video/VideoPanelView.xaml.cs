
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
        VideoPanelViewModel videoPanelViewModel;
        public VideoPanelView()
        {
            InitializeComponent();
            videoPanelViewModel = new VideoPanelViewModel();
            DataContext = videoPanelViewModel;
        }

        public IControlViewModel GetViewModel()
        {
            return videoPanelViewModel;
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            videoPanelViewModel.StartPlay();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            videoPanelViewModel.Pause();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            videoPanelViewModel.Pause();
        }

        private void onSpeedChange(object sender, ContextMenuEventArgs e)
        {
            
        }
    }
}
