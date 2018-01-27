using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileCreator.Models.Analyzed
{
    public class AnalyzedUser
    {
        public string Username { get; set; }
        public string ProfileUrl { get; set; }
        public Dictionary<string, float> Characteristics { get; set; }
        public List<KeyValuePair<string, float>> List
        {
            get
            {
                return Characteristics.Select(k => k).OrderByDescending(p => p.Value).ToList();
            }
        }

        public string Classes => String.Join(", ", List.Select(p => $"{p.Key} - {p.Value}"));
        
    }
}
