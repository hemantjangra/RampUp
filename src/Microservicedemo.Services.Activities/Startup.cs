using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microservicedemo.Common.RabbitMQ;
using Microservicedemo.Common.MongoDB;
using Microservicedemo.Common.Commands;
using Microservicedemo.Services.Activities.Handlers;
using Microservicedemo.Services.Activities.Domain.Repository;
using Microservicedemo.Services.Activities.Repositories;
using Microservicedemo.Services.Activities.Services;

namespace Microservicedemo.Services.Activities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddLogging();
            services.AddMongoDB(Configuration);
            services.AddRabbitMQ(Configuration);
            //services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddScoped<IActivityService, ActivityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //var dbInitializerScopeFactory = app.ApplicationServices.GetService<IDatabaseInitializer>();
            using(var serviceScope=app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<IDatabaseInitializer>().InitializedAsync();
            }
            //app.ApplicationServices.GetService<IDatabaseInitializer>().InitializedAsync();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
