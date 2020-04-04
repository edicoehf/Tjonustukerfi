using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    /// <summary>Provides an Endpoint for all Customer actions</summary>
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>Gets all customers</summary>
        /// <returns>A list of all customers</returns>
        /// <response code="200">List of all customers, empty list if none exist</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("")]
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        /// <summary>Adds a new customer to the database.</summary>
        /// <param name="customer"></param>
        /// <returns>A created at route</returns>
        /// <response code="201">Returns the route to the resource.</response>
        /// <response code="400">Returns bad request if the model state is not valid.</response>
        /// <response code="409">Returns conflict if the identifier (e-mail) already exists.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("")]
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerInputModel customer)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            var entity = _customerService.CreateCustomer(customer);
            
            return CreatedAtRoute("GetCustomerById", new { id = entity.Id }, null);
        }

        /// <summary>Gets a customer by ID</summary>
        /// <returns>A single customer</returns>
        /// <response code="200">Returns a single customer with given ID</response>
        /// <response code="404">Returns not found if the customer with the ID does not exist</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:int}", Name="GetCustomerById")]
        [HttpGet]
        public IActionResult GetCustomerById(long id)
        {
            var customer = _customerService.GetCustomerById(id);

            return Ok(customer);
        }

        /// <summary>Updates customer detailes</summary>
        /// <returns>OK 200 status</returns>
        /// <response code="200">Customer has been successfully updated.</response>
        /// <response code="400">The input model was not valid.</response>
        /// <response code="409">The customer with the given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("{id:int}")]
        [HttpPatch]
        public IActionResult UpdateCustomerDetails([FromBody] CustomerInputModel customer, long id)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            _customerService.UpdateCustomerDetails(customer, id);
            return Ok();
        
        }

        /// <summary>Deletes a customer with the given ID</summary>
        /// <returns>Returns no content</returns>
        /// <response code="204">Customer successfully deleted</response>
        /// <response code="409">Customer with the given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteCustomerById(long id)
        {
            _customerService.DeleteCustomerById(id);

            return NoContent();
        }
    }
}