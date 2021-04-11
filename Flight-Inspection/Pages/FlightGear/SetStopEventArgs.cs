using System;
using System.ComponentModel;

namespace Flight_Inspection.Pages.FlightGear
{
    public class SetStopEventArgs : PropertyChangedEventArgs
    {
        public SetStopEventArgs(bool stop, string name) : base(name)
        {
            Stop = stop;
        }

        public bool Stop { get; set; }
    }
}
