using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    class VMCharts : IControlViewModel
    {
        private ChartsModel charts;
        public event EventHandler Ready;
        Property current;
        public Property Current {
            get => current; set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        }

        public void OnReady()
        {
            Ready?.Invoke(this, EventArgs.Empty);
        }

        public VMCharts()
        {
            charts = new ChartsModel();
        }

        public List<Property> GetNames()
        {
            return charts.GetProperties();
        }

        public Dictionary<int, float> getDataContent(string content)
        {
            return charts.getDataContent(content);

        }
        public List<(float, float)> getDataContentAttach(string content)
        {
            return charts.getDataContentCor(content);
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
            charts.TimeSeries = settingsArgs.ts;
            OnReady();
        }
    }
}
