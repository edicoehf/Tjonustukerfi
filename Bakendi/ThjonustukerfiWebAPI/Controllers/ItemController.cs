using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Controllers
{
    /// <summary>Provides an endpoint for all Item actions</summary>
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>Gets item by given ID</summary>
        /// <returns>Item and its status</returns>
        /// <response code="200">Item was successfully retrieved</response>
        /// <response code="404">Item with given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}")]
        [HttpGet]
        public IActionResult GetItemById(long id)
        {
            return Ok(_itemService.GetItemById(id));
        }

        /// <summary>Changes information of an item with the given input. Empty fields will not be edited.</summary>
        /// <response code="200">Item has been edited successfully.</response>
        /// <response code="400">Input model is not valid.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{id:long}")]
        [HttpPatch]
        public IActionResult EditItem([FromBody] EditItemInput input, long id)
        {
            if(!ModelState.IsValid) { return BadRequest("Input model is not valid"); }

            _itemService.EditItem(input, id);

            return Ok();
        }

        /// <summary>Searches for the Item with the barcode given in a search query.</summary>
        /// <returns>Returns the Item and its status.</returns>
        /// <response code="200">Successfully found the item and returns the item.</response>
        /// <response code="404">The Item with the given barcode was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("search")]
        [HttpGet]
        public IActionResult SearchItem([FromQuery(Name = "barcode")] string search)
        {
            return Ok(_itemService.SearchItem(search));
        }

        /// <summary>Sets the item with the given ID to the complete state.</summary>
        /// <response code="200">Item is set to complete (sótt).</response>
        /// <response code="404">Item with the given ID was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}/complete")]
        [HttpPatch]
        public IActionResult CompleteItem(long id)
        {
            _itemService.CompleteItem(id);

            return Ok();
        }

        /// <summary>Removes Item with the given ID.</summary>
        /// <response code="204">Item was successfully removed.</response>
        /// <response code="404">Item with the given ID was not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:long}")]
        [HttpDelete]
        public IActionResult RemoveItem(long id)
        {
            _itemService.RemoveItem(id);
            return NoContent();
        }

        /// <summary>Removes item by barcode</summary>
        /// <response code="204">Item was successfully removed.</response>
        /// <response code="404">Item with the given ID was not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("remove")]
        [HttpDelete]
        public IActionResult RemoveItemQuery([FromQuery(Name = "barcode")] string barcode)
        {
            _itemService.RemoveItemQuery(barcode);

            return NoContent();
        }

        /// <summary>Changes the state of all the items in the input. Takes in a list of ItemStateChangeInputModel.</summary>
        /// <response code="200">All items have been updated</response>
        /// <response code="404">Input was not valid</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("statechangebyid")]
        [HttpPatch]
        public IActionResult ChangeItemState([FromBody] List<ItemStateChangeInputModel> stateChanges)
        {
            _itemService.ChangeItemState(stateChanges);

            return Ok();
        }

        /// <summary>Changes the state of all the items in the input. Takes in a list of ItemStateChangeInputModel.</summary>
        /// <response code="200">All items have been updated</response>
        /// <response code="404">Input was not valid</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("statechangebybarcode")]
        [HttpPatch]
        public IActionResult ChangeItemStateBarcode([FromBody] List<ItemStateChangeBarcodeInputModel> stateChanges)
        {
            _itemService.ChangeItemStateBarcode(stateChanges);

            return Ok();
        }
    }
}