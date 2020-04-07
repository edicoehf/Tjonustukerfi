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
    }
}