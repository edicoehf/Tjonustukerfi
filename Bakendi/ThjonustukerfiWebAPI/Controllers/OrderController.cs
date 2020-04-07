using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    /// <summary>Provides an Endpoint for orders</summary>
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>Gets all orders</summary>
        /// <returns>A list of all orders</returns>
        /// <response code="200">List of all orders, empty if no order exists</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("")]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        /// <summary>Gets a order by ID</summary>
        /// <returns>A Single order</returns>
        /// <response code="200">Returns a single order with the given ID</response>
        /// <response code="404">Returns not found if the order doesn't exist</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}", Name="GetOrderbyId")]
        [HttpGet]
        public IActionResult GetOrderbyId(long id)
        {
            return Ok(_orderService.GetOrderbyId(id));
        }

        /// <summary>Creates a new order</summary>
        /// <param name="order"></param>
        /// <returns>Created at route</returns>
        /// <response code="201">Returns the route to the resource.</response>
        /// <response code="400">Returns bad request if the model is not valid.</response>
        /// <response code="409">Returns conflict if the identifier (e-mail) already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("")]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderInputModel order)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            var entityId = _orderService.CreateOrder(order);

            return CreatedAtRoute("GetOrderbyId", new { id = entityId }, null);
        }

        /// <summary>Updates Order data</summary>
        /// <returns>OK 200 status</returns>
        /// <response code="200">Order has been successfully created.</response>
        /// <response code="400">Input not valid.</response>
        /// <response code="404">The customer with the given ID was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}")]
        [HttpPatch]
        public IActionResult UpdateOrder([FromBody] OrderInputModel order, long id)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            
            _orderService.UpdateOrder(order, id);

            return Ok();
        }

        /// <summary>Deletes an Order with the given ID.</summary>
        /// <returns>Returns no content.</returns>
        /// <response code="204">Order successfully deleted.</response>
        /// <response code="404">Order with the given ID was not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}")]
        [HttpDelete]
        public IActionResult DeleteByOrderId(long id)
        {
            _orderService.DeleteByOrderId(id);

            return NoContent();
        }

        /// <summary>Completes a whole order, updates all items in order to complete</summary>
        /// <response code="200">Successfully updated order to complete</response>
        /// <response code="404">Order with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}/complete")]
        [HttpPatch]
        public IActionResult CompleteOrder(long id)
        {
            _orderService.CompleteOrder(id);

            return Ok();
        }

        /// <summary>Searches for the order with the given barcode (as a query).</summary>
        /// <returns>Order data transfer object</returns>
        /// <response code="200">Successfully found the order and returns it.</response>
        /// <response code="404">The order with the given barcode was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("search")]
        [HttpGet]
        public IActionResult SearchOrder([FromQuery(Name = "barcode")] string search)
        {
            return Ok(_orderService.SearchOrder(search));
        }

        /// <summary>Removes order by barcode</summary>
        /// <response code="204">Order was successfully removed.</response>
        /// <response code="404">Order with the given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("remove")]
        [HttpDelete]
        public IActionResult RemoveOrderQuery([FromQuery(Name = "barcode")] string barcode)
        {
            _orderService.RemoveOrderQuery(barcode);

            return NoContent();
        }
    }
}