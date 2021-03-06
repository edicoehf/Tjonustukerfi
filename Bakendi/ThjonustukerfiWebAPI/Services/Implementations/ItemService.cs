using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AutoMapper;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Extensions;
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
        private IOrderRepo _orderRepo;
        private ICustomerRepo _customerRepo;
        private IMapper _mapper;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ItemService));

        public ItemService(IItemRepo itemRepo, IInfoRepo infoRepo, IOrderRepo orderRepo, ICustomerRepo customerRepo, IMapper mapper)
        {
            _itemRepo = itemRepo;
            _infoRepo = infoRepo;
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }
        public ItemDTO GetItemById(long itemId) => _itemRepo.GetItemById(itemId);
        public void EditItem(EditItemInput input, long itemId)
        {
            _itemRepo.EditItem(input, itemId);  // edit item
            checkOrderPickupAndSend(_itemRepo.GetOrderIdWithItemId(itemId));  // send Notification if order is ready
        }
        public ItemDTO SearchItem(string search) => _itemRepo.GetItemById(_itemRepo.SearchItem(search));
        public void CompleteItem(long id)
        {
            if(_itemRepo.CompleteItem(id))  // only check if item was changed
            {
                checkOrderPickupAndSend(_itemRepo.GetOrderIdWithItemId(id));    // send Notification if order is ready
            }
        }
        public void RemoveItem(long itemId)
        {
            var orderId = _itemRepo.RemoveItem(itemId);
            checkOrderPickupAndSend(orderId);  // send Notification if order is ready
        }
        public void RemoveItemQuery(string barcode)
        {
            var orderId = _itemRepo.RemoveItem(_itemRepo.SearchItem(barcode));
            checkOrderPickupAndSend(orderId);  // send Notification if order is ready
        }
        public List<ItemStateChangeInput> ChangeItemState(List<ItemStateChangeInput> stateChanges)
        {
            var invalidInputs = _itemRepo.ChangeItemState(stateChanges);

            // remove invalid inputs from the list
            stateChanges.RemoveExisting(invalidInputs);

            // Only check each order once, don't want to send a notification for each item
            var ordersToCheck = new List<long>();
            foreach (var item in stateChanges)
            {
                ordersToCheck.AddIfUnique(_itemRepo.GetOrderIdWithItemId((long)item.ItemId));
            }

            // check all valid orders
            foreach (var orderId in ordersToCheck)
            {
                checkOrderPickupAndSend(orderId);  // send Notification if order is ready
            }

            return invalidInputs;
        }
        public List<ItemStateChangeInputIdScanner> ChangeItemStateByIdScanner(List<ItemStateChangeInputIdScanner> stateChanges)
        {
            var invalidInputs = _itemRepo.ChangeItemStateByIdScanner(stateChanges);

            // remove invalidInputes from the list
            stateChanges.RemoveExisting(invalidInputs);

            // only check each order once, do not send a notification for each item
            var ordersToCheck = new List<long>();
            foreach (var item in stateChanges)
            {
                ordersToCheck.AddIfUnique(_itemRepo.GetOrderIdWithItemId((long)item.ItemId));
            }

            // check all valid orders
            foreach (var orderId in ordersToCheck)
            {
                checkOrderPickupAndSend(orderId);  // send Notification if order is ready
            }

            return invalidInputs;
        }
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

            // update the items, returns any and all inputs that are not correct. This function will send notifications to orders ready to be picked up
            var invalidInputStates = ChangeItemStateByIdScanner(stateChangesWithId);

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

        // Get the ID and use get by ID
        public NextStatesDTO GetItemNextStatesByBarcode(string barcode) => GetItemNextStates(_itemRepo.SearchItem(barcode));

        public ItemPrintDetailsDTO GetItemPrintDetails(long itemId) => _itemRepo.GetItemPrintDetails(itemId);

        //*        Helper Functions        *//
        /// <summary>Checks if the order connected to Item is ready for pickup and sends a notification to the customer</summary>
        private void checkOrderPickupAndSend(long orderId)
        {
            if (_orderRepo.OrderPickupReady(orderId))
            {
                var order    = _orderRepo.GetOrderById(orderId);
                var customer = _customerRepo.GetCustomerById(order.CustomerId);

                if(Constants.sendEmail) { MailService.sendOrderComplete(order, customer); }

                if(Constants.sendSMS)
                {
                    if (!string.IsNullOrEmpty(customer.Phone))
                    {
                        SMSService.sendOrderCompleteSMS(order, customer);
                    }
                    else
                    {
                        _log.Error("No Phone number registered for customer " + customer.Name);
                    }
                }
            }
        }
    }
}