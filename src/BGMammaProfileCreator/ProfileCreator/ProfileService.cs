﻿using Newtonsoft.Json;
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
        }

        public void GetFrequencies()
        {
            TermFrequencyService tfService = new TermFrequencyService();
            IEnumerable<DocumentFrequency> frequencies = tfService.GetTermFrequencies();

            FeelingsClassificator classificator = new FeelingsClassificator();
            WordBlocker wordBlocker = new WordBlocker();
            wordBlocker.Initialize();

            foreach (DocumentFrequency document in frequencies)
            {
                document.Frequencies = document.Frequencies.Where(f => !wordBlocker.IsBlocked(f.Term)).ToList();
                IEnumerable<Frequency> ordered = document.Frequencies.OrderByDescending(f => f.Value);
                var weights = classificator.Decide(ordered.Take(10));
            }
        }
    }
}
