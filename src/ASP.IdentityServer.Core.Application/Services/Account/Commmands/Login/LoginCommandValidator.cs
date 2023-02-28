using ASP.IdentityServer.Core.Application.Interfaces;
using ASP.IdentityServer.Core.Common.Constants;
using FluentValidation;
using System;
using System.Linq;

namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly IAuthenticationDbContext _context;

        public LoginCommandValidator(IAuthenticationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Email"))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email).Must((email) =>
                    {
                        if (ValidatorRegex.MatchRegex(ValidatorRegex.Email, email))
                            return _context.Users.Any(x => x.Email.ToUpper() == email.ToUpper());
                        else
                            return _context.Users.Any(x => x.UserName.ToUpper() == email.ToUpper());
                    }).WithMessage(x => ValidatorMessages.NotFound(x.Email));
                });

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Password"));
            RuleFor(x => x.GrantType).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("GrantType"));
            RuleFor(x => x.ClientId).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("ClientId"));
            RuleFor(x => x.ClientSecret).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("ClientSecret"));
            RuleFor(x => x.Scope).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Scope"));
        }
    }
}
