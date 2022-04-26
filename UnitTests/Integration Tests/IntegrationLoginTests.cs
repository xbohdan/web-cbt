using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

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
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/login");

            var credentials = new Dictionary<string, string>()
            {
                {"Login", "nikita555" },
                {"Password", "sisKa_5" }
            };

            postRequest.Content = new FormUrlEncodedContent(credentials);

            var response = await _httpClient.SendAsync(postRequest);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task Login_NonExistingUser()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/login");

            var credentials = new Dictionary<string, string>()
            {
                {"Login", "non_existing_user" },
                {"Password", "non_existing_password" }
            };
            postRequest.Content = new FormUrlEncodedContent(credentials);

            var response = await _httpClient.SendAsync(postRequest);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
