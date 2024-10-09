using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Services.Mappings
{
    public class ReservationMapping : Profile
    {
        public ReservationMapping()
        {
            CreateMap<Interfaces.Entities.Reservation, Interfaces.Dto.Reservation>()
                .ReverseMap();
        }
    }
}
