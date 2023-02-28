using ASP.IdentityServer.Core.Domain.Entities;
using ASP.IdentityServer.Core.Persistence;
using ASP.IdentityServer.Core.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ASP.IdentityServer.Core.Api.Extensions.Configurations
{
    public static class IdentityServerExtension
    {
        public static void AddIdentityServer4(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(IdentityCustomDbContext).GetTypeInfo().Assembly.GetName().Name;

            var connectionStringAuth = configuration.GetConnectionString("Database");

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddOperationalStore(options =>
                //    options.ConfigureDbContext = (builder) =>
                //        builder.UseSqlServer(connectionStringAuth,
                //            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                //.AddConfigurationStore(options =>
                //    options.ConfigureDbContext = (builder) =>
                //        builder.UseSqlServer(connectionStringAuth,
                //            sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                .AddInMemoryApiResources(IdentityConfig.ApiResources)
                .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
                .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
                .AddInMemoryClients(IdentityConfig.Clients)
                .AddAspNetIdentity<User>();
        }
    }
}
