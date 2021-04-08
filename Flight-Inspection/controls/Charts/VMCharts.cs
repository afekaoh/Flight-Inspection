using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Flight_Inspection.controls
{
    class VMCharts : IControlViewModel
    {
        private ChartsModel charts;
        public event EventHandler Ready;
        Property current;
        SeriesCollection series, series2, series3;

        public SeriesCollection Series
        {
            get => series; set
            {
                series = value;
                OnPropertyChanged("Series");
            }
        }
        public SeriesCollection Series2
        {
            get => series2; set
            {
                series2 = value;
                OnPropertyChanged("Series2");
            }
        }
        public SeriesCollection Series3
        {
            get => series3; set
            {
                series3 = value;
                OnPropertyChanged("Series3");
            }
        }
        public Property Current {
            get => current; set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        }

        public void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }

        public VMCharts()
        {
            charts = new ChartsModel();
            charts.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                Series = charts.Series;
                Series2 = charts.Series2;
                Series3 = charts.Series3;
            };

        }

        public List<Property> GetNames()
        {
            return charts.GetProperties();
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            charts.TimeSeries = settingsArgs.ts;
            OnReady();
        }

        public void updateSeries()
        {
            charts.updateSeries(current.Name);
        }
    }
}
