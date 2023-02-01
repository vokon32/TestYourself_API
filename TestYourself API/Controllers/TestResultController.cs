using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Helper;
using TestYourself_API.Models;
using TestYourself_API.Repository;

namespace TestYourself_API.Controllers
{
    public class TestResultController : Controller
    {
        private readonly ITestResultRepository _testResultRepository;

        public TestResultController(ITestResultRepository testResultRepository)
        {
            _testResultRepository = testResultRepository;
        }

        [HttpGet("index")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Index(int Id)
        {
            if (!_testResultRepository.TestExist(Id))
            {
                return NotFound("Result of the test is not found");
            }
            var test = await _testResultRepository.GetByIdAsync(Id);
            return Ok(test);
        }

        [HttpGet("again")]
        [ProducesResponseType(200, Type = typeof(TestResult))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Again(int testId)
        {   
            if (!_testResultRepository.TestExist(testId))
            {
                return NotFound("Result of the test is not found");
            }

            var curUserId = User.GetUserId();
            var testResult = await _testResultRepository.GetByTestIdAndUserIdAsNoTracking(testId, curUserId);
            if (testResult != null)
            {
                _testResultRepository.Delete(testResult);
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "Question", Action = "Index", Id = testId }));
        }

        [HttpGet("Results")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestResult>))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> UserResults()
        {
            var curUserId = User.GetUserId();
            IEnumerable<TestResult> testResults = await _testResultRepository.GetAllTestResultsByUserId(curUserId);
            return Ok(testResults);
        }
    }
}
