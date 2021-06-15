using Application;
using Application.Commands;
using Application.DataTransfer.Order;
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
    public class OrdersController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public OrdersController(IActor actionExecutor, UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get([FromQuery] OrderSearch search, [FromServices] IGetOrders query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }


        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOrder query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] CreateOrderDto dto, [FromServices] ICreateOrder command)
        {
            executor.ExecuteCommand(command, dto);
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteOrder command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
