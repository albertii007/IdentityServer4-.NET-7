using ASP.IdentityServer.Core.Application.Interfaces;
using ASP.IdentityServer.Core.Common.Constants;
using ASP.IdentityServer.Core.Common.Exceptions;
using ASP.IdentityServer.Core.Common.Extensions;
using ASP.IdentityServer.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginModel>
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationDbContext _context;
        public LoginCommandHandler(IHttpClientFactory httpClient, IConfiguration configuration, IAuthenticationDbContext context)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LoginModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User user = ValidatorRegex.MatchRegex(ValidatorRegex.Email, request.Email)
                ? await _context.Users.FirstOrDefaultAsync(x => x.Email.ToUpper() == request.Email.ToUpper())
                : await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToUpper() == request.Email.ToUpper());

            IDictionary<string, string> command = new Dictionary<string, string>()
            {
                {"grant_type", request.GrantType },
                {"client_id", request.ClientId },
                {"client_secret", request.ClientSecret },
                {"scope", request.Scope },
                {"username", user.UserName },
                {"password", request.Password }
            };

            using var client = _httpClient.CreateClient();

            using HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, _configuration["Endpoints:Service"] + "/connect/token")
            {
                Content = new FormUrlEncodedContent(command)
            };

            using var requestApi = await client.SendAsync(message);

            if (!requestApi.IsSuccessStatusCode)
                throw new OnLoginFailureException();

            var response = await Utilities.GetResponseContent<IDictionary<string, string>>(requestApi);

            return new LoginModel
            {
                AccessToken = response["access_token"],
                Schema = response["token_type"],
                ExpiredIn = response["expires_in"],
                RefreshToken = response.ContainsKey("refresh_token")
                    ? response["refresh_token"]
                    : default
            };
        }
    }
}
