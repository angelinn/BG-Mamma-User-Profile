using DataTransformer.Reader;
using DataTransformer.Transformer;
using ProfileCreator.Models;
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
            IEnumerable<Topic> topics = JsonReader.ReadTopics(@"D:\Repositories\BG-Mamma-User-Profile\src\crawler\topics");
            IEnumerable<ProcessedUser> users = TopicTransformer.Transform(topics);

            JsonReader.WriteUsers(users);
        }
    }
}
