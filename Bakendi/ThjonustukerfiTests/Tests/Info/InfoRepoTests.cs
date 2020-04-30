using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using System.Linq;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Models.Exceptions;
using System;

namespace ThjonustukerfiTests.Tests.Info
{
    [TestClass]
    public class InfoRepoTests
    {
        private Mapper _mapper;
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()    // Initialization before each test, fill the inMemory database
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Thjonustukerfi-info-tests")
                .EnableSensitiveDataLogging()
                .Options;

            FillDatabase();
        }

        [TestCleanup]
        public void Cleanup()   // Cleanup after each test, clear the database after each test
        {
            ClearDatabase();
            _mapper = null;
            _options = null;
        }

        [TestMethod]
        public void GetServices_should_return_an_empty_list()
        {
            //* Arrange
            ClearDatabase();    // Database should be empty hence clear it

            using (var mockContext = new DataContext(_options))
            {
                var infoRepo = new InfoRepo(mockContext, _mapper);

                //* Act
                var result = infoRepo.GetServices();

                //* Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<ServiceDTO>));
                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public void GetStates_should_return_an_empty_list()
        {
            //* Arrange
            ClearDatabase();    // Database should be empty hence clear it

            using (var mockContext = new DataContext(_options))
            {
                var infoRepo = new InfoRepo(mockContext, _mapper);

                //* Act
                var result = infoRepo.GetStates();

                //* Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<StateDTO>));
                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public void GetCategories_should_return_an_empty_list()
        {
            //* Arrange
            ClearDatabase();    // Database should be empty hence clear it

            using(var mockContext = new DataContext(_options))
            {
                var infoRepo = new InfoRepo(mockContext, _mapper);

                //* Act
                var result = infoRepo.GetCategories();

                //* Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<CategoryDTO>));
                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public void Fill_database_should_have_an_inMemory_database_ready()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                Assert.IsNotNull(mockContext);
                Assert.IsTrue(mockContext.Service.Any());
                Assert.IsTrue(mockContext.State.Any());
            }
        }

        [TestMethod]
        public void GetServices_should_return_a_list_of_services_with_the_correct_size()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                // Setup the repo
                var infoRepo = new InfoRepo(mockContext, _mapper);
                var DbSize = mockContext.Service.Count();

                //* Act
                var result = infoRepo.GetServices();

                //* Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<ServiceDTO>));
                Assert.AreEqual(DbSize, result.Count());
            }
        }

        [TestMethod]
        public void GetStates_should_return_a_list_of_statesDTO_with_the_correct_size()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                // setup the repo
                var infoRepo = new InfoRepo(mockContext, _mapper);
                var DbSize = mockContext.State.Count();

                //* Act
                var result = infoRepo.GetStates();

                //* Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<StateDTO>));
                Assert.AreEqual(DbSize, result.Count());
            }
        }

        [TestMethod]
        public void GetCategories_should_return_a_list_of_CategoryDTO_with_correct_size()
        {
            //* Arrange
            using(var mockContext = new DataContext(_options))
            {
                // Setup repo
                var infoRepo = new InfoRepo(mockContext, _mapper);
                var DbTableSize = mockContext.Category.Count();

                //* Act
                var result = infoRepo.GetCategories();

                //* Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(IEnumerable<CategoryDTO>));
                Assert.AreEqual(DbTableSize, result.Count());
            }
        }

        [TestMethod]
        public void GetStateById_should_return_a_valid_state()
        {
            //* Arrange
            long validid = 1;

            using(var mockContext = new DataContext(_options))
            {
                // setup repo
                IInfoRepo infoRepo = new InfoRepo(mockContext, _mapper);

                var correctState = _mapper.Map<StateDTO>(mockContext.State.FirstOrDefault(s => s.Id == validid));

                //* Act
                var value = infoRepo.GetStatebyId(validid);

                //* Assert
                Assert.IsNotNull(value);
                Assert.IsInstanceOfType(value, typeof(StateDTO));
                Assert.AreEqual(correctState, value);
            }
        }

        [TestMethod]
        public void GetStatebyId_should_throw_NotFoundException()
        {
            //* Arrange
            long invalidId = -1;

            using(var mockContext = new DataContext(_options))
            {
                //setup repo
                IInfoRepo infoRepo = new InfoRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(() => infoRepo.GetStatebyId(invalidId));
            }
        }

        [TestMethod]
        public void GetNextStates_should_return_a_list_of_StateDTOs()
        {
            long validServiceID = 1;
            long validStateID = 1;

            using(var mockContext = new DataContext(_options))
            {
                // create repo
                IInfoRepo infoRepo = new InfoRepo(mockContext, _mapper);

                //* Act
                var value = infoRepo.GetNextStates(validServiceID, validStateID);

                //* Assert
                Assert.IsNotNull(value);
                Assert.IsInstanceOfType(value, typeof(List<StateDTO>));
            }
        }

        [TestMethod]
        public void GetItemHistory_should_return_a_list_of_itemTimeStampsDTO()
        {
            //* Arrange
            long itemID = 1;
            using(var mockContext = new DataContext(_options))
            {
                UpdateMapper(mockContext);  // this functions map uses context
                IInfoRepo infoRepo = new InfoRepo(mockContext, _mapper);

                var itemTimestampCount = mockContext.ItemTimestamp.Where(its => its.ItemId == itemID).Count();

                //* Act
                var value = infoRepo.GetItemHistory(itemID);

                //* Assert
                Assert.IsNotNull(value);
                Assert.IsInstanceOfType(value, typeof(List<ItemTimeStampDTO>));
                Assert.AreEqual(itemTimestampCount, value.Count);
            }
        }

        [TestMethod]
        public void GetItemHistory_should_throw_NotFoundException()
        {
            //* Arrange
            long itemID = -100;
            using(var mockContext = new DataContext(_options))
            {
                UpdateMapper(mockContext);  // this functions map uses context
                IInfoRepo infoRepo = new InfoRepo(mockContext, _mapper);

                //* Act and Assert
                Assert.ThrowsException<NotFoundException>(()=> infoRepo.GetItemHistory(itemID));
            }
        }

        //**********     Helper functions     **********//
        private void FillDatabase()
        {
            using(var mockContext = new DataContext(_options))
            {
                UpdateMapper(mockContext);  // update the mapper with the new context

                var orders = new List<Order>()
                {
                    new Order()
                    {
                        Id = 1,
                        CustomerId = 1,
                        Barcode = "82374892374",
                        JSON = "",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue,
                        NotificationCount = 0
                    }
                };

                var itemOrderConnections = new List<ItemOrderConnection>()
                {
                    new ItemOrderConnection { Id = 1, ItemId = 1, OrderId = 1 }
                };

                var items = new List<Item>()
                {
                    new Item()
                    {
                        Id = 1,
                        CategoryId = 1,
                        StateId = 5,
                        ServiceId = 1,
                        Barcode = "4567890",
                        JSON = @"{""location"":""Vinnslu"",""sliced"":true,""filleted"":false,""otherCategory"":"""",""otherService"":""""}",
                        Details = "Hello",
                        DateCreated = DateTime.MinValue,
                        DateModified = DateTime.Now,
                        DateCompleted = DateTime.MaxValue
                    }
                };

                var now = DateTime.Now;
                var item1Timestamps = new List<ItemTimestamp>()
                {
                    new ItemTimestamp { Id = 1, ItemId = 1, StateId = 1, TimeOfChange = now.AddHours(1) },
                    new ItemTimestamp { Id = 2, ItemId = 1, StateId = 2, TimeOfChange = now.AddHours(2) },
                    new ItemTimestamp { Id = 3, ItemId = 1, StateId = 3, TimeOfChange = now.AddHours(3) },
                    new ItemTimestamp { Id = 4, ItemId = 1, StateId = 4, TimeOfChange = now.AddHours(4) },
                    new ItemTimestamp { Id = 5, ItemId = 1, StateId = 5, TimeOfChange = now.AddHours(5) }
                };

                var services = new List<Service>()
                {
                    new Service { Id = 1, Name = "Birkireyking" },
                    new Service { Id = 2, Name = "Taðreyking" },
                    new Service { Id = 3, Name = "Grafið" }
                };

                var states = new List<State>()
                {
                    new State() {Name = "Í vinnslu", Id = 1},
                    new State() {Name = "Kælir 1", Id = 2},
                    new State() {Name = "Kælir 2", Id = 3},
                    new State() {Name = "Frystir", Id = 4},
                    new State() {Name = "Sótt", Id = 5}
                };

                var categories = new List<Category>()
                {
                    // Categories for Reykofninn
                    new Category() {Name = "Lax", Id = 1},
                    new Category() {Name = "Silungur", Id = 2},
                    new Category() {Name = "Lambakjöt", Id = 3}
                };

                var serviceStates = new List<ServiceState>()
                {
                    // Service state fyrir birkireykingu
                    new ServiceState() {Id = 1, ServiceId = 1, StateId = 1, Step = 1},
                    new ServiceState() {Id = 2, ServiceId = 1, StateId = 2, Step = 2},
                    new ServiceState() {Id = 3, ServiceId = 1, StateId = 3, Step = 2},
                    new ServiceState() {Id = 4, ServiceId = 1, StateId = 4, Step = 2},
                    new ServiceState() {Id = 5, ServiceId = 1, StateId = 5, Step = 3}
                };

                // Adding services to the in memory database and saving it
                mockContext.Service.AddRange(services);
                mockContext.State.AddRange(states);
                mockContext.Category.AddRange(categories);
                mockContext.ServiceState.AddRange(serviceStates);
                mockContext.Order.AddRange(orders);
                mockContext.ItemOrderConnection.AddRange(itemOrderConnections);
                mockContext.Item.AddRange(items);
                mockContext.ItemTimestamp.AddRange(item1Timestamps);
                mockContext.SaveChanges();
            }
        }

        /// <summary>Updates the private mapper to have the current mock data context</summary>
        private void UpdateMapper(DataContext context)
        {
            // Needs a seperete automapper that has access to the current instance of db context
            var myProfile = new MappingProfile(context);   // Create a new profile like the one we implemented
            var myConfig = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));   // Setup a configuration with our profile
            _mapper = new Mapper(myConfig); // Create a new mapper with our profile
        }

        private void ClearDatabase()
        {
            using(var mockContext = new DataContext(_options))
            {
                mockContext.Service.RemoveRange(mockContext.Service);
                mockContext.State.RemoveRange(mockContext.State);
                mockContext.Category.RemoveRange(mockContext.Category);
                mockContext.ServiceState.RemoveRange(mockContext.ServiceState);
                mockContext.Order.RemoveRange(mockContext.Order);
                mockContext.ItemOrderConnection.RemoveRange(mockContext.ItemOrderConnection);
                mockContext.Item.RemoveRange(mockContext.Item);
                mockContext.ItemTimestamp.RemoveRange(mockContext.ItemTimestamp);
                mockContext.SaveChanges();
            }
        }
    }
}