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
    }
}