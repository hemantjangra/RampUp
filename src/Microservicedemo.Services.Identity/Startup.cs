using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservicedemo.Common.Commands;
using Microservicedemo.Services.Identity.Domain.Repositories;
using Microservicedemo.Services.Identity.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microservicedemo.Common.MongoDB;
using Microservicedemo.Common.RabbitMQ;
using Microservicedemo.Services.Identity.Services;
using Microservicedemo.Services.Identity.Handlers;
using Microservicedemo.Common.Authentication;

namespace Microservicedemo.Services.Identity
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
            services.AddJWT(Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddMongoDB(Configuration);
            services.AddRabbitMQ(Configuration);
            services.AddScoped<IEncryptor, Encryptor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IJWTHandler, JWTHandler>();
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
            using(var serviceScope=app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<IDatabaseInitializer>().InitializedAsync();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
