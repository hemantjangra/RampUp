using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microservicedemo.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microservicedemo.Common.Events;

namespace microservicedemo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
            .UseRabbitMQ()
            .SubscribeToEvent<ActivityCreated>()
            .Build()
            .Run();
        }
    }
}
