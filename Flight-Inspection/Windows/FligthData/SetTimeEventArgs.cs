using System;

namespace Flight_Inspection
{
    public class SetTimeEventArgs : EventArgs
    {
        public int Time { get; set; }
        public SetTimeEventArgs(int time)
        {
            Time = time;
        }
    }
}
