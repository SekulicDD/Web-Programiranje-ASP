using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(int id, Type type)
            : base($"Entity of type {type.Name} with an id of {id} cannot be deleted due to conflict with other entity.")
        {

        }
    }
}
