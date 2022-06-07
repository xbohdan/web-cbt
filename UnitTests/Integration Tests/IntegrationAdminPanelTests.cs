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
    public class IntegrationAdminPanelTests
    {
        // Our API: https://130.162.232.178:7198/
        // Team CBT API: https://web-cbt.herokuapp.com/
        // Team Typeracers: https://school-se-back.monicz.pl/

        HttpClient _httpClient;
        HttpClient _loginHttpClient;
        HttpClient _getAllHttpClient;

        private string currentApi = "https://130.162.232.178:7198/";

        private string loginPostConnection;
        private string manageAccountPutConnection; 
        private string getAllUsersConnection;

        private string adminLogin = "admin1@gmail.com";
        private string adminPassword = "Password1!";


        public IntegrationAdminPanelTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
            _loginHttpClient = webAppFactory.CreateClient();
            _getAllHttpClient = webAppFactory.CreateClient();
            loginPostConnection = $"{currentApi}user/login";
            manageAccountPutConnection = $"{currentApi}user/"; // + {userId} 
            getAllUsersConnection = $"{currentApi}user";
            if(currentApi == "https://school-se-back.monicz.pl/")
            {
                adminLogin = "admin@admin.com";
                adminPassword = "adminadmin";
            }
        }
        [TestMethod]
        public async Task AdminPanel_GetAllUsersInfo()
        {
            // get bearer token
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", adminLogin },
                {"password", adminPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;

            _getAllHttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            var allUsersResponse = await _getAllHttpClient.GetAsync(getAllUsersConnection);
            

            Assert.IsTrue(allUsersResponse.IsSuccessStatusCode);
        }
        [TestMethod]
        public async Task AdminPanel_TryGetAllUsersWithoutPermission()
        {
            var allUsersResponse = await _getAllHttpClient.GetAsync(getAllUsersConnection);

            Assert.IsTrue(!allUsersResponse.IsSuccessStatusCode);
        }
        [TestMethod]
        public async Task AdminPanel_EditCertainUser()
        {
            // get bearer token
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", adminLogin },
                {"password", adminPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");

            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            var putResponseBody = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", loginHttpContent);
            Assert.IsTrue(putResponseBody.IsSuccessStatusCode);
        }
        [TestMethod]
        public async Task AdminPanel_EditCertainUserWithoutPermission()
        {
            // get bearer token
            var loginCredentials = new Dictionary<string, string>()
            {
                {"login", adminLogin },
                {"password", adminPassword }
            };

            var loginJsonCredentials = JsonConvert.SerializeObject(loginCredentials);
            var loginHttpContent = new StringContent(loginJsonCredentials, System.Text.Encoding.UTF8, "application/json");
            var loginResponse = await _loginHttpClient.PostAsync(loginPostConnection, loginHttpContent);
            loginResponse.EnsureSuccessStatusCode();

            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic loginData = JObject.Parse(responseBody);
            string userId = loginData.userId;

            var putResponseBody = await _httpClient.PutAsync(manageAccountPutConnection + $"{userId}", loginHttpContent);
            Assert.IsTrue(!putResponseBody.IsSuccessStatusCode);
        }
    }
}
