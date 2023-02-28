using ASP.IdentityServer.Core.Application.Infrastructure.Mediator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.IdentityServer.Core.Api.Extensions.Configurations
{
    public static class ValidationExtension
    {
        public static void AddFluentValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(typeof(RequestPerformanceBehavior<,>));
        }
    }
}
