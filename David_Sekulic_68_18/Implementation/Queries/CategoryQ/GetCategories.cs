using Application.DataTransfer;
using Application.Queries.CategoryQ;
using Application.Searches;
using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CategoryQ
{
    public class GetCategories : IGetCategories
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetCategories(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 18;

        public string Name => "Get categories";

        public PagedResponse<CategoryDto> Execute(CategorySearch search)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                string keyword = search.Name.ToLower();

                query = query.Where(x => x.Name.ToLower().Contains(keyword));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = _maper.Map<PagedResponse<CategoryDto>>(search);
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipCount)
              .Take(search.PerPage).Select(x => _maper.Map<CategoryDto>(x)).ToList();

            return response;
        }
    }
}
