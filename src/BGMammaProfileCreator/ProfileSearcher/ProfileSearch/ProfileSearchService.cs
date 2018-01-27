using ProfileCreator;
using ProfileCreator.Models.Analyzed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileSearcher.ProfileSearch
{
    public class ProfileSearchService
    {
        public static IEnumerable<AnalyzedUser> Search(string query, string field)
        {
            ProfileService service = new ProfileService();
            IEnumerable<DocumentFrequency> results = service.Search(query, field);
            return service.GetFrequencies(results);
        }
    }
}
