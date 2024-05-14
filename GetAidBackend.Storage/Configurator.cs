using GetAidBackend.Storage.Abstractions;
using GetAidBackend.Storage.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace GetAidBackend.Storage
{
    public static class Configurator
    {
        public static IServiceCollection AddStorageConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}