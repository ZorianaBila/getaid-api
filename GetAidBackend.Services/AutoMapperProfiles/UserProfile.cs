using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace RaccoonsDragonsChat.Web.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}