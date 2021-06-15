using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer.Cart
{
    public class CreateCartDto : CartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
