using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;
using HotelManagementSystem.Interfaces.Exceptions;
using HotelManagementSystem.Interfaces.Services;
using HotelManagementSystem.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services
{
    public class HotelService(HotelScope hotelScope, IMapper mapper) : IHotelService
    {
        private readonly HotelScope _hotelScope = hotelScope;
        private readonly IMapper _mapper = mapper;

        public async Task<Hotel> CreateAsync(Hotel hotel)
        {
            await Validate(hotel);

            var hotelCreated = await _hotelScope.DbContext.Hotels.AddAsync(_mapper.Map<Interfaces.Entities.Hotel>(hotel));

            await _hotelScope.DbContext.SaveChangesAsync();

            if (hotelCreated.Entity is null)
            {
                throw new Exception("Could not create hotel.");
            }

            hotel.Id = hotelCreated.Entity.Id;

            return hotel;
        }

        public async Task DeleteAsync(int hotelId)
        {
            var hotel = await _hotelScope.DbContext.Hotels
                .AsNoTracking()
                .Where(h => h.Id == hotelId)
                .SingleOrDefaultAsync();

            if (hotel is null)
            {
                throw new NotFoundException($"Hotel {hotelId} could not be found");
            }

            _hotelScope.DbContext.Hotels.Remove(hotel);

            await _hotelScope.DbContext.SaveChangesAsync();
        }


        public async Task<Hotel> GetHotelAsync(int hotelId)
        {
            var hotel = _mapper.Map<Hotel>(await _hotelScope.DbContext.Hotels
                .AsNoTracking()
                .Where(h => h.Id == hotelId)
                .SingleOrDefaultAsync());

            if (hotel is null)
            {
                throw new NotFoundException($"Hotel {hotelId} could not be found");
            }

            return hotel;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return (await _hotelScope.DbContext.Hotels
                .AsNoTracking()
                .ToListAsync())
                .Select(_mapper.Map<Hotel>);
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync(int hotelId)
        {
            var hotel = await _hotelScope.DbContext.Hotels
                .AsNoTracking()
                .Where(h => h.Id == hotelId)
                .SingleOrDefaultAsync();

            if (hotel is null)
            {
                throw new NotFoundException($"Hotel {hotelId} could not be found");
            }

            var rooms = await _hotelScope.DbContext.Rooms
                .AsNoTracking()
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            return rooms.Select(_mapper.Map<Room>);
        }

        public async Task<Hotel> UpdateAsync(Hotel hotel)
        {
            await Validate(hotel);

            var hotelSource = await _hotelScope.DbContext.Hotels
                .AsNoTracking()
                .Where(h => h.Id == hotel.Id)
                .SingleOrDefaultAsync();

            if (hotelSource is null)
            {
                throw new NotFoundException($"Hotel {hotel.Id} could not be found");
            }

            var hotelUpdated = _hotelScope.DbContext.Hotels.Update(_mapper.Map<Interfaces.Entities.Hotel>(hotel));
            
            await _hotelScope.DbContext.SaveChangesAsync();

            if (hotelUpdated.Entity is null)
            {
                throw new Exception($"Hotel {hotel.Id} could not be updated");
            }

            return _mapper.Map<Hotel>(hotelUpdated.Entity);
        }

        private async Task Validate(Hotel hotel)
        {
            if (hotel is null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            if (string.IsNullOrEmpty(hotel.Address))
            {
                throw new ValidationException("Hotel address can not be null");
            }

            if (string.IsNullOrEmpty(hotel.Name))
            {
                throw new ValidationException("Hotel name can not be null");
            }

            var existingHotels = await _hotelScope.DbContext.Hotels
                .AsNoTracking()
                .Where(h => string.Equals(h.Address, hotel.Address) && string.Equals(h.Name, hotel.Name))
                .ToListAsync();

            if (existingHotels.Count != 0)
            {
                throw new ValidationException($"Hotel with address {hotel.Address} and name {hotel.Name} already exists");
            }
        }
    }
}
