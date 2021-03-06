using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using static Flight_Inspection.controls.AnalomyDetectorClass;
using Flight_Inspection.controls.Charts;
using static Flight_Inspection.controls.DllWraper.AnalomyReportWraper;
using System.Windows.Media;

namespace Flight_Inspection.controls
{
    /**
     * The Model that handeles the charts.
     * In the charts we used the library of LiveCharts - > https://lvcharts.net/
     */
    class ChartsModel : INotifyPropertyChanged
    {

        //Property that saves all the anomaly points from the dll
        ChartValues<ObservablePoint> analomyPoints;

        //all the points of the choosen values in function of time
        ChartValues<ObservablePoint> chartVal;

        //all the points of the most correlated values in function of time
        ChartValues<ObservablePoint> chartValAttch;

        //all the points of the chosen values in function of most correlated values
        ChartValues<ObservablePoint> chartValCurrentAndAttach;

        //the last thirty secconds of the points
        ChartValues<ObservablePoint> lastThirty;

        //the linear reg of the chosen values in function of most correlated values
        ChartValues<ObservablePoint> linearRegVal;
        private List<AnomalyReportSafe> lsReports;
        private Dictionary<ObservablePoint, int> mapper;
        private List<Property> properties = new List<Property>();
        private TimeSeries timeSeries;
        private AnalomyDetector anomalyDetector;


        //the max value of the Time (x axis)
        private double xMax;

        //the max value of the most correlated element
        private double xMaxAttach = 1000;

        //the max value of the chosen element (x axis in the third graph)
        private double xMaxThird;

        //the min value of the most correlated element
        private double xMinAttach = 0;

        //the min value of the chosen element (x axis in the third graph)
        private double xMinThird;

        public ChartsModel() { PropertyChanged += updateProperties; }

        public event PropertyChangedEventHandler PropertyChanged;

        //notifies all the observers about the change (mvvm)
        private void INotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        //update all the needed data for the charts.
        private void updateProperties(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "TimeSeries")
                return;
            List<string> ls = timeSeries.GetFeatureNames();
            mapper = new Dictionary<ObservablePoint, int>();
            AnalomyPoints = new ChartValues<ObservablePoint>();
            LastThirty = new ChartValues<ObservablePoint>();
            LinearRegVal = new ChartValues<ObservablePoint>();
            ChartValues = new ChartValues<ObservablePoint>();
            ChartValuesAttach = new ChartValues<ObservablePoint>();
            ChartValuesCurrentAndAttach = new ChartValues<ObservablePoint>();
            //calaulates all the correlated data and saves them in properties.
            int sizeTable = TimeSeries.GetFeatureData(ls[0]).Count;
            for (int i = 0; i < ls.Count; i++)
            {
                float maxVal = 0;
                string maxCor = "";
                float[] data = TimeSeries.GetFeatureData(ls[i]).ToArray();
                for (int j = 0; j < ls.Count; j++)
                {
                    if (i == j)
                        continue;
                    float[] data2 = TimeSeries.GetFeatureData(ls[j]).ToArray();
                    float val = pearson(data, data2, sizeTable, sizeTable);
                    val = Math.Abs(val);
                    if (maxVal <= val)
                    {
                        maxVal = val;
                        maxCor = ls[j];
                    }
                }
                properties.Add(
                    new Property()
                    {
                        Name = ls[i],
                        Attach = maxCor,
                        Data = data.ToList(),
                        LinearReg = getLinearReg(data.ToList(), TimeSeries.GetFeatureData(maxCor))
                    });
            }
        }

        //return the property according to the name
        public Property getData(string property) { return (Property)properties.Find(prop => prop.Name == property); }

        //returns all the properties
        public List<Property> GetProperties() { return properties; }

        public int returnTimeOfPoint(ChartPoint point)
        {
            foreach (var p in mapper)
            {
                if (p.Key.X == point.X && p.Key.Y == point.Y)
                    return p.Value;
            }
            return -1;
        }

        //updates the series according to the new choosen value
        public void updateSeries(string content, int time)
        {
            if (content == "")
                return;
            Property property = getData(content);
            List<float> vs = property.Data;
            List<float> attach = getData(property.Attach).Data;
            //create an array of the new wanted points.
            ObservablePoint[] points = new ObservablePoint[vs.Count];
            ObservablePoint[] points2 = new ObservablePoint[vs.Count];
            ObservablePoint[] points3 = new ObservablePoint[vs.Count];
            for (int i = 0; i < vs.Count; i++)
            {
                points[i] = new ObservablePoint(i, vs[i]);
                points2[i] = new ObservablePoint(i, attach[i]);
                points3[i] = new ObservablePoint(vs[i], attach[i]);
            }
            if (lsReports != null)
            {
                AnalomyPoints.Clear();
                foreach (AnomalyReportSafe reportSafe in lsReports)
                {
                    if (reportSafe.first == content)
                    {
                        AnalomyPoints.Add(points3[reportSafe.time]);
                        mapper.Add(points3[reportSafe.time], time);
                    }
                }
                INotifyPropertyChanged("AnalomyPoints");
            }
            //update the new data
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
            ChartValuesAttach.Clear();
            ChartValuesAttach.AddRange(points2);
            INotifyPropertyChanged("ChartValuesAttach");
            ChartValuesCurrentAndAttach.Clear();
            ChartValuesCurrentAndAttach.AddRange(points3);
            INotifyPropertyChanged("ChartValuesCurrentAndAttach");
            try
            {
                LastThirty.Clear();
                for (int i = time - 200 < 0 ? 0 : time - 200; i < (time < xMax ? time : xMax); i++)
                {
                    LastThirty.Add(ChartValuesCurrentAndAttach[i]);
                }
            }
            catch (Exception)
            {
            }
            LineSafe line = getLinearReg(vs, attach);
            LinearRegVal.Clear();
            float x1 = vs.Min(), y1 = line.b + x1 * line.a;
            float x2 = vs.Max(), y2 = line.b + x2 * line.a;
            LinearRegVal.Add(new ObservablePoint(x1, y1));
            LinearRegVal.Add(new ObservablePoint(x2, y2));
        }
        public void SetSettings(SettingsArgs settingsArgs)
        {
            this.TimeSeries = settingsArgs.Ts;
            if (settingsArgs.DLLPath != null)
            {
                this.anomalyDetector = new AnalomyDetector(settingsArgs);
                lsReports = anomalyDetector.GetAnomalyReport();
            }
        }

        public ChartValues<ObservablePoint> AnalomyPoints
        {
            get => analomyPoints;
            set
            {
                analomyPoints = value;
                INotifyPropertyChanged("AnalomyPoints");
            }
        }

        public ChartValues<ObservablePoint> ChartValues
        {
            get => chartVal;
            set
            {
                INotifyPropertyChanged("ChartValues");
                chartVal = value;
            }
        }

        public ChartValues<ObservablePoint> ChartValuesAttach
        {
            get => chartValAttch;
            set
            {
                INotifyPropertyChanged("ChartValuesAttach");
                chartValAttch = value;
            }
        }

        public ChartValues<ObservablePoint> ChartValuesCurrentAndAttach
        {
            get => chartValCurrentAndAttach;
            set
            {
                chartValCurrentAndAttach = value;
                INotifyPropertyChanged("ChartValuesCurrentAndAttach");
            }
        }

        public ChartValues<ObservablePoint> LastThirty
        {
            get => lastThirty;
            set
            {
                lastThirty = value;
                INotifyPropertyChanged("LastThirty");
            }
        }

        public ChartValues<ObservablePoint> LinearRegVal
        {
            get => linearRegVal;
            set
            {
                linearRegVal = value;
                INotifyPropertyChanged("LinearRegVal");
            }
        }

        //comtains all the information
        public TimeSeries TimeSeries
        {
            get => timeSeries;
            set
            {
                timeSeries = value;
                INotifyPropertyChanged("TimeSeries");
            }
        }

        public double XMax
        {
            get => this.xMax;
            set
            {
                this.xMax = value;
                INotifyPropertyChanged("XMax");
            }
        }

        public double XMaxAttach
        {
            get => this.xMaxAttach;
            set
            {
                this.xMaxAttach = value;
                INotifyPropertyChanged("XMaxAttach");
            }
        }

        public double XMaxThird
        {
            get => this.xMaxThird;
            set
            {
                this.xMaxThird = value;
                INotifyPropertyChanged("XMaxThird");
            }
        }

        public double XMinAttach
        {
            get => this.xMinAttach;
            set
            {
                this.xMinAttach = value;
                INotifyPropertyChanged("XMinAttach");
            }
        }

        public double XMinThird
        {
            get => this.xMinThird;
            set
            {
                this.xMinThird = value;
                INotifyPropertyChanged("XMinThird");
            }
        }
    }
}
