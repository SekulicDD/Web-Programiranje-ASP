using Application;
using Application.Commands;
using Application.DataTransfer;
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
    public class RegisterController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;
        public RegisterController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // POST api/<RegisterController>
        [HttpPost]
        public void Post([FromBody] RegisterUserDto dto,[FromServices] IRegisterUser command)
        {
            _executor.ExecuteCommand(command, dto);
        }

    }
}
