using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    class VMCharts : INotifyPropertyChanged
    {
        private ChartsModel charts;

        public event PropertyChangedEventHandler PropertyChanged;

        public VMCharts()
        {
            charts = new ChartsModel();
        }

        public List<Property> GetNames()
        {
            return charts.GetProperties();
        }
        private List<float> getData(string property)
        {
            return charts.getData(property);
        }

        public Dictionary<int, float> getDataContent(string content)
        {
            return charts.getDataContent(content);

        }
        public List<(float, float)> getDataContent(string content, string second)
        {
            return charts.getDataContent(content, second);
        }

        internal void setTimeSeries(TimeSeries ts)
        {
            charts.TimeSeries = ts;
        }
    }
}
