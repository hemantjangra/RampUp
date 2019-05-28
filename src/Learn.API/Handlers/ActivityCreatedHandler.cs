using System.Threading.Tasks;
using Learn.Common.Events;
using System;

namespace Learn.API.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        public ActivityCreatedHandler()
        {

        }

        public async Task HandleAsync(ActivityCreated Event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Activity Created: {Event.Name}");
        }
    }
}