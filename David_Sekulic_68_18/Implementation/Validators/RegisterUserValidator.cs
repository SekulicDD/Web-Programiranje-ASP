using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DataTransfer;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(Context context)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Email).NotEmpty().Must(x => !context.Users.Any(user => user.Email == x))
                .WithMessage("Email is already taken.")
                .EmailAddress();

            
        }
    }
}
