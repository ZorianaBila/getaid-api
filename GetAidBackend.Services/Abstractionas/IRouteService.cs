using GetAidBackend.Domain;
using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Services.Abstractionas
{
    public interface IRouteService
    {
        Task<RouteDto> CreateOptimalRoute(string[] ordersId, Address startPoint);

        Task<List<RouteDto>> GetRoutes();
    }
}