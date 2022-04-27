using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace UnitTests.Integration_Tests
{
    public class IntegrationRegistrationTests
    {
        private HttpClient _httpClient;
        public IntegrationRegistrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [TestMethod]
        public async Task Register_NonExistingUser()
        {
            // Registration == POST request
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/registration");

            var credentials = new Dictionary<string, string>()
            {
                {"Login", "new_ser" },
                {"Password", "pa//woD2" },
                {"Sex","Male" },
                {"Age", "19" }
            };

            postRequest.Content = new FormUrlEncodedContent(credentials);

            var response = await _httpClient.SendAsync(postRequest);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }
        public async Task Register_ExistingUser()
        {
            // Registration == POST request
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/registration");

            var credentials = new Dictionary<string, string>()
            {
                {"Login", "nikita555" },
                {"Password", "sisKa_5" },
                {"Sex","Male" },
                {"Age", "19" }
            };

            postRequest.Content = new FormUrlEncodedContent(credentials);

            var response = await _httpClient.SendAsync(postRequest);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Conflict);
        }
    }
}
