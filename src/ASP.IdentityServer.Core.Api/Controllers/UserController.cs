using ASP.IdentityServer.Core.Api.Controllers.Base;
using ASP.IdentityServer.Core.Application.Services.Account.Commmands.Add;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.IdentityServer.Core.Api.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        [HttpPost(":add")]
        public async Task<IActionResult> Login([FromBody] AddUserCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> TestAuthorize()
        {
            await Task.Delay(100);
            return Ok("ok");
        }
    }
}
