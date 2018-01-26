using Newtonsoft.Json;
using ProfileCreator.Classification;
using ProfileCreator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProfileCreator
{
    public class ProfileService
    {
        public void CreateProfiles(string usersDirectory)
        {
            List<ProcessedUser> users = new List<ProcessedUser>();
            foreach (string file in Directory.GetFiles(usersDirectory))
            {
                string json = File.ReadAllText(file);
                users.Add(JsonConvert.DeserializeObject<ProcessedUser>(json));
            }

            TermFrequencyService tfService = new TermFrequencyService();
            tfService.CreateIndex(users);
            List<DocumentFrequency> frequencies = tfService.GetTermFrequencies().ToList();

            FeelingsClassificator classificator = new FeelingsClassificator();
            foreach (DocumentFrequency document in frequencies)
            {
                IEnumerable<Frequency> ordered = document.Frequencies.OrderByDescending(f => f.Value);
                ClassificationAnswer answer = classificator.Decide(ordered.Take(10).Select(f => f.Term));
            }

        }
    }
}
