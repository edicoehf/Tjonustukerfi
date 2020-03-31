using System.Collections.Generic;
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

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(responseValue);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(retDTO, responseValue);
        }
    }
}