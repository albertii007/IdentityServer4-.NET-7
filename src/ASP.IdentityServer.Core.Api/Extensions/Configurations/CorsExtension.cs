using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.IdentityServer.Core.Api.Extensions.Configurations
{
    public static class CorsExtension
    {
        public static string corsPolicyName = "SiteCorsPolicy";
        public static void AddDefaultCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName, builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

        }

        public static void UseRegisteredCors(this WebApplication app)
        {
            app.UseCors(corsPolicyName);
        }
    }

}
