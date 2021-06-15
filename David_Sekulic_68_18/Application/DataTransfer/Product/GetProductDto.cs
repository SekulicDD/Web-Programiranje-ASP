using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class GetProductDto:ProductDto
    {
        public IEnumerable<string> Categories { get; set; } = new HashSet<string>();
    }
}
