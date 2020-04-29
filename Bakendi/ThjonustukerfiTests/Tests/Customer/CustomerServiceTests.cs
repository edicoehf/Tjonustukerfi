using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
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
        private Mock<IOrderRepo> _orderRepoMock;

        // This method is excecuted before each test
        [TestInitialize]
        public void Initialize()
        {
            _customerRepoMock = new Mock<ICustomerRepo>();
            _orderRepoMock = new Mock<IOrderRepo>();
        }

        [TestMethod]
        public void CreateCustomer_should_return_a_single_customerDTO()
        {
            //* Arrange
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

            // Craete service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act
            var customerDTOReturn = _customerService.CreateCustomer(inp);

            //* Assert
            Assert.IsNotNull(customerDTOReturn);
            Assert.AreEqual(customerDTOReturn.Id, mockCustomerDTO.Id);
            Assert.AreEqual(customerDTOReturn.Name, mockCustomerDTO.Name);
        }

        [TestMethod]
        public void GetCustomer_should_return_a_single_customerDetailsDTO()
        {
            //* Arrange
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
            // setup mocked method
            _customerRepoMock.Setup(method => method.GetCustomerById(id)).Returns(mockCustomerDetailsDTO);

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act
            var customerDetailsDTO = _customerService.GetCustomerById(id);

            //* Assert
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
            // setup mocked method
            _customerRepoMock.Setup(method => method.GetAllCustomers()).Returns(retDTO);

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act
            var returnvalue = _customerService.GetAllCustomers();

            //* Assert
            Assert.IsNotNull(returnvalue);
            Assert.AreEqual(returnvalue, retDTO);
        }

        [TestMethod]
        public void DeleteCustomerById_should_return_a_OrderDTO_list_of_active_orders()
        {
            //* Arrange
            // mock (do not need to mock customer repo here, since it should not call it)
            _orderRepoMock.Setup(method => method.GetActiveOrdersByCustomerId(It.IsAny<long>())).Returns(CreateOrderDTOList());

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act
            var returnValue = _customerService.DeleteCustomerById(1);

            //* Assert
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOfType(returnValue, typeof(List<OrderDTO>));
            Assert.IsTrue(returnValue.Any());
        }

        [TestMethod]
        public void DeleteCustomerById_should_return_an_empty_OrderDTO_list_of_active_orders()
        {
            //* Arrange
            // mock
            _orderRepoMock.Setup(method => method.GetActiveOrdersByCustomerId(It.IsAny<long>())).Returns(new List<OrderDTO>());
            _customerRepoMock.Setup(method => method.DeleteCustomerById(It.IsAny<long>())).Verifiable();

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act
            var returnValue = _customerService.DeleteCustomerById(1);

            //* Assert
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOfType(returnValue, typeof(List<OrderDTO>));
            Assert.IsFalse(returnValue.Any());
        }

        [TestMethod]
        public void DeleteCustomerByIdAndOrders_should_throw_NotFoundException()
        {
            //* Arrange
            // mock
            _customerRepoMock.Setup(method => method.CustomerExists(It.IsAny<long>())).Returns(false);

            // Create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act and Assert
            Assert.ThrowsException<NotFoundException>(() => _customerService.DeleteCustomerByIdAndOrders(1));
        }

        [TestMethod]
        public void GetPickupOrdersByCustomerId_should_return_list_of_orders()
        {
            //* Arrange
            _customerRepoMock.Setup(method => method.CustomerExists(It.IsAny<long>())).Returns(true);
            _orderRepoMock.Setup(method => method.GetOrdersReadyForPickupByCustomerID(It.IsAny<long>())).Returns(CreateOrderDTOList());

            // create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            var list = _customerService.GetPickupOrdersByCustomerId((long)1);

            Assert.IsNotNull(list);
            Assert.IsInstanceOfType(list, typeof(List<OrderDTO>));
        }

        [TestMethod]
        public void GetPickupOrdersByCustomerId_should_throw_NotFoundException()
        {
            //* Arrange
            _customerRepoMock.Setup(method => method.CustomerExists(It.IsAny<long>())).Returns(false);

            // create service
            _customerService = new CustomerService(_customerRepoMock.Object, _orderRepoMock.Object);

            //* Act and Arrange
            Assert.ThrowsException<NotFoundException>(() => _customerService.GetPickupOrdersByCustomerId((long)1));
        }
        

        //*         Helper functions         *//
        /// <summary>Creates a list of order DTO for testing</summary>
        private List<OrderDTO> CreateOrderDTOList()
        {
            return new List<OrderDTO>()
            {
                new OrderDTO
                {
                Customer = "Kalli Valli",
                Barcode = "0100001111",
                Items = new List<ItemDTO>()
                    {
                        new ItemDTO()
                        {
                            Id = 1,
                            Category = "Ysa bitar",
                            Service = "Birkireyk"
                        }
                    },
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.MinValue,
                    DateCompleted = DateTime.MaxValue
                },
                new OrderDTO
                {
                Customer = "Harpa Varta",
                Barcode = "0100001111",
                Items = new List<ItemDTO>()
                    {
                        new ItemDTO()
                        {
                            Id = 1,
                            Category = "Lax bitar",
                            Service = "Birkireyk"
                        }
                    },
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.MinValue,
                    DateCompleted = DateTime.MaxValue
                }
            };
        }
    }
}