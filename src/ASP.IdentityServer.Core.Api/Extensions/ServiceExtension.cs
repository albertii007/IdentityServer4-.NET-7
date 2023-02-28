using ASP.IdentityServer.Core.Api.Extensions.Configurations;
using ASP.IdentityServer.Core.Api.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASP.IdentityServer.Core.Api.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            services.AddDbContext(configuration);
            services.AddDefaultCors();
            services.AddHealthChecks();
            services.AddSwagger();
            services.AddMediator();
            services.AddHttpClient();
            services.AddIdentityServer4(configuration);
            services.AddAuthentication(configuration);
            services.AddAuthorization();
            services.AddFluentValidations();
            return services;
        }

        public static IMvcBuilder MvcBuildServices(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });

            return builder;
        }

        public static IApplicationBuilder UseServices(this WebApplication app)
        {
            //app.MigrateDbContext();
            app.UseRegisteredCors();
            app.UseHealthChecks("/health");
            app.UseRegisteredSwagger();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            return app;
        }
    }
}
