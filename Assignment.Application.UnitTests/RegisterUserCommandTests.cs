using Assignment.Application.Features.Accounts.Commands;
using Assignment.Domain.Models;
using Assignment.Infrastructure.Data.Contracts;
using Assignment.Infrastructure.Utility.Notification.Contacts;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;
using Assignment.Infrastructure.Utility.Extensions;
using static Assignment.Application.Features.Accounts.Commands.RegisterUserCommand;
using System.Threading.Tasks;
using Assignment.Infrastructure.Data.Exceptions;

namespace Assignment.Application.UnitTests
{
    public class RegisterUserCommandTests
    {

        private readonly AutoMocker _mocker;
        private readonly Mock<IAccountsDbService> _accountsDbServiceMock;
        private readonly Mock<IEmailNotificationUtility> _emailNotificationUtilityMock;
        private readonly IOptions<ActivationMailOptions> _options;


        public RegisterUserCommandTests()
        {
            _mocker = new AutoMocker();
            _accountsDbServiceMock = new Mock<IAccountsDbService>();
            _emailNotificationUtilityMock = new Mock<IEmailNotificationUtility>();
            _options = Options.Create(new ActivationMailOptions() { ActivationLink = "https://localhost:8080", TemplateId = "111" });

            _mocker.Use(_accountsDbServiceMock.Object);
            _mocker.Use(_emailNotificationUtilityMock.Object);
            _mocker.Use(_options);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task TestRegisterUserCommandHandler(RegisterUserCommand request)
        {
            var hashedPassword = request.Password.CreateHash();

            _accountsDbServiceMock.Setup(c => c.CreateUserAsync(It.Is<AppUser>(x => x.FullName == request.FullName && x.PasswordHashed == hashedPassword && x.UserName == request.UserName))).Verifiable();
            _emailNotificationUtilityMock.Setup(c => c.SendTemplateMessage(
                It.Is<string>(x => x == _options.Value.TemplateId),
                It.Is<string>(x => x == request.UserName),
                It.Is<RegisterUserMailParameters>(x => x.ActivationLink.Contains(_options.Value.ActivationLink) && x.Name == request.FullName))).Verifiable();

            var instance = _mocker.CreateInstance<RegisterUserCommandHandler>();

            var result = await instance.Handle(request, new System.Threading.CancellationToken());

            _accountsDbServiceMock.Verify();
            _emailNotificationUtilityMock.Verify();
            Assert.True(result);

        }

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] {
                new RegisterUserCommand { FullName = "fullname", Password = "password", UserName = "username" }
            };
        }
    }
}
