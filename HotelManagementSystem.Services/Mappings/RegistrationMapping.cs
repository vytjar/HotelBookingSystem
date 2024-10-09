using AutoMapper;
using HotelManagementSystem.Interfaces.Dto;

namespace HotelManagementSystem.Services.Mappings
{
    public class RegistrationMapping : Profile
    {
        public RegistrationMapping()
        {
            CreateMap<Interfaces.Entities.Registration, Registration>()
                .ReverseMap();
        }
    }
}
