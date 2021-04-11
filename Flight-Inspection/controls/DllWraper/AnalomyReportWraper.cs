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
        public static extern IntPtr getAnomalyReport();

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
            public IntPtr anomalyReports;
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
            List<AnomalyReports> list = new List<AnomalyReports>();
            unsafe
            {
                IntPtr intPtr = getAnomalyReport();
                AnomalyReportArray wraper = (AnomalyReportArray)Marshal.PtrToStructure(intPtr, typeof(AnomalyReportArray));
                AnomalyReports anomaly;
                IntPtr ptr = wraper.anomalyReports;
                for (int i = 0; i < wraper.size; i++)
                {
                    anomaly = (AnomalyReports)Marshal.PtrToStructure(ptr, typeof(AnomalyReports));
                    //AnomalyReportSafe a = new AnomalyReportSafe();
                    /*a.first = new string(anomaly.first);
                    a.second = new string(anomaly.second);
                    a.time = anomaly.time;*/
                    list.Add(anomaly);
                    ptr = new IntPtr(ptr.ToInt32() + sizeof(AnomalyReports));
                }
                deleteAnomalyReports(intPtr);
            }
            Console.WriteLine("lal");
            return null;
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
