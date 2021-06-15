using Application.Commands;
using Application.DataTransfer.Order;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class CreateOrder : ICreateOrder
    {
        private readonly Context _context;
        private readonly CreateOrderValidator _validator;
        private readonly IMapper _mapper;

        public CreateOrder(Context context, CreateOrderValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public int Id => 7;

        public string Name => "Create order";

        public void Execute(CreateOrderDto request)
        {
            _validator.ValidateAndThrow(request);
            try
            {
                var order = _mapper.Map<Order>(request);

                _context.Orders.Add(order);
                order.OrderedAt = DateTime.Now;
                _context.SaveChanges();

                var carts = _context.Cart.Where(x => x.UserId == order.UserId).Include(x=>x.Product);
              
                List<OrderDetails> orderDetails = new List<OrderDetails>();
                decimal total = 0;
                foreach (var item in carts)
                {
                    orderDetails.Add(new OrderDetails
                    {
                        OrderId=order.Id,
                        ProductId=item.ProductId,
                        ProductName=item.Product.Name,
                        ProductPrice=item.Product.Price,
                        Quantity=item.Quantity
                    });

                    total += item.Product.Price * item.Quantity;
                }
                order.Total = total;

                _context.Cart.RemoveRange(carts);
                _context.OrderDetails.AddRange(orderDetails);
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
