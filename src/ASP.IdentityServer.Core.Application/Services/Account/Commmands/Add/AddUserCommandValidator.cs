using ASP.IdentityServer.Core.Application.Interfaces;
using ASP.IdentityServer.Core.Common.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace ASP.IdentityServer.Core.Application.Services.Account.Commmands.Add
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IAuthenticationDbContext _context;

        public AddUserCommandValidator(IAuthenticationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private void Validations()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Email")).DependentRules(() =>
            {
                RuleFor(x => x.Email).Matches(ValidatorRegex.Email).WithMessage(ValidatorMessages.FormatNotMatch("Email")).DependentRules(() =>
                {
                    RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
                    {
                        return !await _context.Users.AsNoTracking().IgnoreQueryFilters().AnyAsync(x => x.Email.ToLower() == email.ToLower(), cancellation);
                    }).WithMessage(x => ValidatorMessages.AlreadyExists(x.Email));
                });
            });

            RuleFor(x => x.UserName).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("UserName")).DependentRules(() =>
            {
                RuleFor(x => x.UserName).Matches(ValidatorRegex.UserName).WithMessage(ValidatorMessages.FormatNotMatch("UserName")).DependentRules(() =>
                {
                    RuleFor(x => x.UserName).MustAsync(async (UserName, cancellation) =>
                    {
                        return !await _context.Users.AsNoTracking().IgnoreQueryFilters().AnyAsync(x => x.UserName.ToLower() == UserName.ToLower(), cancellation);
                    }).WithMessage(x => ValidatorMessages.AlreadyExists(x.UserName));
                });
            });

            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorMessages.NotEmpty("Password")).DependentRules(() =>
            {
                RuleFor(x => x.Password).MinimumLength(ValidatorConditions.PasswordMinLength).WithMessage(ValidatorMessages.MinLength("Password")).DependentRules(() =>
                {
                    RuleFor(x => x.Password).Matches(ValidatorRegex.Password).WithMessage(ValidatorMessages.FormatNotMatch("Password"));
                });
            });
        }
    }
}
