using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IItemRepo
    {
        /// <summary>Searches for the given barcode.</summary>
        /// <returns>An item state DTO.</returns>
        ItemStateDTO GetItemById(long itemId);

        ItemDTO CreateItem(ItemInputModel item);

        /// <summary>Updates an item with the EditItemInput model, will not edit empty fields.</summary>
        void EditItem(EditItemInput input, long itemId);
        
        /// <summary>Searches for the given barcode.</summary>
        /// <returns>Order ID</returns>
        long SearchItem(string search);

        /// <summary>Sets an item to complete state.</summary>
        void CompleteItem(long id);

         /// <summary>Removes Item with the given ID.</summary>
        void RemoveItem(long itemId);

        /// <summary>Changes the state of all items in the input with item ID.</summary>
        List<ItemStateChangeInputModel> ChangeItemStateById(List<ItemStateChangeInputModel> stateChanges);

        /// <summary>Gets the item entity by ID.</summary>
        /// <returns>Item entity.</returns>
        Item GetItemEntity(long id);
    }
}