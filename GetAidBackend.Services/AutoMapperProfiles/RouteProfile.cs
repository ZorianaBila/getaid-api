using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Services.AutoMapperProfiles
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<Route, RouteDto>()
                .ReverseMap();
        }
    }
}