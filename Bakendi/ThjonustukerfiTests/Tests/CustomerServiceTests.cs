using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private ICustomerService _customerService;
        private Mock<ICustomerRepo> _customerRepoMock;

        // This method is excecuted before each test
        [TestInitialize]
        public void Initialize()
        {
            _customerRepoMock = new Mock<ICustomerRepo>();
        }

        [TestMethod]
        public void CreateCustomer_should_return_a_single_customerDTO()
        {
            // Arrange
            var inp = new CustomerInputModel
            {
                Name = "Viggi Siggi",
                Email = "VS@vigsig.is"
            };

            // Mock dto and repo
            CustomerDTO mockCustomerDTO = new CustomerDTO
            {
                Id = 10,
                Name = inp.Name
            };

            // Config returns values
            _customerRepoMock.Setup(method => method.CreateCustomer(inp)).Returns(mockCustomerDTO);

            _customerService = new CustomerService(_customerRepoMock.Object);

            // Act
            var customerDTOReturn = _customerService.CreateCustomer(inp);

            // Assert
            Assert.IsNotNull(customerDTOReturn);
            Assert.AreEqual(customerDTOReturn.Id, mockCustomerDTO.Id);
            Assert.AreEqual(customerDTOReturn.Name, mockCustomerDTO.Name);
        }

        [TestMethod]
        public void GetCustomer_should_return_a_single_customerDetailsDTO()
        {
            // Arrange
            long id = 10;

            // Mock dto and repo
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
            _customerRepoMock.Setup(method => method.GetCustomerById(id)).Returns(mockCustomerDetailsDTO);

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object);

            // Act
            var customerDetailsDTO = _customerService.GetCustomerById(id);

            //Assert
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
        public void GetAllCustomers_should_return_a_list_of_CustomerDTO()
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
            _customerRepoMock.Setup(method => method.GetAllCustomers()).Returns(retDTO);

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object);

            //* Act
            var returnvalue = _customerService.GetAllCustomers();

            //* Assert
            Assert.IsNotNull(returnvalue);
            Assert.AreEqual(returnvalue, retDTO);
        }
    }
}