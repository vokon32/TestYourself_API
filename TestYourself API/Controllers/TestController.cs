using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Dto;
using TestYourself_API.Helper;
using TestYourself_API.Models;
using TestYourself_API.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace TestYourself_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public TestController(ITestRepository testRepository, IPhotoService photoService, IMapper mapper)
        {
            _testRepository = testRepository;
            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Test>))]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Test> tests = await _testRepository.GetTestsAsync();
            var testsDto = _mapper.Map<IEnumerable<TestDto>>(tests);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(testsDto);
        }

        [HttpGet("{testId}")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Detail(int testId)
        {
            if (!_testRepository.TestExist(testId))
                return NotFound();

            var test = await _testRepository.GetTestAsync(testId);

            var testDto = _mapper.Map<TestDto>(test);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(testDto);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Test))]
        [Route("create")]
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var curUserId = User.GetUserId();
                var userDto = new createTestDto()
                {
                    AppUserId = curUserId
                };
                return Ok(userDto);
            }
            else
                return BadRequest("You shoud be authorized to the system. Access denied");
        }

        [HttpGet("edit/{testId}")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Edit(int testId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var curUserId = User.GetUserId();
                var test = await _testRepository.GetTestAsync(testId);
                var testDto = _mapper.Map<TestDto>(test);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(testDto);
            }
            else
                return BadRequest("You shoud be authorized to the system. Access denied");
        }
        [HttpGet("delete/{testId}")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Delete(int testId)
        {
            var testDetails = await _testRepository.GetTestAsync(testId);
            if (testDetails == null)
                return NotFound("Test is not found");

            var testDto = _mapper.Map<TestDto>(testDetails);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(testDto);
        }
    }
}
