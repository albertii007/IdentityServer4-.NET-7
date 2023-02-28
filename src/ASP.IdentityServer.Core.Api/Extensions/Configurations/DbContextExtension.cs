using ASP.IdentityServer.Core.Application.Interfaces;
using ASP.IdentityServer.Core.Persistence;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ASP.IdentityServer.Core.Api.Extensions.Configurations
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IAuthenticationDbContext, AuthenticationDbContext>();
        }

        public static void MigrateDbContext(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var configurationContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            var persistedContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            var authencticationDbContext = scope.ServiceProvider.GetService<AuthenticationDbContext>();

            configurationContext.Database.Migrate();
            persistedContext.Database.Migrate();
            authencticationDbContext.Database.Migrate();

            IdentityCustomDbContextInitializer.Initialize(configurationContext);
        }
    }
}