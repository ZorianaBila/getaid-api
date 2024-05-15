using AntColony.Algorithm;
using AntColony.Core;
using AntColony.Core.Graphs;
using AutoMapper;
using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using GetAidBackend.Storage.Abstractions;
using MongoDB.Driver;

namespace GetAidBackend.Services.Implementations
{
    public class RouteService : ServiceBase<Route, RouteDto, IRouteRepository>, IRouteService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IGoogleMapsService _googleMapsService;
        private readonly IMongoClient _client;

        public RouteService(
            IMongoClient client,
            IRouteRepository repository,
            IOrderRepository orderRepository,
            IGoogleMapsService googleMapsService,
            IMapper mapper)
            : base(repository, mapper)
        {
            _client = client;
            _googleMapsService = googleMapsService;
            _orderRepository = orderRepository;
        }

        public async Task<List<RouteDto>> GetRoutes()
        {
            var result = await _repository.GetAll();
            return GetDtosFromEntities(result);
        }

        public async Task<RouteDto> CreateOptimalRoute(string[] ordersId)
        {
            var orders = await _orderRepository.GetByIds(ordersId);
            string[] addresses = orders.Select(_ => _.Address.Text).ToArray();

            var distanceMatrix = await _googleMapsService.GetDistanceMatrix(addresses);
            Result result = GetOptimalRoute(addresses.Length, distanceMatrix);

            var route = ConvertResultToRoute(orders, result);
            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            route = await _repository.Add(route, session);
            await _orderRepository.DeliverOrders(ordersId, session);
            await session.CommitTransactionAsync();

            return GetDtoFromEntity(route);
        }

        private Result GetOptimalRoute(int count, int[,] distanceMatrix)
        {
            var random = new Random();
            var config = new Config();
            var graph = new Graph(count, distanceMatrix);

            IAlgorithm antAlgorithm = new AntColonyAlgorithm(graph, config, random);

            return antAlgorithm.Solve();
        }

        private Route ConvertResultToRoute(List<Order> items, Result result)
        {
            var orderedItems = new List<Order>();

            foreach (int j in result.BestPath)
            {
                orderedItems.Add(items[j]);
            }

            var route = new Route()
            {
                Items = orderedItems.Select(_ => new RouteItem() { Address = _.Address, OrderId = _.Id }).ToList(),
                PathLength = result.PathCost
            };

            return route;
        }
    }
}