using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class ItemService : IItemService
    {
        private IItemRepo _itemRepo;
        private IInfoRepo _infoRepo;
        private IMapper _mapper;
        public ItemService(IItemRepo itemRepo, IInfoRepo infoRepo, IMapper mapper)
        {
            _itemRepo = itemRepo;
            _infoRepo = infoRepo;
            _mapper = mapper;
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
        public List<ItemStateChangeInput> ChangeItemState(List<ItemStateChangeInput> stateChanges) => _itemRepo.ChangeItemState(stateChanges);
        public List<ItemStateChangeInputIdScanner> ChangeItemStateByIdScanner(List<ItemStateChangeInputIdScanner> stateChanges) => _itemRepo.ChangeItemStateByIdScanner(stateChanges);
        public List<ItemStateChangeBarcodeScanner> ChangeItemStateBarcodeScanner(List<ItemStateChangeBarcodeScanner> stateChanges)
        {
            var invalidInputs = new List<ItemStateChangeBarcodeScanner>();
            // create list with IDs in stead of barcode
            var stateChangesWithId = new List<ItemStateChangeInputIdScanner>();
            foreach (var change in stateChanges)
            {
                long itemId;

                // If barcode is invalid, add it to invalid list. Will not add to ID list if barcode is invalid
                try 
                {
                    itemId = _itemRepo.SearchItem(change.ItemBarcode);

                    // add with correct ID
                    stateChangesWithId.Add(new ItemStateChangeInputIdScanner
                    {
                        ItemId = itemId,
                        StateChangeBarcode = change.StateChangeBarcode
                    });
                }
                catch (NotFoundException) { invalidInputs.Add(change); }
            }

            // update the items, returns any and all inputs that are not correct
            var invalidInputStates = _itemRepo.ChangeItemStateByIdScanner(stateChangesWithId);

            // Put the invalid inputs together and get the correct barcode
            foreach (var item in invalidInputStates)
            {
                var add = _mapper.Map<ItemStateChangeBarcodeScanner>(item);
                add.ItemBarcode = _itemRepo.GetItemBarcodeById((long)item.ItemId);  // get the barcode

                invalidInputs.Add(add);
            }

            return invalidInputs;
        }
        public NextStatesDTO GetItemNextStates(long id)
        {
            // Get entity
            var entity = _itemRepo.GetItemEntity(id);

            // Get the current and next states
            var nextStateDto = new NextStatesDTO();
            nextStateDto.CurrentState = _infoRepo.GetStatebyId(entity.StateId);
            nextStateDto.NextAvailableStates = _infoRepo.GetNextStates(entity.ServiceId, entity.StateId);

            return nextStateDto;
        }

        public NextStatesDTO GetItemNextStatesByBarcode(string barcode)
        {
            // Get the ID and use get by ID
            return GetItemNextStates(_itemRepo.SearchItem(barcode));
        }
    }
}