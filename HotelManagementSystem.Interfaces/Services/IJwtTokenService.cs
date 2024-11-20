using System.Security.Claims;

namespace HotelManagementSystem.Services
{
    public interface IJwtTokenService
    {
        string CreateAccessToken(string userName, string userId, IEnumerable<string> roles);

        string CreateRefreshToken(string userId);

        bool TryParseRefreshToken(string refreshToken, out ClaimsPrincipal? claimsPrincipal);
    }
}