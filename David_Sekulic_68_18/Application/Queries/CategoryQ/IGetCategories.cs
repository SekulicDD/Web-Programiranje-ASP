using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.CategoryQ
{
    public interface IGetCategories : IQuery<CategorySearch, PagedResponse<CategoryDto>>
    {
    }
}
