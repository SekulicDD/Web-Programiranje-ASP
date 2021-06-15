using Application;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public LoggerController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<LoggerController>
        [HttpGet]
        public IActionResult Get([FromQuery] LogSearch search, [FromServices] IGetLogs query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

     
    }
}
