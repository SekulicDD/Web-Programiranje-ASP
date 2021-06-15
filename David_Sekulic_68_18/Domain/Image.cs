using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
