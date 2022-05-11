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
        private HttpClient _httpClient;
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
                {"login", "nikita555" },
                {"password", "sisKa_5" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://130.162.232.178:7198/user/login", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task Login_NonExistingUser()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"login", "non_existing_user" },
                {"password", "non_existing_password" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);

            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://130.162.232.178:7198/user/login", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
