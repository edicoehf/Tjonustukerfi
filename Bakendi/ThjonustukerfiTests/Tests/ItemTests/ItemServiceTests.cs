using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
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
        private Mock<IInfoRepo> _infoRepoMock;
        private Mapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _itemRepoMock = new Mock<IItemRepo>();
            _infoRepoMock = new Mock<IInfoRepo>();

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
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

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
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

            //* Act
            var retVal = _itemService.GetItemById(itemID);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.IsInstanceOfType(retVal, typeof(ItemStateDTO));
        }

        [TestMethod]
        public void ChangeItemStateBarcode_should_return_invalid_list()
        {
            string validBarcode = "983764892374";
            string invalidbarcode = "-1";
            //* Arrange
            var input = new List<ItemStateChangeBarcodeInputModel>()
            {
                new ItemStateChangeBarcodeInputModel { Barcode = invalidbarcode, StateChangeTo = 1 },
                new ItemStateChangeBarcodeInputModel { Barcode = validBarcode, StateChangeTo = 2 }
            };
            
            var invalidInputs = new List<ItemStateChangeBarcodeInputModel>()
            {
                new ItemStateChangeBarcodeInputModel { Barcode = invalidbarcode, StateChangeTo = 1 }
            };

            // Mock Repo
            _itemRepoMock.Setup(method => method.SearchItem(invalidbarcode)).Throws(new NotFoundException("Some message"));
            _itemRepoMock.Setup(method => method.SearchItem(validBarcode)).Returns(1);
            _itemRepoMock.Setup(method => method.ChangeItemStateById(It.IsAny<List<ItemStateChangeInputModel>>()))
                .Returns(new List<ItemStateChangeInputModel>());

            // create service
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

            //* Act
            var retVal = _itemService.ChangeItemStateBarcode(input);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.AreEqual(invalidInputs.Count, retVal.Count);
            Assert.AreEqual(invalidInputs[0], retVal[0]);
        }

        [TestMethod]
        public void ChangeItemStateBarcode_should_return_empty_list()
        {
            string validBarcode = "983764892374";
            string validBarcode2 = "983764892375";
            //* Arrange
            var input = new List<ItemStateChangeBarcodeInputModel>()
            {
                new ItemStateChangeBarcodeInputModel { Barcode = validBarcode, StateChangeTo = 1 },
                new ItemStateChangeBarcodeInputModel { Barcode = validBarcode2, StateChangeTo = 2 }
            };

            // Mock Repo
            _itemRepoMock.Setup(method => method.SearchItem(validBarcode)).Returns(1);
            _itemRepoMock.Setup(method => method.ChangeItemStateById(It.IsAny<List<ItemStateChangeInputModel>>()))
                .Returns(new List<ItemStateChangeInputModel>());

            // create service
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

            //* Act
            var retVal = _itemService.ChangeItemStateBarcode(input);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.AreEqual(0, retVal.Count);
        }

        [TestMethod]
        public void ChangeItemStateById_should_return_empty_list()
        {
            long validId = 1;
            long validId2 = 2;
            //* Arrange
            var input = new List<ItemStateChangeInputModel>()
            {
                new ItemStateChangeInputModel { ItemId = validId, StateChangeTo = 1 },
                new ItemStateChangeInputModel { ItemId = validId2, StateChangeTo = 2 }
            };

            // Mock Repo
            _itemRepoMock.Setup(method => method.ChangeItemStateById(It.IsAny<List<ItemStateChangeInputModel>>()))
                .Returns(new List<ItemStateChangeInputModel>());

            // create service
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

            //* Act
            var retVal = _itemService.ChangeItemStateById(input);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.AreEqual(0, retVal.Count);
        }

        [TestMethod]
        public void GetItemNextStates_should_return_NextStatesDTO()
        {
            //* Arrange
            long itemId = 1;
            var stateDTOs = new List<StateDTO>()
            {
                new StateDTO {Id = 2, Name = "fridge1"},
                new StateDTO {Id = 3, Name = "fridge2"},
                new StateDTO {Id = 4, Name = "freezer"},
            };

            // Mock repo
            _itemRepoMock.Setup(method => method.GetItemEntity(itemId)).Returns(new Item() { StateId = 1, ServiceId = 1 });
            _infoRepoMock.Setup(method => method.GetStatebyId(1)).Returns(new StateDTO() { Id = 1, Name = "production" });
            _infoRepoMock.Setup(method => method.GetNextStates(1, 1)).Returns(stateDTOs);

            // Create service
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

            // * Act
            var retVal = _itemService.GetItemNextStates(itemId);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.IsInstanceOfType(retVal, typeof(NextStatesDTO));
            Assert.AreEqual(stateDTOs.Count, retVal.NextAvailableStates.Count);
        }

        [TestMethod]
        public void GetItemNextStatesByBarcode_should_return_NextStatesDTO()
        {
            //* Arrange
            string barcode = "is this a barcode?";
            long itemId = 1;
            var stateDTOs = new List<StateDTO>()
            {
                new StateDTO {Id = 2, Name = "fridge1"},
                new StateDTO {Id = 3, Name = "fridge2"},
                new StateDTO {Id = 4, Name = "freezer"},
            };

            // Mock repo
            _itemRepoMock.Setup(method => method.SearchItem(barcode)).Returns(itemId);
            _itemRepoMock.Setup(method => method.GetItemEntity(itemId)).Returns(new Item() { StateId = 1, ServiceId = 1 });
            _infoRepoMock.Setup(method => method.GetStatebyId(1)).Returns(new StateDTO() { Id = 1, Name = "production" });
            _infoRepoMock.Setup(method => method.GetNextStates(1, 1)).Returns(stateDTOs);

            // Create service
            _itemService = new ItemService(_itemRepoMock.Object, _infoRepoMock.Object, _mapper);

            // * Act
            var retVal = _itemService.GetItemNextStatesByBarcode(barcode);

            //* Assert
            Assert.IsNotNull(retVal);
            Assert.IsInstanceOfType(retVal, typeof(NextStatesDTO));
            Assert.AreEqual(stateDTOs.Count, retVal.NextAvailableStates.Count);
        }
    }
}