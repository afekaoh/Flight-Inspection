using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Joystick
{
    class NormelaizedData
    {
        private string name;
        private int canvasDim;
        private float max;
        private float min;
        private float data;
        public event EventHandler Ready;

        public NormelaizedData(string name, int CanvasDim, float max, float min)
        {
            this.name = name;
            this.canvasDim = CanvasDim;
            this.max = max;
            this.min = min;

        }
        public string Name { get => name; set => name = value; }
        public float Normalize { get { 
                return (data - min) * CanvasDim / (max - min); }
        }
        public float Data { get => data; set => data = value; }
        public int CanvasDim { get => canvasDim; }
        public void setCanvasDim(int val)
        {
            canvasDim = val;
            Console.WriteLine(canvasDim + "lala");
        }
    }
}
