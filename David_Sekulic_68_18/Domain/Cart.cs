using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Cart : Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
