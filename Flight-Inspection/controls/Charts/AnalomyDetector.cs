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
        
        private string pathCsv;

        public string PathCsv
        {
            get => pathCsv;
            set
            {
                pathCsv = value;
            }
        }
        private string pathCsvTest;

        public string PathCsvTest
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
                string detect = "C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\plugins\\anomaly_detec_linear_reg.dll";
                LoadTimeSriesTest(detect, properties);
                var ar = GetAnomalyReports(ts);
                return ar;
            }
            return null;
        }
    }
}
