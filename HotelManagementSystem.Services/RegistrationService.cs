using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Exceptions;
using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class RegistrationService(HotelScope hotelScope, IMapper mapper) : IRegistrationService
    {
        private readonly HotelScope _hotelScope = hotelScope;
        private readonly IMapper _mapper = mapper;

        public async Task<Registration> CreateAsync(Registration registration)
        {
            await Validate(registration);

            var registrationCreated = await _hotelScope.DbContext.Registrations.AddAsync(_mapper.Map<Interfaces.Entities.Registration>(registration));

            if (registrationCreated.Entity is null)
            {
                throw new Exception("Could not create registration.");
            }

            await _hotelScope.DbContext.SaveChangesAsync();

            registration.Id = registrationCreated.Entity.Id;

            return registration;
        }

        public async Task<Registration> GetAsync(int registrationId)
        {
            var registration = _mapper.Map<Registration>(await _hotelScope.DbContext.Registrations
                .Where(h => h.Id == registrationId)
                .SingleOrDefaultAsync());

            if (registration is null)
            {
                throw new NotFoundException($"Registration {registrationId} could not be found");
            }

            return registration;
        }

        public async Task RemoveAsync(int registrationId)
        {
            var registration = await _hotelScope.DbContext.Registrations
                .Where(h => h.Id == registrationId)
                .SingleOrDefaultAsync();

            if (registration is null)
            {
                throw new NotFoundException($"Registration {registrationId} could not be found");
            }

            _hotelScope.DbContext.Registrations.Remove(registration);

            await _hotelScope.DbContext.SaveChangesAsync();
        }

        public async Task<Registration> UpdateAsync(Registration registration)
        {
            await Validate(registration);

            var registrationUpdated = _hotelScope.DbContext.Registrations.Update(_mapper.Map<Interfaces.Entities.Registration>(registration));

            await _hotelScope.DbContext.SaveChangesAsync();

            return _mapper.Map<Registration>(registrationUpdated.Entity);
        }

        private async Task Validate(Registration registration)
        {
            if (registration is null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            if (registration.CheckOutDate <= registration.CheckOutDate)
            {
                throw new ValidationException($"Check out date {registration.CheckOutDate:yyyy-MM-dd} can not be before or on the same day as the check in date {registration.CheckInDate:yyyy-MM-dd}");
            }

            if (registration.GuestCount < 1)
            {
                throw new ValidationException($"Guest count {registration.GuestCount} can not be lower than 1");
            }

            var room = await _hotelScope.DbContext.Rooms
                .Where(r => r.Id == registration.RoomId)
                .SingleOrDefaultAsync();

            if (room is null)
            {
                throw new NotFoundException($"Room {registration.RoomId} could not be found");
            }

            var registrationExisting = await _hotelScope.DbContext.Registrations
                .Where(r => r.Id == registration.RoomId &&
                    r.CheckInDate < registration.CheckOutDate &&
                    r.CheckOutDate > registration.CheckInDate
                )
                .SingleOrDefaultAsync();

            if (registrationExisting is not null)
            {
                throw new ValidationException($"Registration check in {registration.CheckInDate:yyyy-MM-dd} and check out {registration.CheckOutDate:yyyy-MM-dd} dates intersect with an already existing registration.");
            }
        }
    }
}
