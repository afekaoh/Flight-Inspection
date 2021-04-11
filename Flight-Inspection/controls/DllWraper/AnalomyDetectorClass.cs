using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    class AnalomyDetectorClass
    {

        [DllImport("anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float pearson([MarshalAs(UnmanagedType.LPArray)] float[] x, [MarshalAs(UnmanagedType.LPArray)] float[] y, int sizeX, int sizeY);

        [DllImport("anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr linear_reg([MarshalAs(UnmanagedType.LPArray)] float[] x, [MarshalAs(UnmanagedType.LPArray)] float[] y, int size);

        [DllImport("anomaly_detection_util.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void freeLine2(IntPtr l);

        unsafe struct Line2
        {
            public float a, b;
        }

        public struct LineSafe
        {
            public float a, b;
        }

        public static LineSafe getLinearReg(List<float> current, List<float> sec)
        {
            LineSafe linearReg;
            unsafe
            {
                Line2* l = (Line2*)linear_reg(current.ToArray(), sec.ToArray(), current.Count);
                if (float.IsNaN(l->b) || float.IsNaN(l->a))
                {
                    linearReg.a = 0;
                    linearReg.b = 0;
                }
                else
                {
                    linearReg.a = l->a;
                    linearReg.b = l->b;
                }
                freeLine2((IntPtr)l);
            }
            return linearReg;
        }

        public static float pearson(List<float> current, List<float> sec)
        {
            return pearson(current.ToArray(), sec.ToArray(), current.Count, sec.Count);
        }
    }
}
