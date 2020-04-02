using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests.ItemTests
{
    [TestClass]
    public class ItemServiceTests
    {
        private IItemService _itemService;
        private Mock<IItemRepo> _itemRepoMock;

        [TestInitialize]
        public void Initialize()
        {
            _itemRepoMock = new Mock<IItemRepo>();
        }

        [TestMethod]
        public void SearchItem_should_return_a_ItemStateDTO()
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
            _itemRepoMock.Setup(method => method.SearchItem("someString")).Returns(retDTO);

            // Create controller
            _itemService = new ItemService(_itemRepoMock.Object);

            //* Act
            var response = _itemService.SearchItem("someString");

            //* Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(ItemStateDTO));
        }
    }
}