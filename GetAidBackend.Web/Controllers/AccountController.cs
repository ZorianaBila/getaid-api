using GetAidBackend.Auth.Requests;
using GetAidBackend.Auth.Responses;
using GetAidBackend.Auth.Services.Abstractions;
using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetAidBackend.Web.Controllers
{
    [Route("api/account")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(
            IAccountService accountService,
            IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var loginResponse = await _accountService.Login(request);
            return Ok(loginResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Registration(RegisterRequest request)
        {
            var loginResponse = await _accountService.Registration(request);
            return Ok(loginResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var loginResponse = await _accountService.RefreshToken(request);
            return Ok(loginResponse);
        }

        [HttpPut("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            await _accountService.ChangePassword(request);
            return Ok();
        }

        [HttpPut("private-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> ChangePrivateData(UserPrivateData userPrivateData)
        {
            var userId = HttpContext.User.FindFirst("UserId").Value;
            var user = await _userService.UpdatePrivateData(userId, userPrivateData);
            return Ok(user);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAccount()
        {
            var userId = HttpContext.User.FindFirst("UserId").Value;
            await _userService.DeleteById(userId);
            return Ok();
        }
    }
}