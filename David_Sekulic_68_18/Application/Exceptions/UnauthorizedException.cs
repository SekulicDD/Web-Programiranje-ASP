using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(IUseCase useCase,IActor actionExecutor)
            :base($"Action executor with an id of{actionExecutor.Id} {actionExecutor.Identity} tried to execute {useCase.Name}.")
        {

        }
    }
}
