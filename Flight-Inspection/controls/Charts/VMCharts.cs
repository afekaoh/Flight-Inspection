using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flight_Inspection.controls
{
    class VMCharts : IControlViewModel
    {
        private ChartsModel charts;
        public event EventHandler Ready;
        Property current;

        public Func<double, string> LabelFormatter => value => value.ToString("F");

        private int currentTime;

        public int CurrentTime
        {
            get => currentTime = 100;
            set
            {
                if (value <= xMax)
                    currentTime = value;
                OnPropertyChanged("CurrentTime");
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
        private object dataMapper;
        public object DataMapper
        {
            get => this.dataMapper;
            set
            {
                this.dataMapper = value;
                OnPropertyChanged("DataMapper");
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

        private object dataMapperAttach;
        public object DataMapperAttach
        {
            get => this.dataMapperAttach;
            set
            {
                this.dataMapperAttach = value;
                OnPropertyChanged("DataMapperAttach");
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

        private object dataMapperCurrentAndAttach;
        public object DataMapperCurrentAndAttach
        {
            get => this.dataMapperCurrentAndAttach;
            set
            {
                this.dataMapperCurrentAndAttach = value;
                OnPropertyChanged("DataMapperCurrentAndAttach");
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
            AnalomyPoints = new ChartValues<ObservablePoint>()
            {
                new ObservablePoint(0,1),
                new ObservablePoint(1,1)
            };
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
                    case "DataMapper":
                        DataMapper = charts.DataMapper;
                        break;
                    case "ChartValues":
                        ChartValues = charts.ChartValues;
                        break;
                    case "DataMapperAttach":
                        DataMapperAttach = charts.DataMapperAttach;
                        break;
                    case "ChartValuesAttach":
                        ChartValuesAttach = charts.ChartValuesAttach;
                        break;
                    case "LinearRegVal":
                        LinearRegVal = charts.LinearRegVal;
                        break;
                    case "ChartValuesCurrentAndAttach":
                        ChartValuesCurrentAndAttach = charts.ChartValuesCurrentAndAttach;
                        break;
                    case "DataMapperCurrentAndAttach":
                        DataMapperCurrentAndAttach = charts.DataMapperCurrentAndAttach;
                        break;
                    case "AnalomyPoints":
                        AnalomyPoints = charts.AnalomyPoints;
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
            charts.updateSeries(current.Name); 
        }

        internal override void setTime(int time)
        {
             this.CurrentTime = time;
        }
    }
}
