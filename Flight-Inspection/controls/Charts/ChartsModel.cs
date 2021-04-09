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
using LiveCharts.Configurations;

namespace Flight_Inspection.controls
{

    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;
        private List<Property> properties = new List<Property>();
        SeriesCollection series3;
        private double xMax;
        public double XMax
        {
            get => this.xMax;
            set
            {
                this.xMax = value;
                INotifyPropertyChanged("XMax");
            }
        }

        private double xMin;
        public double XMin
        {
            get => this.xMin;
            set
            {
                this.xMin = value;
                INotifyPropertyChanged("XMin");
            }
        }

        private double xMaxThird;
        public double XMaxThird
        {
            get => this.xMaxThird;
            set
            {
                this.xMaxThird = value;
                INotifyPropertyChanged("XMaxThird");
            }
        }

        private double xMinThird;
        public double XMinThird
        {
            get => this.xMinThird;
            set
            {
                this.xMinThird = value;
                INotifyPropertyChanged("XMinThird");
            }
        }
        private object dataMapper;
        public object DataMapper
        {
            get => this.dataMapper;
            set
            {
                this.dataMapper = value;
                INotifyPropertyChanged("DataMapper");
            }
        }

        private object dataMapperAttach;
        public object DataMapperAttach
        {
            get => this.dataMapperAttach;
            set
            {
                this.dataMapperAttach = value;
                INotifyPropertyChanged("DataMapperAttach");
            }
        }
        ChartValues<ObservablePoint> chartVal;
        public ChartValues<ObservablePoint> ChartValues { get => chartVal; set
            {
                INotifyPropertyChanged("ChartValues");
                chartVal= value;
            }
        }

        ChartValues<ObservablePoint> chartValAttch;
        public ChartValues<ObservablePoint> ChartValuesAttach { get => chartValAttch; set {
                INotifyPropertyChanged("ChartValuesAttach");
                chartValAttch = value; 
            }
        }

        ChartValues<ObservablePoint> chartValCurrentAndAttach;
        public ChartValues<ObservablePoint> ChartValuesCurrentAndAttach
        {
            get => chartValCurrentAndAttach; set
            {
                chartValCurrentAndAttach = value;
                INotifyPropertyChanged("ChartValuesCurrentAndAttach");
            }
        }

        private object dataMapperCurrentAndAttach;
        public object DataMapperCurrentAndAttach
        {
            get => this.dataMapperCurrentAndAttach;
            set
            {
                this.dataMapperCurrentAndAttach = value;
                INotifyPropertyChanged("DataMapperCurrentAndAttach");
            }
        }
        ChartValues<ObservablePoint> linearRegVal;
        public ChartValues<ObservablePoint> LinearRegVal
        {
            get => linearRegVal; set
            {
                linearRegVal = value;
                INotifyPropertyChanged("LinearRegVal");
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
            ChartValues<ObservablePoint> points = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> points2 = new ChartValues<ObservablePoint>();
            ChartValues<ObservablePoint> points3 = new ChartValues<ObservablePoint>();
            ObservablePoint[] pointUpdate = new ObservablePoint[4];
            for (int i = 0; i < vs.Count-4; i+=4)
            {
                pointUpdate[0] = new ObservablePoint(i, vs[i]);
                pointUpdate[1] = new ObservablePoint(i+1, vs[i+1]);
                pointUpdate[2] = new ObservablePoint(i+2, vs[i+2]);
                pointUpdate[3] = new ObservablePoint(i+3, vs[i+3]);
                points.AddRange(pointUpdate);
                pointUpdate[0] = new ObservablePoint(i, attach[i]);
                pointUpdate[1] = new ObservablePoint(i + 1, attach[i + 1]);
                pointUpdate[2] = new ObservablePoint(i + 2, attach[i + 2]);
                pointUpdate[3] = new ObservablePoint(i + 3, attach[i + 3]);
                points2.AddRange(pointUpdate);
                pointUpdate[0] = new ObservablePoint(vs[i], attach[i]);
                pointUpdate[1] = new ObservablePoint(vs[i + 1], attach[i + 1]);
                pointUpdate[2] = new ObservablePoint(vs[i + 2], attach[i + 2]);
                pointUpdate[3] = new ObservablePoint(vs[i + 3], attach[i + 3]);
                points3.AddRange(pointUpdate);
            }
            ChartValues = points;
            DataMapper = new CartesianMapper<ObservablePoint>().X(point => point.X).Y(point => point.Y);
            ChartValuesAttach = points2;
            DataMapperAttach = new CartesianMapper<ObservablePoint>().X(point => point.X).Y(point => point.Y);
            ChartValuesCurrentAndAttach = points3;
            DataMapperCurrentAndAttach = new CartesianMapper<ObservablePoint>().X(point => point.X).Y(point => point.Y);
            Line line = getLinearReg(vs, attach);
            LinearRegVal = new ChartValues<ObservablePoint>();
            LinearRegVal.Add(new ObservablePoint(line.X1, line.Y1));
            LinearRegVal.Add(new ObservablePoint(line.X2, line.Y2));
            XMax = vs.Count;
            XMin = 0;
            XMaxThird = vs.Max();
            XMinThird = vs.Min();
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
