
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ASP.IdentityServer.Core.Api.Controllers.Base;
using ASP.IdentityServer.Core.Application.Services.Account.Commmands.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace ASP.IdentityServer.Core.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        [HttpPost(":login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost(":logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");

            return Ok();
        }
    }
}
