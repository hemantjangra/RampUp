using System;
using System.Threading.Tasks;
using Learn.Common.Commands;
using Learn.Common.Events;
using RawRabbit;

namespace Learn.Services.Activities.Handlers
{
    public class ActivityCreateHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        public ActivityCreateHandler(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task HandleAsync(CreateActivity Command)
        {
            Console.WriteLine($"Creating Activity: {Command.Name}");
            await _busClient.PublishAsync(new ActivityCreated(Command.Id, Command.UserId, Command.Category, Command.Name));
        }
    }
}