using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Utility.Jwt.Contracts
{
    public interface IJwtUtility
    {
        Task<(string, DateTime)> CreateTokenAsync(string aud, IEnumerable<Claim> claims, int expiresInMinutes);
    }
}
