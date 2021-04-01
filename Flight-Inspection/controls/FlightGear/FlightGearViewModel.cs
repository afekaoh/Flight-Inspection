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
        private bool ready = false;
        private SettingItem csvFileName;
        private SettingItem xMLPath;
        private SettingItem procPath;
        private Save save;

        public FlightGearViewModel()
        {
            /*            List<SettingItem> ls = new List<SettingItem>
                        {
                            new SettingItem("CSV", ".csv"),
                            new SettingItem("XML", ".xml"),
                            new SettingItem("PATH", "")
                        };
                        ls.ForEach(si => si.saved = savedEvent);*/

            this.csvFileName = new SettingItem("CSV", ".csv", this);
            this.xMLPath = new SettingItem("XML", ".xml", this);
            this.procPath = new SettingItem("PATH", "", this);
            csvFileName.saved += savedEvent;
            xMLPath.saved += savedEvent;
            procPath.saved += savedEvent;
            this.save = new Save(SaveInitialization);
            /*            var d = save.OpenData();
                        CsvFileName.Content = d.CSV;
                        XMLPath.Content = d.XML;
                        ProcPath.Content = d.PATH;*/
        }

        void SaveInitialization(object sender, EventArgs e)
        {
            CsvFileName.Content = (e as OnInitializationEventArgs).CSV;
            XMLPath.Content = (e as OnInitializationEventArgs).XML;
            ProcPath.Content = (e as OnInitializationEventArgs).PATH;
        }

        void savedEvent(object sender, EventArgs e)
        {
            var si = sender as SettingItem;
            AddData(si.Name, si.Content);
        }
        public void setReady()
        {
            ready = CsvFileName.Checked && XMLPath.Checked && ProcPath.Checked;
            if (ready)
                flightGearModel = new FlightGearModel(CsvFileName.Content, XMLPath.Content, ProcPath.Content);
        }

        public void AddData(string name, string data)
        {
            save.AddData(name, data);
        }

        /*       public void showData()
               {
                   save.OpenData();
               }*/

        public void SaveData()
        {
            save.SaveData();
        }
        public SettingItem CsvFileName
        {
            get => csvFileName;
            set
            {
                csvFileName = value;
                OnPropertyChanged();
            }
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

        public SettingItem XMLPath
        {
            get => xMLPath;
            set
            {
                xMLPath = value;
                OnPropertyChanged();
            }
        }
        public SettingItem ProcPath
        {
            get => procPath;
            set
            {
                procPath = value;
                OnPropertyChanged();
            }
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.

        public class SettingItem : INotifyPropertyChanged
        {
            private string content;
            private string checkVar;
            private string name;
            private bool isChecked;
            public event EventHandler saved;

            FlightGearViewModel o;

            public SettingItem(string name, string checkVar, FlightGearViewModel o)
            {
                this.checkVar = checkVar;
                this.Name = name;
                this.o = o;
            }

            public string Content
            {
                get { return content; }
                set
                {
                    content = value;
                    if (!(value is null) && value.EndsWith(checkVar))
                    {
                        Checked = true;
                        OnSavedEventArgs e = new OnSavedEventArgs
                        {
                            ToSave = !(o.save is null)
                        };

                        onSave(this, e);
                    }
                    OnPropertyChanged();
                }
            }

            public bool Checked
            {
                get { return isChecked; }
                set
                {
                    isChecked = value;
                    OnPropertyChanged();
                }
            }

            public string Name { get => name; set => name = value; }

            public virtual void onSave(object sender, OnSavedEventArgs e)
            {
                if (e.ToSave)
                    saved?.Invoke(this, e);
            }

            public event PropertyChangedEventHandler PropertyChanged;

            // Create the OnPropertyChanged method to raise the event
            // The calling member's name will be used as the parameter.
            protected void OnPropertyChanged([CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class OnSavedEventArgs : EventArgs
    {
        public bool ToSave { get; set; }
    }
}
