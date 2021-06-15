using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DataTransfer;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator(Context context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").DependentRules(() =>
            {
                RuleFor(x => x.Name).Must(name => !context.Categories.Any(c => c.Name == name))
                .WithMessage("Category with this name already exists");
            });
            
        }
    }
}
