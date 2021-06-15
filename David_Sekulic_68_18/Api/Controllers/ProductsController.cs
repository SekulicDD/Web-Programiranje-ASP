using Application;
using Application.Commands;
using Application.Commands.Product;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using Implementation.Validators;
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
    public class ProductsController : ControllerBase
    {

        private readonly UseCaseExecutor executor;

        public ProductsController(IActor actionExecutor, UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearch search,[FromServices] IGetProducts query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        //GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetProduct query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromForm] CreateProductDto dto, [FromServices] ICreateProduct command)
        {
            executor.ExecuteCommand(command, dto);
            return Ok();
        }

        //PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromForm] CreateProductDto dto,
            [FromServices] UpdateProductValidator validator,
            [FromServices] IUpdateProduct command)
        {
            
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }
      

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProduct command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
