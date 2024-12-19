namespace HotelManagementSystem.Interfaces.Entities
{
    public class Session
    {
        public required DateTimeOffset ExpiresAt { get; set; }

        public Guid Id { get; set; }

        public required DateTimeOffset InitiatedAt { get; set; }

        public required string RefreshToken { get; set; }

        public bool Revoked { get; set; }

        public required string UserId { get; set; }

        public User? User { get; set; }
    }
}
