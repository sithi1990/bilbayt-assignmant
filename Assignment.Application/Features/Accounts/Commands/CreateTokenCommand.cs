using Assignment.Application.Exceptions;
using Assignment.Infrastructure.Data.Contracts;
using Assignment.Infrastructure.Utility.Extensions;
using Assignment.Infrastructure.Utility.Jwt.Contracts;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Application.Features.Accounts.Commands
{
    public class CreateTokenCommandOptions
    {
        public string Audience { get; set; }
        public int ExpireInMinutes { get; set; }
    }

    public class TokenResult
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class CreateTokenCommand : IRequest<TokenResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, TokenResult>
        {
            private readonly IAccountsDbService _accountsDbService;
            private readonly IJwtUtility _jwtUtility;
            private readonly IOptions<CreateTokenCommandOptions> _options;

            public CreateTokenCommandHandler(IAccountsDbService accountsDbService, IJwtUtility jwtUtility, IOptions<CreateTokenCommandOptions> options)
            {
                _accountsDbService = accountsDbService;
                _jwtUtility = jwtUtility;
                _options = options;
            }

            public async Task<TokenResult> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
            {
                var user = await _accountsDbService.GetUserAsync(request.UserName);

                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                var currentPasswordHash = request.Password.CreateHash();

                if (currentPasswordHash != user.PasswordHashed)
                {
                    throw new InvalidPasswordException();
                }

                var subClaim = new Claim(ClaimTypes.NameIdentifier, user.UserId);
                var userNameClaim = new Claim(ClaimTypes.Name, user.UserName);

                var (token, expiration) = await _jwtUtility.CreateTokenAsync(_options.Value.Audience, new Claim[] { subClaim, userNameClaim }, _options.Value.ExpireInMinutes);
                return new TokenResult { AccessToken = token, Expiration = expiration };
            }

        }
    }
}
