using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCbt_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCbt_Backend.Models;
using Moq;
using WebCbt_Backend.Data;

namespace UnitTests
{
    //[TestClass]
    public class LoginTests
    {
        //[TestMethod]
        public void Login_ExistingUser()
        {
            // Arrange
            var mockExistingUser = new LoginUser
            {
                Login = "usrlgn",
                Password = "teiormer"
            };
            var userStore = new Mock<IUserStore<IdentityUser>>();
            var passwordHasher = new Mock<IPasswordHasher<IdentityUser>>();
            var userValidators = new List<IUserValidator<IdentityUser>>
            {
                new UserValidator<IdentityUser>()
            };
            var passwordValidators = new List<IPasswordValidator<IdentityUser>>
            {
                new PasswordValidator<IdentityUser>()
            };
            var mockConfiguration = new Mock<IConfiguration>();
            var mockContext = new Mock<WebCbtDbContext>();
            var mockUserManager =
                new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasher.Object,
                userValidators, passwordValidators, null, null, null, null);
            var controller = new UsersController(mockConfiguration.Object, mockContext.Object, mockUserManager.Object);

            // Act
            var response = controller.LoginUser(mockExistingUser);

            // Assert
            var expected = new UnauthorizedResult();
            Assert.AreEqual(response.Result.GetType(), expected.GetType());

        }

        //[TestMethod]
        public void Login_NonExistingUser()
        {
            // Arrange
            var mockExistingUser = new LoginUser
            {
                Login = "usrlgn",
                Password = "teiormer"
            };
            var userStore = new Mock<IUserStore<IdentityUser>>();
            var passwordHasher = new Mock<IPasswordHasher<IdentityUser>>();
            var userValidators = new List<IUserValidator<IdentityUser>>
            {
                new UserValidator<IdentityUser>()
            };
            var passwordValidators = new List<IPasswordValidator<IdentityUser>>
            {
                new PasswordValidator<IdentityUser>()
            };
            var mockConfiguration = new Mock<IConfiguration>();
            var mockContext = new Mock<WebCbtDbContext>();
            var mockUserManager =
                new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasher.Object,
                userValidators, passwordValidators, null, null, null, null);
            var controller = new UsersController(mockConfiguration.Object, mockContext.Object, mockUserManager.Object);

            // Act
            var actual = controller.LoginUser(mockExistingUser);

            // Assert
            var expected = new UnauthorizedResult();
            Assert.AreEqual(expected.GetType(), actual.Result.GetType());

        }
    }
}
