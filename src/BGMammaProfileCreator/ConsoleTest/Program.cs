using ProfileCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Classificator classificator = new Classificator();
            classificator.Example();
            //TFIDFService tfidf = new TFIDFService();
            //tfidf.Test(null);
        }
    }
}
