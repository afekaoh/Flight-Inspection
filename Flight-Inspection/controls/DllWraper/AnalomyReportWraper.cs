using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
                Console.WriteLine("error");
                return false;
            }
        }

        public static void LoadTimeSriesNormal(string path, List<string> featureNames)
        {
            try
            {
                loadTimeSeriesNormal(path, featureNames.ToArray(), featureNames.Count);
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }
        public static void LoadTimeSriesTest(string path, List<string> featureNames)
        {
            try
            {
                loadTimeSeriesTest(path, featureNames.ToArray(), featureNames.Count);
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }

        public static List<AnomalyReportSafe> GetAnomalyReports()
        {
            List<AnomalyReportSafe> list = new List<AnomalyReportSafe>();
            unsafe
            {
                try
                {
                    IntPtr intPtr = getAnomalyReport();
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
                catch (Exception)
                {
                    Console.WriteLine("error");
                }
                
            }
            return list;
        }

        public static void SetCorralationThreshhold(float threshold)
        {
            try
            {
                setCorralationThreshhold(threshold);
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }

        public static float GetCorralationThreshhold()
        {
            try
            {
                return getCorralationThreshhold();
            }
            catch (Exception)
            {
                Console.WriteLine("error");
                return float.NaN;
            }
        }

        public static void Release()
        {
            releaseMemory();
        }
    }
}
