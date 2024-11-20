using Microsoft.AspNetCore.Identity;

namespace HotelManagementSystem.Interfaces.Entities
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }

        public required string Surname { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = [];

        public ICollection<Session> Sessions { get; set; } = [];
    }
}
