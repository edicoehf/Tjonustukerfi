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
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

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

        [TestMethod]
        public void SearchItems_should_throw_NotFoundException()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                var itemRepo = new ItemRepo(mockContext, _mapper);

                string inp = "This should never work as a barcode I would think...";

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => itemRepo.SearchItem(inp));
            }
        }

        [TestMethod]
        public void EditItem_should_edit_correct_items()
        {
            //* Arrange
            string type = "This is not a fish";
            long stateID = 3;
            long serviceID = 2;
            long orderID = 100;
            long itemID = 1;
            long orderChange = 101;
            var itemInput = new EditItemInput
            {
                Type = type,
                StateId = stateID,
                ServiceID = serviceID,
                OrderId = orderChange
            };

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                // get everything before change
                // order:
                var oldconnections1 = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> oldItemList1 = new List<Item>();
                foreach (var item in oldconnections1)
                {
                    oldItemList1.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }
                var oldOrder1ListSize = oldItemList1.Count;
                var oldOrder2ListSize = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderChange).Count();

                // item:
                var trackedEntity = oldItemList1.FirstOrDefault(i => i.Id == itemID);
                var oldItemEntity = new Item()  // Copying item values (not the reference to the object)
                {
                    Id = trackedEntity.Id,
                    Type = trackedEntity.Type,
                    StateId = trackedEntity.StateId,
                    ServiceId = trackedEntity.ServiceId,
                    Barcode = trackedEntity.Barcode,
                    JSON = trackedEntity.JSON,
                    DateCreated = trackedEntity.DateCreated,
                    DateModified = trackedEntity.DateModified,
                    DateCompleted = trackedEntity.DateCompleted
                };

                //* Act
                itemRepo.EditItem(itemInput, itemID);

                // Should be updated order
                var newconnections1 = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderID).ToList();
                List<Item> newItemList1 = new List<Item>();
                foreach (var item in newconnections1)
                {
                    newItemList1.Add(mockContext.Item.FirstOrDefault(i => i.Id == item.ItemId));
                }
                var newOrder1ListSize = newItemList1.Count;

                // Item
                var changedItem = mockContext.Item.FirstOrDefault(i => i.Id == itemID);
                var newOrder2ListSize = mockContext.ItemOrderConnection.Where(ioc => ioc.OrderId == orderChange).Count();

                //* Assert
                Assert.IsNotNull(changedItem);
                Assert.AreNotEqual(oldItemEntity, changedItem);             // items have been updated
                Assert.AreEqual(oldOrder1ListSize - 1, newOrder1ListSize);  // has been removed from the order
                Assert.AreEqual(oldOrder2ListSize + 1, newOrder2ListSize);  // has been moved to other list

                // Check that item properties have been updated correctly
                Assert.AreEqual(type, changedItem.Type);
                Assert.AreEqual(stateID, changedItem.StateId);
                Assert.AreEqual(serviceID, changedItem.ServiceId);
                Assert.AreEqual(orderChange, mockContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == changedItem.Id).OrderId);
            }
        }

        [TestMethod]
        public void EditItem_should_throw_correct_exceptions()
        {
            //* Arrange
            // All these inputs should throw exceptions, but for different reasons
            var input1 = new EditItemInput { OrderId = null };
            var input2 = new EditItemInput { StateId = -1 };
            var input3 = new EditItemInput { ServiceID = -1 };
            var input4 = new EditItemInput { OrderId = -1 };

            long itemID = 2;

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<BadRequestException>(() => itemRepo.EditItem(input1, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input2, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input3, itemID));
                Assert.ThrowsException<NotFoundException>(() => itemRepo.EditItem(input4, itemID));
            }
        }

        [TestMethod]
        public void FinishItem_should_update_entity_to_done_state()
        {
            //* Arrange
            long itemID = 1;

            using(var mockContext = new DataContext(_options))
            {
                IItemRepo itemRepo = new ItemRepo(mockContext, _mapper);

                var oldStateId = mockContext.Item.FirstOrDefault(i => i.Id == itemID).StateId;

                //* Act
                itemRepo.CompleteItem(itemID);

                //* Assert
                var itemEntity = mockContext.Item.FirstOrDefault(i => i.Id == itemID);

                Assert.IsNotNull(itemEntity);
                Assert.AreNotEqual(oldStateId, itemEntity.StateId);
                Assert.AreEqual(5, itemEntity.StateId); //TODO: same as in the method in repo, too hardcoded state as five. Change later
            }
        }

        //**********     Helper functions     **********//
        private void FillDatabase(DataContext mockContext)
        {
            //! Building Db
            // variables used
            DateTime modifiedDate = DateTime.Now;
            string serviceName = "Birkireyking";
            string serviceName2 = "Taðreyking";

            //* customer 1
            long orderId = 100;
            long customerId = 50;
            string customerName = "Viggi Siggi";
            string OrderBarCode = "20200001";
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

            //* customer 2
            long orderId2 = 101;
            long customerId2 = 51;
            string customerName2 = "Kalli Valli";
            string OrderBarCode2 = "20200002";

            // Mock entity
            Order mockOrder2 = new Order
            {
                Id = orderId2,
                CustomerId = customerId2,
                Barcode = OrderBarCode2,
                DateCreated = DateTime.MinValue,
                DateModified = modifiedDate,
                DateCompleted = DateTime.MaxValue
            };

            Customer mockCustomer2 = new Customer
            {
                Id = customerId2,
                Name = customerName2,
                SSN = "1308943149",
                Email = "valli@kalli.is",
                Phone = "5812345",
                Address = "Gulligull 1",
                PostalCode = "400"
            };

            // just for order 1
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
                .TheNext(1)
                .With(o => o.Id = mockOrder2.Id)
                .With(o => o.CustomerId = mockOrder2.CustomerId)
                .With(o => o.Barcode = mockOrder2.Barcode)
                .With(o => o.DateCreated = mockOrder2.DateCreated)
                .With(o => o.DateModified = mockOrder2.DateModified)
                .With(o => o.DateCompleted = mockOrder2.DateCompleted)
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
                .TheNext(1)
                .With(c => c.Id = mockCustomer2.Id)
                .With(c => c.Name = mockCustomer2.Name)
                .With(c => c.SSN = mockCustomer2.SSN)
                .With(c => c.Email = mockCustomer2.Email)
                .With(c => c.Phone = mockCustomer2.Phone)
                .With(c => c.Address = mockCustomer2.Address)
                .With(c => c.PostalCode = mockCustomer2.PostalCode)
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