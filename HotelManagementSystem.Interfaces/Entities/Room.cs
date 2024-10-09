namespace HotelManagementSystem.Interfaces.Entities
{
    public class Room
    {
        public required int Capacity { get; set; }
        
        public Hotel? Hotel { get; set; }

        public required int HotelId { get; set; }

        public required int Id { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = [];

        public required string RoomNumber { get; set; }
    }
}
