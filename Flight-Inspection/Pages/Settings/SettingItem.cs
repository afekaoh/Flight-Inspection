using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Settings
{
    public class SettingItem : INotifyPropertyChanged
    {
        private string content;
        private string checkVar;
        private string name;
        private bool isChecked;
        public event EventHandler saved;

        public SettingItem(string name, string checkVar)
        {
            this.checkVar = checkVar;
            this.Name = name;
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
                    onSave(this, null);
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

        public virtual void onSave(object sender, EventArgs e)
        {
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
}
