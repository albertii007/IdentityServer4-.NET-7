using ASP.IdentityServer.Core.Application.Interfaces;
using ASP.IdentityServer.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP.IdentityServer.Core.Persistence
{
    public class AuthenticationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IAuthenticationDbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var table = entityType.GetTableName();
                if (table.StartsWith("AspNet"))
                    entityType.SetTableName(table.Substring(6));
            }

            builder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);

            builder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
        }
    }
}
