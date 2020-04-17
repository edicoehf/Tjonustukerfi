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

namespace ThjonustukerfiTests.Tests.Info
{
    [TestClass]
    public class InfoRepoTests
    {
        private Mapper _mapper;
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            // adding automapper with the configuration from the project
            var profile = new MappingProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(config);

            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "Thjonustukerfi-info-tests")
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
        public void GetServices_should_return_an_empty_list()
        {
            //* Arrange
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
                //! Add only once, unless appending changes. This database will live throughout this test class
                FillDatabase(mockContext);

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

        //**********     Helper functions     **********//
        private void FillDatabase(DataContext mockContext)
        {
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
            mockContext.SaveChanges();
        }
    }
}