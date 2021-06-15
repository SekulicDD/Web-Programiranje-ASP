using Application.DataTransfer.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Cart
{
    public interface ICreateCart : ICommand<CreateCartDto>
    {
    }
}
