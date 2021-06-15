using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IUseCase
    {
        int Id { get; }
        string Name { get; }
    }
    public interface ICommand<TRequest> : IUseCase
    {
        void Execute(TRequest request);
    }
    public interface IQuery<TSearch, TResult> : IUseCase
    {
        TResult Execute(TSearch search);
    }
}
