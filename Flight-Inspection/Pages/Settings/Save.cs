using Flight_Inspection.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection.Settings
{
    class Save
    {
        private readonly DataSet data;
        private readonly DataTable table;
        public Save()
        {
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
            /*            OnInitializationEventArgs e = new OnInitializationEventArgs
                        {
                            CSV = table.Rows[0]["CSV"] as string,
                            XML = table.Rows[0]["XML"] as string,
                            PATH = table.Rows[0]["PATH"] as string
                        };
                        OnInitialization(e);*/
        }

        public void AddData(string name, string data)
        {
            table.Rows[0][name] = data;
        }

        public void SaveData()
        {
            data.WriteXml("..\\..\\..\\controls\\FlightGear\\FG_DATA\\save.xml");
        }

        public DataPacket getSettings()
        {
            return new DataPacket
            {
                CSV = table.Rows[0]["CSV"] as string,
                XML = table.Rows[0]["XML"] as string,
                PATH = table.Rows[0]["PATH"] as string
            };
        }
    }

    public class DataPacket
    {
        public string CSV { get; set; }
        public string XML { get; set; }
        public string PATH { get; set; }

        public string GetArg(string name)
        {
            switch (name)
            {
                case "CSV":
                    return CSV;
                case "XML":
                    return XML;
                case "PATH":
                    return PATH;
                default:
                    return null;
            }
        }

    }
}


