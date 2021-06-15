using Application.Commands.Category;
using Application.Exceptions;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using System.Linq;

namespace Implementation.Commands.CategoryC
{
    public class DeleteCategory : IDeleteCategory
    {
        private readonly Context _context;

        public DeleteCategory(Context context)
        {
            _context = context;
        }

        public int Id => 16;

        public string Name => "Delete category";

        public void Execute(int id)
        {
            if (_context.ProductCategories.Any(x => x.CategoryId == id))
                throw new ConflictException(id, typeof(Category));

            try
            {
                var category = _context.Categories.Find(id);

                if (category == null)
                {
                    throw new NotFoundException(id, typeof(Category));
                }

                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(NotFoundException))
                    throw ex;
                Console.Write(ex.Message);
                throw new DatabaseException();
            }
        }
    }
}
