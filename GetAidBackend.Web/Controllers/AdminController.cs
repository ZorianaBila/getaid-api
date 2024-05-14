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

        public AdminController(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetNonDeliveredOrders()
        {
            return await _orderService.GetNonDeliveredOrders();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> CreateNewRoute()
        {
            return await _orderService.GetNonDeliveredOrders();
        }
    }
}