using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace RaccoonsDragonsChat.Web.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ReverseMap();
        }
    }
}