using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Services.Mappings
{
    public class HotelMapping : Profile
    {
        public HotelMapping()
        {
            CreateMap<Interfaces.Entities.Hotel, Hotel>()
                .ReverseMap();
        }
    }
}
