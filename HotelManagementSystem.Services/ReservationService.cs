using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Exceptions;
using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class ReservationService(HotelScope hotelScope, IMapper mapper) : IReservationService
    {
        private readonly HotelScope _hotelScope = hotelScope;
        private readonly IMapper _mapper = mapper;

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await Validate(reservation);

            var reservationCreated = await _hotelScope.DbContext.Registrations.AddAsync(_mapper.Map<Interfaces.Entities.Reservation>(reservation));

            if (reservationCreated.Entity is null)
            {
                throw new Exception("Could not create reservation.");
            }

            await _hotelScope.DbContext.SaveChangesAsync();

            reservation.Id = reservationCreated.Entity.Id;

            return reservation;
        }

        public async Task<Reservation> GetAsync(int reservationId)
        {
            var reservation = _mapper.Map<Reservation>(await _hotelScope.DbContext.Registrations
                .Where(h => h.Id == reservationId)
                .SingleOrDefaultAsync());

            if (reservation is null)
            {
                throw new NotFoundException($"Registration {reservationId} could not be found");
            }

            return reservation;
        }

        public async Task RemoveAsync(int reservationId)
        {
            var reservation = await _hotelScope.DbContext.Registrations
                .Where(h => h.Id == reservationId)
                .SingleOrDefaultAsync();

            if (reservation is null)
            {
                throw new NotFoundException($"Registration {reservationId} could not be found");
            }

            _hotelScope.DbContext.Registrations.Remove(reservation);

            await _hotelScope.DbContext.SaveChangesAsync();
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            await Validate(reservation);

            var reservationUpdated = _hotelScope.DbContext.Registrations.Update(_mapper.Map<Interfaces.Entities.Reservation>(reservation));

            await _hotelScope.DbContext.SaveChangesAsync();

            return _mapper.Map<Reservation>(reservationUpdated.Entity);
        }

        private async Task Validate(Reservation reservation)
        {
            if (reservation is null)
            {
                throw new ArgumentNullException(nameof(reservation));
            }

            if (reservation.CheckOutDate <= reservation.CheckOutDate)
            {
                throw new ValidationException($"Check out date {reservation.CheckOutDate:yyyy-MM-dd} can not be before or on the same day as the check in date {reservation.CheckInDate:yyyy-MM-dd}");
            }

            if (reservation.GuestCount < 1)
            {
                throw new ValidationException($"Guest count {reservation.GuestCount} can not be lower than 1");
            }

            var room = await _hotelScope.DbContext.Rooms
                .Where(r => r.Id == reservation.RoomId)
                .SingleOrDefaultAsync();

            if (room is null)
            {
                throw new NotFoundException($"Room {reservation.RoomId} could not be found");
            }

            var reservationExisting = await _hotelScope.DbContext.Registrations
                .Where(r => r.Id == reservation.RoomId &&
                    r.CheckInDate < reservation.CheckOutDate &&
                    r.CheckOutDate > reservation.CheckInDate
                )
                .SingleOrDefaultAsync();

            if (reservationExisting is not null)
            {
                throw new ValidationException($"Registration check in {reservation.CheckInDate:yyyy-MM-dd} and check out {reservation.CheckOutDate:yyyy-MM-dd} dates intersect with an already existing reservation.");
            }
        }
    }
}
