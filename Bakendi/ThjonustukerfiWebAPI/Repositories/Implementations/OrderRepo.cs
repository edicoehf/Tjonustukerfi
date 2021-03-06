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

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(OrderRepo));

        public OrderRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public OrderDTO GetOrderById(long id)
        {
            // Get order
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == id);
            // Check if order exists
            if(entity == null) { throw new NotFoundException($"Order with id {id} was not found."); }

            return MapOrderToDTO(entity);
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
                    items[i].CopyInputToSelf(order.Items[i]);
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
                    items[i].CopyInputToSelf(order.Items[i]);
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
                    items[i].CopyInputToSelf(order.Items[i]);
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

        public IEnumerable<OrderDTO> GetAllRawOrders()
        {
            var result = from o in _dbContext.Order
                join c in _dbContext.Customer on o.CustomerId equals c.Id
                select new OrderDTO
                {
                    Id            = o.Id,
                    CustomerId    = c.Id,
                    Customer      = c.Name,
                    CustomerEmail = c.Email,
                    Barcode       = o.Barcode,
                    DateCreated   = o.DateCreated,
                    DateModified  = o.DateModified,
                    DateCompleted = o.DateCompleted,
                    Items         = new List<ItemDTO>() // Not sent in this API to speed up things...
                };
            return result;
        }

        public void CompleteOrder(long orderId)
        {
            var orderEntity = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);    // find entity
            if(orderEntity == null) { throw new NotFoundException($"Order with ID {orderId} was not found."); }

            if(orderEntity.DateCompleted != null) { return; }   // order already completed, no need to complete it again
            
            var currentDate = DateTime.Now; // get current date once to use when updating item and order
            orderEntity.DateCompleted = currentDate;
            orderEntity.DateModified = currentDate;

            var orderItemConnection = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderId).ToList();
            foreach (var item in orderItemConnection)
            {
                var itemToChange = _dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId);
                if(itemToChange == null) { throw new NotFoundException($"Item with ID {item.ItemId} was not found in order with order ID {orderId}."); }
                
                // get final state id
                var finalStateId = _dbContext.ServiceState.Where(ss => ss.ServiceId == itemToChange.ServiceId).OrderByDescending(ss => ss.Step).FirstOrDefault().StateId;

                // only change if not already complete
                if(itemToChange.StateId != finalStateId)
                {
                    itemToChange.StateId = finalStateId;    // set to final state
                    itemToChange.DateCompleted = currentDate;
                    itemToChange.DateModified = currentDate;

                    JObject rss = JObject.Parse(itemToChange.JSON);         // parse the entity
                    var prop = rss.Property("location");                    // get the location property
                    prop.Value = "";                                        // set the location to empty (complete)
                    itemToChange.JSON = JsonConvert.SerializeObject(rss);   // serialize back

                    // Create the timestamp
                    _dbContext.ItemTimestamp.Add(_mapper.Map<ItemTimestamp>(itemToChange));
                }
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
                    var lastState = _dbContext.ServiceState.Where(ss => ss.ServiceId == item.ServiceId).OrderByDescending(ss => ss.Step).FirstOrDefault().StateId;
                    if(item.StateId != lastState)    // If any item is not done, add order to active orders and check next order
                    {
                        activeOrders.Add(order);
                        break;
                    }
                }
            }

            return activeOrders;    // Active orders, empty list if there are none
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
            // Get all complete orders for this customer
            var completeOrders = _dbContext.Order.Where(o => o.CustomerId == customerId && o.DateCompleted != null).ToList();

            // Archive the orders
            Archive(completeOrders);
        }

        public bool OrderPickupReady(long orderId)
        {
            var order = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);  // get order
            if(order == null) { throw new NotFoundException($"Order with ID {orderId} was not found."); }

            var result = InternalOrderPickupReady(order);
            return result.Item1;
        }

        public Tuple<bool, List<Item>> InternalOrderPickupReady(Order order)
        {
            var itemOrderConnections = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == order.Id).ToList();
            var itemList = new List<Item>();

            if(itemOrderConnections.Count == 0) { return new Tuple<bool, List<Item>>(false, itemList); }   // if no item exists in order, then it isn't complete
            if(order.DateCompleted != null) { return new Tuple<bool, List<Item>>(false, itemList); }   // order is already complete and customer has picked it up

            foreach (var itemConnection in itemOrderConnections)
            {
                // get item
                var item = _dbContext.Item.FirstOrDefault(i => i.Id == itemConnection.ItemId);
                // get max step (ready for delivery is max step minus 1)
                var lastStep = _dbContext.ServiceState.Where(ss => ss.ServiceId == item.ServiceId).Max(s => s.Step);
                // get items current step
                var itemStep = _dbContext.ServiceState.FirstOrDefault(ss => ss.ServiceId == item.ServiceId && ss.StateId == item.StateId).Step;

                // if the item is not in the next to last step or last step it is not ready to be picked up
                if(itemStep != (lastStep - 1) && itemStep != lastStep) 
                {
                    return new Tuple<bool, List<Item>>(false, itemList);
                }
                else
                {
                    itemList.Add(item);
                }
            }

            return new Tuple<bool, List<Item>>(true, itemList);
        }

        public List<Order> GetOrdersReadyForPickup(long? customerId = null)
        {
            // Get all orders that are ready for pickup
            var orderQuery = _dbContext.Order.AsQueryable();
            if (customerId != null)
            {
                orderQuery = orderQuery.Where(x => x.CustomerId == customerId);
            }
            var orders = orderQuery.ToList();
            var readyOrders = new List<Order>();
            foreach (var order in orders)
            {
                var result = InternalOrderPickupReady(order);
                if (result.Item1 == true)
                {
                    // TODO: add items to order object!
//                    order
                    readyOrders.Add(order);
                }
//                if(OrderPickupReady(order.Id)) { readyOrders.Add(order); }
            }

            return readyOrders; // return the DTO of all these orders
        }

        public List<OrderDTO> GetOrdersReadyForPickupByCustomerID(long customerId)
        {
            // get orders ready for pickup with this customer
            var orders = GetOrdersReadyForPickup(customerId);

            return GetOrderDTOwithOrderList(orders);
        }

        public void IncrementNotification(long orderId)
        {
            var order = _dbContext.Order.FirstOrDefault(o => o.Id == orderId);
            if(order != null)
            {
                _log.Info($"OrderRepo.IncrementNotification: order found, current NotificationCount is {order.NotificationCount}");
                order.NotificationCount = order.NotificationCount + 1;

                _dbContext.SaveChanges();
            }
            else
            {
                _log.Info("OrderRepo.IncrementNotification: order NOT found");
            }
        }

        public List<OrderDTO> GetOrdersByCustomerId(long customerId)
        {
            // get all order entities with this customer, them map the list to DTO list
            return GetOrderDTOwithOrderList(_dbContext.Order.Where(o => o.CustomerId == customerId).ToList());
        }

        public List<ItemPrintDetailsDTO> GetOrderPrintDetails(long orderId)
        {
            var order = GetOrderById(orderId);  // Get order

            return _mapper.Map<List<ItemPrintDetailsDTO>>(order.Items); // return all items in order
        }

        //*     Helper functions     *//

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
                
                //TODO: Not generic, this is reykofninn specific
                itemToAdd.JSON = JsonConvert.SerializeObject(new
                {
                    location = "",
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

        /// <summary>Gets a list of orderDTO with a list of order entity</summary>
        private List<OrderDTO> GetOrderDTOwithOrderList(List<Order> ordersEntity)
        {
            var orders = new List<OrderDTO>();

            // If the list is empty, just return it
            if(!ordersEntity.Any()) { return orders; }

            // Loop through all orders
            foreach (var order in ordersEntity)
            {
                orders.Add(MapOrderToDTO(order));
            }
            
            return orders;
        }

        /// <summary>Maps an order along with its items to DTO.</summary>
        private OrderDTO MapOrderToDTO(Order order)
        {
            var dto = _mapper.Map<OrderDTO>(order);

            // add customer name and email
            var customer = _dbContext.Customer.FirstOrDefault(c => c.Id == dto.CustomerId);
            dto.Customer        = customer.Name;
            dto.CustomerEmail   = customer.Email;
            dto.CustomerComment = customer.Comment;

            // Loop through all items in the order and add them to the DTO
            var itemList = _dbContext.ItemOrderConnection.Where(c => c.OrderId == order.Id).ToList();
            dto.Items = new List<ItemDTO>();
            foreach (var item in itemList)
            {
                var itemDto = _mapper.Map<ItemDTO>(_dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId)); // get item dto
                itemDto.OrderId = item.OrderId;
                itemDto.Category = _dbContext.Category.FirstOrDefault(c => c.Id == itemDto.CategoryId).Name; // Find category name
                itemDto.Service = _dbContext.Service.FirstOrDefault(s => s.Id == itemDto.ServiceId).Name;    // Find Service name
                itemDto.State = _dbContext.State.FirstOrDefault(s => s.Id == itemDto.StateId).Name;          // Find state name

                dto.Items.Add(itemDto);     // Add the itemDTO to the orderDTO
            }

            return dto;
        }

        /// <summary>Archives all orders in a list of orders</summary>
        private void Archive(List<Order> toArchive)
        {
            // just return if there aren't any orders
            if(!toArchive.Any()) { return; }

            // input to handle archive input and all connections
            var input = new List<OrderItemArchiveInput>();

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
                var orderToArchive = _mapper.Map<OrderArchive>(order.Order);
                var customer = _dbContext.Customer.FirstOrDefault(c => c.Id == order.Order.CustomerId); // get customer
                orderToArchive.Customer = customer.Name;                                                // get the customers name
                orderToArchive.CustomerEmail = customer.Email;                                          // set the size of the order

                order.ArchivedOrder = orderToArchive;               // add to archive in order, this will be used to track the ID of the orders after adding to the database
                _dbContext.OrderArchive.Add(order.ArchivedOrder);   // add each order to archive
            }

            _dbContext.SaveChanges();   // Save changes and get order archive IDs

            foreach (var order in input)
            {
                order.ArchivedItems = new List<ItemArchive>();
                foreach (var item in order.Items)
                {
                    var addItem = _mapper.Map<ItemArchive>(item);                                               // map Item to archive
                    addItem.extraDataJSON = item.JSON;                                                          // get the json data
                    addItem.Category = _dbContext.Category.FirstOrDefault(c => c.Id == item.CategoryId).Name;   // Get category name
                    addItem.Service = _dbContext.Service.FirstOrDefault(s => s.Id == item.ServiceId).Name;      // Get service name

                    var timestampList = _dbContext.ItemTimestamp.Where(its => its.ItemId == item.Id).OrderBy(state => state.StateId).ToList();   // get item timestamps in the "correct" order
                    
                    // Add the timestamps to the json
                    var propName = "timestamps";                            // name of new json array
                    JObject json = JObject.Parse(addItem.extraDataJSON);    // parse the json object we want to change
                    json.Add(new JProperty(propName, new JArray()));        // Create a new array as a json property
                    JArray timeStamps = (JArray)json[propName];             // Get the array we created

                    // add all timestamps of the item to the json array
                    foreach (var stamp in timestampList)
                    {
                        timeStamps.Add(JsonConvert.SerializeObject(new
                        {
                            StateId = stamp.StateId,
                            TimeOfChange = stamp.TimeOfChange
                        }));
                    }

                    addItem.extraDataJSON = json.ToString();            // update the json file
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