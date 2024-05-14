using GetAidBackend.Domain;
using GetAidBackend.Services.Abstractionas;
using GetAidBackend.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetAidBackend.Web.Controllers
{
    [Route("api/orders")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class OrderConroller : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderConroller(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDto>> AddOder(Order order)
        {
            var userId = HttpContext.User.FindFirst("UserId").Value;
            var orderDto = await _orderService.Add(userId, order);
            return Ok(orderDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetUserOrders()
        {
            var userId = HttpContext.User.FindFirst("UserId").Value;
            var orders = await _orderService.GetUserOrders(userId);
            return Ok(orders);
        }
    }
}