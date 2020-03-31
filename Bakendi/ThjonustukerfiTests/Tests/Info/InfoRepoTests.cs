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
        public void GetServices_should_return_a_list_of_services_with_the_correct_size()
        {
            //* Arrange
            using (var mockContext = new DataContext(_options))
            {
                var services = new List<Service>()
                {
                    new Service { Id = 1, Name = "Birkireyking" },
                    new Service { Id = 2, Name = "Taðreyking" },
                    new Service { Id = 3, Name = "Grafið" }
                };

                //! Add only once, unless appending changes. This database will live throughout this test class
                // Adding services to the in memory database and saving it
                mockContext.Service.AddRange(services);
                mockContext.SaveChanges();

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
    }
}