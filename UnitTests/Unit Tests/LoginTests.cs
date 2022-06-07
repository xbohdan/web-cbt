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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UnitTests
{
    [TestClass]
    public class LoginTests
    {
        string existingLogin = "amolnikita@gmail.com";
        string existingPassword = "sisKa_5";


        // TODO: Make them work
        //[TestMethod]
        public void Login_ExistingUser()
        {
            // Arrange
            var mockExistingLoginUser = new LoginUser
            {
                Login = existingLogin,
                Password = existingPassword
            };
            var identityUser = new IdentityUser();
            var userStore = new Mock<UserStore<IdentityUser>>();
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
            var mockContext = new WebCbtDbContext();
            var mockUserManager = new Mock<UserManager<IdentityUser>>(userStore.Object, null, passwordHasher.Object,
                userValidators, passwordValidators, null, null, null, null);
            mockUserManager.Setup(x => x.FindByEmailAsync(mockExistingLoginUser.Login)).Returns(Task.FromResult(new IdentityUser()));
            //mockUserManager.Setup(x => x.CheckPasswordAsync(identityUser, mockExistingLoginUser.Password).GetAwaiter().GetResult()).Returns(Task.FromResult(true));
            var controller = new UserController(mockConfiguration.Object, mockContext, mockUserManager.Object);

            // Act
            var response = controller.LoginUser(mockExistingLoginUser);

            // Assert
            var expected = new OkResult();
            Assert.AreEqual(response.Result.GetType(), expected.GetType());

        }

        // TODO: Make them work
        //[TestMethod]
        public void Login_NonExistingUser()
        {
            // Arrange
            var mockExistingUser = new LoginUser
            {
                Login = "nonexisting_user@non_existing@mail.com",
                Password = "nonexisting_password"
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
            var actual = controller.LoginUser(mockExistingUser);

            // Assert
            var expected = new UnauthorizedResult();
            Assert.AreEqual(expected.GetType(), actual.Result.GetType());

        }
    }
}
