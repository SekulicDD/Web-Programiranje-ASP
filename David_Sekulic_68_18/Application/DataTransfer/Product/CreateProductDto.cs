using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class CreateProductDto : ProductDto
    {
        public IFormFile Image { get; set; }
        public IEnumerable<int> CategoryIds { get; set; } = new HashSet<int>();
    }
}
