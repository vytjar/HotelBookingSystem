namespace HotelManagementSystem.Interfaces.Dto.Requests.Authentication
{
    public class ExtendSessionRequest
    {
        public required string RefreshToken { get; set; }

        public required Guid SessionId { get; set; }
    }
}
