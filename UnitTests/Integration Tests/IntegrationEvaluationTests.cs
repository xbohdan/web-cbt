using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace UnitTests.Integration_Tests
{
    [TestClass]
    public class IntegrationEvaluationTests
    {
        // Our API: https://130.162.232.178:7198/
        // Team CBT API: https://web-cbt.herokuapp.com/
        // Team Typeracers: https://school-se-back.monicz.pl/

        private HttpClient _httpClient;
        private HttpClient _loginHttpClient;
        private string evaluationPostEndpoint = "http://130.162.232.178:7198/moodtests";
        private string loginPostConnection = "http://130.162.232.178:7198/user/login";
        string[] categories = { "Depression", "Anxiety", "Addictions", "Anger", "Relationships", "Happiness" };
        private static Random random = new Random(DateTime.Now.Millisecond);
        string existingLogin = "amolnikita@gmail.com";
        string existingPassword = "sisKa_5_";

        private int[] existingAnswers = new int[5];
        private string existingCategory;
        public IntegrationEvaluationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
            _loginHttpClient = webAppFactory.CreateClient();
            existingCategory = categories[random.Next(0, categories.Length - 1)];
            for (int i = 0; i < existingAnswers.Length; i++)
            {
                existingAnswers[i] = random.Next(1, existingAnswers.Length);
            }
        }

        // This test method is runned for configuring settings, its results are not real
        //  [TestMethod]

        [TestMethod]
        public async Task Evaluation_CreateNonExistingEvaluation()
        {
            // get bearer token
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
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;




            var credentials = new Dictionary<string, string>()
            {
                { "userId", $"{userId}" },
                { "category", existingCategory },
                { "question1", $"{existingAnswers[0]}" },
                { "question2", $"{existingAnswers[1]}" },
                { "question3", $"{existingAnswers[2]}" },
                { "question4", $"{existingAnswers[3]}" },
                { "question5", $"{existingAnswers[4]}" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await _httpClient.PostAsync(evaluationPostEndpoint, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Evaluation_CreateEvaluation_Existing()
        {            
            // get bearer token
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
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;
            var credentials = new Dictionary<string, string>()
            {
                { "userId", $"{userId}" },
                { "category", existingCategory },
                { "question1", $"{existingAnswers[0]}" },
                { "question2", $"{existingAnswers[1]}" },
                { "question3", $"{existingAnswers[2]}" },
                { "question4", $"{existingAnswers[3]}" },
                { "question5", $"{existingAnswers[4]}" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            
            var response = await _httpClient.PostAsync(evaluationPostEndpoint, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Evaluation_CreateEvaluation_AnswerLessThanOne()
        {
            // get bearer token
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
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;

            var credentials = new Dictionary<string, string>()
            {
                { "userId", $"{userId}" },
                { "category", "Anger" },
                { "question1", "-228" },
                { "question2", "0" },
                { "question3", "3" },
                { "question4", "5" },
                { "question5", $"{existingAnswers[4]}" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            var response = await _httpClient.PostAsync(evaluationPostEndpoint, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Evaluation_CreateEvaluation_AnswerNonIntegerType()
        {
            // get bearer token
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
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;

            var credentials = new Dictionary<string, string>()
            {
                { "userId", $"{userId}" },
                { "category", "Anger" },
                { "question1", "2.5" },
                { "question2", "4.3" },
                { "question3", "3.1" },
                { "question4", "5" },
                { "question5", "1" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            var response = await _httpClient.PostAsync(evaluationPostEndpoint, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
        
        [TestMethod]
        public async Task Evaluation_CreateEvaluation_AnswerGreaterThanFive()
        {
            // get bearer token
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
            string bearerToken = loginData.accessToken;
            string userId = loginData.userId;

            var credentials = new Dictionary<string, string>()
            {
                { "userId", $"{userId}" },
                { "category", "Anger" },
                { "question1", "555" },
                { "question2", "13" },
                { "question3", "77" },
                { "question4", "5" },
                { "question5", "1" }
            };
            var JsonCredentials = JsonConvert.SerializeObject(credentials);
            var httpContent = new StringContent(JsonCredentials, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization =
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            var response = await _httpClient.PostAsync(evaluationPostEndpoint, httpContent);

            var codeResult = response.StatusCode;

            Assert.AreEqual(codeResult, System.Net.HttpStatusCode.BadRequest);
        }
    }
}