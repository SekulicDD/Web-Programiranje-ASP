using Application.DataTransfer.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Cart
{
    public interface IGetCart : IQuery<int,GetCartDto>
    {
    }
}
