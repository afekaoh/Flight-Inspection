using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Flight_Inspection.controls.DllWraper.AnalomyReportWraper;

namespace Flight_Inspection.controls.Charts
{
    /**
     * AnomalyDetector class to help with the points on the graph
     */
    class AnalomyDetector
    {
        //property of the path to the client's dll
        private string pathDll;
        public string PathDll
        {
            get => pathDll;
            set
            {
                pathDll = value;
            }
        }
        
        private string pathCsv";

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
