using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Interfaces.Services
{
    public interface IRegistrationService
    {
        Task<Registration> CreateAsync(Registration registration);

        Task<Registration> GetAsync(int registrationId);

        Task RemoveAsync(int registrationId);

        Task<Registration> UpdateAsync(Registration registration);
    }
}
