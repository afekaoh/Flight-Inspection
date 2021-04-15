// oz rigler 316291897 15/04/2021
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Joystick
{
    // a class that holds feature properties such as the range that it drawn on the canvas and its max value
    // the main goal is to set a multiplier that will normelaize the data to the specific drawing range for each feture represented
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
        }
    }
}
