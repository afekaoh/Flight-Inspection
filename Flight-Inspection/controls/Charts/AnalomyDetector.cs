using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Flight_Inspection.controls.DllWraper.AnalomyReportWraper;

namespace Flight_Inspection.controls.Charts
{
    class AnalomyDetector
    {
        private string pathDll = "C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\plugins\\anomaly_detec_linear_reg.dll";
        public string PathDll
        {
            get => pathDll;
            set
            {
                pathDll = value;
            }
        }

        private string pathCsv = "C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\Pages\\Settings\\FG_Data\\reg_flight.csv";

        public string PathCsv
        {
            get => pathCsv;
            set
            {
                pathCsv = value;
            }
        }

        public AnalomyDetector()
        {

        }

        public List<AnomalyReportSafe> GetAnomalyReport(List<string> properties, TimeSeries ts)
        {
            var a = LoadDll(pathDll);
            if (a)
            {
                LoadTimeSriesNormal(pathCsv, properties);
                string detect = "C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\Pages\\Settings\\FG_Data\\anomaly_flight.csv";
                LoadTimeSriesTest(detect, properties);
                var ar = GetAnomalyReports(ts);
                return ar;
            }
            return null;
        }
    }
}
