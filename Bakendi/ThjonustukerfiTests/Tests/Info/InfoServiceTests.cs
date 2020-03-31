using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests.Info
{
    [TestClass]
    public class InfoServiceTests
    {
        private IInfoService _infoService;
        private Mock<IInfoRepo> _infoRepoMock;

        [TestInitialize]
        public void Initialize()
        {
            _infoRepoMock = new Mock<IInfoRepo>();
        }

        [TestMethod]
        public void GetServices_should_return_a_list_of_ServiceDTO()
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
            // setup the method being used
            _infoRepoMock.Setup(method => method.GetServices()).Returns(retDTO);

            // Create Service
            _infoService = new InfoService(_infoRepoMock.Object);

            //* Act
            var returnValue = _infoService.GetServices();

            //* Assert
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(returnValue, retDTO);
        }
    }
}