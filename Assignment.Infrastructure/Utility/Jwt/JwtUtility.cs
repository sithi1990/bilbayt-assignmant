using Assignment.Infrastructure.Utility.Jwt.Contracts;
using Assignment.Infrastructure.Utility.Jwt.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Utility.Jwt
{
    public class JwtUtility : IJwtUtility
    {
        private readonly JwtUtilityOptions _options;

        public JwtUtility(JwtUtilityOptions options)
        {
            _options = options;
        }

        public Task<(string, DateTime)> CreateTokenAsync(string aud, IEnumerable<Claim> claims = null, int expiresInMinutes = 60)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(expiresInMinutes);
            var token = new JwtSecurityToken(_options.Issuer,
              aud,
              claims,
              expires: expiration,
              signingCredentials: credentials);

            var tokenString =  new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult((tokenString, expiration));
        }
    }
}
