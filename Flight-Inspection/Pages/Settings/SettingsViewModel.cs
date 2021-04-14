using Flight_Inspection.controls;
using Flight_Inspection.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Pages.Settings
{
    public class SettingsViewModel : IPagesViewModel
    {
        private List<SettingItem> settingItems;
        private bool ready;
        public bool Ready
        {
            get
            {
                return ready;
            }
            private set
            {
                ready = value;
                OnPropertyChanged();
            }
        }
        private Save save;


        public List<SettingItem> SettingItems { get => settingItems; set => settingItems = value; }

        public SettingsViewModel()
        {
            SettingItems = new List<SettingItem>
            {
                {new SettingItem("CSV_Normal", ".csv")},
                {new SettingItem("CSV_Test", ".csv") { Ready = true }},
                {new SettingItem("XML", ".xml")},
                {new SettingItem("DLL_PATH", ".dll") {Ready = true}},
                {new SettingItem("Proc_PATH", "") }
            };
            SettingItems.ForEach(t => t.PropertyChanged += SavedEvent);
            save = new Save();
        }

        public SettingItem getSettingItem(string name)
        {
            return SettingItems.Find(t => t.Name == name);
        }

        void SavedEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "Content")
            {
                var si = sender as SettingItem;
                AddData(si.Name, si.Content);
                Ready = SettingItems.All(t => t.Ready);
            }
            if (e.PropertyName is "Checked")
            {
                var si = sender as SettingItem;
                switch (si.Name)
                {
                    case "CSV_Normal":
                    case "XML":
                    case "Proc_PATH":
                        si.Ready = true;
                        break;
                }
            }
        }

        public void AddData(string name, string data)
        {
            save.AddData(name, data);
        }

        public void SaveData()
        {
            save.SaveData();
        }

        internal OnReadyEventArgs GetSettings(string name)
        {
            var settings = settingItems.FindAll(s => s.Checked).ToArray();
            return new OnReadyEventArgs(name, Ready, settings);
        }

        public override void UpdateSettings()
        {
            var s = save.GetSettings();
            settingItems.ForEach(t => t.Content = s.GetArg(t.Name));
        }

        public override void OnPropertyChanged([CallerMemberName] string name = null, PropertyChangedEventArgs ea = null)
        {
            OnReadyEventArgs e = GetSettings(name);
            base.OnPropertyChanged(name, e);
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
        }
    }
}
