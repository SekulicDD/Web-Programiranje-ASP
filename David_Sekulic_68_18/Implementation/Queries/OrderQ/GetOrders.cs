
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.OrderQ
{
    public class GetOrders : IGetOrders
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetOrders(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 9;

        public string Name => "Get Orders";

        public PagedResponse<GetOrderDto> Execute(OrderSearch search)
        {
            var query = _context.Orders
                .Include(x => x.User)
                .Include(x=>x.OrderDetails)
                .ThenInclude(o=>o.Product).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                string keyword = search.Keyword.ToLower();

                query = query.Where(x =>
                 x.User.FirstName.ToLower().Contains(keyword) ||
                 x.User.LastName.ToLower().Contains(keyword) ||
                 x.User.Address.ToLower().Contains(keyword) ||
                 x.User.Email.ToLower().Contains(keyword)
               );
            }

            if (search.MaxTotal.HasValue)
            {
                query = query.Where(x => x.Total <= search.MaxTotal);
            }

            if (search.MinTotal.HasValue)
            {
                query = query.Where(x => x.Total >= search.MinTotal);
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.OrderedAt >= search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.OrderedAt <= search.DateTo);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = _maper.Map<PagedResponse<GetOrderDto>>(search);
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipCount)
              .Take(search.PerPage).Select(x => _maper.Map<GetOrderDto>(x)).ToList();

            return response;
        }
    }
}
