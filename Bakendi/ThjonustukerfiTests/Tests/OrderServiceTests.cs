using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;
using System;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private IOrderService _orderService;
        private Mock<IOrderRepo> _orderRepoMock;
        private Mock<ICustomerRepo> _customerRepoMock;

        [TestInitialize]
        public void Initialize()
        {
            _orderRepoMock = new Mock<IOrderRepo>();
            _customerRepoMock = new Mock<ICustomerRepo>();
        }

        [TestMethod]
        public void CreateOrder_Should_return_a_single_customerDTO()
        {
            // Arrange
            var order = new OrderInputModel
            {
                CustomerId = 1,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel {
                        Type = "Ysa"
                    }
                }
            };

            long mockOrderDTO = 1;

            _orderRepoMock.Setup(method => method.CreateOrder(order)).Returns(mockOrderDTO);
            _customerRepoMock.Setup(method => method.CustomerExists(order.CustomerId)).Returns(true);

            _orderService = new OrderService(_orderRepoMock.Object, _customerRepoMock.Object);

            // Act
            var orderDTOReturn = _orderService.CreateOrder(order);

            // Assert
            Assert.IsNotNull(orderDTOReturn);
            Assert.AreEqual(orderDTOReturn, mockOrderDTO);
        }

        [TestMethod]
        public void CreateOrder_Should_throw_NotFoundException_OnCustomerId()
        {
             // Arrange
            var order = new OrderInputModel
            {
                CustomerId = -1,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel {
                        Type = "Ysa"
                    }
                }
            };  

            // _orderRepoMock.Setup(method => method.CreateOrder(order)).Returns(mockOrderDTO);
            _customerRepoMock.Setup(method => method.CustomerExists(order.CustomerId)).Returns(false);

            _orderService = new OrderService(_orderRepoMock.Object, _customerRepoMock.Object);

            // Act
            Assert.ThrowsException<NotFoundException>(() => _orderService.CreateOrder(order));
        }

        [TestMethod]
        public void GetOrderById_should_return_a_single_customerDetailsDTO()
        {
            //* Arrange
            long id = 10;

            // Mock dto and repo
            OrderDTO mockOrderDTO = new OrderDTO
            {
                Customer = "Kalli Valli",
                Barcode = "0100001111",
                Items = new List<ItemDTO>()
                    {
                        new ItemDTO()
                        {
                            Id = 1,
                            Type = "Ysa bitar",
                            Service = "Birkireyk"
                        }
                    },
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.MinValue,
                    DateCompleted = DateTime.MaxValue
            };
            _orderRepoMock.Setup(method => method.GetOrderbyId(id)).Returns(mockOrderDTO);

            // Create service
            _orderService = new OrderService(_orderRepoMock.Object, _customerRepoMock.Object);

            //* Act
            var response = _orderService.GetOrderbyId(id);

            //* Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OrderDTO));
        }
    }
}