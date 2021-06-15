using Application;
using Application.Commands.Cart;
using Application.DataTransfer.Cart;
using Application.Queries.Cart;
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
    public class CartsController : ControllerBase
    {

        private readonly UseCaseExecutor executor;

        public CartsController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] CartSearch search, [FromServices] IGetCarts query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        //GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCart query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCartDto dto, [FromServices] ICreateCart command)
        {
            executor.ExecuteCommand(command, dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CartDto dto, [FromServices] IUpdateCart command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return Ok();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCart command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
