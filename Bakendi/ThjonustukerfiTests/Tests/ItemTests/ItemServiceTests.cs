using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Mappings;
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
        private Mapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _itemRepoMock = new Mock<IItemRepo>();

            // Setting up automapper
            var myProfile = new MappingProfile();   // Create a new profile like the one we implemented
            var myConfig = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));   // Setup a configuration with our profile
            _mapper = new Mapper(myConfig); // Create a new mapper with our profile
        }

        [TestMethod]
        public void SearchItem_should_return_a_ItemStateDTO()
        {
            //* Arrange
            long itemID = 1;
            var retDTO = new ItemStateDTO
            {
                Id = itemID,
                OrderId = 1,
                Category = "Test",
                State = "Í vinnslu",
                DateModified = DateTime.Now
            };
            // Mock the method
            _itemRepoMock.Setup(method => method.SearchItem("someString")).Returns(itemID);
            _itemRepoMock.Setup(method => method.GetItemById(itemID)).Returns(retDTO);

            // Create controller
            _itemService = new ItemService(_itemRepoMock.Object, _mapper);

            //* Act
            var response = _itemService.SearchItem("someString");

            //* Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(ItemStateDTO));
        }

        [TestMethod]
        public void GetItemById_should_return_a_single_itemstateDTO()
        {
            //* Arrange
            long itemID = 1;

            // Mock dto and repo
            ItemStateDTO itemstate = new ItemStateDTO
            {
                Id = itemID,
                OrderId = 2,
                Category = "bitar",
                State = "Í vinnslu",
                DateModified = DateTime.Now
            };
            _itemRepoMock.Setup(method => method.GetItemById(itemID)).Returns(itemstate);

            // Create service
            _itemService = new ItemService(_itemRepoMock.Object, _mapper);

            //* Act
            var retVal = _itemService.GetItemById(itemID);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.IsInstanceOfType(retVal, typeof(ItemStateDTO));
        }
    }
}