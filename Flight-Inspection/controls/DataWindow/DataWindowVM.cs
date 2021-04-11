using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.DataWindow
{
    class DataWindowVM : IControlViewModel
    {
        DataWindowModel model;

        DataWindowVM()
        {
            model = new DataWindowModel();
        }
        public override void SetSettings(SettingsArgs settingsArgs)
        {
            model.SetSettings(settingsArgs);
            throw new NotImplementedException();
        }

        internal override void setTime(int time)
        {
            throw new NotImplementedException();
        }
    }
}
