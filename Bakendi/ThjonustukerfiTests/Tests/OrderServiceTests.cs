using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;


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

            OrderDTO mockOrderDTO = new OrderDTO
            {
                Id = 1,
                Barcode = "20200001"
            };

            _orderRepoMock.Setup(method => method.CreateOrder(order)).Returns(mockOrderDTO);
            _customerRepoMock.Setup(method => method.CustomerExists(order.CustomerId)).Returns(true);

            _orderService = new OrderService(_orderRepoMock.Object, _customerRepoMock.Object);

            // Act
            var orderDTOReturn = _orderService.CreateOrder(order);

            // Assert
            Assert.IsNotNull(orderDTOReturn);
            Assert.AreEqual(orderDTOReturn.Id, mockOrderDTO.Id);
            Assert.AreEqual(orderDTOReturn.Barcode, mockOrderDTO.Barcode);
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

    }
}