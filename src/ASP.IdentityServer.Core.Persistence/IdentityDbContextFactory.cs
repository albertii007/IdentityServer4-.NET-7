using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ASP.IdentityServer.Core.Persistence
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityCustomDbContext>
    {
        public IdentityCustomDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            var connectionString = configuration.GetConnectionString("AuthDatabase");
            var storeOptions = new ConfigurationStoreOptions();

            builder.UseSqlServer(connectionString);

            return new IdentityCustomDbContext(builder.Options, storeOptions);
        }
    }
}
