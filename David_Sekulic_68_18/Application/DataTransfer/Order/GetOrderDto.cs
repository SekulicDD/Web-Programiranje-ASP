using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetOrderDto : OrderDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> ProductNames { get; set; } = new HashSet<string>();
       
    }
}
