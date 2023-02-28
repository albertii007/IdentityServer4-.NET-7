using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace ASP.IdentityServer.Core.Persistence
{
    public class IdentityCustomDbContext : ConfigurationDbContext
    {
        public IdentityCustomDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
    }
}
