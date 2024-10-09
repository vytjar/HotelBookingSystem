namespace HotelManagementSystem.Interfaces.Dto
{
    public class Hotel
    {
        public required string Address { get; set; }

        public int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Room> Rooms { get; set; } = [];
    }
}
