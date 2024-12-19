using HotelManagementSystem.Interfaces.Dto.Requests.Authentication;
using HotelManagementSystem.Interfaces.Dto.Responses;
using HotelManagementSystem.Interfaces.Entities;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IUserService
    {
        Task AssignRolesAsync(AssignRolesRequest request);

        Task<IEnumerable<User>> FilterAsync(FilterUsersRequest request);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetAsync(string userId);

        Task<IEnumerable<string>> GetUserRolesAsync(string userId);

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task LogoutAsync(string refreshToken);

        Task<RefreshTokensResponse> RefreshTokens(string refreshToken);

        Task RegisterAsync(RegisterRequest request);
    }
}
