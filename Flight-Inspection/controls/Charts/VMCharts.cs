using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace Flight_Inspection.controls
{
    class VMCharts : IControlViewModel
    {
        private ChartsModel charts;
        public event EventHandler Ready;
        Property current;

        public Func<double, string> LabelFormatter => value => value.ToString("F");

        public Func<double,string> LabelTime => value => {
                int max = (int)value;
                float sec = (float)max / 10.0f;
                TimeSpan time = TimeSpan.FromSeconds(sec);
                return time.ToString(@"mm\:ss");
            };

        private int currentTime = 100;

        public int Time
        {
            get => currentTime;
            set
            {
                if (currentTime != value && value < xMax)
                {           
                    currentTime = value;
                    OnPropertyChanged(value);
                }
            }
        }

        private double xMax = 1000;

        private double xMaxThird = 1000;
        public double XMaxThird
        {
            get => this.xMaxThird;
            set
            {
                this.xMaxThird = value;
                OnPropertyChanged("XMaxThird");
            }
        }

        private double xMinThird = 0;
        public double XMinThird
        {
            get => this.xMinThird;
            set
            {
                this.xMinThird = value;
                OnPropertyChanged("XMinThird");
            }
        }

        private double xMaxAttach = 1000;
        public double XMaxAttach
        {
            get => this.xMaxAttach;
            set
            {
                this.xMaxAttach = value;
                OnPropertyChanged("XMaxAttach");
            }
        }
        private CartesianMapper<ObservablePoint> dataMapper;
        public CartesianMapper<ObservablePoint> DataMapper
        {
            get => this.dataMapper;
            set
            {
                this.dataMapper = value;
                OnPropertyChanged();
            }
        }

        private double xMinAttach = 0;
        public double XMinAttach
        {
            get => this.xMinAttach;
            set
            {
                this.xMinAttach = value;
                OnPropertyChanged("XMinAttach");
            }
        }
        

        ChartValues<ObservablePoint> lastThirty;
        public ChartValues<ObservablePoint> LastThirty
        {
            get => lastThirty; set
            {
                lastThirty = value;
                OnPropertyChanged("LastThirty");
            }
        }


        ChartValues<ObservablePoint> chartVal;
        public ChartValues<ObservablePoint> ChartValues
        {
            get => chartVal; set
            {
                chartVal = value;
                OnPropertyChanged("ChartValues");
            }
        }

        ChartValues<ObservablePoint> chartValAttach;
        public ChartValues<ObservablePoint> ChartValuesAttach
        {
            get => chartValAttach; set
            {
                chartValAttach = value;
                OnPropertyChanged("ChartValuesAttach");
            }
        }

        ChartValues<ObservablePoint> chartValCurrentAndAttach;
        public ChartValues<ObservablePoint> ChartValuesCurrentAndAttach
        {
            get => chartValCurrentAndAttach; set
            {
                chartValCurrentAndAttach = value;
                OnPropertyChanged("ChartValuesCurrentAndAttach");
            }
        }

        ChartValues<ObservablePoint> linearRegVal;
        public ChartValues<ObservablePoint> LinearRegVal
        {
            get => linearRegVal; set
            {
                linearRegVal = value;
                OnPropertyChanged("ChartValuesCurrentAndAttach");
            }
        }
        ChartValues<ObservablePoint> analomyPoints;
        public ChartValues<ObservablePoint> AnalomyPoints
        {
            get => analomyPoints; set
            {
                analomyPoints = value;
                OnPropertyChanged("AnalomyPoints");
            }
        }
        public Property Current
        {
            get => current; set
            {
                current = value;
                OnPropertyChanged("Current");
                updateSeries();
            }
        }

        public void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }

        public VMCharts()
        {
            charts = new ChartsModel();
            charts.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                switch (e.PropertyName)
                {
                    case "XMax":
                        xMax = charts.XMax;
                        break;
                    case "XMaxThird":
                        XMaxThird = charts.XMaxThird;
                        break;
                    case "XMinThird":
                        XMinThird = charts.XMinThird;
                        break;
                    case "XMaxAttach":
                        XMaxAttach = charts.XMaxAttach;
                        break;
                    case "XMinAttach":
                        XMinAttach = charts.XMinAttach;
                        break;
                    case "ChartValues":
                        ChartValues = charts.ChartValues;
                        break;
                    case "ChartValuesAttach":
                        ChartValuesAttach = charts.ChartValuesAttach;
                        break;
                    case "LinearRegVal":
                        LinearRegVal = charts.LinearRegVal;
                        break;
                    case "LastThirty":
                        lastThirty = charts.LastThirty;
                        break;
                    case "ChartValuesCurrentAndAttach":
                        ChartValuesCurrentAndAttach = charts.ChartValuesCurrentAndAttach;
                        break;
                    case "AnalomyPoints":
                        AnalomyPoints = charts.AnalomyPoints;
                        break;
                    case "DataMapper":
                        DataMapper = charts.DataMapper;
                        break;

                }
            };

        }

        public List<Property> GetNames()
        {
            return charts.GetProperties();
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            charts.TimeSeries = settingsArgs.Ts;
            OnReady();
        }

        public void updateSeries()
        {
            if (current != null)
                charts.updateSeries(current.Name);
        }

        internal override void setTime(int time)
        {
             this.Time = time;
        }
    }
}
