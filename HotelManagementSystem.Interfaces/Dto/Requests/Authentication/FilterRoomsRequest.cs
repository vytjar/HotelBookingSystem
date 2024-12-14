namespace HotelManagementSystem.Interfaces.Dto.Requests.Authentication
{
    public class FilterRoomsRequest
    {
        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public int? Capacity { get; set; }

        public int? HotelId { get; set; }

        public string? RoomNumber { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
