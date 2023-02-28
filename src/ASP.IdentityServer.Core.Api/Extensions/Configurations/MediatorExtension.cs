using ASP.IdentityServer.Core.Application.Infrastructure.Mediator;
using ASP.IdentityServer.Core.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.IdentityServer.Core.Api.Extensions.Configurations
{
    public static class MediatorExtension
    {
        public static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(IAuthenticationDbContext).Assembly);
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            });
        }
    }
}
