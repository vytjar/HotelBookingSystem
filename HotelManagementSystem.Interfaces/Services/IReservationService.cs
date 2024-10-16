using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(Reservation reservation);

        Task<Reservation> GetReservationAsync(int reservationId);

        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task DeleteAsync(int reservationId);

        Task<Reservation> UpdateAsync(Reservation reservation);
    }
}
