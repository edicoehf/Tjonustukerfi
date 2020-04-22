using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Controllers;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
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
                Category = "Test",
                State = "Í vinnslu",
                DateModified = DateTime.Now
            };
            // Mock the method
            _itemServiceMock.Setup(method => method.SearchItem("someString")).Returns(retDTO).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.SearchItem("someString") as OkObjectResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value as ItemStateDTO, typeof(ItemStateDTO));
        }

        [TestMethod]
        public void EditItem_should_return_200_status_ok()
        {
            var item = new EditItemInput
            {
                CategoryId = 1
            };

            // setup and create controller
            _itemServiceMock.Setup(method => method.EditItem(It.IsAny<EditItemInput>(), It.IsAny<long>())).Verifiable();
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.EditItem(item, 1) as OkResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void FinishItem_should_return_200_status_ok()
        {
            //* Arrange
            long itemID = 1;

            // setup and create controller
            _itemServiceMock.Setup(method => method.CompleteItem(It.IsAny<long>())).Verifiable();
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.CompleteItem(itemID) as OkResult;
            
            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void GetItemById_should_respond_200OK_and_a_itemstateDTO()
        {
            //* Arrange
            long itemID = 1;

            // Mock dto and service
            ItemStateDTO itemstate = new ItemStateDTO
            {
                Id = itemID,
                OrderId = 2,
                Category = "bitar",
                State = "Í vinnslu",
                DateModified = DateTime.Now
            };
            _itemServiceMock.Setup(method => method.GetItemById(itemID)).Returns(itemstate);

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.GetItemById(itemID) as OkObjectResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value as ItemStateDTO, typeof(ItemStateDTO));
        }

        [TestMethod]
        public void RemoveItem_should_return_no_content_response()
        {
            //* Arrange
            // Mock service
            _itemServiceMock.Setup(method => method.RemoveItem(It.IsAny<long>())).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.RemoveItem(1) as NoContentResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);
        }

        [TestMethod]
        public void RemoveItemQuery_should_return_NoContent_response()
        {
            //* Arrange
            // Mock service
            _itemServiceMock.Setup(method => method.RemoveItemQuery(It.IsAny<string>())).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.RemoveItemQuery("some random string") as NoContentResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);
        }

        [TestMethod]
        public void ChangeItemStateById_should_return_an_OKresult()
        {
            //* Arrange
            var emptyList = new List<ItemStateChangeInputIdScanner>();
            // Mock Service
            _itemServiceMock.Setup(method => method.ChangeItemStateById(It.IsAny<List<ItemStateChangeInputIdScanner>>())).Returns(emptyList).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.ChangeItemStateById(emptyList) as OkResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void ChangeItemStateById_should_return_AcceptedResult_and_list_of_invalid_inputs()
        {
            //* Arrange
            var input = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = -1, StateChangeBarcode = @"Í Vinnslu-{location:""hilla1A""}" },
                new ItemStateChangeInputIdScanner { ItemId = 1, StateChangeBarcode = @"Kælir1-{location:""hilla1A""}" }
            };

            var invalidInputs = new List<ItemStateChangeInputIdScanner>()
            {
                new ItemStateChangeInputIdScanner { ItemId = -1, StateChangeBarcode = @"Í Vinnslu-{location:""hilla1A""}" }
            };

            // Mock service
            _itemServiceMock.Setup(method => method.ChangeItemStateById(input)).Returns(invalidInputs).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.ChangeItemStateById(input) as AcceptedResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status202Accepted, response.StatusCode);
            
            var retVal = response.Value as List<ItemStateChangeInputIdScanner>;
            Assert.AreEqual(invalidInputs.Count, retVal.Count);
        }

        [TestMethod]
        public void ChangeItemStateBarcode_should_return_an_OKresult()
        {
            //* Arrange
            var emptyList = new List<ItemStateChangeBarcodeScanner>();
            // Mock Service
            _itemServiceMock.Setup(method => method.ChangeItemStateBarcode(It.IsAny<List<ItemStateChangeBarcodeScanner>>())).Returns(emptyList).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.ChangeItemStateBarcode(emptyList) as OkResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void ChangeItemStateByBarcode_should_return_AcceptedResult_and_list_of_invalid_inputs()
        {
            //* Arrange
            var input = new List<ItemStateChangeBarcodeScanner>()
            {
                new ItemStateChangeBarcodeScanner { ItemBarcode = "-1", StateChangeBarcode = @"Í Vinnslu-{location:""hilla1A""}" },
                new ItemStateChangeBarcodeScanner { ItemBarcode = "983764892374", StateChangeBarcode = @"Kælir1-{location:""hilla1A""}" }
            };
            
            var invalidInputs = new List<ItemStateChangeBarcodeScanner>()
            {
                new ItemStateChangeBarcodeScanner { ItemBarcode = "-1", StateChangeBarcode = @"Kælir1-{location:""hilla1A""}" }
            };

            // Mock service
            _itemServiceMock.Setup(method => method.ChangeItemStateBarcode(input)).Returns(invalidInputs).Verifiable();

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var response = _itemController.ChangeItemStateBarcode(input) as AcceptedResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status202Accepted, response.StatusCode);
            
            var retVal = response.Value as List<ItemStateChangeBarcodeScanner>;
            Assert.AreEqual(invalidInputs.Count, retVal.Count);
        }

        [TestMethod]
        public void GetItemNextState_should_return_a_NextStatesDTO_when_searching_by_id()
        {
            long inpID = 1;
            string inpString = "This a barcode, yes";
            //* Arrange
            var retList = new NextStatesDTO()
            {
                NextAvailableStates = new List<StateDTO>()
            };

            // Mock service
            _itemServiceMock.Setup(method => method.GetItemNextStates(It.IsAny<long>())).Returns(retList);
            _itemServiceMock.Setup(method => method.GetItemNextStatesByBarcode(It.IsAny<string>())).Returns(retList);

            // Create controller
            _itemController = new ItemController(_itemServiceMock.Object);

            //* Act
            var responseId =        _itemController.GetItemNextStates(inpID, null) as OkObjectResult;
            var responseString =    _itemController.GetItemNextStates(null, inpString) as OkObjectResult;
            var responseNull =      _itemController.GetItemNextStates(null, null) as BadRequestObjectResult;

            //* Assert
            // Assert ID call
            Assert.IsNotNull(responseId);
            Assert.AreEqual(StatusCodes.Status200OK, responseId.StatusCode);
            Assert.IsInstanceOfType(responseId.Value as NextStatesDTO, typeof(NextStatesDTO));

            // Assert barcode call
            Assert.IsNotNull(responseString);
            Assert.AreEqual(StatusCodes.Status200OK, responseString.StatusCode);
            Assert.IsInstanceOfType(responseString.Value as NextStatesDTO, typeof(NextStatesDTO));

            // Assert empty call
            Assert.IsNotNull(responseNull);
            Assert.AreEqual(StatusCodes.Status400BadRequest, responseNull.StatusCode);
            Assert.IsInstanceOfType(responseNull.Value as string, typeof(string));  // make sure that the error response text was returned
        }
    }
}