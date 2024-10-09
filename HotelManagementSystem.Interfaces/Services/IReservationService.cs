using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(Reservation registration);

        Task<Reservation> GetAsync(int registrationId);

        Task RemoveAsync(int registrationId);

        Task<Reservation> UpdateAsync(Reservation registration);
    }
}
