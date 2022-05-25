using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitTests.Integration_Tests
{
    [TestClass]
    public class IntegrationEvaluationTests
    {
        // Our API: https://130.162.232.178:7198
        private HttpClient _httpClient;
        private string loginEndpointConnection = "https://130.162.232.178:7198/user/login";
    }
