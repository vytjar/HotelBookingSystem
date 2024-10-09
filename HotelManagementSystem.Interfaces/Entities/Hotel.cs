namespace HotelManagementSystem.Interfaces.Entities
{
    public class Hotel
    {
        public required string Address { get; set; }

        public required int Id { get; set; }

        public required string Name { get; set; }

        public ICollection<Room> Rooms { get; set; } = [];
    }
}
