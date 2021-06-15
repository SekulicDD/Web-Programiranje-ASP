
using Application;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class GetLogs : IGetLogs
    {
        private readonly Context _context;
        private readonly IMapper _maper;

        public GetLogs(Context context, IMapper maper)
        {
            _context = context;
            _maper = maper;
        }

        public int Id => 4;

        public string Name => "Get Logs";

        public PagedResponse<LogDto> Execute(LogSearch search)
        {
            var query = _context.UseCaseLogs.Include(x=>x.User).Include(x => x.UseCase).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                string keyword = search.Keyword.ToLower();

                query = query.Where(x =>
                 x.Actor.ToLower().Contains(keyword) ||
                 x.User.FirstName.ToLower().Contains(keyword) ||
                 x.User.LastName.ToLower().Contains(keyword) ||
                 x.UseCase.Name.ToLower().Contains(keyword) 
               );
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.Date >= search.DateFrom);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.Date <= search.DateTo);
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var reponse = new PagedResponse<LogDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount)
                .Take(search.PerPage).Select(x => _maper.Map<LogDto>(x)).ToList()
            };

            return reponse;
        }
    }
}
