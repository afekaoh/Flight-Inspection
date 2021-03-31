using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flight_Inspection.controls.FlightGear;

namespace Flight_Inspection.controls
{
    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;

        public ChartsModel()
        {
            timeSeries = new TimeSeries("C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\controls\\FlightGear\\reg_flight.csv", "C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\controls\\FlightGear\\playback_small.xml");
        }

        public List<float> getData(string property)
        {
            return timeSeries.getFeatureData(property);
        }

        public List<Property> GetProperties()
        {
            List<string> ls = timeSeries.getFeatureNames();
            List<Property> lp = new List<Property>();
            foreach(string s in ls)
            {
                lp.Add(new Property() { Name = s });
            }
            return lp;
        }
    }
}
