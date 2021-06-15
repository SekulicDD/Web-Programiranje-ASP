using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public abstract class OrderDto
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}
