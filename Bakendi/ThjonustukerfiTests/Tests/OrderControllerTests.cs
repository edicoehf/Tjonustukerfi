using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

            _orderServiceMock.Setup(method => method.CreateOrder(order)).Returns(new OrderDTO
            {
                Id = 1,
                Barcode = "20200001"
            });

            _orderController = new OrderController(_orderServiceMock.Object);

            // Act
            var response = _orderController.CreateOrder(order) as NoContentResult;

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);
        }
    }
}