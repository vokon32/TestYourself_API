using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Dto;
using TestYourself_API.Helper;
using TestYourself_API.Models;
using TestYourself_API.Repository;

namespace TestYourself_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestResultRepository _testResultRepository;
        private readonly ITestRepository _testRepository;
        private const int MinimalAmountOfQuestions = 5;

        public QuestionController(IQuestionRepository questionRepository, ITestResultRepository testResultRepository,
            ITestRepository testRepository)
        {
            _questionRepository = questionRepository;
            _testResultRepository = testResultRepository;
            _testRepository = testRepository;
        }

        [HttpGet("index")]
        [ProducesResponseType(200, Type = typeof(Question))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Index(int testId)
        {
            var curUserId = User.GetUserId();
            var testResult = await _testResultRepository.GetByTestIdAndUserIdAsNoTracking(testId, curUserId);
            if (testResult != null)
                return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "TestResult", Action = "Index", testResult.Id }));

            var question = await _questionRepository.GetFirstQuestion(testId);
            var questionVM = new QuestionAnswerDto()
            {
                Id = question.Id,
                Contain = question.Contain,
                FirstAnswer = question.FirstAnswer,
                SecondAnswer = question.SecondAnswer,
                CorrectAnswer = question.CorrectAnswer,
                testId = testId,
                CurrentIndex = 0
            };

            return Ok(questionVM);
        }

        [HttpPost("index")]
        [ProducesResponseType(200, Type = typeof(Question))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Index([FromForm] QuestionAnswerDto questionVM)
        {
            questionVM.Questions = await _questionRepository.GetByTestId(questionVM.testId);

            var check = _questionRepository.GetByIdAsyncNoTracking(questionVM.Id).Result.CorrectAnswer;
            if (questionVM.ChosenAnswer == check)
            {
                questionVM.ResultScore++;
            }

            if (questionVM.CurrentIndex == questionVM.Questions.Count() - 1)
            {
                    var curUserId = User.GetUserId();
                    var testResult = new TestResult()
                    {
                        testId = questionVM.testId,
                        AppUserId = curUserId.ToString(),
                        FinalScore = 100 / questionVM.Questions.Count() * questionVM.ResultScore,
                        isPassed = true
                    };
                    _testResultRepository.Add(testResult);
                    return RedirectToAction("Index", new RouteValueDictionary(new { Controller = "TestResult", Action = "Index", Id = testResult.Id }));
            }
            questionVM.CurrentIndex++;
            var question = questionVM.Questions[questionVM.CurrentIndex];
            var nextQuestionVM = new QuestionAnswerDto()
            {
                Id = question.Id,
                Contain = question.Contain,
                FirstAnswer = question.FirstAnswer,
                SecondAnswer = question.SecondAnswer,
                CorrectAnswer = question.CorrectAnswer,
                testId = questionVM.testId,
                ResultScore = questionVM.ResultScore,
                CurrentIndex = questionVM.CurrentIndex
            };

            if (questionVM.CurrentIndex == questionVM.Questions.Count() - 1)
            {
                nextQuestionVM.isCorrect = true;
                var test = await _testRepository.GetTestAsNoTrackingAsync(questionVM.testId);
                test.isPassed = true;
                _testRepository.Update(test);
            }
            return Ok(nextQuestionVM);
        }

        [HttpGet("create")]
        [ProducesResponseType(200, Type = typeof(Question))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult Create(int id)
        {
            var createQuestionVM = new CreateQuestionDto()
            {
                testId = id,
                CurrentAmountOfQuestions = 0
            };
            return Ok(createQuestionVM);
        }
       
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Create([FromForm]CreateQuestionDto createQuestionVM)
        {

            if (ModelState.IsValid)
            {
                var question = new Question()
                {
                    FirstAnswer = createQuestionVM.FirstAnswer,
                    SecondAnswer = createQuestionVM.SecondAnswer,
                    CorrectAnswer = createQuestionVM.ChosenAnswer,
                    Contain = createQuestionVM.Contain,
                    testId = createQuestionVM.testId
                };
                createQuestionVM.CurrentAmountOfQuestions++;
                _questionRepository.Add(question);

                if (createQuestionVM.CurrentAmountOfQuestions >= MinimalAmountOfQuestions)
                {
                    var test = await _testRepository.GetTestAsNoTrackingAsync(createQuestionVM.testId);
                    test.questionsAmount = createQuestionVM.CurrentAmountOfQuestions;
                    _testRepository.Update(test);
                    createQuestionVM.isFull = true;

                }

                var nextCreateQuestionVM = new CreateQuestionDto()
                {
                    testId = createQuestionVM.testId,
                    CurrentAmountOfQuestions = createQuestionVM.CurrentAmountOfQuestions,
                    isFull = createQuestionVM.isFull
                };
                return Ok(nextCreateQuestionVM);
            }
            return Ok(createQuestionVM);
        }

        [HttpGet("finish")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult Finish(int id)
        {
            var finish = new FinishQuestionDto()
            {
                testId = id
            };
            return Ok(finish);
        }

        [HttpPost("finish")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Finish([FromForm] FinishQuestionDto finishQuestionVM)
        {
            if (ModelState.IsValid)
            {
                if (finishQuestionVM.ChosenAnswer == "Yes")
                {
                    var test = await _testRepository.GetTestAsNoTrackingAsync(finishQuestionVM.testId);
                    test.CanBePassedAgain = true;
                    _testRepository.Update(test);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return Ok(finishQuestionVM);
        }
    }
}
