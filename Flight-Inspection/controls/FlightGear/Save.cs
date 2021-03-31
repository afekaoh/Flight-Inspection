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
        private DataSet data;
        private DataTable table;

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
                data = new DataSet();
                table = new DataTable();
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
        }

        public void AddData(string name, string data)
        {
            table.Rows[0][name] = data;
        }

        public void SaveData()
        {
            data.WriteXml("..\\..\\controls\\FlightGear\\FG_DATA\\save.xml");
        }

        public void openData()
        {
            var t = data.Tables[0];
            var r = t.Rows[0];
            var a = r.ItemArray;
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }
    }
}
