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
        private Property getData(string property)
        {
            return charts.getData(property);
        }

        public Dictionary<int, float> getDataContent(string content)
        {
            return charts.getDataContent(content);

        }
        public List<(float, float)> getDataContent(string content, string second)
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
