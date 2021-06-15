using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(int id, Type type)
            : base($"Entity of type {type.Name} with an id of {id} was not found.")
        {

        }
    }
}
