using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ASP.IdentityServer.Core.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
