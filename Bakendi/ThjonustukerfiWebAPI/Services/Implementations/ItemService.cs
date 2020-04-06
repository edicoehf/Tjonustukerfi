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
        public ItemDTO CreateItem(ItemInputModel item)
        {
            return _itemRepo.CreateItem(item);
        }
        public void EditItem(EditItemInput input, long itemId) => _itemRepo.EditItem(input, itemId);
        public ItemStateDTO SearchItem(string search) => _itemRepo.SearchItem(search);
        public void FinishItem(long id) => _itemRepo.FinishItem(id);
    }
}