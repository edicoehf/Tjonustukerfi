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
            Assert.IsInstanceOfType(returnValue, typeof(List<ServiceDTO>));
            Assert.AreEqual(retDTO, returnValue);
        }

        [TestMethod]
        public void GetStates_should_return_a_list_of_service_DTO()
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
            // method setup
            _infoRepoMock.Setup(method => method.GetStates()).Returns(retDTO);

            // Create service
            _infoService = new InfoService(_infoRepoMock.Object);

            //* Act
            var returnValue = _infoService.GetStates();

            //* Assert
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOfType(returnValue, typeof(List<StateDTO>));
            Assert.AreEqual(retDTO, returnValue);
        }

        [TestMethod]
        public void GetCategories_should_return_a_list_of_CategoryDTO()
        {
            //* Arrange
            var retDTO = new List<CategoryDTO>()
            {
                new CategoryDTO { Id = 1, Name = "Lax" },
                new CategoryDTO { Id = 2, Name = "Silungur" }
            };
            // mock method
            _infoRepoMock.Setup(method => method.GetCategories()).Returns(retDTO);

            // Create service
            _infoService = new InfoService(_infoRepoMock.Object);

            //* Act
            var returnValue = _infoService.GetCategories();

            //* Assert
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOfType(returnValue, typeof(List<CategoryDTO>));
            Assert.AreEqual(retDTO, returnValue);
        }
    }
}