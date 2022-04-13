using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCbt_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WebCbt_Backend.Models;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod]
        public void DefaultRegistration()
        {
            // Arrange
            
            var mockUserManager = new Mock<UserManager<IdentityUser>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new UsersController(mockConfiguration.Object, mockUserManager.Object);
            var mockUser = new RegisterUser()
            {
                Login = "usrlgn",
                Password = "teiormer",
                Age = 94
            };

            // Act

            var response = controller.RegisterUser(mockUser);

            // Assert

            Assert.AreEqual(response.Result, StatusCodes.Status200OK);

        }

    }
}