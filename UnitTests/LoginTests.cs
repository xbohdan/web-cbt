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

namespace UnitTests
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Login_ExistingUser()
        {
            // Arrange

            var mockExistingUser = new LoginUser()
            {
                Login = "usrlgn",
                Password = "teiormer"
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
                userValidators, passwordValidators, null, null, null, null );
            var mockConfiguration = new Mock<IConfiguration>();
            var controller = new UsersController(mockConfiguration.Object, mockUserManager.Object);

            // Act

            var response = controller.LoginUser(mockExistingUser);

            // Assert
            UnauthorizedResult expected = new UnauthorizedResult();
            Assert.AreEqual(response.Result.GetType(), expected.GetType());

        }

        [TestMethod]
        public void Login_NonExistingUser()
        {
            // Arrange

            var mockNonExistingUser = new LoginUser()
            {
                Login = "usrlgn",
                Password = "teiormer",
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

            var response = controller.LoginUser(mockNonExistingUser);

            // Assert
            UnauthorizedResult expected = new UnauthorizedResult();
            Assert.AreEqual(response.Result.GetType(), expected.GetType());

        }
    }
}
