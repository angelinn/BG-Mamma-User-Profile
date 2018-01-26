using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileCreator.Models
{
    public class ProcessedComment
    {
        public string Content { get; set; }
        public string Date { get; set; }
        public List<Quote> Quotes { get; set; } = new List<Quote>();

        public ProcessedComment()
        {   }

        public ProcessedComment(Comment comment)
        {
            Content = comment.Content;
            Date = comment.Date;
            Quotes = comment.Quotes;
        }
    }
}
