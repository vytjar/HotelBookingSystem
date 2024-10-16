using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IHotelService
    {
        Task<Hotel> CreateAsync(Hotel hotel);

        Task DeleteAsync(int hotelId);

        Task<Hotel> GetHotelAsync(int hotelId);

        Task<IEnumerable<Hotel>> GetHotelsAsync();

        Task<IEnumerable<Room>> GetRoomsAsync(int hotelId);

        Task<Hotel> UpdateAsync(Hotel hotel);
    }
}
