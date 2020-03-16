using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models;
using FizzWare.NBuilder;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class CustomerRepoTests
    {
        // private IDataContext _dbContextMock;
        private Mapper _mapper;
        private DbContextOptions<DataContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            
            // Add automapper to use in the repo constructor
            var myProfile = new MappingProfile();       // Create a new profile like the one we implemented
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));  // Setup a configuration with our profile
            _mapper = new Mapper(configuration);     // Create a new mapper with our profile

            _options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "Customer")
            .EnableSensitiveDataLogging()
            .Options;
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _mapper = null;
            _options = null;
        }

        [TestMethod]
        public void GetCustomer_should_return_customer_with_id_100()
        {
            // Arrange
            using (var mockContext = new DataContext(_options))
            {
                //! Add only ONCE! Unless appending changes
                // Build a list of size 20, make it queryable for the database mock
                var customers = Builder<Customer>.CreateListOfSize(20)
                .TheFirst(1).With(x => x.Name = "Viggi Siggi").With(x => x.Id = 100).With(x => x.Email = "VS@vigsig.is")
                .Build();

                mockContext.Customer.AddRange(customers);
                mockContext.SaveChanges();

                var customerRepo = new CustomerRepo(mockContext, _mapper);

                // Act
                var result = customerRepo.GetCustomerById(100);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Name, "Viggi Siggi");
                Assert.AreEqual(result.Id, 100);
            }
        }

        [TestMethod]
        public void CreateCustomer_should_create_and_return_customerDTO()
        {
            // Arrange
            var inp = new CustomerInputModel
            {
                Name = "Kalli Valli",
                Email = "KV@vigsig.is"
            };

            using (var mockContext = new DataContext(_options))
            {
                var customerRepo = new CustomerRepo(mockContext, _mapper);

                var dbSize = mockContext.Customer.Count();

                // Act
                var result = customerRepo.CreateCustomer(inp);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(CustomerDTO));
                Assert.AreEqual(mockContext.Customer.Count(), dbSize + 1);
                Assert.AreEqual(result.Name, inp.Name);
                Assert.IsInstanceOfType(result.Id, typeof(long));
            };
        }

        [TestMethod]
        public void GetCustomer_should_throw_NotFoundException()
        {
            using (var mockContext = new DataContext(_options))
            {
                var customerRepo = new CustomerRepo(mockContext, _mapper);

                Assert.ThrowsException<NotFoundException>(() => customerRepo.GetCustomerById(-1));
            }
        }
    }
}