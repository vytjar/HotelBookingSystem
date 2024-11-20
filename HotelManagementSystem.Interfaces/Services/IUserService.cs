using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Dto.Responses;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IUserService
    {
        public Task<RefreshTokensResponse> RefreshTokens(string refreshToken);

        public Task<LoginResponse> LoginAsync(LoginRequest request);

        public Task RegisterAsync(RegisterRequest request);
    }
}
