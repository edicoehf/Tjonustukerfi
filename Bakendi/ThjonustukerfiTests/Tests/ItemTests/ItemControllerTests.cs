using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Controllers;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests.ItemTests
{
    [TestClass]
    public class ItemControllerTests
    {
        private ItemController _itemController;
        private Mock<IItemService> _itemServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _itemServiceMock = new Mock<IItemService>();
        }

        [TestMethod]
        public void SearchItem_should_return_200OK_and_a_ItemStateDTO()
        {
            //* Arrange
            var retDTO = new ItemStateDTO
            {
                Id = 1,
                OrderId = 1,
                Type = "Test",
                State = "Í vinnslu",
                DateModified = DateTime.Now
            };
            // Mock the method
            _itemServiceMock.Setup(method => method.SearchItem("someString")).Returns(retDTO);

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.SearchItem("someString") as OkObjectResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value as ItemStateDTO, typeof(ItemStateDTO));
        }
    }
}