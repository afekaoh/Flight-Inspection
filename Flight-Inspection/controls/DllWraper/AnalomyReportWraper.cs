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
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool loadDLL([MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void loadTimeSeriesNormal([MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPArray)] string[] fetursName, int numOfFeatures);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void loadTimeSeriesTest([MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPArray)] string[] fetursName, int numOfFeatures);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getAnalomyReport();

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void setCorralationThreshhold(float threshold);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float getCorralationThreshhold();

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void deleteAnomalyReports(IntPtr anomalyDetector);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void releaseMemory();


        unsafe struct AnomalyReports
        {
            public char* first;
            public char* second;
            public long time;
        };

        public struct AnomalyReportSafe
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

        public static List<AnomalyReportSafe> GetAnomalyReports()
        {
            List<AnomalyReportSafe> list = new List<AnomalyReportSafe>();
            unsafe
            {
                IntPtr intPtr = getAnalomyReport();
                AnomalyReportArray wraper = (AnomalyReportArray)Marshal.PtrToStructure(intPtr, typeof(AnomalyReportArray));
                for (int i = 0; i < wraper.size; i++)
                {
                    AnomalyReportSafe a = new AnomalyReportSafe();
                    a.first = new string(wraper.anomalyReports[i].first);
                    a.second = new string(wraper.anomalyReports[i].second);
                    a.time = wraper.anomalyReports[i].time;
                    list.Add(a);
                }
                deleteAnomalyReports(intPtr);
            }
            return list;
        }

        public static void SetCorralationThreshhold(float threshold)
        {
            setCorralationThreshhold(threshold);
        }

        public static float GetCorralationThreshhold()
        {
            return getCorralationThreshhold();
        }

        public static void Release()
        {
            releaseMemory();
        }
    }
}
