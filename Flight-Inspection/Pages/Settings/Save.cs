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
        private DataSet data;
        public Save()
        {
            if (File.Exists("save.xml"))
            {
                data = new DataSet();
                data.ReadXml("save.xml");
            }
            else
            {
                data = new DataSet
                {
                    DataSetName = "Settings"
                };
                var table = new DataTable
                {
                    TableName = "Path Locations"
                };
                table.Rows.Add(table.NewRow());
                data.Tables.Add(table);
            }
        }

        public void AddData(string name, string data)
        {
            try
            {
                var temp = this.data.Tables[0].Rows[0][name];
            }
            catch (ArgumentException)
            {
                this.data.Tables[0].Columns.Add(name);
                try
                {
                    var temp = this.data.Tables[0].Rows[0][name];
                }
                catch (ArgumentException)
                {
                    this.data.Tables[0].Rows.Add(this.data.Tables[0].NewRow());
                }
            }
            finally
            {
                this.data.Tables[0].Rows[0][name] = data;
            }
        }

        public void SaveData()
        {
            data.WriteXml("save.xml");
        }

        public DataPacket GetSettings()
        {


            return new DataPacket() { CSV_Normal = getContent("CSV_Normal"), XML = getContent("XML"), Proc_PATH = getContent("Proc_PATH") };
        }

        public string getContent(string name)
        {
            try
            {
                return this.data.Tables[0].Rows[0][name] as string;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }

    public class DataPacket
    {
        public string CSV_Normal { get; set; }
        public string CSV_Test { get; set; }
        public string XML { get; set; }
        public string Proc_PATH { get; set; }
        public string DLL_PATH { get; set; }

        public string GetArg(string name)
        {
            switch (name)
            {
                case "CSV_Normal":
                    return CSV_Normal;
                case "CSV_Test":
                    return CSV_Test;
                case "XML":
                    return XML;
                case "Proc_PATH":
                    return Proc_PATH;
                case "DLL_PATH":
                    return DLL_PATH;
                default:
                    return null;
            }
        }
    }
}


