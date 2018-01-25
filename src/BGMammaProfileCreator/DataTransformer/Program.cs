using DataTransformer.Reader;
using DataTransformer.Reader.Models;
using DataTransformer.Transformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<Topic> topics = JsonReader.ReadTopics("topics");
            IEnumerable<User> users = TopicTransformer.Transform(topics);

            JsonReader.WriteUsers(users);
        }
    }
}
