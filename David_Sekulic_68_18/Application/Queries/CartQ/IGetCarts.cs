using Application.DataTransfer.Cart;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Cart
{
    public interface IGetCarts : IQuery<CartSearch, PagedResponse<GetCartDto>>
    {
    }
}
