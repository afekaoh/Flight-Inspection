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
        private bool ready;
        private Save save;
        private List<SettingItem> settingItems;

        public SettingsViewModel()
        {
            SettingItems = new List<SettingItem>
            {
                {new SettingItem("CSV_Normal", ".csv")},
                {new SettingItem("CSV_Test", ".csv")},
                {new SettingItem("XML", ".xml")},
                {new SettingItem("DLL_PATH", ".dll")},
                {new SettingItem("Proc_PATH", "") }
            };
            SettingItems.ForEach(t => t.PropertyChanged += SavedEvent);
            save = new Save();
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
                        settingItems.Find(s => s.Name == "CSV_Test").Ready = true;
                        si.Ready = true;
                        break;
                    case "XML":
                    case "Proc_PATH":
                        si.Ready = true;
                        break;
                    case "CSV_Test":
                        settingItems.Find(s => s.Name == "DLL_Path").Ready = true;
                        UpdateSettings();
                        break;
                    case "DLL_Path":
                        UpdateSettings();
                        break;
                }
            }
        }

        internal OnReadyEventArgs GetSettings(string name)
        {
            var settings = settingItems.FindAll(s => s.Checked).ToArray();
            return new OnReadyEventArgs(name, Ready, settings);
        }

        public void AddData(string name, string data)
        {
            save.AddData(name, data);
        }

        public SettingItem getSettingItem(string name)
        {
            return SettingItems.Find(t => t.Name == name);
        }

        public override void OnPropertyChanged([CallerMemberName] string name = null, PropertyChangedEventArgs ea = null)
        {
            OnReadyEventArgs e = GetSettings(name);
            base.OnPropertyChanged(name, e);
        }

        public void SaveData()
        {
            save.SaveData();
        }

        public override void SetSettings(SettingsArgs settingsArgs)
        {
        }

        public override void UpdateSettings()
        {
            var s = save.GetSettings();
            settingItems.ForEach(t => t.Content = s.GetArg(t.Name));
        }

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


        public List<SettingItem> SettingItems { get => settingItems; set => settingItems = value; }
    }
}
