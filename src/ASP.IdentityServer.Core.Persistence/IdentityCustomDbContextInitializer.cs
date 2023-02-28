using ASP.IdentityServer.Core.Persistence.Seeds;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;

namespace ASP.IdentityServer.Core.Persistence
{
    public class IdentityCustomDbContextInitializer
    {
        public static void Initialize(ConfigurationDbContext context)
        {
            new IdentityCustomDbContextInitializer()
                .Seed(context);
        }

        private void Seed(ConfigurationDbContext context)
        {
            context.Database.EnsureCreated();

            SeedApiScopes(context);
            SeedApiResources(context);
            SeedIdentityResources(context);
            SeedClients(context);
        }

        private void SeedClients(ConfigurationDbContext context)
        {
            if (context.Clients.Any()) return;

            var clients = IdentityConfig.Clients.Select(x => x.ToEntity()).ToList();

            context.Clients.AddRange(clients);

            context.SaveChanges();
        }

        private void SeedIdentityResources(ConfigurationDbContext context)
        {
            if (context.IdentityResources.Any()) return;

            var identityResources = IdentityConfig.IdentityResources.Select(x => x.ToEntity()).ToList();

            context.IdentityResources.AddRange(identityResources);

            context.SaveChanges();
        }

        private void SeedApiScopes(ConfigurationDbContext context)
        {
            if (context.ApiScopes.Any()) return;

            var apiScopes = IdentityConfig.ApiScopes.Select(x => x.ToEntity()).ToList();

            context.ApiScopes.AddRange(apiScopes);

            context.SaveChanges();
        }

        private void SeedApiResources(ConfigurationDbContext context)
        {
            if (context.ApiResources.Any()) return;

            var apiScopes = IdentityConfig.ApiResources.Select(x => x.ToEntity()).ToList();

            context.ApiResources.AddRange(apiScopes);

            context.SaveChanges();
        }
    }
}
