using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Exceptions;
using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class RoomService(HotelScope hotelScope, IMapper mapper) : IRoomService
    {
        private readonly HotelScope _hotelScope = hotelScope;
        private readonly IMapper _mapper = mapper;

        public async Task<Room> CreateAsync(Room room)
        {
            await Validate(room);

            var roomCreated = await _hotelScope.DbContext.Rooms.AddAsync(_mapper.Map<Interfaces.Entities.Room>(room));

            await _hotelScope.DbContext.SaveChangesAsync();

            if (roomCreated.Entity is null)
            {
                throw new Exception("Could not create room.");
            }

            room.Id = roomCreated.Entity.Id;

            return room;
        }
        public async Task<IEnumerable<Reservation>> GetReservations(int roomId)
        {
            var room = _mapper.Map<Room>(await _hotelScope.DbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == roomId)
                .SingleOrDefaultAsync());

            if (room is null)
            {
                throw new NotFoundException($"Room {roomId} could not be found");
            }

            var reservations = (await _hotelScope.DbContext.Reservations
                .AsNoTracking()
                .Where(r => r.RoomId == roomId)
                .ToListAsync())
                .Select(_mapper.Map<Reservation>);

            return reservations;
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
             var room = _mapper.Map<Room>(await _hotelScope.DbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == roomId)
                .SingleOrDefaultAsync());

            if (room is null)
            {
                throw new NotFoundException($"Room {roomId} could not be found");
            }

            return room;
        }
        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return (await _hotelScope.DbContext.Rooms
                .AsNoTracking()
                .ToListAsync())
                .Select(_mapper.Map<Room>);
        }

        public async Task DeleteAsync(int roomId)
        {
            var room = await _hotelScope.DbContext.Rooms
               .AsNoTracking()
               .Where(r => r.Id == roomId)
               .SingleOrDefaultAsync();

            if (room is null)
            {
                throw new NotFoundException($"Room {roomId} could not be found");
            }

            _hotelScope.DbContext.Rooms.Remove(room);

            await _hotelScope.DbContext.SaveChangesAsync();
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            await Validate(room);

            var roomSource = await _hotelScope.DbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == room.Id)
                .SingleOrDefaultAsync();

            if (roomSource is null)
            {
                throw new NotFoundException($"Room {room.Id} could not be found");
            }

            var roomUpdated = _hotelScope.DbContext.Rooms.Update(_mapper.Map<Interfaces.Entities.Room>(room));

            await _hotelScope.DbContext.SaveChangesAsync();

            if (roomUpdated.Entity is null)
            {
                throw new Exception($"Room {room.Id} could not be updated");
            }

            return _mapper.Map<Room>(roomUpdated.Entity);
        }

        private async Task Validate(Room room)
        {
            if (room is null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            if (room.Capacity < 1)
            {
                throw new ValidationException($"Room capacity {room.Capacity} can not be lower than 1");
            }

            var hotel = await _hotelScope.DbContext.Hotels
                .Where(h => h.Id == room.HotelId)
                .SingleOrDefaultAsync();

            if (hotel is null)
            {
                throw new NotFoundException($"Hotel id {room.HotelId} could not be found");
            }

            if (string.IsNullOrEmpty(room.RoomNumber))
            {
                throw new ValidationException($"Room number can not be empty or null");
            }
        }
    }
}
