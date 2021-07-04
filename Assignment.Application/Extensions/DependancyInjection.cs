using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Assignment.Application.Common.Behaviours;

namespace Assignment.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            return services;
        }
    }
}
