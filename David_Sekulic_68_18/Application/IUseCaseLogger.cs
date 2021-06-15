using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IUseCaseLogger
    {
        void Log(IUseCase useCase, IActor actor, object useCaseData);
    }
}
