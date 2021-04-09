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
using static Flight_Inspection.controls.AnalomyDetectorClass;

namespace Flight_Inspection.controls
{

    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;
        private List<Property> properties = new List<Property>();
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

        public ChartsModel()
        {
            PropertyChanged += updateProperties;
        }

        private void updateProperties(object sender, PropertyChangedEventArgs e)
        {
         
            if (e.PropertyName != "TimeSeries")
                return;
            ReadOnlyCollection<string> ls = timeSeries.getFeatureNames().AsReadOnly();
            LinearRegVal = new ChartValues<ObservablePoint>();
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
            for (int i = 0; i < vs.Count -4; i+=4)
            {
                //loop unroling
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
            LineSafe line = getLinearReg(vs, attach);
            LinearRegVal.Clear();
            float x1 = vs.Min(), y1 = line.b + x1 * line.a;
            float x2 = vs.Max(), y2 = line.b + x2 * line.a;
            LinearRegVal.Add(new ObservablePoint(x1, y1));
            LinearRegVal.Add(new ObservablePoint(x2, y2));
            XMax = vs.Count;
            XMin = 0;
            XMaxThird = vs.Max();
            XMinThird = vs.Min();
        }
       
        
    }
}
