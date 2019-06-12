using Microservicedemo.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System.Threading.Tasks;
using System;
using microservicedemo.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace microservicedemo.API.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;

        private readonly IActivityRepository _activityRepository;

        private readonly ILogger<ActivitiesController> _logger;

        public ActivitiesController(IBusClient busClient, IActivityRepository activityRepo, ILogger<ActivitiesController> logger)
        {
            _activityRepository = activityRepo;
            _busClient = busClient;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));
            return Json(activities.Select(x => new { x.Id, x.Name, x.Category, x.CreatedAt }));
        }

        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            else if (activity.UserId != Guid.Parse(User.Identity.Name))
            {
                return Unauthorized();
            }
            return Json(activity);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation($"Activity Controller Post Method Started and command Email is {command.Name} and Category Catefory {command.Category} and Description is {command.Description}");
            command.Id = Guid.NewGuid();
            command.UserId = Guid.Parse(User.Identity.Name);
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }
    }
}