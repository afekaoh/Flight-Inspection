using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    class Property
    { 
        public string Name { get; set; }
    }
    interface IChartsModel : INotifyPropertyChanged
    {
        //void setTimeSeries();
        List<float> getData(string property);
        List<Property> GetProperties();
    }
}
