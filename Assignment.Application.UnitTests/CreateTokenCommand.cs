using Assignment.Application.Features.Accounts.Commands;
using Assignment.Domain.Models;
using Assignment.Infrastructure.Data.Contracts;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using Xunit;
using Assignment.Infrastructure.Utility.Extensions;
using System.Threading.Tasks;
using Assignment.Infrastructure.Utility.Jwt.Contracts;
using static Assignment.Application.Features.Accounts.Commands.CreateTokenCommand;
using System.Security.Claims;
using Assignment.Application.Exceptions;

namespace Assignment.Application.UnitTests
{
    public class CreateTokenCommandTests
    {

        private readonly AutoMocker _mocker;
        private readonly Mock<IAccountsDbService> _accountsDbServiceMock;
        private readonly Mock<IJwtUtility> _jwtUtility;
        private readonly IOptions<CreateTokenCommandOptions> _options;

        public CreateTokenCommandTests()
        {
            _mocker = new AutoMocker();
            _accountsDbServiceMock = new Mock<IAccountsDbService>();
            _jwtUtility = new Mock<IJwtUtility>();
            _options = Options.Create(new CreateTokenCommandOptions() { Audience = "audience" });

            _mocker.Use(_accountsDbServiceMock.Object);
            _mocker.Use(_jwtUtility.Object);
            _mocker.Use(_options);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task TestRegisterUserCommandHandler(CreateTokenCommand command, bool hasValidPassword, string hashedPassword, bool userExists)
        {
            var user = new AppUser { UserName = command.UserName, FullName = "fullname", PasswordHashed = hashedPassword, UserId = "1" }; 

            if(userExists)
            {
                _accountsDbServiceMock.Setup(c => c.GetUserAsync(It.Is<string>(x => x == command.UserName))).Returns(Task.FromResult(user)).Verifiable();
                
            }
            else
            {
                _accountsDbServiceMock.Setup(c => c.GetUserAsync(It.Is<string>(x => x == command.UserName))).Returns(Task.FromResult(default(AppUser))).Verifiable();
            }

            _jwtUtility.Setup(c => c.CreateTokenAsync(It.IsAny<string>(), It.IsAny<Claim[]>(), It.IsAny<int>())).Returns(Task.FromResult(("token", DateTime.Now)));

            var instance = _mocker.CreateInstance<CreateTokenCommandHandler>();

            if(hasValidPassword && userExists)
            {
                var result = await instance.Handle(command, new System.Threading.CancellationToken());
                Assert.NotNull(result);
            }
            else
            {
                try
                {
                    await instance.Handle(command, new System.Threading.CancellationToken());
                }
                catch (Exception ex)
                {
                    Assert.True(ex.GetType() == typeof(UserNotFoundException) || ex.GetType() == typeof(InvalidPasswordException));
                }
            }

            _accountsDbServiceMock.Verify();

        }

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] {
                new CreateTokenCommand {  UserName = "username", Password = "password" }, true, "password".CreateHash(), true
            }; 

            yield return new object[] {
                new CreateTokenCommand {  UserName = "username", Password = "password" }, false, "password", true
            };
            yield return new object[] {
                new CreateTokenCommand {  UserName = "username", Password = "password" }, true, "password".CreateHash(), false
            };

        }
    }
}
