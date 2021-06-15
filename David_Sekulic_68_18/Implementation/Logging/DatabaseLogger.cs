using Application;
using Application.Exceptions;
using DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Logging
{
    public class DatabaseLogger : IUseCaseLogger
    {
        private readonly Context _context;

        public DatabaseLogger(Context context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IActor actor, object useCaseData)
        {
            try
            {
                var log = new Domain.UseCaseLog
                {
                    Actor = actor.Identity,
                    Data = JsonConvert.SerializeObject(useCaseData),
                    Date = DateTime.UtcNow,
                    UseCaseId = useCase.Id
                };

                if (actor.Id != 0)
                    log.UserId = actor.Id;

                _context.UseCaseLogs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new DatabaseException();
            }
        }
    }
}
