using ASP.IdentityServer.Core.Domain.Entities;
using MediatR;

namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Add
{
    public class AddUserCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public User UserToEntity() => new()
        {
            Email = Email,
            UserName = UserName,
            PhoneNumber = PhoneNumber,
        };
    }
}
