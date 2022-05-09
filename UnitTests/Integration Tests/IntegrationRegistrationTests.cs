using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace UnitTests.Integration_Tests
{
    [TestClass]
    public class IntegrationRegistrationTests
    {
        private HttpClient _httpClient;
        private static Random random = new Random();
        public IntegrationRegistrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

       // [TestMethod]
        public async Task Register_NonExistingUser()
        {
            // Registration == POST request

            // generate random username
            string username = "itest_";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string end = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            username += end;
            
            string password = "aBcD45_/";
            var credentials = new Dictionary<string, string>()
            {
                {"Login", username },
                {"Password", password },
                {"Sex","Male" },
                {"Age", "19" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("https://130.162.232.178:7198/user", httpContent);
            
            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }
        //[TestMethod]
        public async Task Register_ExistingUser()
        {
            // Registration == POST request

            var credentials = new Dictionary<string, string>()
            {
                {"Login", "nikita555" },
                {"Password", "sisKa_5" },
                {"Sex","Male" },
                {"Age", "19" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://130.162.232.178:7198/user", httpContent);

            var codeResult = response.StatusCode;


            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Conflict);
        }
    }
}
