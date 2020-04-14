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
        void ChangeItemState(List<ItemStateChangeInputModel> stateChanges);

        /// <summary>Changes the state of all items in the input with item barcode"</summary>
        void ChangeItemStateBarcode(List<ItemStateChangeBarcodeInputModel> stateChanges);
    }
}