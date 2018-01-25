using DataTransformer.Reader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer.Transformer
{
    public class TopicTransformer
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();

        public IEnumerable<User> Transform(IEnumerable<Topic> topics)
        {
            foreach (Topic topic in topics)
            {
                foreach (Comment comment in topic.Comments)
                {
                    if (!users.ContainsKey(comment.User.Username))
                        users.Add(comment.User.Username, comment.User);

                    users[comment.User.Username].Comments.Add(comment);
                }
            }

            return users.Values;
        }
    }
}
