using Application.Commands.Category;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CategoryC
{
    public class UpdateCategory : IUpdateCategory
    {
        private readonly Context _context;

        public UpdateCategory(Context context)
        {
            _context = context;
        }

        public int Id => 17;

        public string Name => "Update category";

        public void Execute(CategoryDto request)
        {           
           
            var category = _context.Categories.Find(request.Id);

            if (category == null)
                throw new NotFoundException(request.Id, typeof(Category));

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
                throw new ValidationException("", new List<ValidationFailure> { new ValidationFailure("Name", "Name is required.") });
    

            category.Name = request.Name;
            _context.SaveChanges();
           
        }
    }
}
