using Microsoft.AspNetCore.Identity;
using System;

namespace ASP.IdentityServer.Core.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
