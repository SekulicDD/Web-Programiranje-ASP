using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.CategoryQ
{
    public interface IGetCategory:IQuery<int, CategoryDto>
    {
    }
}
