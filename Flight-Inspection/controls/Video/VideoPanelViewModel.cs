using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Video
{
    class VideoPanelViewModel
    {
        private int maxSlider;

        public int MaxSlider
        {
            get {
                return maxSlider; 
            }
            set { maxSlider = value; }
        }


        private int currentTime;

        public int CurrentTime
        {
            get { return currentTime;
            }
            set { currentTime = value; }
        }

    }

}


