using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProfileCreator
{
    public class WordBlocker
    {
        private const string DEFAULT_WORDS_FILE = "blocked_words.txt";
        private List<string> words = new List<string>();

        public void Initialize()
        {
            string[] lines = File.ReadAllLines(DEFAULT_WORDS_FILE, Encoding.GetEncoding(0));

            foreach (string line in lines)
                words.Add(line);
        }

        public bool IsBlocked(string word)
        {
            return words.Any(w => w.Contains(word));
        }
    }
}
