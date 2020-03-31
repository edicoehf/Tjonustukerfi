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
    }
}