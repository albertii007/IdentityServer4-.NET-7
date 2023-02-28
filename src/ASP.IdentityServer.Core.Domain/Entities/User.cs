using Microsoft.AspNetCore.Identity;
using System;

namespace ASP.IdentityServer.Core.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public bool IsDeleted { get; set; }
    }
}
