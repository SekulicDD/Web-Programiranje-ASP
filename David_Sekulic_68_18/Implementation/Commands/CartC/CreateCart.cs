using Application.Commands.Cart;
using Application.DataTransfer.Cart;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.CartC
{
    public class CreateCart : ICreateCart
    {
        private readonly Context _context;
        private readonly CreateCartValidator _validator;
        private readonly IMapper _mapper;

        public CreateCart(Context context, CreateCartValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 14;

        public string Name => "Add to cart";

        public void Execute(CreateCartDto request)
        {
            _validator.ValidateAndThrow(request);
            try
            {
                var cart = _context.Cart.FirstOrDefault(x => x.ProductId == request.ProductId && x.UserId == request.UserId);
                if (cart!=null)
                {
                    cart.Quantity += request.Quantity;
                    _context.SaveChanges();
                    return;
                }
               
                cart = _mapper.Map<Cart>(request);
                cart.AddedAt = DateTime.Now;
                _context.Cart.Add(cart);
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
