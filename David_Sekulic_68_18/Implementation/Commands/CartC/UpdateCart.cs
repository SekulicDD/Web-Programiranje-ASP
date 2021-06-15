using Application.Commands.Cart;
using Application.DataTransfer.Cart;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CartC
{
    public class UpdateCart : IUpdateCart
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UpdateCart(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 21;

        public string Name => "Update cart";

        public void Execute(CartDto request)
        {
            var cart = _context.Cart.Find(request.Id);

            if (cart == null)
                throw new NotFoundException(request.Id, typeof(Cart));

            if (request.Quantity < 1) {
                throw new ValidationException("", new List<ValidationFailure> { new ValidationFailure("Quantity", "Quantity must be 1 or higer") });
            }

            cart.Quantity = request.Quantity;
            _context.SaveChanges();
        }
    }
}
