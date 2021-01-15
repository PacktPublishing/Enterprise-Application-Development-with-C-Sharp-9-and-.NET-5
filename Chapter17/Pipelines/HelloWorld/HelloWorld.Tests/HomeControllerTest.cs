using FakeItEasy;
using HelloWorld.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace HelloWorld.Tests
{
    public class HomeControllerTest
    {
        ILogger<HomeController> logger;

        public HomeControllerTest()
        {
            logger = A.Fake<ILogger<HomeController>>();

        }

        [Fact]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(logger);
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Privacy()
        {
            // Arrange
            HomeController controller = new HomeController(logger);
            // Act
            ViewResult result = controller.Privacy() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }
    }
}
