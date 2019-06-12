using Microsoft.AspNetCore.Mvc;

namespace microservicedemo.API.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content($"This is Home API");
    }
}