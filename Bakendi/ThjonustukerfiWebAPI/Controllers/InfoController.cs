using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    /// <summary>Provides an endpoint for various information from the API</summary>
    [Route("api/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private IInfoService _infoService;

        public InfoController(IInfoService inforservice)
        {
            _infoService = inforservice;
        }

        /// <summary>Gets all available services</summary>
        /// <returns>OK 200 status</returns>
        /// <response code="200">Returns a list of services, empty list if there are none</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("services")]
        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(_infoService.GetServices());
        }

        /// <summary>Gets all available states</summary>
        /// <returns>OK 200 status</returns>
        /// <response code="200">Returns a list of states, empty list if there are none</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("states")]
        [HttpGet]
        public IActionResult GetStates()
        {
            return Ok(_infoService.GetStates());
        }

        /// <summary>Gets all available categories</summary>
        /// <returns>List of categories</returns>
        /// <response code="200">Returns a list of available categories</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("categories")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_infoService.GetCategories());
        }
    }
}