using HotelManagementSystem.Interfaces.Dto.Requests;
using HotelManagementSystem.Interfaces.Entities;
using HotelManagementSystem.Interfaces.Exceptions;
using HotelManagementSystem.Services.Repositories;
using HotelManagementSystem.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelManagementSystem.Services
{
    public class SessionService(HotelScope hotelScope, IConfiguration configuration) : ISessionService
    {
        private readonly HotelScope _hotelScope = hotelScope;
        private readonly int _sessionExpirationDays = configuration.GetValue<int>("SessionExpirationDays");

        public async Task CreateAsync(CreateSessionRequest request)
        {
            _hotelScope.DbContext.Sessions.Add(new Session
            {
                Id = request.SessionId,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(_sessionExpirationDays),
                InitiatedAt = DateTimeOffset.UtcNow,
                RefreshToken = request.RefreshToken.ToSha256(),
                UserId = request.UserId
            });

            await _hotelScope.DbContext.SaveChangesAsync();
        }

        public async Task ExtendAsync(ExtendSessionRequest request)
        {
            var session = await _hotelScope.DbContext.Sessions
                .Where(s => s.Id == request.SessionId)
                .SingleOrDefaultAsync();

            if (session is null)
            {
                throw new NotFoundException("Session not found.");
            }

            session.ExpiresAt = DateTimeOffset.UtcNow.AddDays(3);
            session.RefreshToken = request.RefreshToken;

            await _hotelScope.DbContext.SaveChangesAsync();
        }

        public async Task RevokeAsync(Guid sessionId)
        {
            var session = await _hotelScope.DbContext.Sessions
                .Where(s => s.Id == sessionId)
                .SingleOrDefaultAsync();

            if (session is null)
            {
                throw new NotFoundException("Session not found.");
            }

            session.Revoked = true;

            await _hotelScope.DbContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateAsync(Guid sessionId, string refreshToken)
        {
            var session = await _hotelScope.DbContext.Sessions
                .Where(s => s.Id == sessionId)
                .SingleOrDefaultAsync();

            if (session is null)
            {
                return false;
            }

            return session is not null &&
                string.Equals(session.RefreshToken, refreshToken.ToSha256()) &&
                !session.Revoked &&
                session.ExpiresAt > DateTimeOffset.UtcNow;
        }
    }
}
