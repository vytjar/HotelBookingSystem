using HotelManagementSystem.Interfaces.Entities;

namespace HotelManagementSystem.Interfaces.Dto
{
    public class Reservation
    {
        public required DateTime CheckInDate { get; set; }

        public required DateTime CheckOutDate { get; set; }

        public required int GuestCount { get; set; }

        public int Id { get; set; }

        public Room? Room { get; set; }

        public required int RoomId { get; set; }

        public string? UserId { get; set; }

        public User? User { get; set; }
    }
}
