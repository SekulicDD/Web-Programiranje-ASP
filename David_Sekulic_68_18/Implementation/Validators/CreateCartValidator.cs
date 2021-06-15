
using Application.DataTransfer.Cart;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateCartValidator : AbstractValidator<CreateCartDto>
    {
        public CreateCartValidator(Context context)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User is required").DependentRules(() =>
              {
                  RuleFor(x => x.UserId).Must(id => context.Users.Any(u => u.Id == id))
                  .WithMessage("User does not exist");
              });
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product is required").DependentRules(() =>
            {
                RuleFor(x => x.ProductId).Must(id => context.Products.Any(p => p.Id == id))
                .WithMessage("Product does not exist");
            });
            RuleFor(x => x.Quantity).Must(x => x > 0).WithMessage("Quantity is required");
        }
    }
}
