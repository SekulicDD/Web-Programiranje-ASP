using Application;
using Application.Commands.Category;
using Application.DataTransfer;
using Application.Queries.CategoryQ;
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
    public class CategoriesController : ControllerBase
    {
        private readonly UseCaseExecutor executor;

        public CategoriesController(UseCaseExecutor executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search, [FromServices] IGetCategories query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCategory query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

      
        [HttpPost]
        public void Post([FromBody] CategoryDto dto, [FromServices] ICreateCategory command)
        {
            executor.ExecuteCommand(command, dto);
        }

    
        [HttpPut("{id}")]
        public void Put(int id,[FromBody] CategoryDto dto, [FromServices] IUpdateCategory command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategory command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
