using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
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
using System;

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
                .Build().AsQueryable();
            Debug.WriteLine(customers);
            // Setup the Mock for Customer DbSet
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.GetEnumerator());

            // Create and setup a mock of our DataContext class that is injected to the customer repo constructor
            _mockContext = new Mock<DataContext>();
            _mockContext.Setup(c => c.Customer).Returns(mockSet.Object);
            
            // Create a new customer repository and inject the mock for our database and our mapper
            _customerRepo = new CustomerRepo(_mockContext.Object, mapper);
        }

        [TestMethod]
        public void GetCustomer_should_return_customer_with_id_10()
        {
            // Act
            var result = _customerRepo.GetCustomerById(10);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Viggi Siggi");
            Assert.AreEqual(result.Id, 10);
        }

        [TestMethod]
        public void GetCustomer_should_throw_NotFoundException()
        {
            Assert.ThrowsException<NotFoundException>(() => _customerRepo.GetCustomerById(-1));
        }
    }
}