// Unit Test Project
namespace DriverApi.Tests.Tests
{
    using Xunit;
    using Moq;
    using System.Data;
    using Dapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Common;
    using Api.Controllers.v1;
    using DriverTask;

    public class DriversControllerTests
    {
        private readonly Mock<IDriverRepository> _mockDb;
        private readonly DriverController _controller;

        public DriversControllerTests()
        {
            _mockDb = new Mock<IDriverRepository>();
            _controller = new DriverController(_mockDb.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            var drivers = new List<Driver> { new() { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", PhoneNumber = "1234567890" } };
            _mockDb.Setup(db => db.GetAll()).Returns(drivers);
            var result = _controller.GetAllAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDrivers = Assert.IsType<List<Driver>>(okResult.Value);
            Assert.Single(returnedDrivers);
        }

        [Fact]
        public void GetById_ExistingId_ReturnsOk()
        {
            var driver = new Driver { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", PhoneNumber = "1234567890" };
            _mockDb.Setup(db => db.GetById(It.IsAny<int>())).Returns(driver);

            var result = _controller.GetByIdAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedDriver = Assert.IsType<Driver>(okResult.Value);
            Assert.Equal(driver.Id, returnedDriver.Id);
        }

        [Fact]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            _mockDb.Setup(db => db.GetById(It.IsAny<int>())).Returns((Driver)null);

            var result = _controller.GetByIdAsync(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
