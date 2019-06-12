using System;
using System.Threading.Tasks;
using Microservicedemo.Common.Commands;
using Microservicedemo.Common.Events;
using Microservicedemo.Common.Exceptions;
using Microservicedemo.Services.Activities.Domain.Models;
using Microservicedemo.Services.Activities.Domain.Repository;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Microservicedemo.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;

        private readonly IActivityRepository _activityRepository;

        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient busClient, IActivityRepository activityRepository, ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityRepository = activityRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            if (command != null && !string.IsNullOrEmpty(command.Name))
            {
                try
                {

                    _logger.LogInformation($"Creating Activity with Name {command.Name} and id {command.Id}");
                    await _activityRepository.AddAsync(new Activity(command.Id, command.Name, command.Category, command.Description,
                                                            command.UserId, command.CreatedAt));
                    await _busClient.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Category,
                                                    command.Name, command.Description, command.CreatedAt));
                }
                catch (LearnException ex)
                {
                    _logger.LogError($"Exception occured in MicroserviceDemo.Services.Activities.Handlers --> HandleAsync Method Caught Learn Exceptiom",
                                        ex.StackTrace);
                    await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, ex.Code));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception occured in MicroserviceDemo.Services.Activities.Handlers --> HandleAsync Method Caught Generic Exceptiom",
                                        ex.StackTrace);
                    await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, "error"));
                }
            }
        }
    }
}