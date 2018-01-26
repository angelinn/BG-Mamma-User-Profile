using Newtonsoft.Json;
using ProfileCreator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DataTransformer.Reader
{
    public static class JsonReader
    {
        public static IEnumerable<Topic> ReadTopics(string topicsFolder)
        {
            foreach (string file in Directory.GetFiles(topicsFolder))
            {
                string json = File.ReadAllText(file);
                yield return JsonConvert.DeserializeObject<Topic>(json);
            }
        }

        public static void WriteUsers(IEnumerable<ProcessedUser> users)
        {
            foreach (ProcessedUser user in users)
            {
                try
                {
                    File.WriteAllText($"users/{Regex.Replace(user.Username, "[\x00\\:\\*\\?\"\\/<>\\|\\\'\\^]", "_")}.json", JsonConvert.SerializeObject(user));
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
    }
}
