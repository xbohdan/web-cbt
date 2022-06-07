using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCbt_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCbt_Backend.Models;
using Moq;
using WebCbt_Backend.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UnitTests.Unit_Tests
{
    [TestClass]
    public class EvaluationTests
    {
        //[TestMethod]
        public void Create_NonExistingEvaluation()
        {
            var evaluationToPost = new Evaluation()
            {
                EvaluationId = 1,
                UserId = 325,
                Question1 = 1,
                Question2 = 1,
                Question3 = 1,
                Question4 = 1,
                Question5 = 1,
            };
            var mockContext = new WebCbtDbContext();
            var evaluationController = new EvaluationController(mockContext);
            var result = evaluationController.PostEvaluation(evaluationToPost);

            var expected = new OkResult();
            Assert.AreEqual(result.Result.GetType(), expected.GetType());
        } 
    }
}
