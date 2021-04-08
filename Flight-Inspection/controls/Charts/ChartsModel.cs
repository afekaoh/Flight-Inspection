using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Flight_Inspection.Pages.Settings;
using Flight_Inspection.Settings;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.ObjectModel;

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

        [DllImport("anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linear_reg([MarshalAs(UnmanagedType.LPArray)] float[] x, [MarshalAs(UnmanagedType.LPArray)] float[] y, int size);

        [DllImport("anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void freeLine2(IntPtr l);

        [StructLayout(LayoutKind.Sequential)]
        unsafe struct Line2
        {
            public float a, b;
        }

        public ChartsModel()
        {
            PropertyChanged += updateProperties;
        }

        private void updateProperties(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "TimeSeries")
                return;
            ReadOnlyCollection<string> ls = timeSeries.getFeatureNames().AsReadOnly();
            int sizeTable = TimeSeries.getFeatureData(ls[0]).Count;
            for (int i = 0; i < ls.Count; i++)
            {
                float maxVal = 0;
                string maxCor = "";
                float[] data = TimeSeries.getFeatureData(ls[i]).ToArray();
                for (int j = 0; j < ls.Count; j++)
                {
                    if (i == j)
                        continue;
                    float[] data2 = TimeSeries.getFeatureData(ls[j]).ToArray();
                    float val = pearson(data,data2, data.Length, data2.Length);
                    val = Math.Abs(val);
                    if (maxVal < val)
                    {
                        maxVal = val;
                        maxCor = ls[j];
                    }
                }
                properties.Add(new Property() { Name = ls[i], Attach = maxCor, Data = data.ToList() ,LinearReg = getLinearReg(data.ToList(), TimeSeries.getFeatureData(maxCor)) });
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
            if (content == "")
                return null;
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
            if (content == "")
                return null;
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

        public Line getLinearReg(List<float> current, List<float> sec)
        {
            Line linearReg = new Line();
            unsafe
            {
                Line2* l = (Line2*)linear_reg(current.ToArray(), sec.ToArray(), current.Count);
                double x1 = 0,x2 = current.Max()+10, y1=l->b,y2=y1+x2*l->a;
                linearReg.X1 = x1;
                linearReg.X2 = x2;
                linearReg.Y1 = y1;
                linearReg.Y2 = y2;
            }
            return linearReg;
        }
    }
}
