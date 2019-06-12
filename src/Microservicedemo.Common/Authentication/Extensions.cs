using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microservicedemo.Common.Authentication
{
    public static class Extensions
    {
        public static void AddJWT(this IServiceCollection services, IConfiguration configuration, string sectionName = "jwt")
        {
            var options = new JWTOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(options);
            services.Configure<JWTOptions>(section);
            services.AddSingleton<IJWTHandler, JWTHandler>();
            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = options.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
                };
            });
        }
    }
}