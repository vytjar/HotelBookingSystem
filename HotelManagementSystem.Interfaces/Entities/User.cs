using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Interfaces.Entities
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }

        public required string Surname { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = [];

        [NotMapped]
        public List<string> Roles { get; set; } = [];

        public ICollection<Session> Sessions { get; set; } = [];
    }
}
