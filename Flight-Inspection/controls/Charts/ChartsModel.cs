﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using static Flight_Inspection.controls.AnalomyDetectorClass;

namespace Flight_Inspection.controls
{

    class ChartsModel : IChartsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TimeSeries timeSeries;
        private List<Property> properties = new List<Property>();

        ChartValues<ObservablePoint> analomyPoints;
        public ChartValues<ObservablePoint> AnalomyPoints
        {
            get => analomyPoints; set
            {
                analomyPoints = value;
                INotifyPropertyChanged("AnalomyPoints");
            }
        }

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
        private double xMaxAttach = 1000;
        public double XMaxAttach
        {
            get => this.xMaxAttach;
            set
            {
                this.xMaxAttach = value;
                INotifyPropertyChanged("XMaxAttach");
            }
        }

        private double xMinAttach = 0;
        public double XMinAttach
        {
            get => this.xMinAttach;
            set
            {
                this.xMinAttach = value;
                INotifyPropertyChanged("XMinAttach");
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
            ChartValues = new ChartValues<ObservablePoint>();
            ChartValuesAttach = new ChartValues<ObservablePoint>();
            ChartValuesCurrentAndAttach = new ChartValues<ObservablePoint>();
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
            ObservablePoint[] points = new ObservablePoint[vs.Count];
            ObservablePoint[] points2 = new ObservablePoint[vs.Count];
            ObservablePoint[] points3 = new ObservablePoint[vs.Count];
            for (int i = 0; i < vs.Count; i++)
            {
                points[i] = new ObservablePoint(i, vs[i]);
                points2[i] = new ObservablePoint(i, attach[i]);
                points3[i] = new ObservablePoint(vs[i], attach[i]);
                
            }
            XMax = vs.Count;
            XMaxAttach = attach.Max();
            XMinAttach = attach.Min();
            XMaxThird = vs.Max();
            XMinThird = vs.Min();
            if (XMaxThird == XMinThird)
            {
                XMaxThird += 1;
                XMinThird -= 1;
            }
            ChartValues.Clear();
            ChartValues.AddRange(points);
            INotifyPropertyChanged("ChartValues");
            DataMapper = new CartesianMapper<ObservablePoint>().X(point => point.X).Y(point => point.Y);
            ChartValuesAttach.Clear();
            ChartValuesAttach.AddRange(points2);
            INotifyPropertyChanged("ChartValuesAttach");
            DataMapperAttach = new CartesianMapper<ObservablePoint>().X(point => point.X).Y(point => point.Y);
            ChartValuesCurrentAndAttach.Clear();
            ChartValuesCurrentAndAttach.AddRange(points3);
            INotifyPropertyChanged("ChartValuesCurrentAndAttach");
            DataMapperCurrentAndAttach = new CartesianMapper<ObservablePoint>().X(point => point.X).Y(point => point.Y);
            LineSafe line = getLinearReg(vs, attach);
            LinearRegVal.Clear();
            float x1 = vs.Min(), y1 = line.b + x1 * line.a;
            float x2 = vs.Max(), y2 = line.b + x2 * line.a;
            LinearRegVal.Add(new ObservablePoint(x1, y1));
            LinearRegVal.Add(new ObservablePoint(x2, y2));
        }
    }
}
