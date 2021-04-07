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
        private string name;
        private string attach;
        private List<float> data;
        public string Name { get => name; set => name = value; }
        public string Attach { get => attach; set => attach = value; }
        public List<float> Data { get => data; set {
                if (data == null)
                {
                    data = value;
                }
            
            } 
        }
    }
    interface IChartsModel : INotifyPropertyChanged
    {
        //void setTimeSeries();
        Property getData(string property);
        List<Property> GetProperties();
    }
}
