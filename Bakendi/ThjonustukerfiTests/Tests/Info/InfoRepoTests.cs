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
                List<ServiceDTO> result = infoRepo.GetServices() as List<ServiceDTO>;

                //* Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Count);
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

                //*
                Assert.IsNotNull(result);
                Assert.AreEqual(DbSize, result.Count());
            }
        }

        //*     Helper functions     *//
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

            // Adding services to the in memory database and saving it
            mockContext.Service.AddRange(services);
            mockContext.State.AddRange(states);
            mockContext.SaveChanges();
        }
    }
}