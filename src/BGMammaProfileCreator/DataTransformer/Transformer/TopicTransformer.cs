using ProfileCreator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer.Transformer
{
    public static class TopicTransformer
    {
        public static IEnumerable<ProcessedUser> Transform(IEnumerable<Topic> topics)
        {
            Dictionary<string, ProcessedUser> users = new Dictionary<string, ProcessedUser>();

            foreach (Topic topic in topics)
            {
                foreach (Comment comment in topic.Comments)
                {
                    if (comment == null)
                        continue;

                    if (!users.ContainsKey(comment.User.Username))
                        users.Add(comment.User.Username, new ProcessedUser(comment.User));

                    users[comment.User.Username].Comments.Add(new ProcessedComment(comment));
                }
            }

            return users.Values;
        }
    }
}
