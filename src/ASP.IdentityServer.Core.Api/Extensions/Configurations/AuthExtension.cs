using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.IdentityServer.Core.Api.Extensions.Configurations
{
    public static class AuthExtension
    {

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var schema = configuration["Authentication:Schema"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = schema;
                options.DefaultChallengeScheme = schema;
            }).AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:4001";
                options.ApiName = "api_account";
                options.ApiSecret = "api_account_secret";
            });
        }
    }
}
