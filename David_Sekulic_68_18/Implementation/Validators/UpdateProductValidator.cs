using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
  
     public class UpdateProductValidator : AbstractValidator<CreateProductDto>
    {
        public UpdateProductValidator(Context context)
        {
           
            RuleFor(x => x.Name).Must((dto, name) => !context.Products.Any(p => p.Name == name && p.Id != dto.Id))
            .WithMessage("Provided product name already exist in database");
            

            RuleFor(x => x.CategoryIds)
                .Must(cats => !cats.Any() || cats.All(catId => context.Categories.Any(c => c.Id == catId)))
                .WithMessage("Some of categories listed don't exist");


        }
    }
}
