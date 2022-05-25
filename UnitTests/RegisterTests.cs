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
using System;
using System.Linq;

namespace UnitTests
{
    //[TestClass]
    public class RegisterTests
    {
        private static Random random = new Random(55);
        //[TestMethod]
        public void DefaultRegistration()
        {
            // Arrange
            string username = "utest_";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string end = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            string password = "aBcD45_/";
            username += end;

            var mockUser = new RegisterUser()
            {
                Login = username,
                Password = password,
                Gender = "other",
                Age = 20
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
        //[TestMethod]
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
            var actual = controller.RegisterUser(mockUser);

            // Assert
            var expected1 = new OkResult();
            var expected2 = new ConflictResult();

            Assert.AreEqual(expected1.GetType(), actual.Result.GetType());
            Assert.AreEqual(expected2.GetType(), actual.Result.GetType());
        }
        

    }
}