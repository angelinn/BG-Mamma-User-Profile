using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileCreator.Models
{
    public class Topic
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
