using System.Collections.Generic;

namespace Bkav.eGovCloud.Search
{
    public class SearchView
    {
        public IEnumerable<SearchItemView> Items { get; set; }

        public ICollection<KeyValuePair<string, KeyValuePair<string, int>>> FacetDocField { get; set; }

        public ICollection<KeyValuePair<string, KeyValuePair<string, int>>> FacetDocType { get; set; }

        public IEnumerable<KeyValuePair<string, int>> FacetCreatedDate { get; set; }

        public string DidYouMean { get; set; }

        public long TotalResult { get; set; }
    }
}
