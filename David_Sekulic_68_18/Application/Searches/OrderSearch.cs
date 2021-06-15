using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class OrderSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo{ get; set; }
        public int? MaxTotal { get; set; }
        public int? MinTotal { get; set; }
    }
}
