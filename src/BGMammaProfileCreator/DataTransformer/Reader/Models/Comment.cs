using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransformer.Reader.Models
{
    public class Comment
    {
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public List<Quote> Quotes { get; set; } = new List<Quote>();
    }
}
