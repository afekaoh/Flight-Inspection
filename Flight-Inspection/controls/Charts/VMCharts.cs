using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Flight_Inspection.controls
{
    /**
     * The View Model that handeles the charts.
     *  In the charts we used the library of LiveCharts - > https://lvcharts.net/
     */
    class VMCharts : IControlViewModel
    {

        //Property that saves all the anomaly points from the dll
        ChartValues<ObservablePoint> analomyPoints;
        private ChartsModel charts;

        //all the points of the choosen values in function of time
        ChartValues<ObservablePoint> chartVal;

        //all the points of the most correlated values in function of time
        ChartValues<ObservablePoint> chartValAttach;

        //all the points of the chosen values in function of most correlated values
        ChartValues<ObservablePoint> chartValCurrentAndAttach;
        Property current;

        private int currentTime = 100;

        //the last thirty secconds of the points
        ChartValues<ObservablePoint> lastThirty;

        //the linear reg of the chosen values in function of most correlated values
        ChartValues<ObservablePoint> linearRegVal;

        private double xMax = 1000;

        //the max value of the most correlated element
        private double xMaxAttach = 1000;
        //the max value of the Time (x axis)
        private double xMaxThird = 1000;

        //the min value of the most correlated element
        private double xMinAttach = 0;

        //the max value of the chosen element (x axis in the third graph)
        private double xMinThird = 0;

        //the constructor of vm.
        public VMCharts()
        {
            charts = new ChartsModel();
            LastThirty = new ChartValues<ObservablePoint>();
            charts.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                //update the changed value
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
                    case "ChartValuesCurrentAndAttach":
                        ChartValuesCurrentAndAttach = charts.ChartValuesCurrentAndAttach;
                        break;
                    case "AnalomyPoints":
                        AnalomyPoints = charts.AnalomyPoints;
                        break;
                    case "LastThirty":
                        LastThirty = charts.LastThirty;
                        break;
                }
            };
        }

        public event EventHandler Ready;

        private void updateLastThirte(int time)
        {
            int num = time + 1 < (int)xMax ? time + 1 : (int)xMax;
            for (int i = Time; i < num; i++)
            {
                LastThirty.RemoveAt(0);
                LastThirty.Add(ChartValuesCurrentAndAttach[i]);
            }
        }

        internal override void setTime(int time)
        {
            updateLastThirte(time);
            this.Time = time;
        }

        // //returns all the properties
        public List<Property> GetNames() { return charts.GetProperties(); }

        public void OnReady() { Ready?.Invoke(this, EventArgs.Empty); }


        public override void SetSettings(SettingsArgs settingsArgs)
        {
            charts.SetSettings(settingsArgs);
            //charts.TimeSeries = settingsArgs.Ts;
            OnReady();
        }

        //updates the series according to the changed curent
        public void updateSeries()
        {
            if (current != null)
                charts.updateSeries(current.Name, Time);
        }

        public void updateTimeAccordingToPoint(ChartPoint point) { Time = charts.returnTimeOfPoint(point); }

        public ChartValues<ObservablePoint> AnalomyPoints
        {
            get => analomyPoints;
            set
            {
                analomyPoints = value;
                OnPropertyChanged("AnalomyPoints");
            }
        }

        public ChartValues<ObservablePoint> ChartValues
        {
            get => chartVal;
            set
            {
                chartVal = value;
                OnPropertyChanged("ChartValues");
            }
        }

        public ChartValues<ObservablePoint> ChartValuesAttach
        {
            get => chartValAttach;
            set
            {
                chartValAttach = value;
                OnPropertyChanged("ChartValuesAttach");
            }
        }

        public ChartValues<ObservablePoint> ChartValuesCurrentAndAttach
        {
            get => chartValCurrentAndAttach;
            set
            {
                chartValCurrentAndAttach = value;
                OnPropertyChanged("ChartValuesCurrentAndAttach");
            }
        }

        //the now choosen property
        public Property Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged("Current");
                updateSeries();
            }
        }

        public Func<double, string> LabelFormatter => value => value.ToString("F");

        // transforms the int to time
        public Func<double, string> LabelTime => value =>
        {
            int max = (int)value;
            float sec = (float)max / 10.0f;
            TimeSpan time = TimeSpan.FromSeconds(sec);
            return time.ToString(@"mm\:ss");
        };

        public ChartValues<ObservablePoint> LastThirty
        {
            get => lastThirty;
            set
            {
                lastThirty = value;
                OnPropertyChanged("LastThirty");
            }
        }

        public ChartValues<ObservablePoint> LinearRegVal
        {
            get => linearRegVal;
            set
            {
                linearRegVal = value;
                OnPropertyChanged("ChartValuesCurrentAndAttach");
            }
        }

        //the property that saves the current time of the program.
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

        public double XMaxAttach
        {
            get => this.xMaxAttach;
            set
            {
                this.xMaxAttach = value;
                OnPropertyChanged("XMaxAttach");
            }
        }

        public double XMaxThird
        {
            get => this.xMaxThird;
            set
            {
                this.xMaxThird = value;
                OnPropertyChanged("XMaxThird");
            }
        }

        public double XMinAttach
        {
            get => this.xMinAttach;
            set
            {
                this.xMinAttach = value;
                OnPropertyChanged("XMinAttach");
            }
        }

        public double XMinThird
        {
            get => this.xMinThird;
            set
            {
                this.xMinThird = value;
                OnPropertyChanged("XMinThird");
            }
        }
    }
}
