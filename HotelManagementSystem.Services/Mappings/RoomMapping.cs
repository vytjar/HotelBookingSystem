using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Services.Mappings
{
    public class RoomMapping : Profile
    {
        public RoomMapping()
        {
            CreateMap<Interfaces.Entities.Room, Room>()
                .ReverseMap();
        }
    }
}
