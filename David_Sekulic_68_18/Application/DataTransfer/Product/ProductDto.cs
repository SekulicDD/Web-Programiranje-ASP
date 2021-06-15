using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public abstract class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
