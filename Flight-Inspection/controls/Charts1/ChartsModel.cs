using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Flight_Inspection.Pages.Settings;
using Flight_Inspection.Settings;

namespace Flight_Inspection.controls
{

    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;
        private List<Property> properties = new List<Property>();

        public TimeSeries TimeSeries
        {
            get => timeSeries; set
            {
                timeSeries = value;
                INotifyPropertyChanged("TimeSeries");
            }
        }

        private void INotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        [DllImport("anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float pearson([MarshalAs(UnmanagedType.LPArray)] float[] x, [MarshalAs(UnmanagedType.LPArray)] float[] y, int sizeX, int sizeY);

        public ChartsModel()
        {
            PropertyChanged += updateProperties;
        }

        private void updateProperties(object sender, PropertyChangedEventArgs e)
        {
            List<string> ls = timeSeries.getFeatureNames();
            int sizeTable = TimeSeries.getFeatureData(ls[0]).Count;
            for (int i = 0; i<ls.Count; i++)
            {
                float maxVal = 0;
                string maxCor = "";
                float[] data = TimeSeries.getFeatureData(ls[i]).ToArray();
                for (int j = i + 1; j < ls.Count; j++)
                {
                    float val = pearson(data,
                    TimeSeries.getFeatureData(ls[j]).ToArray(), sizeTable, sizeTable);
                    val = Math.Abs(val);
                    if (maxVal < val)
                    {
                        maxVal = val;
                        maxCor = ls[j];
                    }
                }
                properties.Add(new Property() { Name = ls[i], Attach = maxCor, Data = data.ToList() });
            }
        }
        public Property getData(string property)
        {
            return (Property)properties.Find(prop => prop.Name == property);
        }

        public List<Property> GetProperties()
        {
            return properties;
        }
        public Dictionary<int, float> getDataContent(string content)
        {
            List<float> vs = getData(content).Data;
            Dictionary<int, float> value = new Dictionary<int, float>();
            for (int i = 150; i < 300; i++)
            {
                value.Add(i, vs[i]);
            }
            return value;
        }
        public List<(float, float)> getDataContentCor(string content)
        {
            Property property = getData(content);
            List<float> vs = property.Data;
            List<float> sec = getData(property.Attach).Data;
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
