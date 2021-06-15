using Application.Commands;
using Application.DataTransfer;
using Application.Email;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class RegisterUser : IRegisterUser
    {
        public int Id => 2;

        public string Name => "Register user";

        private readonly Context  _context;
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _sender;
        private readonly IMapper _mapper;

        public RegisterUser(Context context, RegisterUserValidator validator, IEmailSender sender, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
            _mapper = mapper;
        }
        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);
            try
            {
                var user = _mapper.Map<User>(request);

                int[] cases = { 3, 6, 7, 10, 12, 13, 14, 21 };

                _context.Users.Add(user);
                foreach (int caseId in cases)
                {
                    _context.UserUseCase.Add(new UserUseCase { User = user, UseCaseId = caseId });
                }
                _context.SaveChanges();

                _sender.Send(new SendEmailDto
                {
                    Content = "<h1>Successfull registration!</h1>",
                    SendTo = request.Email,
                    Subject = "Registration"
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new DatabaseException();
            }
            
        }
    }
}
