using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Order:Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderedAt { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();
    }
}
