using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCbt_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebCbt_Backend.Models;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class RegisterTests
    {
        //[TestMethod]
        public void DefaultRegistration()
        {
            // Arrange
            var mockUser = new RegisterUser()
            {
                Login = "usrlgn",
                Password = "teiormer",
                Age = 94
            };

            // user manager constructor elements:
            var usrStore = new Mock<IUserStore<IdentityUser>>();
            var paswdh = new Mock<IPasswordHasher<IdentityUser>>();
            IList<IUserValidator<IdentityUser>> userValidators = new List<IUserValidator<IdentityUser>>
            {
                new UserValidator<IdentityUser>()
            };
            IList<IPasswordValidator<IdentityUser>> passwordValidators = new List<IPasswordValidator<IdentityUser>>
            {
                new PasswordValidator<IdentityUser>()
            };

            var mockUserManager =
                new Mock<UserManager<IdentityUser>>(usrStore.Object, null, paswdh.Object,
                userValidators, passwordValidators, null, null, null, null);
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new UsersController(mockConfiguration.Object, mockUserManager.Object);


            // Act

            var response = controller.RegisterUser(mockUser);

            // Assert

            OkResult okResult = new OkResult();

            Assert.AreEqual(response.Result.GetType(), okResult.GetType());

        }
        //[TestMethod]
        public void RegisterExistingUser()
        {
            // Arrange
            var mockUser = new RegisterUser()
            {
                Login = "usrlgn",
                Password = "teiormer",
                Age = 94
            };

            // user manager constructor elements:
            var usrStore = new Mock<IUserStore<IdentityUser>>();
            var paswdh = new Mock<IPasswordHasher<IdentityUser>>();
            IList<IUserValidator<IdentityUser>> userValidators = new List<IUserValidator<IdentityUser>>
            {
                new UserValidator<IdentityUser>()
            };
            IList<IPasswordValidator<IdentityUser>> passwordValidators = new List<IPasswordValidator<IdentityUser>>
            {
                new PasswordValidator<IdentityUser>()
            };

            var mockUserManager =
                new Mock<UserManager<IdentityUser>>(usrStore.Object, null, paswdh.Object,
                userValidators, passwordValidators, null, null, null, null);
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new UsersController(mockConfiguration.Object, mockUserManager.Object);


            // Act

            var response_1 = controller.RegisterUser(mockUser);
            var response_2 = controller.RegisterUser(mockUser);

            // Assert
            OkResult okResult = new OkResult();
            ConflictResult conflictResult = new ConflictResult();

            Assert.AreEqual(response_1.Result.GetType(), okResult.GetType());
            Assert.AreEqual(response_2.Result.GetType(), conflictResult.GetType());
        }
        

    }
}