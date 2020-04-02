using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface IItemService
    {
        /// <summary>Updates an item with the EditItemInput model, will not edit empty fields.</summary>
        void EditItem(EditItemInput input, long itemId);

        /// <summary>Searches for the given barcode</summary>
        ItemStateDTO SearchItem(string search);
    }
}