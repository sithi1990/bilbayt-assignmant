using System.Threading.Tasks;
using Assignment.Application.Features.Accounts.Commands;
using Assignment.Application.Features.Accounts.Queries;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Web.Controllers
{
    public class AccountsController : ApiController
    {

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ResponseMetadata))]
        [ProducesResponseType(400, Type = typeof(ResponseMetadata))]
        public async Task<ActionResult<ResponseMetadata>> Register([FromBody] RegisterUserCommand requestCommand)
        {
            await Mediator.Send(requestCommand);
            return Ok(new ResponseMetadata());

        }

        [HttpPost("token")]
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        [ProducesResponseType(400, Type = typeof(ResponseMetadata))]
        public async Task<ActionResult<TokenResponse>> CreateAndGetAccessToken([FromBody] CreateTokenCommand requestCommand)
        {

            var result = await Mediator.Send(requestCommand);
            return Ok(new TokenResponse { AccessToken = result.AccessToken, Expiration = result.Expiration });

        }

        [Authorize]
        [HttpGet("user-info")]
        [ProducesResponseType(200, Type = typeof(UserInfoResponse))]
        [ProducesResponseType(400, Type = typeof(ResponseMetadata))]
        public async Task<ActionResult<UserInfoResponse>> GetUserInfo()
        {
            var userName = HttpContext.User.Identity.Name;
            var result = await Mediator.Send(new GetUserInfoQuery {  UserName = userName });
            return Ok(new UserInfoResponse { UserInfo = result });
        }

    }
}
