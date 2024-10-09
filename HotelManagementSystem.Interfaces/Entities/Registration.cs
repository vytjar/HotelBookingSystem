namespace HotelManagementSystem.Interfaces.Entities
{
    public class Registration
    {
        public required DateTime CheckInDate { get; set; }
        
        public required DateTime CheckOutDate { get; set; }

        public required int GuestCount { get; set; }

        public required int Id { get; set; }

        public required Room Room { get; set; }

        public required int RoomId { get; set; }
    }
}
