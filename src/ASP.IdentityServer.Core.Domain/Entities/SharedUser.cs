using ASP.IdentityServer.Core.Domain.Entities.Base;
using System;

namespace ASP.IdentityServer.Core.Domain.Entities
{
    public class SharedUser : Entity<Guid>
    {
        public string DatabaseName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }
    }
}
