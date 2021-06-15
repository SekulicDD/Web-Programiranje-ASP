using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class GetProduct : IGetProduct
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetProduct(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 6;

        public string Name => "Get product details";

        public GetProductDto Execute(int id)
        {
            var product = _context.Products.Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);

            if (product == null)
                throw new NotFoundException(id, typeof(Product));

            return _maper.Map<GetProductDto>(product);
            
        }
    }
}
