using GetAidBackend.Auth.Configuration;
using GetAidBackend.Services;
using GetAidBackend.Storage;
using MongoDB.Driver;

namespace GetAidBackend.Web
{
    public static class Configurator
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
            });

            services.AddAuthConfiguration();
            services.AddStorageConfiguration();
            services.AddServicesConfiguration();

            return services;
        }
    }
}