using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Controllers;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests
{
    // TODO Test for service
    // TODO test for repo
    [TestClass]
    public class CustomerControllerTests
    {
        private CustomerController _customerController;
        private Mock<ICustomerService> _customerServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            // Mocking CustomerService
            _customerServiceMock = new Mock<ICustomerService>();
        }

        // [TestMethod]
        // public void CreateNewCustomer_CheckingResponseIsCreatedAtRoute()
        // {
        //     // Arrange
        //     // Mock service
        //     _customerServiceMock.Setup(method => method.CreateCustomer(null)).Returns(new CustomerDTO 
        //     {
        //         Id = 1,
        //         Name = "Siggi Viggi"
        //     });

        //     // Create controller
        //     _customerController = new CustomerController(_customerServiceMock.Object);

        //     // Create input
        //     CustomerInputModel customer = new CustomerInputModel 
        //     {
        //         Name = "Siggi Viggi"
        //     };

        //     // Act
        //     var response = _customerController.CreateCustomer(customer) as CreatedAtRouteResult;

        //     // Assert
        //     Assert.IsNotNull(response);
        //     Assert.AreEqual("GetCustomerById", response.RouteName);
        //     Assert.AreEqual(1, response.RouteValues["id"]);
        //     // Assert.AreEqual(201, response.StatusCode);
        // }

        [TestMethod]
        public void GetCustomer_response_should_return_200_and_a_customerdto()
        {
             // Arrange
            long id = 10;

            // Mock dto and service
            CustomerDTO mockCustomerDTO = new CustomerDTO
            {
                Id = id,
                Name = "Viggi Siggi"
            };
            _customerServiceMock.Setup(method => method.GetCustomerById(id)).Returns(mockCustomerDTO);

            // Create controller
            _customerController = new CustomerController(_customerServiceMock.Object);

            // Act
            var response = _customerController.GetCustomerById(id) as OkObjectResult;

            //Assert
            // Check if got response with correct status code
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            // Check if response contains correct data
            CustomerDTO customerDTO = response.Value as CustomerDTO;
            Assert.IsNotNull(customerDTO);
            Assert.AreEqual(mockCustomerDTO.Id, customerDTO.Id);
            Assert.AreEqual(mockCustomerDTO.Name, customerDTO.Name);
        }

        [TestMethod]
        public void UpdateCustomerDetails_should_return_200_ok()
        {
            var inp = new CustomerInputModel
            {
                Name = "Siggi Viggi",
                Email = "Siggi@viggi.is",
                Address = "Hvergigata 1898"
            };

            _customerServiceMock.Setup(method => method.UpdateCustomerDetails(It.IsAny<CustomerInputModel>(), It.IsAny<long>())).Verifiable();

            _customerController = new CustomerController(_customerServiceMock.Object);

            var response = _customerController.UpdateCustomerDetails(inp, 1) as OkResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }
    }
}