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
        public List<ItemStateChangeInputModel> ChangeItemStateById(List<ItemStateChangeInputModel> stateChanges) => _itemRepo.ChangeItemStateById(stateChanges);
        public List<ItemStateChangeBarcodeInputModel> ChangeItemStateBarcode(List<ItemStateChangeBarcodeInputModel> stateChanges)
        {
            var invalidInputs = new List<ItemStateChangeBarcodeInputModel>();
            // create list with IDs in stead of barcode
            var stateChangesWithId = new List<ItemStateChangeInputModel>();
            foreach (var change in stateChanges)
            {
                long itemId;

                // If barcode is invalid, add it to invalid list. Will not add to ID list if barcode is invalid
                try 
                {
                    itemId = _itemRepo.SearchItem(change.Barcode);

                    // add with correct ID
                    stateChangesWithId.Add(new ItemStateChangeInputModel
                    {
                        ItemId = itemId,
                        Barcode = change.Barcode,
                        StateChangeTo = change.StateChangeTo
                    });
                }
                catch (NotFoundException) { invalidInputs.Add(change); }
            }

            // update the items, returns any and all inputs that are not correct
            var invalidInputStates = _itemRepo.ChangeItemStateById(stateChangesWithId);

            // Put the invalid inputs together
            invalidInputs.AddRange(_mapper.Map<List<ItemStateChangeBarcodeInputModel>>(invalidInputStates));

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
    }
}