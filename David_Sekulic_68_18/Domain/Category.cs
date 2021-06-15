using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<ProductCategory> CategoryProducts { get; set; } = new HashSet<ProductCategory>();
    }
}
