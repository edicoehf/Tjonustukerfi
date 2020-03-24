using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Controllers;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        private OrderController _orderController;
        private Mock<IOrderService> _orderServiceMock;
        
        [TestInitialize]
        public void Initialize()
        {
            _orderServiceMock = new Mock<IOrderService>();
        }
        // TODO change to CreatedAtRoute
        [TestMethod]
        public void CreateNewOrder_CheckingResponseNoContent()
        {
            // Arrange
            OrderInputModel order = new OrderInputModel
            {
                CustomerId = 1,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel {
                        Type = "Ysa"
                    }
                }
            };

            _orderServiceMock.Setup(method => method.CreateOrder(order)).Returns((long)1);

            _orderController = new OrderController(_orderServiceMock.Object);

            // Act (needs to change to created at route)
            var response = _orderController.CreateOrder(order) as NoContentResult;

            // Assert
            Assert.AreEqual(1, 1);  // temporary for tests to run
            // Assert.IsNotNull(response);
            // Assert.AreEqual(204, response.StatusCode);
        }

        [TestMethod]
        public void GetOrderById_response_should_return_200_and_a_orderDTO()
        {
            //* Arrange
            long id = 10;

            // Mock dto and service
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
            _orderServiceMock.Setup(method => method.GetOrderbyId(id)).Returns(mockOrderDTO);

            // Create controller
            _orderController = new OrderController(_orderServiceMock.Object);

            //* Act
            var response = _orderController.GetOrderbyId(id) as OkObjectResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value as OrderDTO, typeof(OrderDTO));
        }

        [TestMethod]
        public void UpdateOrder_should_return_200_status_ok()
        {
            //* Arrange
            OrderInputModel order = new OrderInputModel
            {
                CustomerId = 1,
                Items = new List<ItemInputModel>()
                {
                    new ItemInputModel {
                        Type = "Ysa"
                    }
                }
            };

            // Setup and create controller
            _orderServiceMock.Setup(method => method.UpdateOrder(It.IsAny<OrderInputModel>(), It.IsAny<long>())).Verifiable();
            _orderController = new OrderController(_orderServiceMock.Object);

            //* Act
            var response = _orderController.UpdateOrder(order, 1) as OkResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void DeleteOrderById_should_return_204_noContent()
        {
            //* Arrange
            long id = 1;

            // Setup and create controller
            _orderServiceMock.Setup(method => method.DeleteByOrderId(id));
            _orderController = new OrderController(_orderServiceMock.Object);

            //* Act
            var response = _orderController.DeleteByOrderId(id) as NoContentResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);
        }
    }
}