using Application.DataTransfer.Cart;
using Application.Queries.Cart;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CartQ
{
    public class GetCarts : IGetCarts
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetCarts(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 11;

        public string Name => "Get Carts";

        public PagedResponse<GetCartDto> Execute(CartSearch search)
        {
            var query = _context.Cart
               .Include(x => x.User)
               .Include(x => x.Product).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                string keyword = search.Keyword.ToLower();

                query = query.Where(x =>
                 x.User.FirstName.ToLower().Contains(keyword) ||
                 x.User.LastName.ToLower().Contains(keyword) ||
                 x.User.Email.ToLower().Contains(keyword) ||
                 x.Product.Name.ToLower().Contains(keyword)
               );
            }

            if (search.AddedFrom.HasValue)
            {
                query = query.Where(x => x.AddedAt >= search.AddedFrom);
            }

            if (search.AddedTo.HasValue)
            {
                query = query.Where(x => x.AddedAt <= search.AddedTo);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = _maper.Map<PagedResponse<GetCartDto>>(search);
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipCount)
              .Take(search.PerPage).Select(x => _maper.Map<GetCartDto>(x)).ToList();

            return response;
        }
    }
}
