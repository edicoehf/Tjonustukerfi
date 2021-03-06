using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThjonustukerfiWebAPI.Extensions;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;

namespace ThjonustukerfiWebAPI.Repositories.Implementations
{
    public class ItemRepo : IItemRepo
    {
        private DataContext _dbContext;
        private IMapper _mapper;
        public ItemRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public ItemDTO GetItemById(long itemId)
        {
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == itemId);   // get entity
            if(entity == null) {throw new NotFoundException($"Item with ID {itemId} was not found."); } // entity not found

            var dto = _mapper.Map<ItemDTO>(entity);
            // Get the connections for the DTO
            dto.OrderId = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id).OrderId;   // get order ID
            dto.Category = _dbContext.Category.FirstOrDefault(c => c.Id == entity.CategoryId).Name;                // get category name
            dto.Service = _dbContext.Service.FirstOrDefault(s => s.Id == entity.ServiceId).Name;                   // get service name
            dto.State = _dbContext.State.FirstOrDefault(s => s.Id == entity.StateId).Name;                         // get state name

            return dto;
        }

        public void EditItem(EditItemInput input, long itemId)
        {
            // search for entity, handle if not found
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == itemId);
            if(entity == null) { throw new NotFoundException($"Item with ID {itemId} was not found."); }

            bool editState, editService, editOrder, editCategory, checkOrderComplete, editSlice, editFilleted, editOtherCategory, editOtherService, editDetails;
            editState = editService = editOrder = editCategory = checkOrderComplete = editSlice = editFilleted = editOtherCategory = editOtherService = editDetails = false;
            long orderID = -1;

            // finish all checks before editing anything, unfilled inputs will not be edited
            if(input.StateId != null)
            {
                if(_dbContext.State.FirstOrDefault(s => s.Id == input.StateId) == null) { throw new NotFoundException($"State with ID {input.StateId} was not found."); }

                // get final state ID
                var finalStateID = _dbContext.ServiceState.Where(ss => ss.ServiceId == entity.ServiceId).OrderByDescending(ss => ss.Step).FirstOrDefault().StateId;

                // Negation of => Dont change if already at final step and changing to final step
                if(input.StateId != finalStateID || entity.StateId != finalStateID) { editState = true; }
            }

            if(input.ServiceID != null)
            {
                if(_dbContext.Service.FirstOrDefault(s => s.Id == input.ServiceID) == null) { throw new NotFoundException($"Service with ID {input.ServiceID} was not found."); }
                editService = true;
            }

            if(input.OrderId != null)
            {
                if(_dbContext.Order.FirstOrDefault(o => o.Id == input.OrderId) == null) { throw new NotFoundException($"Order with ID {input.OrderId} was not found."); }
                editOrder = true;
            }
            if(input.CategoryId != null)
            {
                if(_dbContext.Category.FirstOrDefault(t => t.Id == input.CategoryId) == null) { throw new NotFoundException($"Category with ID {input.CategoryId} was not found."); }
                editCategory = true;
            }
            //TODO: Reykofninn specific functions with json
            if(input.Sliced != null) { editSlice = true; }
            if(input.Filleted != null) { editFilleted = true; }
            if(input.OtherCategory != null) { editOtherCategory = true; }
            if(input.OtherService != null) { editOtherService = true; }
            if(input.Details != null) { editDetails = true; }

            // Update Category
            if(editCategory) { entity.CategoryId = (long)input.CategoryId; }

            // check and update JSON objects
            JObject rss = JObject.Parse(entity.JSON);
            if(editSlice)           { rss.Property("sliced").Value = input.Sliced; }
            if(editFilleted)        { rss.Property("filleted").Value = input.Filleted; }
            if(editOtherCategory)   { rss.Property("otherCategory").Value = input.OtherCategory; }
            if(editOtherService)    { rss.Property("otherService").Value = input.OtherService; }

            // Update State, also check if location needs to be updated
            if(editState) 
            {
                entity.StateId = (long)input.StateId; 

                // Create timestamp
                _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(entity));

                orderID = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId).OrderId;   // get the id of the connected order
                checkOrderComplete = true;  // state has changed, must check if whole order is complete

                // get all steps for this order in ascending order
                var steps = _dbContext.ServiceState.Where(ss => ss.ServiceId == entity.ServiceId).OrderBy(ss => ss.Step).ToList();
                var currStep = steps.FirstOrDefault(s => s.StateId == entity.StateId).Step; // get item current step

                //TODO: Reykofninn specific since first step has no location
                // set location to empty if first or last step
                if(currStep == steps.Min(s => s.Step) || currStep == steps.Max(s => s.Step)) { rss.Property("location").Value = ""; }
            }

            entity.JSON = JsonConvert.SerializeObject(rss);

            // Update Service
            if(editService) { entity.ServiceId = (long)input.ServiceID; }

            // Update order
            if(editOrder)
            {
                var connection = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId);    // get the item order connection
                connection.OrderId = (long)input.OrderId;   // update the connection to the order
            }

            // Update details
            if(editDetails) { entity.Details = input.Details; }

            // If no changes are made, send a bad request response
            if(!editCategory && !editState && !editService && !editOrder && !editDetails && !editSlice && !editFilleted && !editOtherCategory && !editOtherService) 
            {
                throw new BadRequestException($"The input had no valid values. No changes made.");
            }
            else 
            {
                // item and the order connected to it modified on this date
                entity.DateModified = DateTime.Now;

                if(editOrder)
                {
                      // get the order that was changed
                    _dbContext.Order.FirstOrDefault(o => o.Id == (long)input.OrderId).DateModified = DateTime.Now;   // Update the date modified attribute in the order connected to this item
                }
                else
                {
                    _dbContext.Order.FirstOrDefault(o =>
                        o.Id == _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id).OrderId)  // find order connected to this item
                            .DateModified = DateTime.Now;   // Update the date modified attribute in the order connected to this item
                }
            }

            _dbContext.SaveChanges();

            //  If the state was changed, check if the order is complete
            if(checkOrderComplete && orderID != -1) { SetOrderCompleteStatus(new List<long>() { orderID }); }
        }

        public long SearchItem(string search)
        {
            // Get entity
            var entity = _dbContext.Item.FirstOrDefault(i => i.Barcode == search);

            // Entity not found
            if(entity == null) { throw new NotFoundException($"Item with barcode {search} was not found."); }

            return entity.Id;
        }

        public bool CompleteItem(long id)
        {
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == id);
            if(entity == null) { throw new NotFoundException($"Item with id {id} was not found."); }

            // Gets max (final) step and then the relevant stateID of the step
            var finalStateID = _dbContext.ServiceState.Where(ss => ss.ServiceId == entity.ServiceId).OrderByDescending(ss => ss.Step).FirstOrDefault().StateId;
            if(entity.StateId == finalStateID) { return false; }  // already at complete state, do nothing

            entity.StateId = finalStateID;
            entity.DateModified = DateTime.Now;
            entity.DateCompleted = DateTime.Now;

            // Create timestamp
            _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(entity));

            // Update date modified of order connected to this
            var orderId = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id).OrderId;

            _dbContext.Order.FirstOrDefault(o => o.Id == orderId)
                .DateModified = DateTime.Now;

            _dbContext.SaveChanges();

            SetOrderCompleteStatus(new List<long>() { orderId });

            // item updated
            return true;
        }

        public long RemoveItem(long itemId)
        {
            // Get entity
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == itemId);
            if(entity == null) { throw new NotFoundException($"Item with ID {itemId} was not found. "); }

            // Get connection and remove the connection
            var itemOrderConnection = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id);
            long orderId = -1;
            if(itemOrderConnection != null) 
            {
                _dbContext.ItemOrderConnection.Remove(itemOrderConnection);
                orderId = itemOrderConnection.OrderId;
            }

            // Get the timestamp and remove it
            var itemTimestamps = _dbContext.ItemTimestamp.Where(ts => ts.ItemId == entity.Id).ToList();
            if(itemTimestamps != null) { _dbContext.RemoveRange(itemTimestamps); }

            // Remove entity
            _dbContext.Remove(entity);

            // Update date modified of order connected to this
            if(orderId != -1)
            {
                _dbContext.Order.FirstOrDefault(o => o.Id == orderId)
                    .DateModified = DateTime.Now;
            }

            _dbContext.SaveChanges();

            // Check if the order is complete
            if(orderId != -1) { SetOrderCompleteStatus(new List<long>() { orderId }); }

            return orderId;
        }

        public List<ItemStateChangeInput> ChangeItemState(List<ItemStateChangeInput> stateChanges)
        {
            var invalidInputs = new List<ItemStateChangeInput>();

            // Gets invalid inputs if there are any
            foreach (var item in stateChanges)
            {
                // Check that item IDs are valid
                if(_dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId) == null) { invalidInputs.Add(item); }
                // check if all state changes are valid
                else if(_dbContext.State.FirstOrDefault(s => s.Id == item.StateChangeTo) == null) { invalidInputs.Add(item); }
            }

            // remove Items that are invalid
            stateChanges.RemoveExisting(invalidInputs);

            // if all inputs are invalid 
            if(!stateChanges.Any()) { throw new NotFoundException($"Input not valid, no changes made."); }

            var ordersToCheck = new List<long>();   // All orders to check that are connected to the items being modified

            foreach (var item in stateChanges)
            {
                // change the state and check all things needed, if item is changed then check order
                if(changeState((long)item.ItemId, (long)item.StateChangeTo, item.Location))
                {
                    // add order ID if it has not been added before
                    ordersToCheck.AddIfUnique(_dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == item.ItemId).OrderId);
                }

            }

            _dbContext.SaveChanges();

            SetOrderCompleteStatus(ordersToCheck);

            return invalidInputs;
        }

        public List<ItemStateChangeInputIdScanner> ChangeItemStateByIdScanner(List<ItemStateChangeInputIdScanner> stateChanges)
        {
            var invalidInputs = new List<ItemStateChangeInputIdScanner>();
            // Gets invalid inputs if any
            foreach (var item in stateChanges)
            {
                var stateName = item.StateChangeBarcode.Split("-").FirstOrDefault();    // gets the first part of the bracode (state name)
                // check if all item IDs are valid
                if(_dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId) == null) { invalidInputs.Add(item); }
                // check if all state changes are valid
                else if(_dbContext.State.FirstOrDefault(s => s.Name == stateName) == null) { invalidInputs.Add(item); }
            }

            // Remove items that are invalid
            stateChanges.RemoveExisting(invalidInputs);

            // If all inputs are invalid
            if(!stateChanges.Any()) { throw new NotFoundException("Input not valid, no changes made."); }

            var ordersToCheck = new List<long>();   // All orders to check that are connected to the items being modified

            foreach (var item in stateChanges)
            {
                // parse the barcode - first item is state, second item is location
                var parsedBarcode = item.StateChangeBarcode.Split("-");

                // change the state and check all things neededm, if item is changed then check order
                if(changeState((long)item.ItemId, _dbContext.State.FirstOrDefault(s => s.Name == parsedBarcode[0]).Id, parsedBarcode[1]))
                {
                    // add order ID if it has not been added before
                    ordersToCheck.AddIfUnique(_dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == item.ItemId).OrderId);
                }

            }

            _dbContext.SaveChanges();

            SetOrderCompleteStatus(ordersToCheck);

            return invalidInputs;
        }

        public Item GetItemEntity(long id)
        {
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == id);
            if(entity == null) { throw new NotFoundException($"Item with ID {id} was not found."); }

            return entity;
        }

        public string GetItemBarcodeById(long id)
        {
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == id);
            if(entity == null) { throw new NotFoundException($"Item with ID {id} was not found."); }

            return entity.Barcode;
        }

        public long GetOrderIdWithItemId(long itemId)
        {
            var IoConnection = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId);
            if(IoConnection == null) { throw new NotFoundException($"Can't find order connected to the item with item ID {itemId}."); }

            return IoConnection.OrderId;
        }

        public ItemPrintDetailsDTO GetItemPrintDetails(long itemId)
        {
            // gets item dto
            var item = GetItemById(itemId);

            // maps item dto to item print details
            return _mapper.Map<ItemPrintDetailsDTO>(item);
        }

        //*     Helper functions     *//

        /// <summary>Changtes the state of an item, location is only changed if provided. The state ID is the state that the item should be changed to.</summary>
        private bool changeState(long itemId, long stateId, string location = null)
        {
            var timeNow = DateTime.Now;

            // get entity
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == itemId);

            // find the final-step-id
            var serviceSteps =  _dbContext.ServiceState.Where(ss => ss.ServiceId == entity.ServiceId)
                                    .OrderByDescending(ss => ss.Step).ToList();  // get all service steps
            
            var StateComplete = serviceSteps.FirstOrDefault().StateId;  // get final step's state ID
            if(stateId == StateComplete && entity.StateId == StateComplete) { return false; }       // if changing from final state to final state do nothing

            // update the entity itself
            entity.StateId = stateId;
            entity.DateModified = DateTime.Now;

            var currStep = serviceSteps.FirstOrDefault(ss => ss.StateId == entity.StateId).Step;                // get current step of state change
            var maxStep = serviceSteps.Max(ss => ss.Step);                                                      // Find max step
            var minStep = serviceSteps.Min(ss => ss.Step);                                                      // Find min step

            // is at last step
            if(currStep == maxStep) { entity.DateCompleted = timeNow; }
            else { entity.DateCompleted = null; }

            //TODO: Min step with location empty string, is company specific
            // Get the json object and change it and write it back (making sure to only change the location property if there are any other properties there)
            if(entity.JSON != null && location != null && currStep != minStep && currStep != maxStep)
            {
                JObject rss = JObject.Parse(entity.JSON);           // parse the entity
                var prop = rss.Property("location");                // get the location property
                prop.Value = location;                              // set the location
                entity.JSON = JsonConvert.SerializeObject(rss);     // serialize back
            }
            else if(entity.JSON != null && (currStep == minStep || currStep == maxStep)) // set location to empty string if in first or last step
            {
                JObject rss = JObject.Parse(entity.JSON);           // parse the entity
                var prop = rss.Property("location");                // get the location property
                prop.Value = "";                                    // set the location
                entity.JSON = JsonConvert.SerializeObject(rss);     // serialize back
            }

            // Update/create timestamp
            _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(entity));

            // updated item return true
            return true;
        }

        /// <summary>Sets all orders in list date completed, only if order is complete, else it sets it to null</summary>
        private void SetOrderCompleteStatus(List<long> orders)
        {
            // check all order statuses
            foreach (var order in orders)
            {
                var entity = _dbContext.Order.FirstOrDefault(o => o.Id == order);

                if (IsOrderComplete(entity.Id)) { entity.DateCompleted = DateTime.Now; }
                else { entity.DateCompleted = null; }
            }

            _dbContext.SaveChanges();
        }

        /// <summary>Checks if an order is complete</summary>
        /// <returns>True if all items in order are in complete state</returns>
        private bool IsOrderComplete(long orderId)
        {
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);
            var connection = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderId).ToList();

            // list is empty, return false
            if(!connection.Any()) { return false; }

            foreach (var itemConection in connection)
            {
                var item = _dbContext.Item.FirstOrDefault(i => i.Id == itemConection.ItemId);
                var finalState = _dbContext.ServiceState.Where(ss => ss.ServiceId == item.ServiceId).OrderByDescending(ss => ss.Step).FirstOrDefault().StateId;
                // Return false if any item is not complete
                if(item.StateId != finalState) { return false; }
            }

            return true;
        }
    }
}