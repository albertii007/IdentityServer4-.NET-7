using MediatR;

namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Login
{
    public class LoginCommand : IRequest<LoginModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
    }
}
