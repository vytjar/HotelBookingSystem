using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(Reservation reservation);

        Task<Reservation> GetReservationAsync(int reservationId, string userId);

        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task DeleteAsync(int reservationId, string userId);

        Task<Reservation> UpdateAsync(Reservation reservation, string userId);
    }
}
