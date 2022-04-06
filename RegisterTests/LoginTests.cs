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
    public class LoginTests
    {
        [TestMethod]
        public void Login_ExistingUser()
        {
            // Arrange

            var mockExistingUser = new User()
            {
                UserID = 1,
                Login = "usrlgn",
                Password = "teiormer",
                Age = 94,
                UserStatus = 12,
                Banned = false
            };
            var mockUserManager = new Mock<UserManager<IdentityUser>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new UsersController(mockConfiguration.Object, mockUserManager.Object);

            // Act

            var response = controller.LoginUser(mockExistingUser);

            // Assert

            Assert.AreEqual(response.Result, StatusCodes.Status200OK);

        }

        [TestMethod]
        public void Login_NonExistingUser()
        {
            // Arrange

            var mockNonExistingUser = new User()
            {
                UserID = -1,
                Login = "usrlgn",
                Password = "teiormer",
                Age = 94,
                UserStatus = 12,
                Banned = false
            };
            var mockUserManager = new Mock<UserManager<IdentityUser>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new UsersController(mockConfiguration.Object, mockUserManager.Object);

            // Act

            var response = controller.LoginUser(mockNonExistingUser);

            // Assert

            Assert.AreEqual(response.Result, StatusCodes.Status401Unauthorized);

        }
    }
}
