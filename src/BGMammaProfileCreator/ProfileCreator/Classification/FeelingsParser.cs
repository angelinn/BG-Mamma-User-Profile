using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProfileCreator.Classification
{
    public class Feeling
    {
        public float Weight { get; set; }
        public string Value { get; set; }
    }

    public static class FeelingsParser
    {
        private const string DEFAULT_FEELINGS_FILE = "feelings.txt";

        public static Dictionary<string, List<Feeling>> Parse(string feelingsFile = null)
        {
            feelingsFile = feelingsFile ?? DEFAULT_FEELINGS_FILE;

            Dictionary<string, List<Feeling>> classes = new Dictionary<string, List<Feeling>>();

            string[] lines = File.ReadAllLines(feelingsFile);

            int i = 0;

            while (true)
            {
                if (i + 1 >= lines.Length)
                    break;

                while (!lines[i].Contains("class="))
                    ++i;

                string className = lines[i].Split('=')[1];
                classes.Add(className, new List<Feeling>());

                while (!lines[i].Contains("w="))
                    ++i;

                float w = Single.Parse(lines[i].Split('=')[1]);

                while (i + 1 < lines.Length && !lines[++i].Contains("class="))
                    classes[className].Add(new Feeling { Weight = w, Value = lines[i] });
            }

            return classes;
        }
    }
}
