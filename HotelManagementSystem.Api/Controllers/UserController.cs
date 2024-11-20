using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Users")]
    public class UserController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

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

        [HttpPost]
        [Route("Refresh")]
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
                HttpOnly = true,
                Secure = false, // TODO: Set to true in production
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            HttpContext.Response.Cookies.Append(CookieNames.RefreshToken, refreshToken, cookieOptions);
        }
    }
}
