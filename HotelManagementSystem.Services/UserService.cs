using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Dto.Responses;
using HotelManagementSystem.Interfaces.Entities;
using HotelManagementSystem.Interfaces.Exceptions;
using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelManagementSystem.Services
{
    public class UserService(HotelScope hotelScope, IJwtTokenService jwtTokenService, UserManager<User> userManager) : IUserService
    {
        private readonly HotelScope _hotelScope = hotelScope;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null)
            {
                throw new NotFoundException($"User {request.UserName} does not exist.");
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new ValidationException("Invalid username or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new LoginResponse
            {
                AccessToken = _jwtTokenService.CreateAccessToken(user.UserName!, user.Id, roles),
                RefreshToken = _jwtTokenService.CreateRefreshToken(user.Id)
            };
        }

        public async Task<RefreshTokensResponse> RefreshTokens(string refreshToken)
        {
            if (!_jwtTokenService.TryParseRefreshToken(refreshToken, out var claimsPrincipal) || claimsPrincipal is null)
            {
                throw new ValidationException("Invalid refresh token.");
            }

            var userId = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (string.IsNullOrEmpty(userId))
            {
                throw new ValidationException("Invalid refresh token.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new RefreshTokensResponse
            {
                AccessToken = _jwtTokenService.CreateAccessToken(user.UserName!, user.Id, roles),
                RefreshToken = _jwtTokenService.CreateRefreshToken(user.Id)
            };
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is not null)
            {
                throw new ValidationException($"User {request.UserName} already exists.");
            }

            user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                Name = request.Name,
                Surname = request.Surname,
            };

            using var transaction = await _hotelScope.DbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new ValidationException(result.Errors.Select(e => e.Description).First());
                }

                await _userManager.AddToRoleAsync(user, Roles.User);

                await transaction.CommitAsync();
            }
            catch (ValidationException ex)
            {
                await transaction.RollbackAsync();

                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw new Exception(ex.Message);
            }
        }
    }
}
