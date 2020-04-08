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

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class OrderRepoTests
    {
        private Mapper _mapper;
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            // Setting up automapper and dbContext options
            var myProfile = new MappingProfile();   // Create a new profile like the one we implemented
            var myConfig = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));   // Setup a configuration with our profile
            _mapper = new Mapper(myConfig); // Create a new mapper with our profile

            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ThjonuskerfiDB")
                .EnableSensitiveDataLogging()
                .Options;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mapper = null;
            _options = null;
        }

        [TestMethod]
        public void GetAllOrders_should_return_an_empty_list()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
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
                //! Note this test method is creating the in memory database,
                //! so this arrange when build DB should only be run once, unless adding more info to it.
                //! This Database will live through all the tests in this class
                FillDatabase(mockContext);

                //* Assert
                // Checks if all added entities are actually there
                Assert.IsNotNull(mockContext);
                Assert.IsTrue(mockContext.Order.Any());
                Assert.IsTrue(mockContext.Customer.Any());
                Assert.IsTrue(mockContext.ItemOrderConnection.Any());
                Assert.IsTrue(mockContext.Item.Any());
                Assert.IsTrue(mockContext.Service.Any());
                Assert.IsTrue(mockContext.State.Any());
            }
        }

        [TestMethod]
        public void GetOrderById_should_return_orderDTO()
        {
            using (var mockContext = new DataContext(_options))
            {
                //* Arrange
                string serviceName = "Birkireyking";
                long orderId = 100;
                string customerName = "Viggi Siggi";
                string OrderBarCode = "20200001";
                DateTime modifiedDate = mockContext.Order.FirstOrDefault(o => o.Id == orderId).DateModified;

                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Create a list that should be the same as the list returned in OrderDTO
                var itemListDTO = new List<ItemDTO>();
                var mockItems = mockContext.Item.ToList();
                foreach (var item in mockItems)
                {
                    var add = _mapper.Map<ItemDTO>(item);
                    add.Service = serviceName;
                    itemListDTO.Add(add);
                }

                //* Act
                OrderDTO orderDTO = orderRepo.GetOrderbyId(orderId);

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
                Assert.AreEqual(orderDTO.Items.Count, mockItems.Count);
                Assert.AreEqual(orderDTO.Items[0], itemListDTO[0]);
                Assert.AreEqual(orderDTO.Items[1], itemListDTO[1]);
            };
        }

        [TestMethod]
        public void GetOrderbyId_should_throw_NotFoundException()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => orderRepo.GetOrderbyId(-1));
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
                // Get all items added to the database using ItemOrderConnection
                var itemListDTO = new List<Item>();
                foreach (var item in itemOrderConnectionDTOList)
                {
                    var add = _mapper.Map<Item>(mockContext.Item.Find(item.ItemId));
                    itemListDTO.Add(add);
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
        public void GetActiveOrdersByCustomerId_should_retrieve_correct_order()
        {
            //* Arrange
            long customerId = 50;
            long orderIdExpected = 100;
            
            // This order was created in the build database test. This person should only have this one order
            using(var mockContext = new DataContext(_options))
            {
                IOrderRepo orderRepo = new OrderRepo(mockContext, _mapper);

                //* Act
                var activeList = orderRepo.GetActiveOrdersByCustomerId(customerId);

                Assert.IsNotNull(activeList);
                Assert.AreEqual(1, activeList.Count);
                Assert.AreEqual(orderIdExpected, activeList[0].Id);
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_order_correctly_and_itemlist_should_grow()
        {
            long orderID = 100;
            long custId = 500;
            //* Arrange
            var orperInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 2
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 3
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 4
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
                Assert.AreEqual(newConnection.Count, oldConnection.Count + 1);  // list is going from two to three
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(newItemList.Count, oldItemList.Count + 1);      // list is going from two to three
                // check type
                Assert.AreEqual(newItemList[0].CategoryId, orperInput.Items[0].CategoryId);
                Assert.AreEqual(newItemList[1].CategoryId, orperInput.Items[1].CategoryId);
                Assert.AreEqual(newItemList[2].CategoryId, orperInput.Items[2].CategoryId);
                // check service ID
                Assert.AreEqual(newItemList[0].ServiceId, orperInput.Items[0].ServiceId);
                Assert.AreEqual(newItemList[1].ServiceId, orperInput.Items[1].ServiceId);
                Assert.AreEqual(newItemList[2].ServiceId, orperInput.Items[2].ServiceId);
            }
        }

        [TestMethod]
        public void UpdateOrder_should_update_order_correctly_and_itemlist_should_shrink()
        {
            long orderID = 100;
            long custId = 500;
            //* Arrange
            var orperInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 1,
                        ServiceId = 2
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
                Assert.AreEqual(newConnection.Count, oldConnection.Count - 2);  // list is going from 3 to one
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(newItemList.Count, oldItemList.Count - 2);      // list is going from 3 to one
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
            long custId = 500;
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
            long custId = 500;
            //* Arrange
            var clearItems = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
            };
            var orperInput = new OrderInputModel
            {
                CustomerId = custId,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        CategoryId = 2,
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

                // Get old item order connection to get the old list of items.
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(clearItems, orderID);
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
                Assert.AreEqual(newConnection.Count, oldConnection.Count + 5);  // list is going from zero to five
                
                // Assert items
                Assert.IsNotNull(newItemList);
                Assert.AreEqual(newItemList.Count, oldItemList.Count + 5);      // list is going from zero to five
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
                }

                //* Act
                orderRepo.CompleteOrder(orderID);

                orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var newItemStates = new List<Item>();
                foreach (var item in itemOrderConnection)
                {
                    newItemStates.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
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

                var orderTableSize = mockContext.Order.Count();
                var itemTableSize = mockContext.Item.Count();
                var itemOrderConnectionTableSize = mockContext.ItemOrderConnection.Count();

                // size of item list
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();

                //* Act
                orderRepo.DeleteByOrderId(orderID);

                //* Assert
                Assert.AreEqual(orderTableSize - 1, mockContext.Order.Count());
                Assert.AreEqual(itemTableSize - newConnection.Count, mockContext.Item.Count());
                Assert.AreEqual(itemOrderConnectionTableSize - newConnection.Count, mockContext.ItemOrderConnection.Count());
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

        //**********     Helper functions     **********//
        private void FillDatabase(DataContext mockContext)
        {
            //! Note this test method is creating the in memory database,
            //! so this arrange when build DB should only be run once, unless adding more info to it.
            //! This Database will live through all the tests in this class
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
                DateCreated = DateTime.MinValue,
                DateModified = modifiedDate,
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

            // just for this order
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
                    DateCreated = DateTime.MinValue,
                    DateModified = DateTime.Now,
                }
            };

            // Adding service
            var mockServices = new List<Service>()
            {
                new Service() { Name = serviceName, Id = 1 },
                new Service() { Name = "Taðreyking", Id = 2 },
                new Service() { Name = "Viðarreyking", Id = 3 },
                new Service() { Name = "Salt pækill", Id = 4 }
            };

            // Build a list of size 20, make it queryable for the database mock
            var orders = Builder<Order>.CreateListOfSize(20)
                .TheFirst(1)
                .With(o => o.Id = mockOrder.Id)
                .With(o => o.CustomerId = mockOrder.CustomerId)
                .With(o => o.Barcode = mockOrder.Barcode)
                .With(o => o.DateCreated = mockOrder.DateCreated)
                .With(o => o.DateModified = mockOrder.DateModified)
                .With(o => o.DateCompleted = mockOrder.DateCompleted)
                .TheRest()
                .With(o => o.Barcode = "30200001")
                .With(o => o.CustomerId = 2)
                .Build();

            // Build a list of size 20, make it queryable for the database mock
            var customers = Builder<Customer>.CreateListOfSize(20)
                .TheFirst(1)
                .With(c => c.Id = mockCustomer.Id)
                .With(c => c.Name = mockCustomer.Name)
                .With(c => c.SSN = mockCustomer.SSN)
                .With(c => c.Email = mockCustomer.Email)
                .With(c => c.Phone = mockCustomer.Phone)
                .With(c => c.Address = mockCustomer.Address)
                .With(c => c.PostalCode = mockCustomer.PostalCode)
                .Build();

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
                new Category() {Name = "Silungur", Id = 2}
            };

            // Adding all entities to the in memory database
            mockContext.Order.AddRange(orders);
            mockContext.Customer.AddRange(customers);
            mockContext.ItemOrderConnection.AddRange(mockIOConnect);
            mockContext.Item.AddRange(mockItems);
            mockContext.Service.AddRange(mockServices);
            mockContext.State.AddRange(states);
            mockContext.Category.AddRange(categories);
            mockContext.SaveChanges();
            //! Building DB done
        }
    }
}