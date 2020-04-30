using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Controllers;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests.Info
{
    [TestClass]
    public class InfoControllerTests
    {
        private InfoController _infoController;
        private Mock<IInfoService> _infoServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            // Mock infoservice
            _infoServiceMock = new Mock<IInfoService>();
        }

        [TestMethod]
        public void GetServices_should_return_200OK_and_a_list_of_ServiceDTO()
        {
            //* Arrange
            var retDTO = new List<ServiceDTO>
            {
                new ServiceDTO
                {
                    Id = 1,
                    Name = "Birkireyking"
                },
                new ServiceDTO
                {
                    Id = 2,
                    Name = "Taðreyking"
                }
            };
            // Mock method
            _infoServiceMock.Setup(method => method.GetServices()).Returns(retDTO);

            // Create controller
            _infoController = new InfoController(_infoServiceMock.Object);

            //* Act
            var response = _infoController.GetServices() as OkObjectResult;
            List<ServiceDTO> responseValue = response.Value as List<ServiceDTO>;

            //* Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(responseValue);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(retDTO, responseValue);
        }

        [TestMethod]
        public void GetStates_should_return_200OK_and_a_list_of_states()
        {
            //* Arrange
            var retDTO = new List<StateDTO>
            {
                new StateDTO
                {
                    Id = 1,
                    Name = "Í vinnslu"
                },
                new StateDTO
                {
                    Id = 2,
                    Name = "Kælir 1"
                }
            };

            // Mock method
            _infoServiceMock.Setup(method => method.GetStates()).Returns(retDTO);

            // Create controller
            _infoController = new InfoController(_infoServiceMock.Object);

            //* Act
            var response = _infoController.GetStates() as OkObjectResult;
            List<StateDTO> responseValue = response.Value as List<StateDTO>;

            //* Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(responseValue);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(retDTO, responseValue);
        }

        [TestMethod]
        public void GetCategories_should_return_200OK_and_a_list_of_states()
        {
            //* Arrange
            var retDTO = new List<CategoryDTO>()
            {
                new CategoryDTO { Id = 1, Name = "Lax" },
                new CategoryDTO { Id = 2, Name = "Silungur" }
            };

            //Mock method
            _infoServiceMock.Setup(method => method.GetCategories()).Returns(retDTO);

            // Create controller
            _infoController = new InfoController(_infoServiceMock.Object);

            //* Act
            var response = _infoController.GetCategories() as OkObjectResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Value);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
            Assert.IsInstanceOfType(response.Value as List<CategoryDTO>, typeof(List<CategoryDTO>));
        }

        [TestMethod]
        public void GetItemHistory_should_return_a_list_of_an_items_history()
        {
            //* Arrange
            var retDTO = new List<ItemTimeStampDTO>()
            {
                new ItemTimeStampDTO { ItemId = 1, StateId = 1, State = "working", TimeOfChange = DateTime.MinValue }
            };

            // mock method
            _infoServiceMock.Setup(method => method.GetItemHistory(It.IsAny<long>())).Returns(retDTO);

            // create controller
            _infoController = new InfoController(_infoServiceMock.Object);

            //* Act
            var response = _infoController.GetItemHistory((long)1) as OkObjectResult;

            //* Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
            Assert.IsInstanceOfType(response.Value as List<ItemTimeStampDTO>, typeof(List<ItemTimeStampDTO>));
        }
    }
}