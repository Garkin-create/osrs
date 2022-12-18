using OSRS.Api.Controllers.v1.Users.Requests;
using OSRS.ApiFramework.Tools;
using OSRS.Application.Users.Command.CreateUser;
using OSRS.Application.Users.Command.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace OSRS.Api.Controllers.v1.Users
{
    [ApiVersion("1")]
    public class UserController : BaseController
    {
        [HttpPost("signup")]
        [SwaggerOperation("sign up user")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<bool>> SingUpAsync(SingUpRequest request, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<SingUpRequest, CreateUserCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);
            return new ApiResult<bool>(result);
        }

        [HttpPost("login")]
        [SwaggerOperation("login by username and password")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<LoginResponse>> LoginAsync([FromForm] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<LoginRequest, LoginCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);
            return new ApiResult<LoginResponse>(result);
        }
    }
}
