using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class DeleteOrder : IDeleteOrder
    {
        private readonly Context _context;

        public DeleteOrder(Context context)
        {
            _context = context;
        }
        public int Id => 8;

        public string Name => "Delete order";

        public void Execute(int request)
        {
            try
            {
                var order = _context.Orders.Find(request);


                if (order == null)
                {
                    throw new NotFoundException(request, typeof(Order));
                }

                _context.RemoveRange(order.OrderDetails);
                _context.Orders.Remove(order);
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
