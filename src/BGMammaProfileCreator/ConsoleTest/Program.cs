using Newtonsoft.Json;
using ProfileCreator;
using ProfileCreator.Classification;
using ProfileCreator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Classificator classificator = new Classificator();
            // classificator.Example();

            //List<ProcessedUser> users = new List<ProcessedUser>();
            //foreach (string file in Directory.GetFiles(@"D:\Repositories\BG-Mamma-User-Profile\src\BGMammaProfileCreator\DataTransformer\bin\Debug\users"))
            //{
            //    string json = File.ReadAllText(file);
            //    users.Add(JsonConvert.DeserializeObject<ProcessedUser>(json));
            //}

            //TFIDFService tfidf = new TFIDFService();
            //tfidf.CreateIndex(users);
            //List<DocumentFrequency> frequencies = tfidf.GetTermFrequencies().ToList();

            var feelings = FeelingsParser.Parse();
        }
    }
}
