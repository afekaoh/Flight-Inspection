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
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;
using System.Drawing;

namespace Flight_Inspection.controls
{

    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;
        private List<Property> properties = new List<Property>();
        SeriesCollection series, series2, series3;

        public SeriesCollection Series
        {
            get => series; set
            {
                series = value;
                //INotifyPropertyChanged("Series");
            }
        }
        public SeriesCollection Series2
        {
            get => series2; set
            {
                series2 = value;
                //INotifyPropertyChanged("Series2");
            }
        }
        public SeriesCollection Series3
        {
            get => series3; set
            {
                series3 = value;
                INotifyPropertyChanged("Series3");
            }
        }
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
            Series = new SeriesCollection();
            Series2 = new SeriesCollection();
            Series3 = new SeriesCollection();
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
                    float val = pearson(data,data2, sizeTable, sizeTable);
                    val = Math.Abs(val);
                    if (maxVal <= val)
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
        public void updateSeries(string content)
        {
            if (content == "")
                return;
            Property property = getData(content);
            List<float> vs = property.Data;
            List<float> attach = getData(property.Attach).Data;
            ChartValues<ScatterPoint> points = new ChartValues<ScatterPoint>();
            ChartValues<ScatterPoint> points2 = new ChartValues<ScatterPoint>();
            ChartValues<ScatterPoint> points3 = new ChartValues<ScatterPoint>();
            for (int i = 0; i < vs.Count; i++)
            {
                points.Add(new ScatterPoint(i, vs[i]));
                points2.Add(new ScatterPoint(i, attach[i]));
                points3.Add(new ScatterPoint(vs[i], attach[i]));
            }
            Series.Clear();
            Series.Add(new ScatterSeries
            {
                Values = points,
                MaxPointShapeDiameter = 5,
                MinPointShapeDiameter = 5
            });
            Series2.Clear();
            Series2.Add(new ScatterSeries
            {
                Values = points2,
                MaxPointShapeDiameter = 5,
                MinPointShapeDiameter = 5
            });
            Series3.Clear();
            Series3.Add(new ScatterSeries
            {
                 Values = points3,
                 MaxPointShapeDiameter=5,
                 MinPointShapeDiameter=5
            });
            Series3.Add(new LineSeries
            {
                Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(property.LinearReg.X1,property.LinearReg.Y1),
                        new ScatterPoint(property.LinearReg.X2,property.LinearReg.Y2),
                    },
                PointGeometry = null,
                Fill = System.Windows.Media.Brushes.Transparent
            });
        }
       
        public Line getLinearReg(List<float> current, List<float> sec)
        {
            Line linearReg = new Line();
            unsafe
            {
                Line2* l = (Line2*)linear_reg(current.ToArray(), sec.ToArray(), current.Count);
                if (float.IsNaN(l->b) || float.IsNaN(l->a))
                {
                    linearReg.X1 = 0;
                    linearReg.X2 = 0;
                    linearReg.Y1 = 0;
                    linearReg.Y2 = 0;
                }
                else
                {
                    double x1 = current.Min(), x2 = current.Max(), y1 = l->b + x1 * l->a, y2 = l->b + x2 * l->a;
                    linearReg.X1 = x1;
                    linearReg.X2 = x2;
                    linearReg.Y1 = y1;
                    linearReg.Y2 = y2;
                }
                freeLine2((IntPtr)l);
            }
            return linearReg;
        }
    }
}
