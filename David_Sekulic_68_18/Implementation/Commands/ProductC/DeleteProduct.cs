using Application.Commands;
using Application.Exceptions;
using DataAccess;
using Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands.ProductC
{
    public class DeleteProduct : IDeleteProduct
    {
        private readonly Context _context;

        public DeleteProduct(Context context)
        {
            _context = context;
        }

        public int Id => 5;

        public string Name => "Delete product";

        public void Execute(int request)
        {
            try
            {
                var product = _context.Products.Find(request);

                if (product == null)
                {
                    throw new NotFoundException(request, typeof(Product));
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.GetType()==typeof(NotFoundException))
                    throw ex;
                Console.Write(ex.Message);
                throw new DatabaseException();
            }
        }
    }
}
