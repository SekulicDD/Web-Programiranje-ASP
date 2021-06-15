using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.CategoryQ;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CategoryQ
{
    public class GetCategory : IGetCategory
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetCategory(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 19;

        public string Name => "Get category";

        public CategoryDto Execute(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
                throw new NotFoundException(id, typeof(Category));

            return _maper.Map<CategoryDto>(category);
        }
    }
}
