using System.Collections.Generic;
using System.Linq;
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

        /// <summary>Changes the state of all the items in the input. Takes in a list of ItemStateChangeInputModel. You can either use itemId or barcode.</summary>
        /// <response code="200">All items have been updated</response>
        /// <response code="202">Partial success, some inputs are invalid. A list of invalid inputs are returned.</response>
        /// <response code="404">Input was not valid, no changes were made.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("statechange")]
        [HttpPatch]
        public IActionResult ChangeItemState([FromBody] List<ItemStateChangeInput> stateChanges)
        {
            var invalidInput = _itemService.ChangeItemState(stateChanges);

            if(invalidInput.Any()) { return Accepted(invalidInput); }

            return Ok();
        }

        /// <summary>Gets the next states for the Item. Returns an empty list if the item is in its last state.</summary>
        /// <param name="itemid">Use item ID to get next states</param>
        /// <param name="barcode">Use item barcode to get next states</param>
        /// <response code="200">States successfully retrieved.</response>
        /// <response code="404">Item was not found.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("nextstate")]
        [HttpGet]
        public IActionResult GetItemNextStates([FromQuery(Name = "itemid")] long? itemid, [FromQuery(Name = "barcode")] string barcode)
        {
            if (itemid != null)         // Check if searching via ID
            {
                return Ok(_itemService.GetItemNextStates((long)itemid));
            }
            else if(barcode != null)    // Check if searching via barcode
            {
                return Ok(_itemService.GetItemNextStatesByBarcode(barcode));
            }
            
            // Search parameters are not correct or non existing
            return BadRequest("You have to enter a valid itemid or barcode as a query parameter.");
        }

        /// <summary>Changes the state of all the items in the input. Takes in a list of ItemStateChangeInputIdScanner.</summary>
        /// <response code="200">All items have been updated</response>
        /// <response code="202">Partial success, some inputs are invalid. A list of invalid inputs are returned.</response>
        /// <response code="404">Input was not valid, no changes were made.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("scanner/statechangebyid")]
        [HttpPatch]
        public IActionResult ChangeItemStateByIdScanner([FromBody] List<ItemStateChangeInputIdScanner> stateChanges)
        {
            var invalidInput = _itemService.ChangeItemStateByIdScanner(stateChanges);

            if(invalidInput.Any()) { return Accepted(invalidInput); }

            return Ok();
        }

        /// <summary>Changes the state of all the items in the input. Takes in a list of ItemStateChangeBarcodeScanner.</summary>
        /// <response code="200">All items have been updated.</response>
        /// <response code="202">Partial success, some inputs are invalid. A list of invalid inputs are returned.</response>
        /// <response code="404">Input was not valid, no changes were made.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("scanner/statechangebybarcode")]
        [HttpPatch]
        public IActionResult ChangeItemStateBarcodeScanner([FromBody] List<ItemStateChangeBarcodeScanner> stateChanges)
        {
            var invalidInput = _itemService.ChangeItemStateBarcodeScanner(stateChanges);

            if(invalidInput.Any()) { return Accepted(invalidInput); }

            return Ok();
        }
    }
}