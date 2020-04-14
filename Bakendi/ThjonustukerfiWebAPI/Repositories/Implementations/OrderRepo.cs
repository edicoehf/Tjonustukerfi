using System;
using System.Collections.Generic;
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
                add.State = _dbContext.State.FirstOrDefault(s => s.Id == itemEntity.StateId).Name;          // Find state name
                add.Category = _dbContext.Category.FirstOrDefault(c => c.Id == itemEntity.CategoryId).Name; // Find category name 
                dto.Items.Add(add);     // add item DTO to orderDTO item list
            }

            return dto;
        }

        public long CreateOrder(OrderInputModel order)
        {
            // TODO replace for actual barcode in both order and Item
            foreach(ItemInputModel item in order.Items)
            {
                if(_dbContext.Service.FirstOrDefault(s => s.Id == item.ServiceId) == null) { throw new NotFoundException($"Service with ID {item.ServiceId} was not found."); }
                if(_dbContext.Category.FirstOrDefault(c => c.Id == item.CategoryId) == null) { throw new NotFoundException($"Category with ID {item.CategoryId} was not found."); }
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
            
            long newOrderId = 1;
            // New ID will be 1 if no orders exist
            if(_dbContext.Order.Any()) { newOrderId = _dbContext.Order.Max(o => o.Id) + 1; }
            orderToAdd.Id = newOrderId;

            var entity = _dbContext.Order.Add(orderToAdd).Entity;

            // Add items toDatabase
            AddMultipleItems(order.Items, newOrderId);

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public void UpdateOrder(OrderInputModel order, long id)
        {
            // find order
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == id);
            if(entity == null) { throw new NotFoundException($"Order with id {id} was not found."); }

            // Customer in input not valid
            if(_dbContext.Customer.FirstOrDefault(c => c.Id == order.CustomerId) == null) { throw new NotFoundException($"Customer with ID {order.CustomerId} was not found."); }

            // Make sure that the items that are being added have the correct service and category
            foreach(ItemInputModel item in order.Items)
            {
                if(_dbContext.Service.FirstOrDefault(s => s.Id == item.ServiceId) == null) { throw new NotFoundException($"Service with ID {item.ServiceId} was not found."); }
                if(_dbContext.Category.FirstOrDefault(c => c.Id == item.CategoryId) == null) { throw new NotFoundException($"Category with ID {item.CategoryId} was not found."); }
            }

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
                    items[i].CategoryId = (long)order.Items[i].CategoryId;
                    items[i].ServiceId = (long)order.Items[i].ServiceId;
                    items[i].DateModified = DateTime.Now;
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
                    items[i].CategoryId = (long)order.Items[i].CategoryId;
                    items[i].ServiceId = (long)order.Items[i].ServiceId;
                    items[i].DateModified = DateTime.Now;
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
                    items[i].CategoryId = (long)order.Items[i].CategoryId;
                    items[i].ServiceId = (long)order.Items[i].ServiceId;
                    items[i].DateModified = DateTime.Now;
                }

                // rest of the items to delete off the tail as well as the timestamp
                var itemListToDelete = new List<Item>();
                var timestampsToDelete = new List<ItemTimestamp>();
                for( ; i < items.Count; i++)
                {
                    itemListToDelete.Add(items[i]);
                    timestampsToDelete.AddRange(_dbContext.ItemTimestamp.Where(ts => ts.ItemId == items[i].Id).ToList());    // Get timestamp with item
                }

                // get the connection table variables that connect the order with the items about to be removed
                var itemConnectionListToDelete = new List<ItemOrderConnection>();
                foreach (var item in itemListToDelete)
                {
                    itemConnectionListToDelete.Add(_dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == item.Id));
                }

                // remove timestamps, items and their order connections
                _dbContext.Item.RemoveRange(itemListToDelete);
                _dbContext.ItemTimestamp.RemoveRange(timestampsToDelete);
                _dbContext.ItemOrderConnection.RemoveRange(itemConnectionListToDelete);
            }

            // save the changes
            _dbContext.SaveChanges();
        }

        /// <summary>Gets the next barcode number</summary>
        private string GetItemBarcode()
        {
            string code = "";
            
            // This try-catch block is only used for the tests since in memory Db does not work the same
            // way as a regular Db connection when searching for max value of string
            try { code = _dbContext.Item.Max(i => i.Barcode); }
            catch (System.Exception)
            {
                var maxItem = _dbContext.Item.OrderByDescending(i => i.Barcode).FirstOrDefault();
                code = maxItem.Barcode;
            }

            if(code == null) { code = "50500000"; }

            var barcode = int.Parse(code);
            barcode++;

            return barcode.ToString();

        }

        //! Doesn't do SaveChanges(), remember to use save changes after calling this function
        /// <summary>Used to add multiple items in order input</summary>
        private void AddMultipleItems(List<ItemInputModel> inpItems, long orderId)
        {
            // Sets the ID
            long newItemId = 1;
            if(_dbContext.Item.Any()) { newItemId = _dbContext.Item.Max(i => i.Id) + 1; }
            int newItemBarcode = int.Parse(GetItemBarcode());

            // Add items toDatabase
            foreach(var item in inpItems)
            {
                // Creates a custom ID to make sure everything is connected correctly
                var itemToAdd = _mapper.Map<Item>(item);
                itemToAdd.Id = newItemId;
                itemToAdd.Barcode = newItemBarcode.ToString();
                _dbContext.Item.Add(itemToAdd);

                // Create Timestamp
                _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(itemToAdd));

                var itemOrderConnection = new ItemOrderConnection {
                    OrderId = orderId,
                    ItemId = newItemId
                };
                
                // Increment itemId for next Item
                newItemId++;
                newItemBarcode++;
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
            
            // Get all items in order and timestamp
            var itemList = new List<Item>();
            var itemTimestamps = new List<ItemTimestamp>();
            foreach (var item in itemListConnections)
            {
                itemList.Add(_dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                itemTimestamps.Add(_dbContext.ItemTimestamp.FirstOrDefault(ts => ts.ItemId == item.ItemId));
            }

            // remove items
            if(itemList.Count > 0) { _dbContext.Item.RemoveRange(itemList); }
            // remove timestamps
            if(itemTimestamps.Count > 0) {  _dbContext.ItemTimestamp.RemoveRange(itemTimestamps); }
            // remove connections
            if(itemListConnections.Count > 0) { _dbContext.ItemOrderConnection.RemoveRange(itemListConnections); }
            // remove entity
            _dbContext.Remove(entity);

            _dbContext.SaveChanges();
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var ordersEntity = _dbContext.Order.ToList();
            
            return GetOrderDTOwithOrderList(ordersEntity);
        }

        public void CompleteOrder(long orderId)
        {
            var orderEntity = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);    // find entity
            if(orderEntity == null) { throw new NotFoundException($"Order with ID {orderId} was not found."); }
            
            var currentDate = DateTime.Now; // get current date once to use when updating item and order
            orderEntity.DateCompleted = currentDate;
            orderEntity.DateModified = currentDate;

            var orderItemConnection = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderId).ToList();
            foreach (var item in orderItemConnection)
            {
                var itemToChange = _dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId);
                if(itemToChange == null) { throw new NotFoundException($"Item with ID {item.ItemId} was not found in order with order ID {orderId}."); }
                
                itemToChange.StateId = 5;  //TODO: Same as in complete Item, update id to be more general (e.g. some global enunm or somthing)
                itemToChange.DateCompleted = currentDate;
                itemToChange.DateModified = currentDate;

                // Update the timestamp
                // state and item need to be the same else create a new timestamp
                var timeStamp = _dbContext.ItemTimestamp.FirstOrDefault(ts => ts.ItemId == itemToChange.Id && ts.StateId == itemToChange.StateId);
                if (timeStamp == null) { _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(itemToChange)); }
                else { timeStamp.TimeOfChange = currentDate; }
            }

            _dbContext.SaveChanges();
        }

        public long SearchOrder(string barcode)
        {
            var entity = _dbContext.Order.FirstOrDefault(o => o.Barcode == barcode);
            if(entity == null) { throw new NotFoundException($"Order with barcode {barcode} was not found."); }

            return entity.Id;
        }

        public List<OrderDTO> GetActiveOrdersByCustomerId(long customerId)
        {
            var orders = _dbContext.Order.Where(o => o.CustomerId == customerId).ToList();  // get orders with this customer
            var orderDTOs = GetOrderDTOwithOrderList(orders);   // Get DTO list of orders that has the item connection

            var activeOrders = new List<OrderDTO>();    // Create a new list of active orders

            foreach (var order in orderDTOs)    // loop through all orders
            {
                foreach (var item in order.Items)   // loop through all items in all orders
                {
                    if(item.State != "Sótt")    // If any item is not done, add order to active orders and check next order
                    {
                        activeOrders.Add(order);
                        break;
                    }
                }
            }

            return activeOrders;    // Active orders, empty list if there are none
        }

        private List<OrderDTO> GetOrderDTOwithOrderList(List<Order> ordersEntity)
        {
            var orders = new List<OrderDTO>();

            // If the list is empty, just return it
            if(!ordersEntity.Any()) { return orders; }

            // Loop through all orders
            foreach (var order in ordersEntity)
            {
                var dto = _mapper.Map<OrderDTO>(order);
                dto.Customer = _dbContext.Customer.FirstOrDefault(c => c.Id == order.CustomerId).Name; // get customer name
                
                // Loop through all items in the order and add them to the DTO
                var itemList = _dbContext.ItemOrderConnection.Where(c => c.OrderId == order.Id).ToList();
                dto.Items = new List<ItemDTO>();
                foreach (var item in itemList)
                {
                    var itemEntity = _dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId);                  // get item entity
                    var add = _mapper.Map<ItemDTO>(itemEntity);                                                 // map to DTO
                    add.Service = _dbContext.Service.FirstOrDefault(s => s.Id == itemEntity.ServiceId).Name;    // Find Service name
                    add.State = _dbContext.State.FirstOrDefault(s => s.Id == itemEntity.StateId).Name;          // Find state name
                    add.Category = _dbContext.Category.FirstOrDefault(c => c.Id == itemEntity.CategoryId).Name; // Find category name
                    dto.Items.Add(add);     // Add the itemDTO to the orderDTO
                }

                orders.Add(dto);
            }
            
            return orders;
        }
    }
}