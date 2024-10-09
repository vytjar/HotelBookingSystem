using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Responses;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IHotelService
    {
        Task<Hotel> CreateAsync(Hotel hotel);

        Task<Hotel> GetAsync(int hotelId);

        Task<IEnumerable<Room>> GetRoomsAsync(int hotelId);

        Task RemoveAsync(int hotelId);

        Task<Hotel> UpdateAsync(Hotel hotel);
    }
}
