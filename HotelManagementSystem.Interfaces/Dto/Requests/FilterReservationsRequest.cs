namespace HotelManagementSystem.Interfaces.Dto.Requests
{
    public class FilterReservationsRequest
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int RoomId { get; set; }
    }
}
