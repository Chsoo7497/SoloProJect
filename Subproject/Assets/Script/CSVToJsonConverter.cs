using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace CSVToJsonConverter
{
    public class CSVToJsonConverter
    {
        private static string ResourcesPath = Application.dataPath + "/Resources/";
        private static string CSVPath = "CSVTable/";
        private static string JsonPath = "JsonTable/";
        public static StringBuilder CSVToJson(TextAsset CSVData)
        {
            List<string> Lines = new List<string>();
            foreach(string Line in CSVData.text.Split(new char[] { '\r' }))
            {
                if(Line.Length > 1)
                {
                    Lines.Add(Line.Replace("\r", ""));
                }
            }
            if (Lines.Count <= 1) return null;

            List<string> DataNames = new List<string>();
            foreach (string DataName in Lines[0].Split(new char[] { ',' }))
            {
                DataNames.Add(DataName.Replace(",", ""));
            }

            var JSONString = new StringBuilder();
            JSONString.Append("[\n");
            for (int i = 1; i < Lines.Count; i++)
            {
                JSONString.Append("  {\n");
                List<string> Elements = new List<string>();
                foreach (string Element in Lines[i].Split(new char[] { ',' }))
                {
                    Elements.Add(Element.Replace(",", ""));
                }
                for (int j = 0; j < Elements.Count; j++)
                {
                    if (DataNames[j][0] != '#')
                    {
                        JSONString.Append("    \"" + DataNames[j] + "\": ");
                        if (int.TryParse(Elements[j], out int IntElement))
                        {
                            JSONString.Append(IntElement);
                        }
                        else if (double.TryParse(Elements[j], out double DoubleElement))
                        {
                            JSONString.Append(DoubleElement);
                        }
                        else
                        {
                            JSONString.Append("\"" + Elements[j] + "\"");
                        }
                        if (j != Elements.Count - 1)
                        {
                            JSONString.Append(",\n");
                        }
                        else
                        {
                            JSONString.Append("\n");
                        }
                    }
                }
                JSONString.Append("  }");
                if (i != Lines.Count - 1)
                {
                    JSONString.Append(",\n");
                }
                else
                {
                    JSONString.Append("\n");
                }
            }
            JSONString.Append("]");

            return JSONString;
        }


        [MenuItem("Custom/CSV To Json Convert")]
        private static void ConvertToJson()
        {
            DirectoryInfo di = new DirectoryInfo(ResourcesPath + CSVPath);

            foreach (FileInfo CSVFile in di.GetFiles())
            {
                //디렉토리 경로를 포함한 내용 출력 
                //Console.WriteLine(CSVFile.FullName);
                string CSVName = CSVFile.Name.Replace(".csv", "");
                TextAsset CSVData = Resources.Load<TextAsset>(CSVPath + CSVName);
                var JSONString = CSVToJson(CSVData);
                string jsonData = JSONString.ToString();
                File.WriteAllText(ResourcesPath + JsonPath + CSVName + ".json", jsonData);
            }
        }
    }
}