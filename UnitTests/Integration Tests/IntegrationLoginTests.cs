using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests.Integration_Tests
{
    [TestClass]
    public class IntegrationLoginTests
    {
        // Our API: https://130.162.232.178:7198
        private HttpClient _httpClient;
        private string loginEndpointConnection = "https://130.162.232.178:7198/user/login";
        string existingLogin = "amolnikita@gmail.com";
        string existingPassword = "sisKa_5";
        public IntegrationLoginTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [TestMethod]
        public async Task Login_ExistingUser()
        {
            // Login  == POST request

            var credentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", existingPassword }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(loginEndpointConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task Login_NonExistingUser()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"login", "non_existing_user@test.com" },
                {"password", "non_existing_password" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);

            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(loginEndpointConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Unauthorized);
        }
        [TestMethod]
        public async Task Login_EmptyPassword_ExistingEmail()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", "" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);

            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(loginEndpointConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Unauthorized);
        }
        [TestMethod]
        public async Task Login_EmptyEmail_ExistingPassword()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"login", "" },
                {"password", existingPassword }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);

            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(loginEndpointConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task Login_EmptyBothFields()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"login", "" },
                {"password", "" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);

            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(loginEndpointConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
    }
}
