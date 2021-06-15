using Application.Commands.Product;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Implementation.Commands.ProductC
{
    public class UpdateProduct : IUpdateProduct
    {
        private readonly Context _context;
        private readonly UpdateProductValidator _validator;
        private readonly IMapper _mapper;

        public UpdateProduct(Context context, UpdateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }
        public int Id => 20;

        public string Name => "Update product";

        public void Execute(CreateProductDto request)
        {
            _validator.ValidateAndThrow(request);
            var product = _context.Products.Find(request.Id);
            if (product == null)
                throw new NotFoundException(request.Id, typeof(Product));

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
                request.Name = product.Name;

            if (request.Price <= 0.1m)
                request.Price = product.Price;

            if (request.Image != null)
            {
                var guid = Guid.NewGuid();
                var extension = Path.GetExtension(request.Image.FileName);
                var newFileName = guid + extension;

                var path = Path.Combine("wwwroot", "images", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    request.Image.CopyTo(fileStream);
                }

                _context.Images.Add(new Image
                {
                    Path = newFileName,
                    Product = product,
                });
            } 
        
            try
            {
                if (request.CategoryIds.Any())
                {
                    _context.ProductCategories
                        .RemoveRange(_context.ProductCategories.Where(x => x.ProductId == request.Id));
                    _context.SaveChanges();
                }
                _mapper.Map(request, product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new DatabaseException();
            }
        }
    }
}
