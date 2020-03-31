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
        [Route("")]
        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(_infoService.GetServices());
        }
    }
}