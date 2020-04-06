using ThjonustukerfiWebAPI.Models.DTOs;
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

        /// <summary>Sets an item to complete state</summary>
        void CompleteItem(long id);
    }
}