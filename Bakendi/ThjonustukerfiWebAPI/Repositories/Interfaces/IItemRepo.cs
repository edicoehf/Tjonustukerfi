using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    /// <summary>Repository for accessing the Database for Items.</summary>
    public interface IItemRepo
    {
        /// <summary>Searches for the given barcode.</summary>
        /// <returns>An item state DTO.</returns>
        ItemDTO GetItemById(long itemId);

        /// <summary>Updates an item with the EditItemInput model, will not edit empty fields.</summary>
        void EditItem(EditItemInput input, long itemId);
        
        /// <summary>Searches for the given barcode.</summary>
        /// <returns>Order ID</returns>
        long SearchItem(string search);

        /// <summary>Sets an item to complete state.</summary>
        bool CompleteItem(long id);

        /// <summary>Removes Item with the given ID.</summary>
        /// <returns>Returns the ID of the order that the item was removed from</returns>
        long RemoveItem(long itemId);

        /// <summary>Changes the state of all items in the input</summary>
        /// <returns>An list of invalid inputs</returns>
        List<ItemStateChangeInput> ChangeItemState(List<ItemStateChangeInput> stateChanges);

        /// <summary>Changes the state of all items in the input with item ID.</summary>
        List<ItemStateChangeInputIdScanner> ChangeItemStateByIdScanner(List<ItemStateChangeInputIdScanner> stateChanges);

        /// <summary>Gets the item entity by ID.</summary>
        /// <returns>Item entity.</returns>
        Item GetItemEntity(long id);

        /// <summary>Gets the items barcode with given ID</summary>
        /// <returns>Barcode in string format</returns>
        string GetItemBarcodeById(long id);

        /// <summary>Gets orderId with itemId</summary>
        /// <returns>OrderId (long)</returns>
        long GetOrderIdWithItemId(long itemId);

        /// <summary> Gets item information from printing.</summary>
        /// <returns>DTO that has item info and image of barcode as a byte array.</returns>
        ItemPrintDetailsDTO GetItemPrintDetails(long itemId);
    }
}