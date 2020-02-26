using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [Route("")]
        [HttpPost]
        public IActionResult CreateItem([FromBody] ItemInputModel item)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }
            var entity = _itemService.CreateItem(item);
            
            return NoContent();
        } 
    }
}