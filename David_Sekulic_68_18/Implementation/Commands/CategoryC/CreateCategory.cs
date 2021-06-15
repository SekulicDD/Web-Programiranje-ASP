using Application.Commands.Category;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CategoryC
{
    public class CreateCategory : ICreateCategory
    {
        private readonly Context _context;
        private readonly CreateCategoryValidator _validator;
        private readonly IMapper _mapper;

        public CreateCategory(Context context, CreateCategoryValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 15;

        public string Name => "Create category";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            try
            {
                var category = _mapper.Map<Category>(request);

                _context.Categories.Add(category);
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
