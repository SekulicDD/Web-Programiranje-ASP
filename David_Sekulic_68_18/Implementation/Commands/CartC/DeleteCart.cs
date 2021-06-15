using Application.Commands.Cart;
using Application.Exceptions;
using DataAccess;
using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.CartC
{
    public class DeleteCart : IDeleteCart
    {
        private readonly Context _context;

        public DeleteCart(Context context)
        {
            _context = context;
        }

        public int Id => 13;

        public string Name => "Delete from cart";

        public void Execute(int request)
        {
            try
            {
                var cart = _context.Cart.Find(request);

                if (cart == null)
                {
                    throw new NotFoundException(request, typeof(Cart));
                }

                _context.Cart.Remove(cart);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message);
                throw new DatabaseException();
            }
        }
    }
}
