using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface IItemService
    {
        /// <summary>Searches for the given barcode.</summary>
        /// <returns>An item state DTO.</returns>
        ItemStateDTO GetItemById(long itemId);

        /// <summary>Updates an item with the EditItemInput model, will not edit empty fields.</summary>
        void EditItem(EditItemInput input, long itemId);

        /// <summary>Searches for the given barcode.</summary>
        /// <returns>An item state DTO.</returns>
        ItemStateDTO SearchItem(string search);

        /// <summary>Sets an item to complete state</summary>
        void CompleteItem(long id);

        /// <summary>Removes Item with the given ID</summary>
        void RemoveItem(long itemId);

        /// <summary>Removes item with the given barcode</summary>
        void RemoveItemQuery(string barcode);

        /// <summary>Changes the state of all items in the input with item ID</summary>
        /// <returns>List of inputs that are not correct and did not update, if any</returns>
        List<ItemStateChangeInputModel> ChangeItemStateById(List<ItemStateChangeInputModel> stateChanges);

        /// <summary>Changes the state of all items in the input with item barcode"</summary>
        /// <returns>List of inputs that are not correct and did not update, if any</returns>
        List<ItemStateChangeBarcodeInputModel> ChangeItemStateBarcode(List<ItemStateChangeBarcodeInputModel> stateChanges);

        /// <summary>Gets the current state of Item and next availables states</summary>
        /// <returns>Current state and a list of next states</returns>
        NextStatesDTO GetItemNextStates(long id);
    }
}