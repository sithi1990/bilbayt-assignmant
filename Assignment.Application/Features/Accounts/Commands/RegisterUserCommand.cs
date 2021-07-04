using Assignment.Application.Exceptions;
using Assignment.Domain.Models;
using Assignment.Infrastructure.Data.Contracts;
using Assignment.Infrastructure.Utility.Extensions;
using Assignment.Infrastructure.Utility.Notification.Contacts;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment.Application.Features.Accounts.Commands
{
    public class ActivationMailOptions
    {
        public string TemplateId { get; set; }
        public string ActivationLink { get; set; }
    }

    public class RegisterUserMailParameters
    {
        public string Name { get; set; }
        public string ActivationLink { get; set; }
    }


    public class RegisterUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
        {
            private readonly IAccountsDbService _accountsDbService;
            private readonly IEmailNotificationUtility _emailNotificationUtility;
            private readonly IOptions<ActivationMailOptions> _options;

            public RegisterUserCommandHandler(IAccountsDbService accountsDbService, IEmailNotificationUtility emailNotificationUtility, IOptions<ActivationMailOptions> options)
            {
                _accountsDbService = accountsDbService;
                _emailNotificationUtility = emailNotificationUtility;
                _options = options;
            }

            public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                var user = new AppUser { 
                    UserName = request.UserName, 
                    UserId = Guid.NewGuid().ToString(), 
                    PasswordHashed = request.Password.CreateHash(),
                    FullName = request.FullName
                };
                await _accountsDbService.CreateUserAsync(user);
                var activationCode = Guid.NewGuid();

                await _emailNotificationUtility.SendTemplateMessage(_options.Value.TemplateId, user.UserName, new RegisterUserMailParameters
                {
                    ActivationLink = $"{_options.Value.ActivationLink}?code={activationCode}",
                    Name = user.FullName
                });
                return true;

            }
        }
    }
}
