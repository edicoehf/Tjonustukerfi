using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class ItemService : IItemService
    {
        private IItemRepo _itemRepo;
        public ItemService(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }
        public ItemStateDTO GetItemById(long itemId) => _itemRepo.GetItemById(itemId);
        public ItemDTO CreateItem(ItemInputModel item)
        {
            return _itemRepo.CreateItem(item);
        }
        public void EditItem(EditItemInput input, long itemId) => _itemRepo.EditItem(input, itemId);
        public ItemStateDTO SearchItem(string search) => _itemRepo.GetItemById(_itemRepo.SearchItem(search));
        public void CompleteItem(long id) => _itemRepo.CompleteItem(id);
        public void RemoveItem(long itemId) => _itemRepo.RemoveItem(itemId);
        public void RemoveItemQuery(string barcode) => _itemRepo.RemoveItem(_itemRepo.SearchItem(barcode));
        public void ChangeItemState(List<ItemStateChangeInputModel> stateChanges) { _itemRepo.ChangeItemState(stateChanges); }
        public void ChangeItemStateBarcode(List<ItemStateChangeBarcodeInputModel> stateChanges)
        {
            // create list with IDs in stead of barcode
            var stateChangesWithId = new List<ItemStateChangeInputModel>();
            foreach (var change in stateChanges)
            {
                // add with correct ID
                stateChangesWithId.Add(new ItemStateChangeInputModel
                {
                    ItemId = _itemRepo.SearchItem(change.Barcode),
                    StateChangeTo = change.StateChangeTo
                });
            }

            // update the items
            _itemRepo.ChangeItemState(stateChangesWithId);
        }
    }
}