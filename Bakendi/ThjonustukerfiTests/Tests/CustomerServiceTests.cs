using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class CustomerServiceTests
    {
        private ICustomerService _customerService;
        private Mock<ICustomerRepo> _customerRepoMock;

        // This method is excecuted before each test
        [TestInitialize]
        public void Initialize()
        {
            _customerRepoMock = new Mock<ICustomerRepo>();
        }

        [TestMethod]
        public void CreateCustomer_should_return_a_single_customerDTO()
        {
            // Arrange
            var inp = new CustomerInputModel
            {
                Name = "Viggi Siggi",
                Email = "VS@vigsig.is"
            };

            // Config returns values
            _customerRepoMock.Setup(method => method.CreateCustomer(inp)).Returns(new CustomerDTO
            {
                Id = 10,
                Name = inp.Name
            });

            _customerService = new CustomerService(_customerRepoMock.Object);

            // Act
            var customerDTOReturn = _customerService.CreateCustomer(inp);

            // Assert
            Assert.IsNotNull(customerDTOReturn);
            Assert.AreEqual(customerDTOReturn.Id, 10);
            Assert.AreEqual(customerDTOReturn.Name, "Viggi Siggi");
        }
    }
}