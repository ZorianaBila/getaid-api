using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetAidBackend.Web.Controllers
{
    [Route("api/admin")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IRouteService _routeService;

        public AdminController(IOrderService orderService, IRouteService routeService)
        {
            _orderService = orderService;
            _routeService = routeService;
        }

        [HttpGet("orders/non-delivered")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetNonDeliveredOrders()
        {
            return await _orderService.GetNonDeliveredOrders();
        }

        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            return await _orderService.GetAllOrders();
        }

        [HttpGet("orders/non-collected")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetNonCollectedOrders()
        {
            return await _orderService.GetNonCollectedOrders();
        }

        [HttpGet("orders/collected")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetCollectedOrders()
        {
            return await _orderService.GetNonDeliveredCollectedOrders();
        }

        [HttpPut("orders/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task CollectOrder(string orderId)
        {
            await _orderService.CollectOrder(orderId);
        }

        [HttpGet("routes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<RouteDto>>> GetRoutesHistory()
        {
            return await _routeService.GetRoutes();
        }

        [HttpPost("routes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RouteDto>> CreateNewRoute(CreateneRouteRequest request)
        {
            return await _routeService.CreateOptimalRoute(request.OrdersId, request.StartPoint);
        }

        public class CreateneRouteRequest
        {
            public string[] OrdersId { get; set; }
            public Address StartPoint { get; set; }
        }
    }
}