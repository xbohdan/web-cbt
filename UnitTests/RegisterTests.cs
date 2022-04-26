using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCbt_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebCbt_Backend.Models;
using Moq;
using WebCbt_Backend.Data;

namespace UnitTests
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod]
        public void DefaultRegistration()
        {
            // Arrange
            var mockUser = new RegisterUser()
            {
                Login = "usrlgn",
                Password = "teiormer",
                Gender = "other"
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
            var controller = new UserController(mockConfiguration.Object, mockContext.Object, mockUserManager.Object);

            // Act
            var actual = controller.RegisterUser(mockUser);

            // Assert
            var expected = new OkResult();
            Assert.AreEqual(expected.GetType(), actual.Result.GetType());

        }
        [TestMethod]
        public void RegisterExistingUser()
        {
            // Arrange
            var mockUser = new RegisterUser()
            {
                Login = "nikita555",
                Password = "sisKa_5",
                Gender = "Male",
                Age = 19
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
            var controller = new UserController(mockConfiguration.Object, mockContext.Object, mockUserManager.Object);

            // Act
            var actual1 = controller.RegisterUser(mockUser);
            var actual2 = controller.RegisterUser(mockUser);

            // Assert
            var expected1 = new OkResult();
            var expected2 = new ConflictResult();

            Assert.AreEqual(expected1.GetType(), actual1.Result.GetType());
            Assert.AreEqual(expected2.GetType(), actual2.Result.GetType());
        }
        

    }
}