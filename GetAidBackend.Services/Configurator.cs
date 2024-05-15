using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.AutoMapperProfiles;
using GetAidBackend.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace GetAidBackend.Services
{
    public static class Configurator
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapperConfiguration();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IGoogleMapsService, GoogleMapsService>();
            services.AddTransient<IRouteService, RouteService>();

            return services;
        }
    }
}