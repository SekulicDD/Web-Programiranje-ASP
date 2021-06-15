using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Application.DataTransfer;
using System.Linq;

namespace Implementation.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator(Context context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(name => !context.Products.Any(p => p.Name == name))
                .WithMessage("Provided product name already exist in database");
            });

            RuleFor(x => x.Price).Must(price => price > 0.1m).WithMessage("Price value must be over 0.1$");

            RuleFor(x => x.CategoryIds)
                .Must(cats => !cats.Any() || cats.All(catId => context.Categories.Any(c => c.Id == catId)))
                .WithMessage("Some of categories listed don't exist");

            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required.");

        }
    }
}
