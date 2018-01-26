using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileCreator.Models
{
    public class ProcessedUser
    {
        public string Username { get; set; }
        public string ProfileUrl { get; set; }
        public List<ProcessedComment> Comments { get; set; } = new List<ProcessedComment>();

        public ProcessedUser()
        {   }

        public ProcessedUser(User user)
        {
            Username = user.Username;
            ProfileUrl = user.ProfileUrl;
        }
    }
}
