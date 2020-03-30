using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class OrderRepo : IOrderRepo
    {
        private DataContext _dbContext;
        private IMapper _mapper;
        public OrderRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public OrderDTO GetOrderbyId(long id)
        {
            // Get order
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == id);
            // Check if order exists
            if(entity == null) { throw new NotFoundException($"Order with id {id} was not found."); }

            //! many calls to DB :(, suggestion: add order ID in Item, then only need to search id DB once to get all items
            var dto = _mapper.Map<OrderDTO>(entity);
            dto.Customer = _dbContext.Customer.FirstOrDefault(c => c.Id == entity.CustomerId).Name; // get customer name

            // Loop through a list of item order connections where all elements in list have this order ID
            var itemList = _dbContext.ItemOrderConnection.Where(c => c.OrderId == id).ToList();
            dto.Items = new List<ItemDTO>();
            foreach (var item in itemList)
            {
                var itemEntity = _dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId);                  // get item entity
                var add = _mapper.Map<ItemDTO>(itemEntity);                                                 // map to DTO
                add.Service = _dbContext.Service.FirstOrDefault(s => s.Id == itemEntity.ServiceId).Name;    // Find service name
                dto.Items.Add(add);     // add item DTO to orderDTO item list
            }

            return dto;
        }

        public long CreateOrder(OrderInputModel order)
        {
            // TODO replace for actual barcode
            foreach(ItemInputModel item in order.Items)
            {
                if(_dbContext.Service.FirstOrDefault(s => s.Id == item.ServiceId) == null) { throw new NotFoundException($"Service with id {item.ServiceId} was not found."); }
            }
            var orderToAdd = _mapper.Map<Order>(order);
            var barcodeEntry = _dbContext.Order.OrderByDescending(o => o.Barcode).FirstOrDefault();
            if(barcodeEntry == null)
            {
                orderToAdd.Barcode = "20200001";
            }
            else
            {
                var newBarcode = int.Parse(barcodeEntry.Barcode);
                newBarcode++;
                orderToAdd.Barcode = newBarcode.ToString();
            }

            // Get next Id for order and item, make sure the new order has correct id
            long newItemId = 1;
            // throws error if list is empty
            if(_dbContext.Item.Any()) { newItemId = _dbContext.Item.Max(i => i.Id) + 1; }
            
            long newOrderId = 1;
            // throws error if list is empty
            if(_dbContext.Order.Any()) { newOrderId = _dbContext.Order.Max(o => o.Id) + 1; }
            orderToAdd.Id = newOrderId;

            var entity = _dbContext.Order.Add(orderToAdd).Entity;

            // Add items toDatabase
            foreach(ItemInputModel item in order.Items)
            {
                // Creates a custom ID to make sure everything is connected correctly
                var itemToAdd = _mapper.Map<Item>(item);
                itemToAdd.Id = newItemId;
                _dbContext.Item.Add(itemToAdd);

                var itemOrderConnection = new ItemOrderConnection {
                    OrderId = newOrderId,
                    ItemId = newItemId
                };
                
                // Increment itemId for next Item
                newItemId++;
                _dbContext.ItemOrderConnection.Add(itemOrderConnection);
            }

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public void UpdateOrder(OrderInputModel order, long id)
        {
            // find order
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == id);
            if(entity == null) { throw new NotFoundException($"Order with id {id} was not found."); }

            // update the customer ID
            entity.CustomerId = order.CustomerId;

            // Get connections then Items
            var itemListConnections = _dbContext.ItemOrderConnection.Where(c => c.OrderId == entity.Id).ToList();
            var items = new List<Item>();
            foreach (var item in itemListConnections)
            {
                items.Add(_dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
            }

            // Update to be the same as the input
            if(items.Count != 0 && items.Count == order.Items.Count)    // if lists are of same size just update
            {
                for(var i = 0; i < items.Count; i++)
                {
                    items[i].Type = order.Items[i].Type;
                    items[i].ServiceId = order.Items[i].ServiceId;
                }
            }
            else if(items.Count == 0 && order.Items.Count > 0)      // if the list in the database is empty, just add the new ones
            {
                AddMultipleItems(order.Items, entity.Id);
            }
            else if(items.Count < order.Items.Count)    // if adding more items and/or updating to existing list
            {
                // update first items
                var i = 0;
                for( ; i < items.Count; i++)
                {
                    items[i].Type = order.Items[i].Type;
                    items[i].ServiceId = order.Items[i].ServiceId;
                }

                // Build the rest of the list
                var tmpList = new List<ItemInputModel>();
                for( ; i < order.Items.Count; i++)
                {
                    tmpList.Add(order.Items[i]);
                }

                // Add the rest of the items
                AddMultipleItems(tmpList, entity.Id);
            }
            else if(items.Count > order.Items.Count)    // if some items are removed from the list
            {
                // Update the list up to the same size
                var i = 0;
                for( ; i < order.Items.Count; i++)
                {
                    items[i].Type = order.Items[i].Type;
                    items[i].ServiceId = order.Items[i].ServiceId;
                }

                // rest of the items to delete off the tail
                var itemListToDelete = new List<Item>();
                for( ; i < items.Count; i++)
                {
                    itemListToDelete.Add(items[i]);
                }

                // get the connection table variables that connect the order with the items about to be removed
                var itemConnectionListToDelete = new List<ItemOrderConnection>();
                foreach (var item in itemListToDelete)
                {
                    itemConnectionListToDelete.Add(_dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == item.Id));
                }

                // remove both items and their order connections
                _dbContext.Item.RemoveRange(itemListToDelete);
                _dbContext.ItemOrderConnection.RemoveRange(itemConnectionListToDelete);
            }

            // save the changes
            _dbContext.SaveChanges();
        }

        //! Doesn't do SaveChanges(), rember to use save changes after calling this function
        private void AddMultipleItems(List<ItemInputModel> inpItems, long orderId)
        {
            long newItemId = _dbContext.Item.Max(i => i.Id) + 1;
            // Add items toDatabase
            foreach(var item in inpItems)
            {
                // Creates a custom ID to make sure everything is connected correctly
                var itemToAdd = _mapper.Map<Item>(item);
                itemToAdd.Id = newItemId;
                _dbContext.Item.Add(itemToAdd);

                var itemOrderConnection = new ItemOrderConnection {
                    OrderId = orderId,
                    ItemId = newItemId
                };
                
                // Increment itemId for next Item
                newItemId++;
                _dbContext.ItemOrderConnection.Add(itemOrderConnection);
            }
        }

        public void DeleteByOrderId(long id)
        {
            // find order
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == id);
            if(entity == null) { throw new NotFoundException($"Order with id {id} was not found"); }

            // Get list connections
            var itemListConnections = _dbContext.ItemOrderConnection.Where(c => c.OrderId == entity.Id).ToList();
            
            // Get all items in order
            var itemList = new List<Item>();
            foreach (var item in itemListConnections)
            {
                itemList.Add(_dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
            }

            // remove items
            if(itemList.Count > 0) { _dbContext.Item.RemoveRange(itemList); }
            // remove connections
            if(itemListConnections.Count > 0) { _dbContext.ItemOrderConnection.RemoveRange(itemListConnections); }
            // remove entity
            _dbContext.Remove(entity);

            _dbContext.SaveChanges();
        }
    }
}