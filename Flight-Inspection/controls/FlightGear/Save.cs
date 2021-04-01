using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.controls.FlightGear
{
    class Save
    {
        private readonly DataSet data;
        private readonly DataTable table;
        public event EventHandler Initialization;
        public Save(EventHandler saveInitialization)
        {
            Initialization += saveInitialization;
            if (File.Exists("..\\..\\controls\\FlightGear\\FG_DATA\\save.xml"))
            {
                data = new DataSet();
                data.ReadXml("..\\..\\controls\\FlightGear\\FG_DATA\\save.xml");
                table = data.Tables[0];
            }
            else
            {
                data = new DataSet
                {
                    DataSetName = "Settings"
                };
                table = new DataTable
                {
                    TableName = "Path Locations"
                };
                data.Tables.Add(table);
                var col1 = new DataColumn
                {
                    ColumnName = "CSV"
                };
                var col2 = new DataColumn
                {
                    ColumnName = "XML"
                };
                var col3 = new DataColumn
                {
                    ColumnName = "PATH"
                };

                table.Columns.Add(col1);
                table.Columns.Add(col2);
                table.Columns.Add(col3);
                var row = table.NewRow();
                row["CSV"] = null;
                row["XML"] = null;
                row["PATH"] = null;
                table.Rows.Add(row);
            }
            OnInitializationEventArgs e = new OnInitializationEventArgs
            {
                CSV = table.Rows[0]["CSV"] as string,
                XML = table.Rows[0]["XML"] as string,
                PATH = table.Rows[0]["PATH"] as string
            };

            OnInitialization(e);
        }

        public void AddData(string name, string data)
        {
            table.Rows[0][name] = data;
        }

        public void SaveData()
        {
            data.WriteXml("..\\..\\controls\\FlightGear\\FG_DATA\\save.xml");
        }

/*        public SettingPacket OpenData()
        {
            return new SettingPacket
            {
                CSV = table.Rows[0]["CSV"] as string,
                XML = table.Rows[0]["XML"] as string,
                PATH = table.Rows[0]["PATH"] as string
            };

        }*/
/*        public class SettingPacket
        {
            private string csv;
            private string xml;
            private string path;

            public string CSV { get => csv; set => csv = value; }
            public string XML { get => xml; set => xml = value; }
            public string PATH { get => path; set => path = value; }
        }*/

        public void OnInitialization(OnInitializationEventArgs e)
        {
            Initialization?.Invoke(this, e);
        }
    }

    class OnInitializationEventArgs : EventArgs
    {
        public string CSV { get; set; }
        public string XML { get; set; }
        public string PATH { get; set; }

    }
}


