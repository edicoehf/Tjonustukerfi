using System;
using System.Linq;
using AutoMapper;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

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

        public ItemStateDTO GetItemById(long itemId)
        {
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == itemId);   // get entity
            if(entity == null) {throw new NotFoundException($"Item with ID {itemId} was not found."); } // entity not found

            // Map the DTO
            var stateDTO = _mapper.Map<ItemStateDTO>(entity);

            // Get the connections for the DTO, order id it belongs to and in what state it is
            stateDTO.OrderId = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id).OrderId;
            stateDTO.State = _dbContext.State.FirstOrDefault(s => s.Id == entity.StateId).Name;

            return stateDTO;
        }

        public ItemDTO CreateItem(ItemInputModel item)
        {
            // Mapping from input to entity and adding to database
            var entity = _dbContext.Item.Add(_mapper.Map<Item>(item)).Entity;
            _dbContext.SaveChanges();
            // Mapping from entity to DTO
            return _mapper.Map<ItemDTO>(entity);
        }

        public void EditItem(EditItemInput input, long itemId)
        {
            // search for entity, handle if not found
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == itemId);
            if(entity == null) { throw new NotFoundException($"Item with ID {itemId} was not found."); }

            bool editState, editService, editOrder;
            editState = editService = editOrder = false;

            // finish all checks before editing anything, unfilled inputs will not be edited
            if(input.StateId != null)
            {
                if(_dbContext.State.FirstOrDefault(s => s.Id == input.StateId) == null) { throw new NotFoundException($"State with ID {input.StateId} was not found."); }
                editState = true;
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

            if(input.Type != null) { entity.Type = input.Type; }  // just edit if not empty
            if(editState) { entity.StateId = (long)input.StateId; }
            if(editService) { entity.ServiceId = (long)input.ServiceID; }
            if(editOrder)
            {
                var connection = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == itemId);
                connection.OrderId = (long)input.OrderId;
            }

            // If no changes are made, send a bad request response
            if(input.Type == null && !editState && !editService && !editOrder) {throw new BadRequestException($"The input had no valid values. No changes made."); }
            else 
            {
                // item and the order connected to it modified on this date
                entity.DateModified = DateTime.Now;

                _dbContext.Order.FirstOrDefault(o =>
                    o.Id == _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id).OrderId)  // find order connected to this item
                        .DateModified = DateTime.Now;
            }

            _dbContext.SaveChanges();
        }

        public long SearchItem(string search)
        {
            // Get entity
            var entity = _dbContext.Item.FirstOrDefault(i => i.Barcode == search);

            // Entity not found
            if(entity == null) { throw new NotFoundException($"Item with barcode {search} was not found."); }

            return entity.Id;
        }

        public void CompleteItem(long id)
        {
            var entity = _dbContext.Item.FirstOrDefault(i => i.Id == id);
            if(entity == null) { throw new NotFoundException($"Item with id {id} was not found."); }

            entity.StateId = 5; //TODO: Prety hardcoded, when config for company ready then maybe make this more general
            entity.DateModified = DateTime.Now;
            entity.DateCompleted = DateTime.Now;

            // Update date modified of order connected to this
            _dbContext.Order.FirstOrDefault(o =>
                    o.Id == _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == entity.Id).OrderId)  // find order connected to this item
                        .DateModified = DateTime.Now;

            _dbContext.SaveChanges();
        }
    }
}