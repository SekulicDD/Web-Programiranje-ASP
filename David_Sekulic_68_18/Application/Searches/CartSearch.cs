using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class CartSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public DateTime? AddedFrom { get; set; }
        public DateTime? AddedTo { get; set; }
    }
}
