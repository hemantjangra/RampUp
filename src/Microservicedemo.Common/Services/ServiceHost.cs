using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using RawRabbit;
using Microservicedemo.Common.Commands;
using Microservicedemo.Common.Events;
using Microservicedemo.Common.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace Microservicedemo.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;
        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartUp>(string[] args) where TStartUp : class
        {
            Console.Title = typeof(TStartUp).Namespace;
            var config = new ConfigurationBuilder()
                        .AddEnvironmentVariables()
                        .AddCommandLine(args)
                        .Build();
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                                    .UseConfiguration(config)
                                    .UseStartup<TStartUp>();
            return new HostBuilder(webHostBuilder.Build());

        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private IBusClient _bus;

            private readonly IWebHost _webHost;
            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseRabbitMQ()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));
                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private readonly IBusClient _bus;
            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var commandHandlerServiceScope = _webHost.Services.GetService<IServiceScopeFactory>();
                using(var scope = commandHandlerServiceScope.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<ICommandHandler<TCommand>>();
                    _bus.WithCommandHandlerAsync(handler);
                }
                //var handler = (ICommandHandler<TCommand>)_webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
                //_bus.WithCommandHandlerAsync(handler); 

                return this;
            }

            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var eventHandlerScopedService = _webHost.Services.GetService<IServiceScopeFactory>();
                using(var scope = eventHandlerScopedService.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetService<IEventHandler<TEvent>>();
                    _bus.WithEventHandlerAsync(handler);
                }
                //var handler = (IEventHandler<TEvent>)_webHost.Services.GetService(typeof(IEventHandler<TEvent>));
                //_bus.WithEventHandlerAsync(handler);

                return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }




    }
}