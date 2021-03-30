using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls
{
    class VMCharts : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public class Name {
            public string VM_Property
            {
                get; set;
            }
        }
        public VMCharts() {
            
        }

        public List<Name> GetNames()
        {
            List<Name> ls = new List<Name>();
            ls.Add(new Name() { VM_Property = "A" });
            ls.Add(new Name() { VM_Property = "B" });
            ls.Add(new Name() { VM_Property = "C" });
            ls.Add(new Name() { VM_Property = "D" });
            return ls;
        }
    }
}
