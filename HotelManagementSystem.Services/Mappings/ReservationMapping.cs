using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Services.Mappings
{
    public class ReservationMapping : Profile
    {
        public ReservationMapping()
        {
            CreateMap<Interfaces.Entities.Reservation, Reservation>()
                .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CheckInDate, DateTimeKind.Utc)))
                .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CheckOutDate, DateTimeKind.Utc)))
                .ReverseMap();
        }
    }
}
