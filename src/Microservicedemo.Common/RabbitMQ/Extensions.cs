using System.Reflection;
using System.Threading.Tasks;
using Microservicedemo.Common.Commands;
using Microservicedemo.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;

namespace Microservicedemo.Common.RabbitMQ
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient _bus, ICommandHandler<TCommand> handler) where TCommand : ICommand
        => _bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg), ctx => ctx.UseSubscribeConfiguration
                (cfg => cfg.FromDeclaredQueue(q=>q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient _bus, IEventHandler<TEvent> handler) where TEvent : IEvent
        => _bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg), ctx => ctx.UseSubscribeConfiguration
                (cfg => cfg.FromDeclaredQueue(q=>q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>()=>$"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration, string sectionName = "rabbitmq")
        {
            var options = new RabbitMQOptions();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
                {
                    ClientConfiguration = options
                });
            services.AddSingleton<IBusClient>(client);
        }
    }
}