using System.Threading.Tasks;
using Learn.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;

namespace Learn.API.Controllers
{
    [Route("[controller]")]
    public class ActivitiesController: Controller
    {
        private readonly IBusClient _busClient;
        public ActivitiesController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            command.Id = new System.Guid();
            command.CreatedAt = DateTime.UtcNow;

            await _busClient.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }
    }
}