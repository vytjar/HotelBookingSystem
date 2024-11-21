using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Users")]
    public class UserController(IConfiguration configuration, IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly int _refreshTokenExpirationDays = configuration.GetValue<int>(Jwt.RefreshTokenExpirationDays);

        /// <summary>
        /// Assigns a specified role to a user.
        /// </summary>
        /// <param name="request">Role assignment request</param>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [Route("AssignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRole(AssignRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AssignRoleAsync(request);

            return Ok();
        }

        /// <summary>
        /// Log in a user.
        /// </summary>
        /// <param name="request">Log in request.</param>
        /// <returns>Access token.</returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.LoginAsync(request);

            UpdateCookieRefreshToken(response.RefreshToken);

            return Ok(new AccessToken
            {
                Token = response.AccessToken
            });
        }

        /// <summary>
        /// Logs a user out
        /// </summary>
        [HttpPost]
        [Route("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Request.Cookies.TryGetValue(CookieNames.RefreshToken, out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            await _userService.LogoutAsync(refreshToken);

            HttpContext.Response.Cookies.Delete(CookieNames.RefreshToken);

            return Ok();
        }

        /// <summary>
        /// Refreshes users tokens
        /// </summary>
        /// <returns>Access token</returns>
        [HttpPost]
        [Route("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Refresh()
        {
            if (!HttpContext.Request.Cookies.TryGetValue(CookieNames.RefreshToken, out var refreshToken))
            {
                return Unauthorized();
            }

            var response = await _userService.RefreshTokens(refreshToken);

            UpdateCookieRefreshToken(response.RefreshToken);

            return Ok(new AccessToken
            {
                Token = response.AccessToken
            });
        }

        /// <summary>
        /// Registers a new user with the "User" role
        /// </summary>
        /// <param name="request">Registration request</param>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.RegisterAsync(request);

            return Created();
        }

        private void UpdateCookieRefreshToken(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(_refreshTokenExpirationDays),
                HttpOnly = true,
                Secure = false, // TODO: Set to true in production
                SameSite = SameSiteMode.Lax,
            };

            HttpContext.Response.Cookies.Append(CookieNames.RefreshToken, refreshToken, cookieOptions);
        }
    }
}
