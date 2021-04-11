using System;
using System.ComponentModel;

namespace Flight_Inspection
{
    public class SetTimeEventArgs : PropertyChangedEventArgs
    {
        public int Time { get; set; }
        public SetTimeEventArgs(int time, string name) : base(name)
        {
            Time = time;
        }
    }
}
