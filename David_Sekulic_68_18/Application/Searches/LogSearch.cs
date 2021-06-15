using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public class LogSearch : PagedSearch
    {
        public string Keyword { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
