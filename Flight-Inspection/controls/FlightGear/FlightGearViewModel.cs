using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.FlightGear
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private FlightGearModel flightGearModel;
        private bool ready;

        public FlightGearViewModel(SettingPacket settings)
        {
            this.ready = !(settings is null) && settings.ready;
            if (ready)
                this.flightGearModel = new FlightGearModel(settings.CSV.Content, settings.XML.Content, settings.PATH.Content);
        }



        public void StartFG()
        {
            if (ready)
                flightGearModel.StartFG();

        }

        public void StartPlay()
        {
            if (ready)
                flightGearModel.StartPlay();

        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void setSettings(SettingPacket settingPacket)
        {
            if (!ready)
            {
                this.ready = !(settingPacket is null) && settingPacket.ready;
                if (ready)
                    this.flightGearModel = new FlightGearModel(settingPacket.CSV.Content, settingPacket.XML.Content, settingPacket.PATH.Content);
            }
        }
    }
}
