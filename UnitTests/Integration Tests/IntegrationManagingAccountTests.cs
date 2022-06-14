using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UnitTests.Integration_Tests
{
    [TestClass]
    public class IntegrationManagingAccountTests
    {
        // Our API: https://130.162.232.178:7198/
        // Team CBT API: https://web-cbt.herokuapp.com/
        // Team Typeracers: https://school-se-back.monicz.pl/

        HttpClient _httpClient;
        HttpClient _loginHttpClient;
        HttpClient _getHttpClient;

        private string manageAccountPutConnection = "http://130.162.232.178:7198/user/"; // + {userId} 
        private string loginPostConnection = "https://130.162.232.178:7198/user/login";
        private string getUserConnection = "https://130.162.232.178:7198/user/";

        string existingLogin = "amolnikita@gmail.com";
        string existingPassword = "sisKa_5_";


        public IntegrationManagingAccountTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
            _loginHttpClient = webAppFactory.CreateClient();
            _getHttpClient = webAppFactory.CreateClient();
        }

        [TestMethod]
        public async Task Manage_ChangeEmailOnly_LeaveOtherFieldsEmpty()
        {
            // get userId
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", existingPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody); 
            string userId = loginData.userId;
            string bearerToken = loginData.accessToken;

            // PUT: /user/5
            var credentials = new Dictionary<string, string>()
            {
                {"login", existingLogin }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);


            var response = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);

        }

        [TestMethod]
        public async Task Manage_ChangePasswordOnly_LeaveOtherFieldsFieldsEmpty()
        {
            // get userId
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", existingPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string userId = loginData.userId;
            string bearerToken = loginData.accessToken;

            // PUT: /user/5
            var credentials = new Dictionary<string, string>()
            {
                { "password", existingPassword }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);


            var response = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task Manage_LeaveAllFieldsEmpty()
        {
            // get userId
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", existingPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string userId = loginData.userId;
            string bearerToken = loginData.accessToken;

            // PUT: /user/5
            var credentials = new Dictionary<string, string>()
            {
                // empty
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);


            var response = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task Manage_EnterAgeNotNumeric()
        {
            // get userId
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", existingPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string userId = loginData.userId;
            string bearerToken = loginData.accessToken;

            // PUT: /user/5
            var credentials = new Dictionary<string, string>()
            {
                { "Age", "stringvalue" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);


            var response = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
        [TestMethod]
        public async Task Manage_EnterIncorrectEmail()
        {
            // get userId
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", existingLogin },
                {"password", existingPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string userId = loginData.userId;
            string bearerToken = loginData.accessToken;

            // PUT: /user/5
            var credentials = new Dictionary<string, string>()
            {
                { "login", "incorrectemailvalue" }
            };

            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);


            var response = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
    }
}
