using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests.Integration_Tests
{
    [TestClass]
    public class IntegrationManagingAccountTests
    {
        HttpClient _httpClient;
        private string manageAccountPutConnection = "";
        public IntegrationManagingAccountTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }
    }
}
