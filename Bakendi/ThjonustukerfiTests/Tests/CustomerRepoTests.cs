using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Models;
using FizzWare.NBuilder;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ThjonustukerfiWebAPI.Mappings;
using System.Diagnostics;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class CustomerRepoTests
    {
        // private IDataContext _dbContextMock;
        private ICustomerRepo _customerRepo;
        private Mock<DataContext> _mockContext;

        [TestInitialize]
        public void Initialize()
        {
            
            // Add automapper to use in the repo constructor
            var myProfile = new MappingProfile();       // Create a new profile like the one we implemented
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));  // Setup a configuration with our profile
            var mapper = new Mapper(configuration);     // Create a new mapper with our profile

            // Mock dbcontext and data
            // Build a list of size 20, make it queryable for the database mock
            var customers = Builder<Customer>.CreateListOfSize(20)
                .TheFirst(1).With(x => x.Name = "Viggi Siggi").With(x => x.Id = 10).With(x => x.Email = "VS@vigsig.is")
                .Build();
            var customersQueryable = customers.AsQueryable();
        
            // Setup the Mock for Customer DbSet
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customersQueryable.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customersQueryable.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customersQueryable.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customersQueryable.GetEnumerator());
            mockSet.Setup(m => m.Remove(It.IsAny<Customer>())).Callback((Customer c) => customers.Remove(c));

            // Create and setup a mock of our DataContext class that is injected to the customer repo constructor
            _mockContext = new Mock<DataContext>();
            _mockContext.Setup(c => c.Customer).Returns(mockSet.Object);
            
            // Create a new customer repository and inject the mock for our database and our mapper
            _customerRepo = new CustomerRepo(_mockContext.Object, mapper);
        }

        [TestMethod]
        public void GetCustomer_should_return_customer_with_id_10()
        {
            // Arrange
            
            // Act
            var result = _customerRepo.GetCustomerById(10);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Viggi Siggi");
            Assert.AreEqual(result.Id, 10);
        }
        [TestMethod]
        public void DeleteCustomer_should_remove_customer_with_id_10()
        {
            // Arrange

            // Act
            _customerRepo.DeleteCustomerById(10);
            var result = _mockContext.Object.Customer.FirstOrDefault(c => c.Id == 10);
            Debug.WriteLine("check result");
            Debug.WriteLine(result);
            // Assert
            Assert.IsNull(result);
        }
    }
}