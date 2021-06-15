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

namespace Implementation.Queries
{
    public class GetProducts : IGetProducts
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetProducts(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 3;
        public string Name => "Product search";

        public PagedResponse<GetProductDto> Execute(ProductSearch search)
        {
            var query = _context.Products.Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                string keyword = search.Keyword.ToLower();

                query = query.Where(x =>
                 x.Name.ToLower().Contains(keyword) ||
                 x.Description.ToLower().Contains(keyword) ||
                 x.ProductCategories.Any(x => x.Category.Name.ToLower().Contains(keyword))
               );
            }

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => x.Price < search.MaxPrice);
            }

            if (search.MinPrice.HasValue)
            {
                query = query.Where(x => x.Price > search.MinPrice);
            }

            if (search.CategoryIds.Any())
            {
                query = query.Where(x => x.ProductCategories.Any(c => search.CategoryIds.Contains(c.CategoryId)));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = _maper.Map<PagedResponse<GetProductDto>>(search);
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipCount)
              .Take(search.PerPage).Select(x => _maper.Map<GetProductDto>(x)).ToList();

            return response;
        }
    }
}
