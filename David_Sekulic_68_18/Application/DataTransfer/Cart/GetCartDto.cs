using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer.Cart
{
    public class GetCartDto : CartDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
    }
}
