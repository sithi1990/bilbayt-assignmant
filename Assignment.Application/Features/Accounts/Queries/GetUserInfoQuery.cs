using Assignment.Application.Exceptions;
using Assignment.Application.Features.Accounts.Queries;
using Assignment.Infrastructure.Data.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Application.Features.Accounts.Commands
{
    public class GetUserInfoQuery : IRequest<UserInfoResult>
    {
        public string UserName { get; set; }

        public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserInfoResult>
        {
            private readonly IAccountsDbService _accountsDbService;

            public GetUserInfoQueryHandler(IAccountsDbService accountsDbService)
            {
                _accountsDbService = accountsDbService;
            }

            public async Task<UserInfoResult> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
            {
                var user = await _accountsDbService.GetUserAsync(request.UserName);

                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                return new UserInfoResult
                {
                    FullName = user.FullName,
                    UserId = user.UserId,
                    UserName = user.UserName
                };
            }

        }
    }

}
