
namespace Flight_Inspection.controls
{
    public class SettingsArgs
    {
        private TimeSeries ts;
        private string procPath;

        public TimeSeries Ts { get => ts; set => ts = value; }
        public string ProcPath { get => procPath; set => procPath = value; }
    }
}