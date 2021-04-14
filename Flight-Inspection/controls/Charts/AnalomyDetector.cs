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

        private string pathCsvNormal;

        public string PathCsvNormal
        {
            get => pathCsvNormal;
            set
            {
                pathCsvNormal = value;
            }
        }
        private string pathCsvTest;

        public string PathCsvTest
        {
            get => pathCsvTest;
            set
            {
                pathCsvTest = value;
            }
        }
        public TimeSeries ts { get; set; }

        public AnalomyDetector(SettingsArgs settingsArgs)
        {
            this.PathCsvNormal = settingsArgs.CSV_Normal;
            this.PathCsvTest = settingsArgs.CSV_Test;
            this.PathDll = settingsArgs.DLLPath;
            this.ts = settingsArgs.Ts;
        }

        public List<AnomalyReportSafe> GetAnomalyReport()
        {
            var properties = ts.GetFeatureNames();
            var a = LoadDll(pathDll);
            if (a)
            {
                LoadTimeSriesNormal(pathCsvNormal, properties);
                LoadTimeSriesTest(pathCsvTest, properties);
                var ar = GetAnomalyReports(ts);
                return ar;
            }
            return null;
        }
    }
}
