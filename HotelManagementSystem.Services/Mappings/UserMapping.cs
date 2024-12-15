using AutoMapper;

namespace HotelManagementSystem.Services.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<Interfaces.Entities.User, Interfaces.Dto.UserInfo>()
                .ReverseMap();
        }
    }
}
