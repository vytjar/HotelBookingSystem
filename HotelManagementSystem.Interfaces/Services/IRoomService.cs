using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IRoomService
    {
        Task<Room> CreateAsync(Room room);

        Task DeleteAsync(int roomId);

        Task<Room> GetRoomAsync(int roomId);

        Task<IEnumerable<Room>> GetRoomsAsync();

        Task<IEnumerable<Reservation>> GetReservations(int roomId);

        Task<Room> UpdateAsync(Room room);
    }
}
