namespace HotelManagementSystem.Interfaces.Dto
{
    public class UserInfo
    {
        public required string Email { get; set; }

        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = [];
        
        public List<string> Roles { get; set; } = [];

        public required string Username { get; set; }
    }
}
