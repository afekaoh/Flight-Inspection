using Flight_Inspection.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Pages.Settings
{
    public class SettingsViewModel
    {
        public static SettingPacket settingPacket { get; set; }
        private List<SettingItem> settingItems;
        private Save save;

        public List<SettingItem> SettingItems { get => settingItems; set => settingItems = value; }

        public SettingsViewModel()
        {
            SettingItems = new List<SettingItem>
            {
                {new SettingItem("CSV", ".csv")},
                {new SettingItem("XML", ".xml")},
                {new SettingItem("PATH", "") }
            };
            settingPacket = new SettingPacket();
            SettingItems.ForEach(t => t.saved += SavedEvent);
            save = new Save(SaveInitialization);
        }

        public SettingItem getSettingItem(string name)
        {
            return SettingItems.Find(t => t.Name == name);
        }

        void SaveInitialization(object sender, EventArgs e)
        {
            foreach (var item in SettingItems)
            {
                item.Content = (e as OnInitializationEventArgs).GetArg(item.Name);
            }
        }

        void SavedEvent(object sender, EventArgs e)
        {
            if (!(save is null))
            {
                var si = sender as SettingItem;
                AddData(si.Name, si.Content);
            }
        }

        public bool Ready()
        {
            return SettingItems.All(t => t.Checked);
        }

        public void AddData(string name, string data)
        {
            save.AddData(name, data);
        }

        public void SaveData()
        {
            save.SaveData();
        }

        internal SettingPacket GetSettings()
        {
            settingPacket.CSV = getSettingItem("CSV");
            settingPacket.XML = getSettingItem("XML");
            settingPacket.PATH = getSettingItem("PATH");
            settingPacket.ready = Ready();
            return settingPacket;
        }
    }

    public class OnSavedEventArgs : EventArgs
    {
        public bool ToSave { get; set; }
    }

}
