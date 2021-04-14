
namespace Flight_Inspection.controls
{
    public class SettingsArgs
    {
        private string procPath;

        public TimeSeries Ts { get; set; }
        public string ProcPath { get; set; }
        public string CSV_Normal { get; set; }
        public string CSV_Test { get; set ; }
        public string DLLPath { get ; set; }
    }
}