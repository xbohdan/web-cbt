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
        // Our API: https://130.162.232.178:7198
        private HttpClient _httpClient;
        private static Random random = new Random();
        string registrationPostConnection = "https://130.162.232.178:7198/user";
        string existingLogin = "amolnikita@gmail.com";
        string existingPassword = "sisKa_5";
        public IntegrationRegistrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }

        [TestMethod]
        public async Task Register_NonExistingUser()
        {
            // Registration == POST request

            // generate random username
            string username = "itest_";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string end = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            username += end;
            username += "@test.amol";

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
            
            var response = await _httpClient.PostAsync(registrationPostConnection, httpContent);
            
            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task Register_ExistingUser()
        {
            // Registration == POST request

            var credentials = new Dictionary<string, string>()
            {
                {"Login", existingLogin },
                {"Password", existingPassword },
                {"Sex","Male" },
                {"Age", "19" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(registrationPostConnection, httpContent);

            var codeResult = response.StatusCode;


            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Conflict);
        }
        [TestMethod]
        public async Task Register_EmptyPassword_NonExistingEmail()
        {
            // generate random username
            string username = "itest_";
            string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            string end = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            username += end;
            username += "@test.amol";

            var credentials = new Dictionary<string, string>()
            {
                {"Login", username },
                {"Password", "" },
                {"Sex","Male" },
                {"Age", "19" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(registrationPostConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);

        }

        [TestMethod]
        public async Task Register_EmptyPassword_ExistingEmail()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"Login", existingLogin },
                {"Password", "" },
                {"Sex","Male" },
                {"Age", "19" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(registrationPostConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.Conflict);

        }

        [TestMethod]
        public async Task Register_ExistingPassword_EmptyEmail()
        {
            var credentials = new Dictionary<string, string>()
            {
                {"Login", "" },
                {"Password", existingPassword },
                {"Sex","Male" },
                {"Age", "19" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(registrationPostConnection, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);

        }
    }
}
