using System;
using System.Collections.Generic;
using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
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
    }
}