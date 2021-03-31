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
        public List<float> getData(string property)
        {
            return charts.getData(property);
        }
    }
}
