using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            return _mapper.Map<OrderDTO>(entity);
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

            var entity = _dbContext.Order.Add(orderToAdd).Entity;

            _dbContext.SaveChanges();   // Save and get order ID

            // Add items toDatabase
            AddMultipleItems(order.Items, entity.Id);

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
                    items[i].Details = order.Items[i].Details;

                    JObject rss = JObject.Parse(items[i].JSON);
                    rss.Property("sliced").Value = order.Items[i].Sliced;
                    rss.Property("filleted").Value = order.Items[i].Filleted;
                    rss.Property("otherCategory").Value = order.Items[i].OtherCategory;
                    rss.Property("otherService").Value = order.Items[i].OtherService;
                    items[i].JSON = JsonConvert.SerializeObject(rss);
                }
            }
            else if(items.Count == 0 && order.Items.Count > 0)      // if the list in the database is empty, just add the new ones
            {
                AddMultipleItems(order.Items, entity.Id);
                entity.DateCompleted = null;    // if adding items then the order is not complete
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

                    items[i].Details = order.Items[i].Details;

                    JObject rss = JObject.Parse(items[i].JSON);
                    rss.Property("sliced").Value = order.Items[i].Sliced;
                    rss.Property("filleted").Value = order.Items[i].Filleted;
                    rss.Property("otherCategory").Value = order.Items[i].OtherCategory;
                    rss.Property("otherService").Value = order.Items[i].OtherService;
                    items[i].JSON = JsonConvert.SerializeObject(rss);
                }

                // Build the rest of the list
                var tmpList = new List<ItemInputModel>();
                for( ; i < order.Items.Count; i++)
                {
                    tmpList.Add(order.Items[i]);
                }

                // Add the rest of the items
                AddMultipleItems(tmpList, entity.Id);
                entity.DateCompleted = null;    // if adding items then the order is not complete
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

                    items[i].Details = order.Items[i].Details;

                    JObject rss = JObject.Parse(items[i].JSON);
                    rss.Property("sliced").Value = order.Items[i].Sliced;
                    rss.Property("filleted").Value = order.Items[i].Filleted;
                    rss.Property("otherCategory").Value = order.Items[i].OtherCategory;
                    rss.Property("otherService").Value = order.Items[i].OtherService;
                    items[i].JSON = JsonConvert.SerializeObject(rss);
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
                if(maxItem == null) { code = null; }
                else { code = maxItem.Barcode; }
            }

            if(code == null) { code = "50500000"; }

            var barcode = int.Parse(code);
            barcode++;

            return barcode.ToString();

        }

        /// <summary>Used to add multiple items in order input</summary>
        private void AddMultipleItems(List<ItemInputModel> inpItems, long orderId)
        {
            // Get new barcode
            int newItemBarcode = int.Parse(GetItemBarcode());

            var addItems = new List<Item>();

            // Ready Items for DB input
            foreach(var item in inpItems)
            {
                var itemToAdd = _mapper.Map<Item>(item);
                itemToAdd.Barcode = newItemBarcode.ToString();
                
                itemToAdd.JSON = JsonConvert.SerializeObject(new
                {
                    location = "Vinnslu",
                    sliced = item.Sliced,
                    filleted = item.Filleted,
                    otherCategory = item.OtherCategory == null ? "" : item.OtherCategory,
                    otherService = item.OtherService == null ? "" : item.OtherService
                });

                addItems.Add(itemToAdd);

                // Increment barcode
                newItemBarcode++;
            }

            _dbContext.Item.AddRange(addItems);
            _dbContext.SaveChanges();   // Save changes and get Item IDs

            foreach (var item in addItems)
            {
                // Create Timestamp
                _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(item));
                
                // Add connection
                _dbContext.ItemOrderConnection.Add(new ItemOrderConnection
                {
                    OrderId = orderId,
                    ItemId = item.Id
                });
            }

            _dbContext.SaveChanges();   // Save changes to ItemOrderConnections and Timestamp
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
                itemTimestamps.AddRange(_dbContext.ItemTimestamp.Where(ts => ts.ItemId == item.ItemId).ToList());
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

        public OrderDTO CompleteOrder(long orderId)
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

            return _mapper.Map<OrderDTO>(orderEntity);
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
                orders.Add(_mapper.Map<OrderDTO>(order));
            }
            
            return orders;
        }

        public void ArchiveOldOrders()
        {
            var completeOrders = _dbContext.Order.Where(o => o.DateCompleted != null).ToList(); // Get all complete orders
            
            // Get all orders older than 3 months
            var oldOrders = new List<Order>();
            var dateNow = DateTime.Now;
            foreach (var order in completeOrders)
            {
                if(dateNow.Subtract((DateTime)order.DateCompleted).TotalDays > 90) { oldOrders.Add(order); }
            }

            Archive(oldOrders);
        }

        public void ArchiveCompleteOrdersByCustomerId(long customerId)
        {
            var completeOrders = _dbContext.Order.Where(o => o.CustomerId == customerId && o.DateCompleted != null).ToList();  // Get all complete orders for this customer

            // Archive the orders
            Archive(completeOrders);
        }

        public bool OrderPickupReady(long orderId)
        {
            var order = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);  // get order
            if(order == null) { throw new NotFoundException($"Order with ID {orderId} was not found."); }

            var itemOrderConnections = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderId).ToList();

            if(itemOrderConnections.Count == 0) { return false; }   // if no item exists in order, then it isn't complete

            foreach (var itemConnection in itemOrderConnections)
            {
                // get item
                var item = _dbContext.Item.FirstOrDefault(i => i.Id == itemConnection.ItemId);
                // get max step (ready for delivery is max step minus 1)
                var lastStep = _dbContext.ServiceState.Where(ss => ss.ServiceId == item.ServiceId).Max(s => s.Step);
                // get items current step
                var itemStep = _dbContext.ServiceState.FirstOrDefault(ss => ss.ServiceId == item.ServiceId && ss.StateId == item.StateId).Step;

                // if the item is not in the next to last step it is not ready to be picked up
                if(itemStep != (lastStep - 1)) { return false; }
            }

            return true;
        }

        public List<Order> GetOrdersReadyForPickup()
        {
            // Get all orders that are ready for pickup
            var orders = _dbContext.Order.ToList();
            var readyOrders = new List<Order>();
            foreach (var order in orders)
            {
                if(OrderPickupReady(order.Id)) { readyOrders.Add(order); }
            }

            return readyOrders; // return the DTO of all these orders
        }

        public void IncrementNotification(long orderId)
        {
            var order = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);
            if(order != null)
            {
                order.NotificationCount++;

                _dbContext.SaveChanges();
            }
        }

        //*     Helper functions     *//
        private void Archive(List<Order> toArchive)
        {
            // input to handle archive input and all connections
            var input = new List<OrderItemArchiveInput>();

            // just return if there aren't any orders
            if(!toArchive.Any()) { return; }

            var ordersToDelete = new List<long>();  // create a list of orderIDs to remove those that are archived
            foreach (var order in toArchive)
            {
                // get connections
                var itemOrderConnections = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == order.Id).ToList();
                
                // Get all items in order
                var itemList = new List<Item>();
                foreach (var connection in itemOrderConnections)
                {
                    itemList.Add(_dbContext.Item.FirstOrDefault(i => i.Id == connection.ItemId));
                }

                // Set order and items to the input model
                input.Add(new OrderItemArchiveInput() {
                    Order = order,
                    Items = itemList
                });

                // Add this order to delete list
                ordersToDelete.Add(order.Id);
            }

            foreach (var order in input)
            {
                order.ArchivedOrder = _mapper.Map<OrderArchive>(order.Order);   // archive the order
                _dbContext.OrderArchive.Add(order.ArchivedOrder);   // add each order to archive
            }

            _dbContext.SaveChanges();   // Save changes and get order archive IDs

            foreach (var order in input)
            {
                order.ArchivedItems = new List<ItemArchive>();
                foreach (var item in order.Items)
                {
                    var addItem = _mapper.Map<ItemArchive>(item);       // map Item to archive
                    addItem.OrderArchiveId = order.ArchivedOrder.Id;    // set the archive order ID
                    
                    order.ArchivedItems.Add(addItem);   // set the archived items
                }

                _dbContext.ItemArchive.AddRange(order.ArchivedItems);   // set the archived items to db
            }

            _dbContext.SaveChanges();   // save the database

            // all orders are stored to archive, remove them
            foreach (var order in ordersToDelete)
            {
                DeleteByOrderId(order);
            }
        }
    }
}