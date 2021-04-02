using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flight_Inspection.controls.FlightGear;
using Flight_Inspection.Pages.Settings;
using Flight_Inspection.Settings;

namespace Flight_Inspection.controls
{
    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;

        public ChartsModel()
        {
            timeSeries = new TimeSeries(SettingsViewModel.settingPacket.CSV.Content, SettingsViewModel.settingPacket.XML.Content);
        }

        public List<float> getData(string property)
        {
            return timeSeries.getFeatureData(property);
        }

        public List<Property> GetProperties()
        {
            List<string> ls = timeSeries.getFeatureNames();
            List<Property> lp = new List<Property>();
            foreach (string s in ls)
            {
                lp.Add(new Property() { Name = s });
            }
            return lp;
        }
        public Dictionary<int, float> getDataContent(string content)
        {
            List<float> vs = getData(content);
            Dictionary<int, float> value = new Dictionary<int, float>();
            for (int i = 150; i < 300; i++)
            {
                value.Add(i, vs[i]);
            }
            return value;
        }
        public List<(float, float)> getDataContent(string content, string second)
        {
            List<float> vs = getData(content);
            List<float> sec = getData(second);
            List<(float, float)> value = new List<(float, float)>();
            int size = Math.Min(vs.Count, sec.Count); ;
            for (int i = 0; i < size; i++)
            {
                value.Add((vs[i], sec[i]));
            }
            return value;
        }
    }
}
