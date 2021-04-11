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
        private string pathDll = "C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\plugins\\anomaly_detec_linear_reg.dll";
        private string pathCsv = "C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\Pages\\Settings\\FG_Data\\reg_flight.csv";


        public AnalomyDetector()
        {

        }

        public List<AnomalyReportSafe> GetAnomalyReport(List<string> properties)
        {
            LoadDll(pathDll);
            LoadTimeSriesNormal(pathCsv, properties);
            string detect = "C:\\Users\\avri2\\OneDrive\\Desktop\\exersice\\anomaly_flight.csv";
            LoadTimeSriesTest(detect, properties);
            return GetAnomalyReports();
        }

    }
}
