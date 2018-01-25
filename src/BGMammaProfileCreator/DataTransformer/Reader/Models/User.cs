using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransformer.Reader.Models
{
    public class User
    {
        [JsonProperty("user_name")]
        public string Username { get; set; }
        [JsonProperty("profile_url")]
        public string ProfileUrl { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
