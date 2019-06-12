using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microservicedemo.Common.Services;
using Microservicedemo.Common.Commands;

namespace Microservicedemo.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ServiceHost.Create<Startup>(args)
            .UseRabbitMQ()
            .SubscribeToCommand<CreateUser>()
            .Build()
            .Run();
        }
    }
}
