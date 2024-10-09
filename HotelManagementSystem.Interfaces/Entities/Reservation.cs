namespace HotelManagementSystem.Interfaces.Entities
{
    public class Reservation
    {
        public required DateTime CheckInDate { get; set; }
        
        public required DateTime CheckOutDate { get; set; }

        public required int GuestCount { get; set; }

        public required int Id { get; set; }

        public Room? Room { get; set; }

        public required int RoomId { get; set; }
    }
}
