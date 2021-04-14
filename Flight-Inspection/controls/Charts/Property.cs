using System;
using System.Collections.Generic;
using static Flight_Inspection.controls.AnalomyDetectorClass;

namespace Flight_Inspection.controls
{
    /**
     * class that saves all the important informations of the given property
     */
    class Property
    {
        private string name;
        private string attach;
        private LineSafe linearReg;
        private List<float> data;
        
        public string Name { get => name; set => name = value; }
        //the name of the most correlated feature
        public string Attach { get => attach; set => attach = value; }
        public List<float> Data
        {
            get => data; set
            {
                if (data == null)
                {
                    data = value;
                }

            }
        }
        //the linear reg of the 2 features
        public LineSafe LinearReg { get => linearReg; set => linearReg = value; }
       
    }
}
