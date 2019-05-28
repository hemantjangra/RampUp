using Microsoft.AspNetCore.Mvc;

namespace Learn.API.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Get() => Content("Hello from Learn.API");
    }
}