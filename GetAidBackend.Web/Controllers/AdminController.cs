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

        [HttpGet("orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetNonDeliveredOrders()
        {
            return await _orderService.GetNonDeliveredOrders();
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
        public async Task<ActionResult<RouteDto>> CreateNewRoute(string[] ordersId)
        {
            return await _routeService.CreateOptimalRoute(ordersId);
        }
    }
}