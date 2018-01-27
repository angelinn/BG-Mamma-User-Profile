using Newtonsoft.Json;
using ProfileCreator;
using ProfileCreator.Classification;
using ProfileCreator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProfileService service = new ProfileService();
            //service.CreateProfiles(@"C:\temp\users");
            service.GetFrequencies();
        }
    }
}
