using Assignment.Infrastructure.Utility.Jwt.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Infrastructure.Utility.Jwt.Extensions
{
    public class JwtUtilityOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
    }

    public static class JwtUtilityExtensions
    {
        public static IServiceCollection AddJwtUtilities(this IServiceCollection services, JwtUtilityOptions options)
        {
            return services.AddSingleton<IJwtUtility>(new JwtUtility(options));
        }
    }
}
