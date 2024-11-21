using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Dto.Responses;
using HotelManagementSystem.Interfaces.Entities;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IUserService
    {
        public Task AssignRoleAsync(AssignRoleRequest request);

        public Task<User> GetUser(string userId);

        public Task<IEnumerable<string>> GetUserRoles(string userId);

        public Task<LoginResponse> LoginAsync(LoginRequest request);

        public Task LogoutAsync(string refreshToken);

        public Task<RefreshTokensResponse> RefreshTokens(string refreshToken);

        public Task RegisterAsync(RegisterRequest request);
    }
}
