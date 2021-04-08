using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

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

        public Dictionary<int, float> getDataContent()
        {
            return charts.getDataContent(current.Name);

        }
        public Dictionary<int, float> getDataContentAttach()
        {
            return charts.getDataContent(current.Attach);

        }
        public List<(float, float)> getDataContentCurretnAndAttach()
        {
            return charts.getDataContentCor(current.Name);
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
            charts.TimeSeries = settingsArgs.ts;
            OnReady();
        }

        public Line getLinearReg()
        {
            return current.LinearReg;
        }
    }
}
