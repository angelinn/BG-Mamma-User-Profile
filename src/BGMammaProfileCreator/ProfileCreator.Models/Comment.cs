using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileCreator.Models
{
    public class Comment
    {
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public List<Quote> Quotes { get; set; } = new List<Quote>();
    }
}
