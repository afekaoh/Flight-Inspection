namespace Flight_Inspection.controls
{
    public class MyCommand<T>
    {
        public System.Action<object> ExecuteDelegate { get; internal set; }
    }
}