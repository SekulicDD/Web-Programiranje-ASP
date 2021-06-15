using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.OrderQ
{
    public class GetOrder : IGetOrder
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetOrder(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }
        public int Id => 10;

        public string Name => "Get Order";

        public GetOrderDto Execute(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                throw new NotFoundException(id, typeof(Order));

            return _maper.Map<GetOrderDto>(order);
        }
    }
}
