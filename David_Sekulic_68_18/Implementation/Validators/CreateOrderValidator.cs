using Application.DataTransfer.Order;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator(Context context)
        {
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required."); 
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User is required");
            RuleFor(x => x.UserId).Must(id => context.Cart.Any(x => x.UserId == id))
                .WithMessage("Can't order with empty cart");
        }
    }
}
