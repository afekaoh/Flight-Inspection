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
        private string pathDll = "C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\plugins\\anomaly_detec_linear_reg.dll";
        //property of the path to the client's dll
        public string PathDll
        {
            get => pathDll;
            set
            {
                pathDll = value;
            }
        }
        
        //property to the path of the new timeSeries
        private string pathCsv = "C:\\Users\\avri2\\source\\repos\\Flight-Inspection_\\Flight-Inspection\\Pages\\Settings\\FG_Data\\reg_flight.csv";

        public string PathCsv
        {
            get => pathCsv;
            set
            {
                pathCsv = value;
            }
        }

        //get all the points that are unnormal.
        public List<AnomalyReportSafe> GetAnomalyReport(List<string> properties)
        {
            var a = LoadDll(pathDll);
            if (a)
            {
                LoadTimeSriesNormal(pathCsv, properties);
                string detect = "C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\Pages\\Settings\\FG_Data\\anomaly_flight.csv";
                LoadTimeSriesTest(detect, properties);
                return GetAnomalyReports();
            }
            else
            {
                Console.WriteLine("oof");
                return null;
            }
        }
    }
}
