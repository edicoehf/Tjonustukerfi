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
using ThjonustukerfiWebAPI.Repositories.Implementations;

namespace ThjonustukerfiTests.Tests.ItemTests
{
    [TestClass]
    public class ItemRepoTests
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
                .UseInMemoryDatabase(databaseName: "ThjonuskerfiDB-Item")
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
        public void Fill_database_should_have_an_inMemory_database_ready()
        {
            //! Note this test method is creating the in memory database,
            //! so this arrange when build DB should only be run once, unless adding more info to it.
            //! This Database will live through all the tests in this class
            //*Arrange
            using(var mockContext = new DataContext(_options))
            {
                FillDatabase(mockContext);

                //* Act

                //* Assert
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
        public void SearchItems_should_return_the_correct_ItemStateDTO()
        {
            //* Arrange
            string barcodeToSearch = "50500002";
            using(var mockContext = new DataContext(_options))
            {
                // Create repo
                var itemRepo = new ItemRepo(mockContext, _mapper);
                // Get correct entity
                var correctEntity = mockContext.Item.FirstOrDefault(i => i.Barcode == barcodeToSearch);
                // Map to DTO
                var correctDTO = _mapper.Map<ItemStateDTO>(correctEntity);
                // Get correct values for the rest of the DTO
                correctDTO.OrderId = mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == correctDTO.Id).OrderId;
                correctDTO.State = mockContext.State.FirstOrDefault(s => s.Id == correctEntity.StateId).Name;

                //* Act
                var returnValue = itemRepo.SearchItem(barcodeToSearch);

                //* Assert
                Assert.IsNotNull(returnValue);
                Assert.IsInstanceOfType(returnValue, typeof(ItemStateDTO));
                Assert.AreEqual(correctDTO, returnValue);
            }
        }

        // [TestMethod]
        // public void SearchItems_should_throw_NotFoundException()
        // {
        //     using(var mockContext = new DataContext(_options))
        //     {
        //         var orderRepo = new OrderRepo(mockContext, _mapper);

        //         string inp = "This should never work as a barcode I would think...";

        //         Assert.ThrowsException<NotFoundException>(() => orderRepo.SearchItem(inp));
        //     }
        // }

        //*     Helper functions     *//
        private void FillDatabase(DataContext mockContext)
        {
            // variables evaluating
                long orderId = 100;
                long customerId = 50;
                string customerName = "Viggi Siggi";
                string OrderBarCode = "20200001";
                DateTime modifiedDate = DateTime.Now;
                string serviceName = "Birkireyking";
                string serviceName2 = "Taðreyking";
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
                        Barcode = "50500001",
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
                        Barcode = "50500002",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue
                    }
                };

                // Adding service
                var MockServiceList = new List<Service>()
                {
                    new Service()
                    {
                        Name = serviceName,
                        Id = 1
                    },
                    new Service()
                    {
                        Name = serviceName2,
                        Id = 2
                    }
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

                // Adding all entities to the in memory database
                mockContext.Order.AddRange(orders);
                mockContext.Customer.AddRange(customers);
                mockContext.ItemOrderConnection.AddRange(mockIOConnect);
                mockContext.Item.AddRange(mockItems);
                mockContext.Service.AddRange(MockServiceList);
                mockContext.State.AddRange(states);
                mockContext.SaveChanges();
                //! Building DB done
        }
    }
}