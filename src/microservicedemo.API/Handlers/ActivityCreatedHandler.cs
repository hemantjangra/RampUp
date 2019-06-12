using System;
using System.Threading.Tasks;
using microservicedemo.API.Models;
using microservicedemo.API.Repositories;
using Microservicedemo.Common.Events;
using Microsoft.Extensions.Logging;

namespace microservicedemo.API.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        private readonly ILogger<ActivityCreatedHandler> _logger;
        public ActivityCreatedHandler(IActivityRepository activityRepo, ILogger<ActivityCreatedHandler> logger)
        {
            _activityRepository = activityRepo;
            _logger  = logger;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity{
                Id = @event.Id,
                Category = @event.Category,
                CreatedAt = @event.CreatedAt,
                Description = @event.Description,
                UserId = @event.UserId,
                Name = @event.Name
            });
            // await Task.CompletedTask;
            // Console.WriteLine($"Activity Created {@event.Name}");
            _logger.LogInformation($"Acvity Created with Event Name: {@event.Name}");
        }
    }
}