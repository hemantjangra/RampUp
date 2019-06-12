using System.IO;
using Microservicedemo.Common.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservicedemo.Common.MongoDB
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configurations, string sectionName="mongo")
        {
            var mongoOptions = new MongoOptions();
            var mongoSection = configurations.GetSection(sectionName);
            var configBuilder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = configBuilder.Build();
            //mongoSection.Bind(mongoOptions);
            services.Configure<MongoOptions>(configurations.GetSection(sectionName));
            services.AddSingleton<MongoClient>(c=>{
                var options = c.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c=>{
                var options = c.GetService<IOptions<MongoOptions>>();
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(options.Value.Database);
            });
            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }
    }
}