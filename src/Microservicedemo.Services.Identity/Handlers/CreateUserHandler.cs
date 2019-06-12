using System;
using System.Threading.Tasks;
using Microservicedemo.Common.Commands;
using Microservicedemo.Common.Events;
using Microservicedemo.Common.Exceptions;
using Microservicedemo.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Microservicedemo.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {

        private readonly ILogger<CreateUserHandler> _logger;

        private readonly IUserService _userService;

        private readonly IBusClient _busClient;

        public CreateUserHandler(ILogger<CreateUserHandler> logger, IUserService userService, IBusClient busClient)
        {
            _logger = logger;
            _busClient = busClient;
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Create USer Handler called with params: User Email as {command.Email} and User Name as {command.Name}");
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                _logger.LogInformation($"User created with Email {command.Email} and name {command.Name}");
            }
            catch(LearnException learnEx)
            {
                _logger.LogError(learnEx, learnEx.Message);
                await _busClient.PublishAsync(new CreateUserRejected(learnEx.Message, learnEx.Code, command.Email));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserRejected(ex.Message, "error", command.Email));
            }
        }
    }
}