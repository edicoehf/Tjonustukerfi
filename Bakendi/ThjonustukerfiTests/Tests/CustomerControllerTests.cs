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
        public void GetCustomer_response_should_return_200_and_a_customerdetailsdto()
        {
             // Arrange
            long id = 10;

            // Mock dto and service
            CustomerDetailsDTO mockCustomerDetailsDTO = new CustomerDetailsDTO
            {
                Id = id,
                Name = "Viggi Siggi",
                SSN = "1308943149",
                Email = "viggi@siggi.is",
                Phone = "5812345",
                Address = "Bakkabakki 1",
                PostalCode = "800"
            };
            _customerServiceMock.Setup(method => method.GetCustomerById(id)).Returns(mockCustomerDetailsDTO);

            // Create controller
            _customerController = new CustomerController(_customerServiceMock.Object);

            // Act
            var response = _customerController.GetCustomerById(id) as OkObjectResult;

            //Assert
            // Check if got response with correct status code
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            // Check if response contains correct data
            CustomerDetailsDTO customerDetailsDTO = response.Value as CustomerDetailsDTO;
            Assert.IsNotNull(customerDetailsDTO);
            Assert.AreEqual(mockCustomerDetailsDTO.Id, customerDetailsDTO.Id);
            Assert.AreEqual(mockCustomerDetailsDTO.Name, customerDetailsDTO.Name);
            Assert.AreEqual(mockCustomerDetailsDTO.SSN, customerDetailsDTO.SSN);
            Assert.AreEqual(mockCustomerDetailsDTO.Email, customerDetailsDTO.Email);
            Assert.AreEqual(mockCustomerDetailsDTO.Phone, customerDetailsDTO.Phone);
            Assert.AreEqual(mockCustomerDetailsDTO.Address, customerDetailsDTO.Address);
            Assert.AreEqual(mockCustomerDetailsDTO.PostalCode, customerDetailsDTO.PostalCode);
        }
    }
}