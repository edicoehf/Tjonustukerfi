using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class OrderRepoTests
    {
        private Mapper _mapper;
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()    // Each initialize fills the database with information for testing
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ThjonuskerfiDB")
                .EnableSensitiveDataLogging()
                .Options;

            var myProfile = new MappingProfile();   // Create a new profile like the one we implemented
            var myConfig = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));   // Setup a configuration with our profile
            _mapper = new Mapper(myConfig); // Create a new mapper with our profile

            FillDatabase();
        }

        [TestCleanup]
        public void Cleanup()   // Clears the database for the next test
        {
            ClearDatabase();
            _mapper = null;
            _options = null;
        }

        [TestMethod]
        public void GetAllOrders_should_return_an_empty_list()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                ClearDatabase();    // clear database since for this test it should be empty
                var orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                var result = orderRepo.GetAllOrders();

                //*
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public void Fill_database_should_have_an_inMemory_database_ready()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                //* Act

                //* Assert
                // Checks if all added entities are actually there
                Assert.IsNotNull(mockContext);
                Assert.IsTrue(mockContext.Order.Any());
                Assert.IsTrue(mockContext.Customer.Any());
                Assert.IsTrue(mockContext.ItemOrderConnection.Any());
                Assert.IsTrue(mockContext.Item.Any());
                Assert.IsTrue(mockContext.Service.Any());
                Assert.IsTrue(mockContext.State.Any());
                Assert.IsTrue(mockContext.ItemTimestamp.Any());
            }
        }

        [TestMethod]
        public void GetOrderById_should_return_orderDTO()
        {
            using (var mockContext = new DataContext(_options))
            {
                //* Arrange
                long orderId = 100;
                string customerName = "Viggi Siggi";
                string OrderBarCode = "20200001";
                DateTime modifiedDate = mockContext.Order.FirstOrDefault(o => o.Id == orderId).DateModified;

                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Create a list that should be the same as the list returned in OrderDTO
                var itemListDTO = new List<ItemDTO>();
                var itemOrderConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderId).ToList();
                
                foreach (var item in itemOrderConnection)
                {
                    var entity = mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId);
                    var add = _mapper.Map<ItemDTO>(entity);
                    add.Service = mockContext.Service.FirstOrDefault(s => s.Id == entity.ServiceId).Name;
                    add.State = mockContext.State.FirstOrDefault(s => s.Id == entity.StateId).Name;
                    add.Category = mockContext.Category.FirstOrDefault(c => c.Id == entity.CategoryId).Name;
                    itemListDTO.Add(add);
                }

                //* Act
                OrderDTO orderDTO = orderRepo.GetOrderById(orderId);

                //* Assert
                // Asserting order
                Assert.IsNotNull(orderDTO);
                Assert.IsInstanceOfType(orderDTO, typeof(OrderDTO));
                Assert.AreEqual(orderDTO.Customer, customerName);
                Assert.AreEqual(orderDTO.Barcode, OrderBarCode);
                Assert.AreEqual(orderDTO.DateCreated, DateTime.MinValue);
                Assert.AreEqual(orderDTO.DateModified, modifiedDate);
                Assert.IsNull(orderDTO.DateCompleted);

                // Asserting items in order
                Assert.IsNotNull(orderDTO.Items);
                Assert.AreEqual(orderDTO.Items.Count, itemListDTO.Count);
                Assert.AreEqual(orderDTO.Items[0], itemListDTO[0]);
                Assert.AreEqual(orderDTO.Items[1], itemListDTO[1]);
            };
        }

        [TestMethod]
        public void GetOrderById_should_throw_NotFoundException()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.GetOrderById(-1));
            }
        }

        [TestMethod]
        public void CreateOrder_should_create_and_return_OrderId()
        {
            //* Arrange
            var inp = new OrderInputModel
            {
                CustomerId = 2,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);
                
                // Check length of all db's before test
                var orderDbSize = mockContext.Order.Count();
                var itemDbSize = mockContext.Item.Count();
                var itemOrderDbSize = mockContext.ItemOrderConnection.Count();

                //* Act
                var result = orderRepo.CreateOrder(inp);
                // GetDTO to compare with input later
                var resultOrderDTO = mockContext.Order.OrderByDescending(o => o.Id).FirstOrDefault();
                // Get a list of all itemOrderConnections created
                var itemOrderConnectionDTOList = mockContext.ItemOrderConnection.Where(i => i.OrderId == result).ToList();
                // Get all items and timestamps added to the database using ItemOrderConnection
                var itemListDTO = new List<Item>();
                var itemTimestamps = new List<ItemTimestamp>();
                foreach (var item in itemOrderConnectionDTOList)
                {
                    var add = _mapper.Map<Item>(mockContext.Item.Find(item.ItemId));
                    itemListDTO.Add(add);

                    itemTimestamps.AddRange(mockContext.ItemTimestamp.Where(ts => ts.ItemId == item.ItemId).ToList());
                }

                //* Assert
                // Assert Order
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(long));
                Assert.AreEqual(resultOrderDTO.CustomerId, inp.CustomerId);
                Assert.AreEqual(mockContext.Order.Count(), orderDbSize + 1);

                // Assert Item
                Assert.AreEqual(mockContext.Item.Count(), itemDbSize + inp.Items.Count());
                Assert.AreEqual(itemListDTO[0].CategoryId, inp.Items[0].CategoryId);
                Assert.AreEqual(itemListDTO[0].ServiceId, inp.Items[0].ServiceId);
                Assert.AreEqual(itemListDTO[1].CategoryId, inp.Items[1].CategoryId);
                Assert.AreEqual(itemListDTO[1].ServiceId, inp.Items[1].ServiceId);

                // Assert ItemOrderConnection
                Assert.AreEqual(mockContext.ItemOrderConnection.Count(), itemOrderDbSize + inp.Items.Count());
                Assert.AreEqual(itemOrderConnectionDTOList[0].OrderId, resultOrderDTO.Id);
                Assert.AreEqual(itemOrderConnectionDTOList[0].ItemId, itemListDTO[0].Id);
                Assert.AreEqual(itemOrderConnectionDTOList[1].OrderId, resultOrderDTO.Id);
                Assert.AreEqual(itemOrderConnectionDTOList[1].ItemId, itemListDTO[1].Id);

                // Assert Timestamp
                // since no updates have been made since creating, there should be the same amout of timestamps as their are items in the order
                Assert.AreEqual(itemListDTO.Count, itemTimestamps.Count);
            };
        }

        [TestMethod]
        public void CreateOrder_should_throw_NotFoundException()
        {
            //* Arrange
            var inp = new OrderInputModel
            {
                CustomerId = 2,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = -1
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 2
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.CreateOrder(inp));
            }
        }

        [TestMethod]
        public void GetActiveOrdersByCustomerId_should_retrieve_correct_orders()
        {
            //* Arrange
            long customerId = 50;
            long orderIdExpected1 = 100;
            long orderIdExpected2 = 101;
            
            // These orders were created in the build database test. This person should have 2 orders
            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                var activeList = orderRepo.GetActiveOrdersByCustomerId(customerId);

                Assert.IsNotNull(activeList);
                Assert.AreEqual(2, activeList.Count);

                foreach (var order in activeList)   // should be equal to these two orders
                {
                    Assert.IsTrue(order.Id == orderIdExpected1 || order.Id == orderIdExpected2);
                }
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_order_correctly_and_itemlist_should_grow()
        {
            long orderID = 100;
            long custId = 50;
            //* Arrange
            var orderInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 3,
                        ServiceId = 2,
                        Sliced = true,
                        Filleted = true
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 3,
                        ServiceId = 3,
                        Sliced = false,
                        Filleted = false
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 4,
                        Sliced = true,
                        Filleted = false
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Get old item order connection to get the old list of items.
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    var itemAdd = mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId);
                    oldItemList.Add(new Item()  // copy to a new item (not copy reference)
                    {
                        Id = itemAdd.Id,
                        CategoryId = itemAdd.CategoryId,
                        StateId = itemAdd.StateId,
                        ServiceId = itemAdd.ServiceId,
                        Barcode = itemAdd.Barcode,
                        JSON = itemAdd.JSON,
                        DateCreated = itemAdd.DateCreated,
                        DateModified = itemAdd.DateModified,
                        DateCompleted = itemAdd.DateCompleted
                    });
                }

                //* Act
                orderRepo.UpdateOrder(orderInput, orderID);

                //* Assert
                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);   // get order entity to check
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();  // new connections to get the new list
                List<Item> newItemList = new List<Item>();
                foreach (var item in newConnection)
                {
                    newItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                // assert order
                Assert.IsNotNull(orderEntity);
                Assert.AreEqual(orderEntity.CustomerId, custId);
                // assert connections
                Assert.IsNotNull(newConnection);
                Assert.AreEqual(newConnection.Count, oldConnection.Count + 1);  // list has incremented by 1
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(newItemList.Count, oldItemList.Count + 1);      // list has been incremented by 1

                // check older items updated
                foreach (var item in oldItemList)
                {
                    var newItem = newItemList.FirstOrDefault(i => i.Id == item.Id);
                    Assert.AreNotEqual(item.CategoryId, newItem.CategoryId);
                    Assert.AreNotEqual(item.ServiceId, newItem.ServiceId);

                    newItemList.Remove(newItem);    // remove checked item from list
                }

                // should be 1 item left in the newItemList (that was added to the order), should be same as the third item in input (that was added)
                Assert.AreEqual(orderInput.Items[2].CategoryId, newItemList[0].CategoryId);
                Assert.AreEqual(orderInput.Items[2].ServiceId, newItemList[0].ServiceId);
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_order_correctly_and_itemlist_should_shrink()
        {
            long orderID = 100;
            long custId = 50;
            //* Arrange
            var orperInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 2,
                        Sliced = true,
                        Filleted = true
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Get old item order connection to get the old list of items.
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(orperInput, orderID);

                //* Assert
                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);   // get order entity to check
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();  // new connections to get the new list
                List<Item> newItemList = new List<Item>();
                foreach (var item in newConnection)
                {
                    newItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                // assert order
                Assert.IsNotNull(orderEntity);
                Assert.AreEqual(orderEntity.CustomerId, custId);
                // assert connections
                Assert.IsNotNull(newConnection);
                Assert.AreEqual(oldConnection.Count - 1, newConnection.Count);  // list is going from 2 to one
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(oldItemList.Count - 1, newItemList.Count);      // list is going from 2 to one
                // check type
                Assert.AreEqual(newItemList[0].CategoryId, orperInput.Items[0].CategoryId);
                // check service ID
                Assert.AreEqual(newItemList[0].ServiceId, orperInput.Items[0].ServiceId);
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_order_correctly_and_itemlist_should_be_empty()
        {
            long orderID = 100;
            long custId = 50;
            //* Arrange
            var orperInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Get old item order connection to get the old list of items.
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(orperInput, orderID);

                //* Assert
                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);   // get order entity to check
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();  // new connections to get the new list
                List<Item> newItemList = new List<Item>();
                foreach (var item in newConnection)
                {
                    newItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                // assert order
                Assert.IsNotNull(orderEntity);
                Assert.AreEqual(orderEntity.CustomerId, custId);
                // assert connections
                Assert.IsNotNull(newConnection);
                Assert.AreEqual(newConnection.Count, 0);    // list is going from one to zero
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(newItemList.Count,0);       // list is going from one to zero
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_order_correctly_and_itemlist_should_grow_to_five()
        {
            long orderID = 100;
            long custId = 50;
            //* Arrange
            var clearItems = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
            };
            var orderInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1,
                        Sliced = true,
                        Filleted = true
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1,
                        Sliced = false,
                        Filleted = false
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1,
                        Sliced = true,
                        Filleted = false
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1,
                        Sliced = false,
                        Filleted = true
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1,
                        Sliced = true,
                        Filleted = true
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Get old item order connection to get the old list of items.
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(clearItems, orderID);     // should clear all items in order
                orderRepo.UpdateOrder(orderInput, orderID);     // should add 5 items to the now cleared list

                //* Assert
                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);   // get order entity to check
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();  // new connections to get the new list
                List<Item> newItemList = new List<Item>();
                foreach (var item in newConnection)
                {
                    newItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                // assert order
                Assert.IsNotNull(orderEntity);
                Assert.AreEqual(orderEntity.CustomerId, custId);
                // assert connections
                Assert.IsNotNull(newConnection);
                Assert.AreEqual(orderInput.Items.Count, newConnection.Count);   // list is going from zero to five
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(orderInput.Items.Count, newItemList.Count);     // list is going from zero to five
                // check type
                Assert.AreEqual(newItemList[0].CategoryId, 2);
                Assert.AreEqual(newItemList[1].CategoryId, 2);
                Assert.AreEqual(newItemList[2].CategoryId, 2);
                Assert.AreEqual(newItemList[3].CategoryId, 2);
                Assert.AreEqual(newItemList[4].CategoryId, 2);
                // check service ID
                Assert.AreEqual(newItemList[0].ServiceId, (long)1);
                Assert.AreEqual(newItemList[1].ServiceId, (long)1);
                Assert.AreEqual(newItemList[2].ServiceId, (long)1);
                Assert.AreEqual(newItemList[3].ServiceId, (long)1);
                Assert.AreEqual(newItemList[4].ServiceId, (long)1);
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_json_file_and_details_correctly()
        {
            long orderID = 100;
            long custId = 50;
            //* Arrange
            var orderInput = new OrderInputModel
            {
                CustomerId = custId,

                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1,
                        Sliced = true,
                        Filleted = true,
                        OtherCategory = "Rubber boot",
                        OtherService = "Melt it",
                        Details = "Now this is a strange item"
                    }
                }
            };

            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                orderRepo.UpdateOrder(orderInput, orderID);

                // get the only updated item
                var itemId = mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.OrderId == orderID).ItemId;
                var item = mockContext.Item.FirstOrDefault(i => i.Id == itemId);

                //* Assert
                Assert.IsNotNull(item);
                Assert.IsNotNull(item.JSON);

                var itemInp = orderInput.Items.FirstOrDefault();   // get item from input
                Assert.AreEqual(itemInp.CategoryId, item.CategoryId);
                Assert.AreEqual(itemInp.ServiceId, item.ServiceId);
                Assert.AreEqual(itemInp.Details, item.Details);

                // Get json and check all json fields
                JObject rss = JObject.Parse(item.JSON);
                Assert.AreEqual(itemInp.Sliced, rss.Property("sliced").Value);
                Assert.AreEqual(itemInp.Filleted, rss.Property("filleted").Value);
                Assert.AreEqual(itemInp.OtherCategory, rss.Property("otherCategory").Value);
                Assert.AreEqual(itemInp.OtherService, rss.Property("otherService").Value);
            }
        }

        [TestMethod]
        public void UpdateOrder_should_throw_NotFoundException()
        {
            //* Arrange
            var inp = new OrderInputModel
            {
                CustomerId = 1337,
                Items = new List<ItemInputModel>()
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act then assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.UpdateOrder(inp, -1));
                Assert.ThrowsException<NotFoundException>(() => orderRepo.UpdateOrder(inp, 100));
            }
        }

        [TestMethod]
        public void CompleteOrder_should_set_all_itemstates_in_order_to_complete()
        {
            //* Arrange
            long orderID = 100;

            using(var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var oldOrder = new Order()  // copy the order, not the reference
                {
                    CustomerId = orderEntity.CustomerId,
                    Barcode = orderEntity.Barcode,
                    JSON = orderEntity.JSON,
                    DateCreated = orderEntity.DateCreated,
                    DateModified = orderEntity.DateModified,
                    DateCompleted = orderEntity.DateCompleted
                };
                var itemOrderConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                var oldItemsStates = new List<Item>();
                var oldItemsTimestamps = new List<ItemTimestamp>();
                foreach (var item in itemOrderConnection)
                {
                    var entity = mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId);
                    oldItemsStates.Add(new Item()   // copying each variable rather than the reference of the objects
                    {
                        Id = entity.Id,
                        CategoryId = entity.CategoryId,
                        StateId = entity.StateId,
                        ServiceId = entity.ServiceId,
                        Barcode = entity.Barcode,
                        JSON = entity.JSON,
                        DateCreated = entity.DateCreated,
                        DateModified = entity.DateModified,
                        DateCompleted = entity.DateCompleted
                    });

                    // adding timestamps
                    oldItemsTimestamps.AddRange(mockContext.ItemTimestamp.Where(ts => ts.ItemId == entity.Id).ToList());
                }

                //* Act
                orderRepo.CompleteOrder(orderID);

                orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var newItemStates = new List<Item>();
                var newItemsTimestamps = new List<ItemTimestamp>();
                foreach (var item in itemOrderConnection)
                {
                    newItemStates.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                    newItemsTimestamps.AddRange(mockContext.ItemTimestamp.Where(ts => ts.ItemId == item.ItemId).ToList());
                }

                //* Assert
                // Order
                Assert.IsNotNull(orderEntity);
                Assert.AreNotEqual(oldOrder, orderEntity);      // old and new entity not the same
                Assert.IsNull(oldOrder.DateCompleted);          // make sure the old datecomplete is null
                Assert.IsNotNull(orderEntity.DateCompleted);    // datecompleted should be updated

                Assert.AreEqual(oldItemsStates.Count, newItemStates.Count); // lists should be of the same size
                for (var i = 0; i < newItemStates.Count; i++)
                {
                    Assert.IsNotNull(newItemStates[i]);
                    Assert.IsNull(oldItemsStates[i].DateCompleted);     // old date completed should be null
                    Assert.IsNotNull(newItemStates[i].DateCompleted);   // date completed should be updated
                    Assert.AreNotEqual(oldItemsStates[i], newItemStates[i]);
                    Assert.AreEqual(5, newItemStates[i].StateId);   //TODO more general that 5, same as before
                }

                // check the timestamps, each item should have 1 new timestamp (timestamp when completed)
                Assert.AreEqual(oldItemsTimestamps.Count + newItemStates.Count, newItemsTimestamps.Count);
            }
        }

        [TestMethod]
        public void CompleteOrder_should_throw_NotFoundException()
        {
            //* Arrange
            long orderId = -1;

            using(var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.CompleteOrder(orderId));
            }
        }

        [TestMethod]
        public void SearchOrder_should_return_the_correct_ID()
        {
            //* Arrange
            string barcodeToSearch = "20200001";
            using(var mockContext = new DataContext(_options))
            {
                // Create repo
                var orderRepo = new OrderRepo(mockContext, _mapper);
                // Get correct entity
                var correctEntity = mockContext.Order.FirstOrDefault(o => o.Barcode == barcodeToSearch);

                //* Act
                var returnValue = orderRepo.SearchOrder(barcodeToSearch);

                //* Assert
                Assert.IsNotNull(returnValue);
                Assert.IsInstanceOfType(returnValue, typeof(long));
                Assert.AreEqual(correctEntity.Id, returnValue);
            }
        }

        [TestMethod]
        public void SearchOrder_should_throw_NotFoundException()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                string inp = "This barcode should never exist, I mean this is no barcode.";

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.SearchOrder(inp));
            }
        }

        [TestMethod]
        public void DeleteOrderById_should_remove_order_with_Id_100()
        {
            //* Arrange
            long orderID = 100;

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                var orderTableSize = mockContext.Order.Count(); // size of ordertable before delete
                var itemTableSize = mockContext.Item.Count();   // item table size before delete
                var itemOrderConnectionTableSize = mockContext.ItemOrderConnection.Count(); // itemorderconnection table size before delete
                var timestampTableSize = mockContext.ItemTimestamp.Count(); // size of timestamp table before delete

                // size of item list
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();  // item order connections in order before delete
                var oldTimestamps = new List<ItemTimestamp>();  // all old time stamps for all items in order
                foreach (var item in newConnection)
                {
                    oldTimestamps.AddRange(mockContext.ItemTimestamp.Where(ts => ts.ItemId == item.ItemId).ToList());
                }

                //* Act
                orderRepo.DeleteByOrderId(orderID);

                //* Assert
                Assert.AreEqual(orderTableSize - 1, mockContext.Order.Count());                 // removed 1 order
                Assert.AreEqual(itemTableSize - newConnection.Count, mockContext.Item.Count()); // removed all items
                Assert.AreEqual(itemOrderConnectionTableSize - newConnection.Count, mockContext.ItemOrderConnection.Count());   // removed all connections
                Assert.AreEqual(timestampTableSize - oldTimestamps.Count, mockContext.ItemTimestamp.Count());                   // removed all timestamps
            }
        }

        [TestMethod]
        public void DeleteOrderById_should_throw_NotFoundException()
        {
            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                Assert.ThrowsException<NotFoundException>(() => orderRepo.DeleteByOrderId(-1));
            }
        }

        [TestMethod]
        public void GetAllOrders_should_return_list_of_correct_size()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);
                var DbSize = mockContext.Order.Count();

                //* Act
                var result = orderRepo.GetAllOrders();

                //* Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(DbSize, result.Count());
            }
        }

        [TestMethod]
        public void ArchiveOrder_should_archive_order()
        {
            //* Arrange
            // input for create order
            var orderInput = new OrderInputModel()
            {
                CustomerId = 50,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel() { CategoryId = 1, ServiceId = 1, },
                    new ItemInputModel() { CategoryId = 2, ServiceId = 2, },
                    new ItemInputModel() { CategoryId = 1, ServiceId = 3, },
                    new ItemInputModel() { CategoryId = 2, ServiceId = 4, },
                    new ItemInputModel() { CategoryId = 1, ServiceId = 3, },
                    new ItemInputModel() { CategoryId = 2, ServiceId = 2, }
                }
            };

            using(var mockContext = new DataContext(_options))
            {
                // craeate repo
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                var orderId = orderRepo.CreateOrder(orderInput);        // Add new order
                orderRepo.CompleteOrder(orderId);                       // complete the order
                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderId);   // get order entity
                orderEntity.DateCompleted = DateTime.Now.AddYears(-1);  // make the order complete a year old (has to be more than 90 days)

                //* Act
                orderRepo.ArchiveOldOrders();

                // Get stuff
                var archivedOrder = mockContext.OrderArchive.First();
                var archivedItems = mockContext.ItemArchive.ToList();
                var customer = mockContext.Customer.FirstOrDefault(c => c.Id == orderInput.CustomerId);

                //* Assert
                Assert.IsNotNull(archivedOrder);
                Assert.IsNotNull(archivedItems);

                Assert.AreEqual(customer.Email, archivedOrder.CustomerEmail);
                Assert.AreEqual(customer.Name, archivedOrder.Customer);

                Assert.AreEqual(orderInput.Items.Count, archivedItems.Count);
                var classProps = typeof(ItemArchive).GetProperties();   // getting all properties of the class
                foreach (var item in archivedItems)
                {
                    // loop through all properties and assert that they are not null
                    foreach (var prop in classProps)
                    {
                        Assert.IsNotNull(prop.GetValue(item));
                    }

                    Assert.AreEqual(archivedOrder.Id, item.OrderArchiveId); // Correct order?
                }
            }
        }

        [TestMethod]
        public void ArchiveCompleteOrderByCustomerId_should_archive_complete_order_with_customer_ID()
        {
            // This method does the same thing as archive order except the order doesn't need to be old
            // and should be done via customer ID

            //* Arrange
            long customerID = 50;
            // input for create order
            var orderInput = new OrderInputModel()
            {
                CustomerId = customerID,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel() { CategoryId = 1, ServiceId = 1, },
                    new ItemInputModel() { CategoryId = 2, ServiceId = 2, },
                    new ItemInputModel() { CategoryId = 1, ServiceId = 3, },
                    new ItemInputModel() { CategoryId = 2, ServiceId = 4, },
                    new ItemInputModel() { CategoryId = 1, ServiceId = 3, },
                    new ItemInputModel() { CategoryId = 2, ServiceId = 2, }
                }
            };

            using(var mockContext = new DataContext(_options))
            {
                // craeate repo
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                var orderId = orderRepo.CreateOrder(orderInput);        // Add new order
                orderRepo.CompleteOrder(orderId);                       // complete the order
                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderId);   // get order entity

                //* Act
                orderRepo.ArchiveCompleteOrdersByCustomerId(customerID);

                // Get stuff
                var archivedOrder = mockContext.OrderArchive.First();
                var archivedItems = mockContext.ItemArchive.ToList();
                var customer = mockContext.Customer.FirstOrDefault(c => c.Id == orderInput.CustomerId);

                //* Assert
                Assert.IsNotNull(archivedOrder);
                Assert.IsNotNull(archivedItems);

                Assert.AreEqual(customer.Email, archivedOrder.CustomerEmail);
                Assert.AreEqual(customer.Name, archivedOrder.Customer);

                Assert.AreEqual(orderInput.Items.Count, archivedItems.Count);
                var classProps = typeof(ItemArchive).GetProperties();   // getting all properties of the class
                foreach (var item in archivedItems)
                {
                    // loop through all properties and assert that they are not null
                    foreach (var prop in classProps)
                    {
                        Assert.IsNotNull(prop.GetValue(item));
                    }

                    Assert.AreEqual(archivedOrder.Id, item.OrderArchiveId);
                }
            }
        }

        [TestMethod]
        public void OrderPickupReady_should_return_order_ready_if_it_is_else_not()
        {
            //* Arrange
            long orderNotReadyID = 100;
            long orderReadyID = 101;
            long orderEmptyID = 10000;

            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.IsTrue(orderRepo.OrderPickupReady(orderReadyID));        // order created in initialize that is ready
                Assert.IsFalse(orderRepo.OrderPickupReady(orderNotReadyID));    // order created in initialize that is not ready
                Assert.IsFalse(orderRepo.OrderPickupReady(orderEmptyID));       // order should have an empty list of items and return false
            }
        }

        [TestMethod]
        public void OrderPickupReady_should_throw_NotFoundException()
        {
            //* Arrange
            long invalidOrder = -100;

            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.OrderPickupReady(invalidOrder));
            }
        }

        [TestMethod]
        public void GetOrdersReadyForPickup_should_return_a_list_of_ready_order()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                var orderToPickup = orderRepo.GetOrdersReadyForPickup();

                //* Assert
                // There should be only one ready order
                Assert.IsNotNull(orderToPickup);
                Assert.AreEqual(1, orderToPickup.Count);
                Assert.AreEqual(101, orderToPickup.FirstOrDefault().Id);
            }
        }

        [TestMethod]
        public void GetOrdersReadyForPickup_should_return_an_empty_list()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                ClearDatabase();
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                var orderToPickup = orderRepo.GetOrdersReadyForPickup();

                //* Assert
                // There should be only one ready order
                Assert.IsNotNull(orderToPickup);
                Assert.IsFalse(orderToPickup.Any());
            }
        }

        [TestMethod]
        public void IncrementNotification_should_increment_notificationcount_of_item_by_one()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                var order = mockContext.Order.FirstOrDefault();
                var oldNotificationCount = order.NotificationCount;

                //* Act
                orderRepo.IncrementNotification(order.Id);

                //* Assert
                // order is a reference, hence its notfication count should be updated after running the method
                Assert.AreEqual(oldNotificationCount + 1, order.NotificationCount);
            }
        }

        [TestMethod]
        public void GetOrderReadyForPickupByCustomerID_should_return_list_of_order()
        {
            long customerID = 50;   // this customer should have one ready order and one not ready
            
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                var orders = orderRepo.GetOrdersReadyForPickupByCustomerID(customerID);

                //* Assert
                Assert.IsNotNull(orders);
                Assert.IsInstanceOfType(orders, typeof(List<OrderDTO>));
                Assert.AreEqual(1, orders.Count);
            }
        }

        [TestMethod]
        public void GetOrdersByCustomerId_should_return_list_of_order()
        {
            long customerID = 50;   // this customer should have two orders

            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                // get order count
                var orderCount = mockContext.Order.Where(o => o.CustomerId == customerID).Count();

                //* Act
                var orders = orderRepo.GetOrdersByCustomerId(customerID);

                //* Assert
                Assert.IsNotNull(orders);
                Assert.IsInstanceOfType(orders, typeof(List<OrderDTO>));
                Assert.AreEqual(orderCount, orders.Count);
            }
        }

        [TestMethod]
        public void GetOrderPrintDetails_should_return_a_list_of_ItemPrintDetailsDTO()
        {
            //* Arrange
            long orderId = 100;

            using(var mockContext = new DataContext(_options))
            {
                // update mapper and create repo
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                // Get the size of the list
                var itemListSize = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderId).Count();

                //* Act
                var orderItems = orderRepo.GetOrderPrintDetails(orderId);

                //* Assert
                Assert.IsNotNull(orderItems);
                Assert.IsInstanceOfType(orderItems, typeof(List<ItemPrintDetailsDTO>));
                Assert.AreEqual(itemListSize, orderItems.Count);
            }
        }

        //**********     Helper functions     **********//
        private void FillDatabase()
        {
            using(var mockContext = new DataContext(_options))
            {
                //* Arrange
                // variables evaluating
                long orderId = 100;
                long customerId = 50;
                string customerName = "Viggi Siggi";
                string OrderBarCode = "20200001";
                DateTime modifiedDate = DateTime.Now;
                string serviceName = "Birkireyking";
                // Mock entity
                Order mockOrder = new Order
                {
                    Id = orderId,
                    CustomerId = customerId,
                    Barcode = OrderBarCode,
                    NotificationCount = 0,
                    DateCreated = DateTime.MinValue,
                    DateModified = modifiedDate,
                };

                Order mockOrder2 = new Order
                {
                    Id = orderId + 1,
                    CustomerId = customerId,
                    Barcode = "20200001",
                    NotificationCount = 0,
                    DateCreated = DateTime.Now,
                    DateModified = modifiedDate
                };

                Customer mockCustomer = new Customer
                {
                    Id = customerId,
                    Name = customerName,
                    SSN = "1308943149",
                    Email = "viggi@siggi.is",
                    Phone = "5812345",
                    Address = "Bakkabakki 1",
                    PostalCode = "800"
                };

                // for order 1 and 1 order ready for pickup
                List<ItemOrderConnection> mockIOConnect = new List<ItemOrderConnection>()
                {
                    new ItemOrderConnection
                    {
                        Id = 1,
                        ItemId = 1,
                        OrderId = orderId
                    },
                    new ItemOrderConnection
                    {
                        Id = 2,
                        ItemId = 2,
                        OrderId = orderId
                    },
                    new ItemOrderConnection
                    {
                        Id = 3,
                        ItemId = 3,
                        OrderId = orderId + 1
                    }
                };

                // adding items
                List<Item> mockItems = new List<Item>()
                {
                    new Item
                    {
                        Id = 1,
                        CategoryId = 1,
                        StateId = 1,
                        ServiceId = 1,
                        Barcode = "50500001",
                        JSON = @"{""location"":""Vinnslu"",""sliced"":true,""filleted"":false,""otherCategory"":"""",""otherService"":""""}",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                    },
                    new Item
                    {
                        Id = 2,
                        CategoryId = 2,
                        StateId = 1,
                        ServiceId = 1,
                        Barcode = "50500002",
                        JSON = @"{""location"":""Vinnslu"",""sliced"":true,""filleted"":false,""otherCategory"":"""",""otherService"":""""}",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                    },
                    new Item
                    {
                        Id = 3,
                        CategoryId = 2,
                        StateId = 2,
                        ServiceId = 1,
                        Barcode = "50500003",
                        JSON = @"{""location"":""Vinnslu"",""sliced"":true,""filleted"":false,""otherCategory"":"""",""otherService"":""""}",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                    }
                };

                // Adding timestamp
                List<ItemTimestamp> mockTimestamps = new List<ItemTimestamp>();
                foreach (var item in mockItems)
                {
                    mockTimestamps.Add(_mapper.Map<ItemTimestamp>(item));
                }

                // Adding service
                var mockServices = new List<Service>()
                {
                    new Service() { Name = serviceName, Id = 1 },
                    new Service() { Name = "Taðreyking", Id = 2 },
                    new Service() { Name = "Viðarreyking", Id = 3 },
                    new Service() { Name = "Salt pækill", Id = 4 },
                    new Service() { Name = "Other", Id = 5 }
                };

                //* Add Service states here
                var serviceStates = new List<ServiceState>()
                {
                    // Service state fyrir birkireykingu
                    new ServiceState() {Id = 1, ServiceId = 1, StateId = 1, Step = 1},
                    new ServiceState() {Id = 2, ServiceId = 1, StateId = 2, Step = 2},
                    new ServiceState() {Id = 3, ServiceId = 1, StateId = 3, Step = 2},
                    new ServiceState() {Id = 4, ServiceId = 1, StateId = 4, Step = 2},
                    new ServiceState() {Id = 5, ServiceId = 1, StateId = 5, Step = 3},

                    // Service state fyrir Taðreykingu
                    new ServiceState() {Id = 6, ServiceId = 2, StateId = 1, Step = 1},
                    new ServiceState() {Id = 7, ServiceId = 2, StateId = 2, Step = 2},
                    new ServiceState() {Id = 8, ServiceId = 2, StateId = 3, Step = 2},
                    new ServiceState() {Id = 9, ServiceId = 2, StateId = 4, Step = 2},
                    new ServiceState() {Id = 10, ServiceId = 2, StateId = 5, Step = 3},

                    // Service state fyrir viðarReykingu
                    new ServiceState() {Id = 11, ServiceId = 3, StateId = 1, Step = 1},
                    new ServiceState() {Id = 12, ServiceId = 3, StateId = 2, Step = 2},
                    new ServiceState() {Id = 13, ServiceId = 3, StateId = 3, Step = 2},
                    new ServiceState() {Id = 14, ServiceId = 3, StateId = 4, Step = 2},
                    new ServiceState() {Id = 15, ServiceId = 3, StateId = 5, Step = 3},

                    // Service state fyrir salt pækil
                    new ServiceState() {Id = 16, ServiceId = 4, StateId = 1, Step = 1},
                    new ServiceState() {Id = 17, ServiceId = 4, StateId = 2, Step = 2},
                    new ServiceState() {Id = 18, ServiceId = 4, StateId = 3, Step = 2},
                    new ServiceState() {Id = 19, ServiceId = 4, StateId = 4, Step = 2},
                    new ServiceState() {Id = 20, ServiceId = 4, StateId = 5, Step = 3},

                    // Service state fyrir other
                    new ServiceState() {Id = 21, ServiceId = 5, StateId = 1, Step = 1},
                    new ServiceState() {Id = 22, ServiceId = 5, StateId = 2, Step = 2},
                    new ServiceState() {Id = 23, ServiceId = 5, StateId = 3, Step = 2},
                    new ServiceState() {Id = 24, ServiceId = 5, StateId = 4, Step = 2},
                    new ServiceState() {Id = 25, ServiceId = 5, StateId = 5, Step = 3}
                };

                // Build a list of size 20, make it queryable for the database mock
                var orders = Builder<Order>.CreateListOfSize(20)
                    .All()
                    .With(o => o.Barcode = "30200001")
                    .With(o => o.CustomerId = 2)
                    .With(o => o.DateCompleted = null)
                    .TheLast(1)
                    .With(o => o.Id = 10000)
                    .Build();
                orders.Add(mockOrder);
                orders.Add(mockOrder2);

                // Build a list of size 20, make it queryable for the database mock
                var customers = Builder<Customer>.CreateListOfSize(20).Build();
                customers.Add(mockCustomer);

                // Adding states for lookup
                var states = new List<State>()
                {
                    // states fyrir Reykofninn
                    new State() {Name = "Í vinnslu", Id = 1},
                    new State() {Name = "Kælir 1", Id = 2},
                    new State() {Name = "Kælir 2", Id = 3},
                    new State() {Name = "Frystir", Id = 4},
                    new State() {Name = "Sótt", Id = 5}
                };

                // Adding categories
                var categories = new List<Category>()
                {
                    // Catagories for Reykofninn
                    new Category() {Name = "Lax", Id = 1},
                    new Category() {Name = "Silungur", Id = 2},
                    new Category() {Name = "Ysa", Id = 3},
                    new Category() {Name = "Other", Id = 4}
                };

                // Adding all entities to the in memory database
                mockContext.Order.AddRange(orders);
                mockContext.Customer.AddRange(customers);
                mockContext.ItemOrderConnection.AddRange(mockIOConnect);
                mockContext.Item.AddRange(mockItems);
                mockContext.ItemTimestamp.AddRange(mockTimestamps);
                mockContext.Service.AddRange(mockServices);
                mockContext.State.AddRange(states);
                mockContext.Category.AddRange(categories);
                mockContext.ServiceState.AddRange(serviceStates);
                mockContext.SaveChanges();
                //! Building DB done
            }
        }

        private void ClearDatabase()
        {
            using(var mockContext = new DataContext(_options))
            {
                // Removing all entities to the in memory database
                mockContext.Order.RemoveRange(mockContext.Order);
                mockContext.Customer.RemoveRange(mockContext.Customer);
                mockContext.ItemOrderConnection.RemoveRange(mockContext.ItemOrderConnection);
                mockContext.Item.RemoveRange(mockContext.Item);
                mockContext.ItemTimestamp.RemoveRange(mockContext.ItemTimestamp);
                mockContext.Service.RemoveRange(mockContext.Service);
                mockContext.State.RemoveRange(mockContext.State);
                mockContext.Category.RemoveRange(mockContext.Category);
                mockContext.ItemArchive.RemoveRange(mockContext.ItemArchive);
                mockContext.OrderArchive.RemoveRange(mockContext.OrderArchive);
                mockContext.ServiceState.RemoveRange(mockContext.ServiceState);
                mockContext.SaveChanges();
            }
        }
    }
}