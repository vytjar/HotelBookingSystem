using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Dto.Requests;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(Reservation reservation);

        Task<IEnumerable<Reservation>> FilterAsync(FilterReservationsRequest request); 

        Task<Reservation> GetReservationAsync(int reservationId, string userId);

        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task DeleteAsync(int reservationId, string userId);

        Task<Reservation> UpdateAsync(Reservation reservation, string userId);
    }
}
