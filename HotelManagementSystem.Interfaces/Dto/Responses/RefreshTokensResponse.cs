namespace HotelManagementSystem.Interfaces.Dto.Responses
{
    public class RefreshTokensResponse
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set;}
    }
}
