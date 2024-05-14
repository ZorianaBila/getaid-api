using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RaccoonsDragonsChat.Web.AutoMapperProfiles;

namespace GetAidBackend.Services.AutoMapperProfiles
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(
           this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new OrderProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}