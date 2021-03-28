﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Flight_Inspection.controls.FlightGear
{
    class TimeSeries
    {
        private Dictionary<string, List<float>> TableByFeaturs;
        public List<String> Rows { get; set; }
        private List<string> features;


        public TimeSeries(string CsvFileName)
        {
            XmlReader r = XmlReader.Create("C:\\Users\\afeka\\OneDrive - Bar-Ilan University\\Code projects\\Advance-Programming-2\\Flight-Inspection\\Flight-Inspection\\controls\\FlightGear\\playback_small.xml");
            this.features = new List<string>();
            this.TableByFeaturs = new Dictionary<string, List<float>>();
            Rows = new List<string>();
            createTable(CsvFileName, r);
        }


        // saving all the csv data in a map for future use
        private void createTable(string csvFileName, XmlReader r)
        {
            // opening the csv
            var reader = new StreamReader(csvFileName);
            string row;
            string word;
            char delim = ',';
            int j = 0;
            // creating the features table and initialized the main table

            while (r.Read())
            {
                if (r.Name == "output")
                {
                    r.Skip();
                }

                if (r.Name == "name")
                {
                    //Extract the value of the Name attribute
                    string w = r.ReadElementContentAsString();
                    if (TableByFeaturs.ContainsKey(w))
                    {
                        w += "_" + j++;
                    }
                    features.Add(w);
                    TableByFeaturs.Add(w, new List<float>());
                }
            }
            int colIndex = 0;
            var length = features.Count;
            // creating a vector of insert iterators for all the vectors in dataTable
            while (!reader.EndOfStream)
            {
                row = reader.ReadLine();
                Rows.Add(row);
                var values = row.Split(',');
                foreach (string s in values)
                {
                    // converting column based table (each feature in a column) to a row based table (each feature in a row)
                    TableByFeaturs[features[(colIndex++) % length]].Add(float.Parse(s));
                }
            }
            reader.Close();
        }

        // returning all the data of a given feature
        public List<float> getFeatureData(string feature)
        {
            return TableByFeaturs[feature];
        }

        // returning all the features names
        public List<string> getFeatureNames()
        {
            return features;
        }

    }
}