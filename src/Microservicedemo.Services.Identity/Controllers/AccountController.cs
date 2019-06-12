using Microservicedemo.Common.Commands;
using Microservicedemo.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Microservicedemo.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
        {
            Console.WriteLine("Login path called");
            return Json(await _userService.LoginAsync(command.Email, command.Password));
        }

        [HttpGet("/")]
        public string Name()
        {
            return "Working";
        }
    }
}