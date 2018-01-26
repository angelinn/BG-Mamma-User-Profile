using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileCreator.Models
{
    public class User
    {
        [JsonProperty("user_name")]
        public string Username { get; set; }
        [JsonProperty("profile_url")]
        public string ProfileUrl { get; set; }
    }
}
