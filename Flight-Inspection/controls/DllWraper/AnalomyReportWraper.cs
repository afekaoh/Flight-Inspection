using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Flight_Inspection.controls.DllWraper
{
    public class AnalomyReportWraper
    {

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool loadDLL([MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport("anom_detec_conv.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void loadTimeSeriesNormal([MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] fetursName, int numOfFeatures);

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


        unsafe public struct AnomalyReports
        {
            public int first;
            public int second;
            public int time;
        };

        public struct AnomalyReportSafe
        {
            public string first;
            public string second;
            public int time;
        };

        unsafe public struct AnomalyReportArray
        {

            public AnomalyReports* anomalyReports;
            public int size;
        };

        public static bool LoadDll(string path)
        {
            try
            {
                return loadDLL(path);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void LoadTimeSriesNormal(string path, List<string> featureNames)
        {
            loadTimeSeriesNormal(path, featureNames.ToArray(), featureNames.Count);
        }
        public static void LoadTimeSriesTest(string path, List<string> featureNames)
        {

            loadTimeSeriesTest(path, featureNames.ToArray(), featureNames.Count);

        }

        public static List<AnomalyReportSafe> GetAnomalyReports(TimeSeries ts)
        {
            List<AnomalyReportSafe> list = new List<AnomalyReportSafe>();
            unsafe
            {
                    IntPtr intPtr = getAnomalyReport();
                AnomalyReportArray wraper = (AnomalyReportArray)Marshal.PtrToStructure(intPtr, typeof(AnomalyReportArray));
                //AnomalyReportArray arr = getAnomalyReport();
                for (int i = 0; i < wraper.size; i++)
                {
                    AnomalyReports anomalyReports = wraper.anomalyReports[i];
                    // AnomalyReports anomalyReports = (AnomalyReports)Marshal.PtrToStructure(wraper.anomalyReports[i], typeof(AnomalyReports));
                    AnomalyReportSafe a = new AnomalyReportSafe();
                    a.first = ts.GetFeatureNames()[anomalyReports.first];
                    a.second = ts.GetFeatureNames()[anomalyReports.second];
                    a.time = anomalyReports.time;
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
