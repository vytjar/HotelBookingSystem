namespace HotelManagementSystem.Interfaces.Dto
{
    public class Registration
    {
        public required DateTime CheckInDate { get; set; }

        public required DateTime CheckOutDate { get; set; }

        public required int GuestCount { get; set; }

        public int Id { get; set; }

        public Room? Room { get; set; }

        public required int RoomId { get; set; }
    }
}
