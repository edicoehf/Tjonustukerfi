using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Models;
using FizzWare.NBuilder;
using NSubstitute;
using System.Collections.Generic;


namespace ThjonustukerfiTests.Tests
{
    [TestClass]
    public class CustomerRepoTests
    {
        private IDataContext _dbContextMock;
        private ICustomerRepo _customerRepo;

        [TestInitialize]
        public void Initialize()
        {
            
            _dbContextMock = Substitute.For<IDataContext>();
        }

        [TestMethod]
        public void GetCustomer_should_()
        {
            //Arrange
            //Mock dbcontext and data
            // var customers = Builder<Customer>.CreateListOfSize(20).Build();
            // _dbContextMock.Customer.Returns(new List<Customer>(customers));
            //Create repo
            //_customerRepo = new CustomerRepo(_dbContextMock, ); Needs to mock or get the automapper
            
        }
    }
}