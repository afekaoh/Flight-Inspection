using Flight_Inspection.Windows.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.DllWraper
{
    class AnalomyReportWraper
    {
        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern bool loadDLL([MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void loadTimeSeriesNormal([MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPArray)] string[] fetursName, int numOfFeatures);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void loadTimeSeriesTest([MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPArray)] string[] fetursName, int numOfFeatures);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getAnalomyReport();

        unsafe struct AnomalyReports
        {
            public char* first;
            public char* second;
            public long time;
        };

        public struct AnomalyReportsSafe
        {
            public string first;
            public string second;
            public long time;
        };

        unsafe struct AnomalyReportArray
        {
            public AnomalyReports* anomalyReports;
            public int size;
        };

        public static bool LoadDll(string path)
        {
            return loadDLL(path);
        }

        public static void LoadTimeSriesNormal(string path, List<string> featureNames)
        {
            loadTimeSeriesNormal(path, featureNames.ToArray(), featureNames.Count);
        }
        public static void LoadTimeSriesTest(string path, List<string> featureNames)
        {
            loadTimeSeriesTest(path, featureNames.ToArray(), featureNames.Count);
        }

        public static List<AnomalyReportsSafe> GetAnomalyReports()
        {
            List<AnomalyReportsSafe> list = new List<AnomalyReportsSafe>();
            unsafe
            {
                AnomalyReportArray wraper = (AnomalyReportArray)Marshal.PtrToStructure(getAnalomyReport(), typeof(AnomalyReportArray));
                for(int i = 0; i < wraper.size; i++)
                {
                    AnomalyReportsSafe a = new AnomalyReportsSafe();
                    a.first = new string(wraper.anomalyReports[i].first);
                    a.second = new string(wraper.anomalyReports[i].second);
                    a.time = wraper.anomalyReports[i].time;
                    list.Add(a);
                }
            }
            return list;
        }
    }
}
