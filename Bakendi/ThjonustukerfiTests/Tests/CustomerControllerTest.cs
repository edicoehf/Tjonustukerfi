using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Controllers;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        private CustomerController _customerController;
        private Mock<ICustomerService> _customerServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            // Mocking CustomerService
            _customerServiceMock = new Mock<ICustomerService>();
            _customerServiceMock.Setup(method => method.CreateCustomer(null)).Returns(new CustomerDTO 
            {
                Id = 1,
                Name = "Siggi Viggi"
            });

            _customerController = new CustomerController(_customerServiceMock.Object);
        }

        [TestMethod]
        public void CreateNewCustomer_CheckingResponseIsCreatedAtRoute()
        {
            // Arrange
            CustomerInputModel customer = new CustomerInputModel 
            {
                Name = "Siggi Viggi"
            };

            // Act
            var createdResponse = _customerController.CreateCustomer(customer);
            var okResult = createdResponse as NoContentResult;

            // Assert
            Assert.AreEqual(204, okResult.StatusCode);
        }
    }
}