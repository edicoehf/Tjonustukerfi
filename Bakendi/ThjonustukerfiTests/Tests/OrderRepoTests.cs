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
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Repositories.Implementations;

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
        public void GetOrderById_should_return_orderDTO()
        {
            //! Note this test method is creating the in memory database,
            //! so this arrange when build DB should only be run once, unless adding more info to it.
            //! This Database will live through all the tests in this class
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
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
                    DateCompleted = DateTime.MaxValue
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
                        Type = "Ysa bitar",
                        StateId = 1,
                        ServiceId = 1,
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue
                    },
                    new Item
                    {
                        Id = 2,
                        Type = "Lax heil flok",
                        StateId = 1,
                        ServiceId = 1,
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue
                    }
                };

                // Adding service
                Service mockService = new Service()
                {
                    Name = serviceName,
                    Id = 1
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

                // Adding all entities to the in memory database
                mockContext.Order.AddRange(orders);
                mockContext.Customer.AddRange(customers);
                mockContext.ItemOrderConnection.AddRange(mockIOConnect);
                mockContext.Item.AddRange(mockItems);
                mockContext.Service.Add(mockService);
                mockContext.SaveChanges();
                //! Building DB done

                var orderRepo = new OrderRepo(mockContext, _mapper);

                // Create a list that should be the same as the list returned in OrderDTO
                var itemListDTO = new List<ItemDTO>();
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
                Assert.AreEqual(orderDTO.DateCompleted, DateTime.MaxValue);

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
            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                Assert.ThrowsException<NotFoundException>(() => orderRepo.GetOrderbyId(-1));
            }
        }

        [TestMethod]
        public void CreateOrder_should_create_and_return_OrderId()
        {
            // Arrange
            var inp = new OrderInputModel
            {
                CustomerId = 1,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel 
                    {
                        Type = "Ysa",
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        Type = "Lax",
                        ServiceId = 2
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

                // Act
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

                // Assert

                // Assert Order
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(long));
                Assert.AreEqual(resultOrderDTO.CustomerId, inp.CustomerId);
                Assert.AreEqual(mockContext.Order.Count(), orderDbSize + 1);

                // Assert Item
                Assert.AreEqual(mockContext.Item.Count(), itemDbSize + inp.Items.Count());
                Assert.AreEqual(itemListDTO[0].Type, inp.Items[0].Type);
                Assert.AreEqual(itemListDTO[0].ServiceId, inp.Items[0].ServiceId);
                Assert.AreEqual(itemListDTO[1].Type, inp.Items[1].Type);
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
                        Type = "BREYTT",
                        ServiceId = 2
                    },
                    new ItemInputModel 
                    {
                        Type = "BREYTT",
                        ServiceId = 3
                    },
                    new ItemInputModel 
                    {
                        Type = "OGSTAEKKA",
                        ServiceId = 4
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(orperInput, orderID);

                //* Assert
                orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
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
                Assert.AreEqual(newItemList[0].Type, orperInput.Items[0].Type);
                Assert.AreEqual(newItemList[1].Type, orperInput.Items[1].Type);
                Assert.AreEqual(newItemList[2].Type, orperInput.Items[2].Type);
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
                        Type = "MINNKA",
                        ServiceId = 2
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(orperInput, orderID);

                //* Assert
                orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
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
                Assert.AreEqual(newItemList[0].Type, orperInput.Items[0].Type);
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

                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var oldConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList = new List<Item>();
                foreach (var item in oldConnection)
                {
                    oldItemList.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }

                //* Act
                orderRepo.UpdateOrder(orperInput, orderID);

                //* Assert
                orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
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
                        Type = "STÆKKUN",
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        Type = "STÆKKUN",
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        Type = "STÆKKUN",
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        Type = "STÆKKUN",
                        ServiceId = 1
                    },
                    new ItemInputModel 
                    {
                        Type = "STÆKKUN",
                        ServiceId = 1
                    }
                }
            };

            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                var orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
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
                orderEntity = mockContext.Order.FirstOrDefault(o => o.Id == orderID);
                var newConnection = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
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
                Assert.AreEqual(newItemList[0].Type, "STÆKKUN");
                Assert.AreEqual(newItemList[1].Type, "STÆKKUN");
                Assert.AreEqual(newItemList[2].Type, "STÆKKUN");
                Assert.AreEqual(newItemList[3].Type, "STÆKKUN");
                Assert.AreEqual(newItemList[4].Type, "STÆKKUN");
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
        public void DeleteORderById_should_throw_NotFoundException()
        {
            using (var mockContext = new DataContext(_options))
            {
                var orderRepo = new OrderRepo(mockContext, _mapper);

                Assert.ThrowsException<NotFoundException>(() => orderRepo.DeleteByOrderId(-1));
            }
        }
    }
}