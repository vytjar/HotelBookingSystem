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

        public async Task<Room> GetAsync(int roomId)
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

        public async Task<IEnumerable<Registration>> GetRegistrations(int roomId)
        {
            var room = _mapper.Map<Room>(await _hotelScope.DbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == roomId)
                .Include(r => r.Registrations)
                .SingleOrDefaultAsync());

            if (room is null)
            {
                throw new NotFoundException($"Room {roomId} could not be found");
            }

            return room.Registrations;
        }

        public async Task<bool> RemoveAsync(int roomId)
        {
            var room = await _hotelScope.DbContext.Rooms
               .AsNoTracking()
               .Where(r => r.Id == roomId)
               .SingleOrDefaultAsync();

            if (room is null)
            {
                return false;
            }

            _hotelScope.DbContext.Rooms.Remove(room);

            await _hotelScope.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            await Validate(room);

            var roomUpdated = _hotelScope.DbContext.Rooms.Update(_mapper.Map<Interfaces.Entities.Room>(room));

            await _hotelScope.DbContext.SaveChangesAsync();

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
