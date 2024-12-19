using AutoMapper;
using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Requests.Authentication;
using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HotelManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("Api/Users")]
    public class UserController(
        IConfiguration configuration,
        IJwtTokenService jwtTokenService,
        IMapper mapper,
        IUserService userService
    ) : Controller
    {
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        private readonly int _refreshTokenExpirationDays = configuration.GetValue<int>(Jwt.RefreshTokenExpirationDays);

        /// <summary>
        /// Assigns a specified role to a user.
        /// </summary>
        /// <param name="request">Role assignment request</param>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [Route("AssignRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRoles(AssignRolesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AssignRolesAsync(request);

            return Ok();
        }

        /// <summary>
        /// Filters users based on the provided request.
        /// </summary>
        /// <param name="request">Filtering request.</param>
        /// <returns>A list of users.</returns>
        [HttpPost]
        [Route("Filter")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Filter(FilterUsersRequest request)
        {
            var users = (await _userService
                .FilterAsync(request))
                .Select(_mapper.Map<UserInfo>)
                .ToList();

            return Ok(users);
        }

        /// <summary>
        /// Gets the currently logged in user.
        /// </summary>
        /// <returns>Currently logged in user.</returns>
        [HttpGet]
        [Authorize(Roles = Roles.User)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            HttpContext.Request.Cookies.TryGetValue(CookieNames.RefreshToken, out var refreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            if (_jwtTokenService.TryParseRefreshToken(refreshToken, out var claimsPrincipal) && claimsPrincipal is not null)
            {
                if (claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value is string userId)
                {
                    return Ok(_mapper.Map<UserInfo>(await _userService.GetAsync(userId)));
                }
            }

            return BadRequest();
        }


        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var users = (await _userService
                .GetAllAsync())
                .Select(_mapper.Map<UserInfo>)
                .ToList();

            return Ok(users);
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// 
        /// <param name="userId">User id.</param>
        /// <returns>User.</returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string userId)
        {
            return Ok(_mapper.Map<UserInfo>(await _userService.GetAsync(userId)));
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
                Expires = DateTimeOffset.UtcNow.AddDays(_refreshTokenExpirationDays),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };

            HttpContext.Response.Cookies.Append(CookieNames.RefreshToken, refreshToken, cookieOptions);
        }
    }
}
