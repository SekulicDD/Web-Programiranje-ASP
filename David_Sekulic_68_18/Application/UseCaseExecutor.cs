using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Application.Exceptions;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IActor actor;
        private readonly IUseCaseLogger logger;

        public UseCaseExecutor(IActor actor, IUseCaseLogger logger)
        {
            this.actor = actor;
            this.logger = logger;
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            logger.Log(command, actor, request);

            if (!actor.AllowedUseCases.Contains(command.Id))
                throw new UnauthorizedException(command,actor);

            command.Execute(request);
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            logger.Log(query, actor, search);

            if (!actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedException(query, actor);
            }

            return query.Execute(search);
        }
    }
}
