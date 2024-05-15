using GetAidBackend.Services.Dtos;

namespace GetAidBackend.Services.Abstractionas
{
    public interface IRouteService
    {
        Task<RouteDto> CreateOptimalRoute(string[] ordersId);
        Task<List<RouteDto>> GetRoutes();
    }
}