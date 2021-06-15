using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
