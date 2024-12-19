namespace HotelManagementSystem.Interfaces.Constants
{
    public static class Jwt
    {
        public const string AccessTokenExpirationMinutes = "Jwt:AccessTokenExpirationMinutes";
     
        public const string RefreshTokenExpirationDays = "Jwt:RefreshTokenExpirationDays";
        
        public const string Secret = "Jwt:Secret";
        
        public const string ValidAudience = "Jwt:ValidAudience";
        
        public const string ValidIssuer = "Jwt:ValidIssuer";
    }
}
