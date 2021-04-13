using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Flight_Inspection.controls
{
    public class TimeSeries
    {
        readonly List<string> features;
        readonly Dictionary<string, List<float>> TableByFeaturs;


        public TimeSeries(string CsvFileName, String XMLFileName)
        {
            this.features = new List<string>();
            this.TableByFeaturs = new Dictionary<string, List<float>>();
            Rows = new List<string>();
            createTable(CsvFileName, XMLFileName);
        }


        // saving all the CSV data in a map for future use
        private void createTable(string csvFileName, string XMLFileName)
        {
            // opening the CSV
            string row;
            char delim = ',';
            ParseFeatureNames(XMLFileName);
            // creating a vector of insert iterators for all the vectors in dataTable
            using (var reader = new StreamReader(csvFileName))
            {
                while (!reader.EndOfStream)
                {
                    row = reader.ReadLine();
                    Rows.Add(row);
                    var values = row.Split(delim);
                    for (int i = 0; i < values.Length; i++)
                    {
                        // converting column based table (each feature in a column) to a row based table (each feature in a row)
                        TableByFeaturs[features[i]].Add(float.Parse(values[i]));
                    }
                }
            }
        }

        private void ParseFeatureNames(string XMLFileName)
        {
            int duplicates = 0;
            // creating the features table and initialized the main table
            using (XmlReader reader = XmlReader.Create(XMLFileName))
            {
                while (reader.Read())
                {
                    if (reader.Name == "output")
                    {
                        reader.Skip();
                    }

                    if (reader.Name == "name")
                    {
                        //Extract the value of the Name attribute
                        string name = reader.ReadElementContentAsString();
                        if (TableByFeaturs.ContainsKey(name))
                        {
                            name += "_" + duplicates++;
                        }
                        features.Add(name);
                        TableByFeaturs.Add(name, new List<float>());
                    }
                }

            }
        }

        // returning all the data of a given feature
        public List<float> GetFeatureData(string feature) { return TableByFeaturs[feature]; }

        // returning all the features names
        public List<string> GetFeatureNames() { return features; }

        // Saving all the rows of the CSV
        public List<String> Rows { get; set; }
    }
}
