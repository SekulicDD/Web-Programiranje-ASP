using Application.Commands;
using Application.DataTransfer;
using DataAccess;
using Domain;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using AutoMapper;
using Application.Exceptions;
using System.IO;

namespace Implementation.Commands.ProductC
{
    public class CreateProduct : ICreateProduct
    {
        private readonly Context _context;
        private readonly CreateProductValidator _validator;
        private readonly IMapper _mapper;

        public CreateProduct(Context context, CreateProductValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 1;

        public string Name => "Create Product";

        public void Execute(CreateProductDto request)
        {
            _validator.ValidateAndThrow(request);

            var guid = Guid.NewGuid();
            var extension = Path.GetExtension(request.Image.FileName);
            var newFileName = guid + extension;

            var path = Path.Combine("wwwroot", "images", newFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                request.Image.CopyTo(fileStream);
            }

            try
            {       
                var product = _mapper.Map<Product>(request);

                foreach (var item in request.CategoryIds)
                {
                    product.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = item });
                }

                _context.Images.Add(new Image
                {
                    Path = newFileName,
                    Product = product,
                });

                _context.Products.Add(product);
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
