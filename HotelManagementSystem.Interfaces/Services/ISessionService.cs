using HotelManagementSystem.Interfaces.Dto.Requests.Authentication;

namespace HotelManagementSystem.Services
{
    public interface ISessionService
    {
        Task CreateAsync(CreateSessionRequest request);

        Task ExtendAsync(ExtendSessionRequest request);
        
        Task RevokeAsync(Guid sessionId);
        
        Task<bool> ValidateAsync(Guid sessionId, string refreshToken);
    }
}