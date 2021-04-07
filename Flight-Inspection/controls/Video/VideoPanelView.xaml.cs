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
    public partial class VideoPanelView : UserControl
    {
        VideoPanelViewModel videoPanelViewModel;
        public VideoPanelView()
        {
            InitializeComponent();
            videoPanelViewModel = new VideoPanelViewModel(new VideoPanelModel());
            DataContext = videoPanelViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            videoPanelViewModel.MaxSliderUpdate(2);

        }
    }
}
