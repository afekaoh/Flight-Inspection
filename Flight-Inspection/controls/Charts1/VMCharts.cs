using System.Collections.Generic;
using System.ComponentModel;

namespace Flight_Inspection.controls
{
    class VMCharts : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ChartsModel charts;
        public VMCharts()
        {
            charts = new ChartsModel();
        }

        public List<Property> GetNames()
        {
            return charts.GetProperties();
        }

        public List<float> getData(string property)
        {
            return charts.getData(property);
        }
    }
}
