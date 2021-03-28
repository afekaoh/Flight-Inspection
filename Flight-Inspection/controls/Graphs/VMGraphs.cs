using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.Graphs
{
    class VMGraphs : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public class Name {
            public string VM_Property
            {
                get; set;
            }
        }
        public VMGraphs() {
            
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
