using ASP.IdentityServer.Core.Application.Interfaces;
using ASP.IdentityServer.Core.Domain.Entities;
using ASP.IdentityServer.Core.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Add
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public AddUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserToEntity();

            await _userManager.CreateAsync(user, request.Password);

            await CreateUserClaims(user);

            return Unit.Value;
        }

        private async Task CreateUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(UserClaimConstants.Email, user.Email ?? string.Empty),
                new Claim(UserClaimConstants.UserName, user.UserName ?? string.Empty)
            };

            await _userManager.AddClaimsAsync(user, claims);
        }
    }
}
