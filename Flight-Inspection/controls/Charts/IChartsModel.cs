using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static Flight_Inspection.controls.AnalomyDetectorClass;

namespace Flight_Inspection.controls
{
    class Property
    {
        private string name;
        private string attach;
        private LineSafe linearReg;
        private List<float> data;
        
        public string Name { get => name; set => name = value; }
        public string Attach { get => attach; set => attach = value; }
        public List<float> Data
        {
            get => data; set
            {
                if (data == null)
                {
                    data = value;
                }

            }
        }
        public LineSafe LinearReg { get => linearReg; set => linearReg = value; }
       
    }
    interface IChartsModel : INotifyPropertyChanged
    {
        //void setTimeSeries();
        Property getData(string property);
        List<Property> GetProperties();
    }
}
