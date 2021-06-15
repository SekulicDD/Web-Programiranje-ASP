using Application.DataTransfer.Cart;
using Application.Exceptions;
using Application.Queries.Cart;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CartQ
{
    public class GetCart : IGetCart
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetCart(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 12;

        public string Name => "Get Cart";

        public GetCartDto Execute(int id)
        {
            var order = _context.Cart.Include(x=>x.Product).Include(x => x.User).FirstOrDefault(x => x.Id == id);

            if (order == null)
                throw new NotFoundException(id, typeof(Cart));

            return _maper.Map<GetCartDto>(order);
        }
    }
}
