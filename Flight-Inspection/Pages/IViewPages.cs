using Flight_Inspection.controls;
using Flight_Inspection.Pages.Settings;
using System;

namespace Flight_Inspection
{
    public interface IViewPages
    {
        event EventHandler<OnReadyEventArgs> OnReady;
        event EventHandler NewWindow;
        event EventHandler Closed;
        IPagesViewModel GetViewModel();

        string Name { get; set; }
    }
}
