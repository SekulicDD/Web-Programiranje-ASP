using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class ProductSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public IEnumerable<int> CategoryIds { get; set; } = new HashSet<int>();

    }
}
