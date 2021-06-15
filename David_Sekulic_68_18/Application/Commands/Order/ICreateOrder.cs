
using Application.DataTransfer.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ICreateOrder : ICommand<CreateOrderDto>
    {
    }
}
