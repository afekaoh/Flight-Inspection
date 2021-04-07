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
            if (File.Exists("..\\..\\Pages\\Settings\\FG_DATA\\save.xml"))
            {
                data = new DataSet();
                data.ReadXml("..\\..\\Pages\\Settings\\FG_DATA\\save.xml");
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
            data.WriteXml("..\\..\\..\\Pages\\Settings\\FG_DATA\\save.xml");
        }

        public DataPacket GetSettings()
        {


            return new DataPacket() { CSV = getContent("CSV"), XML = getContent("XML"), PATH = getContent("PATH") };
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


