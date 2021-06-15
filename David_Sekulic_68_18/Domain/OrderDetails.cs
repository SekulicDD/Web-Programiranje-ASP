using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderDetails:Entity
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
