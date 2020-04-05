using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IItemRepo
    {
        ItemDTO CreateItem(ItemInputModel item);

        /// <summary>Updates an item with the EditItemInput model, will not edit empty fields.</summary>
        void EditItem(EditItemInput input, long itemId);
        
        /// <summary>Searches for the given barcode.</summary>
        /// <returns>An Item State DTO.</returns>
        ItemStateDTO SearchItem(string search);
    }
}