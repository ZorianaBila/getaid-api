using GetAidBackend.Domain;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;

namespace GetAidBackend.Storage.Implementations
{
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        private static string _collectionName = "routes";

        public RouteRepository(IMongoClient client) : base(client, _collectionName)
        {
        }
    }
}