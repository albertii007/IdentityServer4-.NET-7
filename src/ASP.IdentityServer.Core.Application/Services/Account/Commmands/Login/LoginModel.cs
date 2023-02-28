namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Login
{
    public class LoginModel
    {
        public string AccessToken { get; set; }
        public string ExpiredIn { get; set; }
        public string Schema { get; set; }
        public string RefreshToken { get; set; }
    }
}