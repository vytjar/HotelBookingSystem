namespace HotelManagementSystem.Interfaces.Entities
{
    public class Room
    {
        public required int Capacity { get; set; }
        
        public Hotel? Hotel { get; set; }

        public required int HotelId { get; set; }

        public required int Id { get; set; }

        public ICollection<Registration> Registrations { get; set; } = [];

        public required string RoomNumber { get; set; }
    }
}
