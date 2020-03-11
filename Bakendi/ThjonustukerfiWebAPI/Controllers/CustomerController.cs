using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("")]
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerInputModel customer)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            var entity = _customerService.CreateCustomer(customer);
            
            return NoContent();
        }

        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetCustomerById(long id)
        {
            var customer = _customerService.GetCustomerById(id);

            return Ok(customer);
        }
    }
}