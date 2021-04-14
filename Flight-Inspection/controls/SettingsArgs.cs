
namespace Flight_Inspection.controls
{
    public class SettingsArgs
    {
        private TimeSeries ts;
        private string procPath;

        public TimeSeries Ts { get => ts; set => ts = value; }
        public string ProcPath { get => procPath; set => procPath = value; }
        public string CSV_Normal { get => procPath; set => procPath = value; }
        public string CSV_Test { get => procPath; set => procPath = value; }
        public string DLLPath { get => procPath; set => procPath = value; }
    }
}