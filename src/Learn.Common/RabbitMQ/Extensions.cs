using System.Reflection;
using System.Threading.Tasks;
using Learn.Common.Constants;
using Learn.Common.Commands;
using Learn.Common.Events;
using Learn.Common.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RawRabbit;
using RawRabbit.Instantiation;

namespace Learn.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            //return bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
            //                                     context => context.UseSubscribeConfiguration(cfg =>
            //                                     cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));



        }

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
            IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                context => context.UseSubscribeConfiguration(cfg =>
                cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>()
        {
            return $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
        }

        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration, string sectionName="rabbitmq")
        {
            var section = configuration.GetSection(sectionName);
            var options = new RabbitMQOptions()
            {
                Hostname = section.GetValue(RabbitMQSettings.Host,"localhost"),
                Port = section.GetValue(RabbitMQSettings.Port, 5672),
                Username = section.GetValue(RabbitMQSettings.UserName, "guest"),
                Password = section.GetValue(RabbitMQSettings.PassWord, "guest")
            };
            IConnection connection = CreateQueue(options);
            services.AddSingleton<IConnection>(connection);
        }

        private static IConnection CreateQueue(RabbitMQOptions options)
        {
            var factory = new ConnectionFactory()
            {
                HostName = options?.Hostname,
                Port = options.Port,
                UserName = options?.Username,
                Password = options?.Password
            };
            IConnection connection = factory.CreateConnection();
            return connection;
        }
    }
}