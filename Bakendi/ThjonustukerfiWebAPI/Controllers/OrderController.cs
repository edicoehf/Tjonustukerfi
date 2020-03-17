using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderInputModel order)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            var entity = _orderService.CreateOrder(order);

            return NoContent();
        }
    }
}