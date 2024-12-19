using HotelManagementSystem.Interfaces.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementSystem.Services
{
    public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
    {
        private readonly int _accessTokenExpirationMinutes = configuration.GetValue<int>(Jwt.AccessTokenExpirationMinutes);
        private readonly string? _audience = configuration[Jwt.ValidAudience];
        private readonly SymmetricSecurityKey _authSigninKey = new(Encoding.UTF8.GetBytes(configuration[Jwt.Secret]!));
        private readonly string? _issuer = configuration[Jwt.ValidIssuer];
        private readonly int _refreshTokenExpirationDays = configuration.GetValue<int>(Jwt.RefreshTokenExpirationDays);

        public string CreateAccessToken(string userName, string userId, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Name, userName),
                new(JwtRegisteredClaimNames.Sub, userId)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
                issuer: _issuer,
                signingCredentials: new SigningCredentials(_authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken(Guid sessionId, string userId)
        {
            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimNames.SessionId, sessionId.ToString()),
                new(JwtRegisteredClaimNames.Sub, userId)
            };

            var token = new JwtSecurityToken
            (
                audience: _audience,
                expires: DateTime.UtcNow.AddDays(_refreshTokenExpirationDays),
                claims: claims,
                issuer: _issuer,
                signingCredentials: new SigningCredentials(_authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool TryParseRefreshToken(string refreshToken, out ClaimsPrincipal? claimsPrincipal)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler() { MapInboundClaims = false };

                claimsPrincipal = tokenHandler.ValidateToken(
                    refreshToken,
                    new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = _audience,
                        ValidIssuer = _issuer,
                        IssuerSigningKey = _authSigninKey
                    },
                    out _
                );

                return true;
            }
            catch (Exception e)
            {
                claimsPrincipal = null;

                Console.WriteLine(e);

                return false;
            }
        }
    }
}
