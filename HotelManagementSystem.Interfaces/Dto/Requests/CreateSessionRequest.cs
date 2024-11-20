namespace HotelManagementSystem.Interfaces.Dto.Requests
{
    public class CreateSessionRequest
    {
        public required string RefreshToken { get; set; }
        
        public required Guid SessionId { get; set; }

        public required string UserId { get; set; }
    }
}
