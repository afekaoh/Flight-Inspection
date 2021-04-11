using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Joystick
{
    class JoyStickData
    {
        private string name;
        private float normalize;
        private float data;

        public JoyStickData(string name, float CanvasDim, float maxVal)
        {
            this.name = name;
            this.normalize = CanvasDim/maxVal;
        }
        public string Name { get => name; set => name = value; }
        public float Normalize { get => normalize; set => normalize = value; }
        public float Data { get => data; set => data = value; }
    }
}
