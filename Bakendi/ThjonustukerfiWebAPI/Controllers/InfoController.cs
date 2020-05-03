using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Configurations;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    /// <summary>
    ///     Provides endpoints to retrieve various information from the API.
    /// </summary>
    [Route("api/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private IInfoService _infoService;

        public InfoController(IInfoService inforservice)
        {
            _infoService = inforservice;
        }

        /// <summary>Gets all available services.</summary>
        /// <returns>OK 200 status.</returns>
        /// <response code="200">Returns a list of services, empty list if there are none.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("services")]
        [HttpGet]
        public IActionResult GetServices()
        {
            return Ok(_infoService.GetServices());
        }

        /// <summary>Gets all available states.</summary>
        /// <returns>OK 200 status.</returns>
        /// <response code="200">Returns a list of states, empty list if there are none.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("states")]
        [HttpGet]
        public IActionResult GetStates()
        {
            return Ok(_infoService.GetStates());
        }

        /// <summary>Gets all available categories.</summary>
        /// <returns>List of categories.</returns>
        /// <response code="200">Returns a list of available categories.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("categories")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_infoService.GetCategories());
        }

        /// <summary>Gets all archived orders.</summary>
        /// <returns>List of orders and their Items.</returns>
        /// <response code="200">Returns a list of all orders in the archive.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("orderarchives")]
        [HttpGet]
        public IActionResult GetArchivedOrders()
        {
            return Ok(_infoService.GetArchivedOrders());
        }

        /// <summary>Gets an Item history by its ID.</summary>
        /// <returns>List of representing an items history.</returns>
        /// <response code="200">List of item history returned.</response>
        /// <response code="404">Item with given ID not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}/itemhistory")]
        [HttpGet]
        public IActionResult GetItemHistory(long id)
        {
            return Ok(_infoService.GetItemHistory(id));
        }

        /// <summary>Gets all locations available.</summary>
        /// <returns>A list of locations for items.</returns>
        /// <response code="200">Returns a list of locations for items. Empty list if their are none.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("itemlocations")]
        [HttpGet]
        public IActionResult GetItemLocations()
        {
            return Ok(Constants.Locations);
        }
    }
}