using DataTransformer.Reader.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        public static void WriteUsers(IEnumerable<User> users)
        {
            foreach (User user in users)
                File.WriteAllText($"users/{user.Username}", JsonConvert.SerializeObject(user));
        }
    }
}
