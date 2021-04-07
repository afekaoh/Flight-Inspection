namespace Flight_Inspection.controls
{
    public interface IControlView
    {
        string Name { get; }
        IControlViewModel GetViewModel();
    }
}
