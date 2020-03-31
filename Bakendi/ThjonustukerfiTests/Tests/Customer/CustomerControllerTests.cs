using System.Collections.Generic;
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

        [TestMethod]
        public void CreateNewCustomer_CheckingResponseIsCreatedAtRoute()
        {
            // Arrange
            // Mock service
            CustomerInputModel customer = new CustomerInputModel 
            {
                Name = "Siggi Viggi"
            };

            _customerServiceMock.Setup(method => method.CreateCustomer(customer)).Returns(new CustomerDTO 
            {
                Id = 1,
                Name = "Siggi Viggi"
            });

            // Create controller
            _customerController = new CustomerController(_customerServiceMock.Object);
            long expectedId = 1;

            // Create input

            // Act
            var response = _customerController.CreateCustomer(customer) as CreatedAtRouteResult;

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("GetCustomerById", response.RouteName);
            Assert.AreEqual(expectedId, response.RouteValues["id"]);
            Assert.AreEqual(201, response.StatusCode);
        }

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

        [TestMethod]
        public void DeleteCustomer_response_should_return_204_noContent()
        {
            // Arrange
            long id = 7;
            
            // mock DTO and service
            CustomerDTO mockCustomerDTO = new CustomerDTO
            {
                Id = id,
                Name = "Siggi Biggi"
            };
            _customerServiceMock.Setup(method => method.DeleteCustomerById(id));

            // Creat contoller
            _customerController = new CustomerController(_customerServiceMock.Object);

            // Act
            var response = _customerController.DeleteCustomerById(id) as NoContentResult;

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);
        }

        [TestMethod]
        public void GetCustomers_should_return_200OK_and_a_list_of_CustomerDto()
        {
            //* Arrange
            var retDTO = new List<CustomerDTO>()
            {
                new CustomerDTO
                {
                    Id = 1,
                    Name = "Siggi Viggi"
                },
                new CustomerDTO
                {
                    Id = 2,
                    Name = "Kalli Valli"
                }
            };
            // Mock method
            _customerServiceMock.Setup(method => method.GetAllCustomers()).Returns(retDTO);

            // Create Controller
            _customerController = new CustomerController(_customerServiceMock.Object);
            
            //* Act
            var response = _customerController.GetAllCustomers() as OkObjectResult;
            List<CustomerDTO> responseValue = response.Value as List<CustomerDTO>;

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(responseValue.Count, retDTO.Count);
        }
    }
}