using AutoMapper;
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

        [HttpGet("detail/{testId}")]
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

        [HttpPost("detail/{testId}"), ActionName("Detail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DetailCheck([FromQuery] int testId)
        {
            return RedirectToAction("Index", "Test", testId);
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


        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] createTestDto testDto)
        {
            if (ModelState.IsValid)
            {
                var curUserId = User.GetUserId();
                var result = await _photoService.AddPhotoAsync(testDto.Image);
                var test = new Test
                {
                    Title = testDto.Title,
                    Description = testDto.Description,
                    Image = result.Url.ToString(),
                    TestCategory = testDto.TestCategory,
                    AppUserId = curUserId
                };
                _testRepository.Add(test);
                return Ok(test);
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(testDto);
        }

        [HttpGet("edit/{testId}")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Edit(int testId)
        {
            var test = await _testRepository.GetTestAsync(testId);
            var testDto = _mapper.Map<TestDto>(test);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(testDto);

        }

        [HttpPost("edit/{testId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Edit(int testId, [FromForm] EditTestDto testVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return BadRequest(ModelState);
            }
            var userTest = await _testRepository.GetTestAsNoTrackingAsync(testId);
            if (userTest != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userTest.Image);
                }
                catch
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return BadRequest(ModelState);
                }
                var photoResult = await _photoService.AddPhotoAsync(testVM.Image);
                var curUserId = User.GetUserId();
                var test = new Test
                {
                    Id = testId,
                    Title = testVM.Title,
                    Description = testVM.Description,
                    Image = photoResult.Url.ToString(),
                    TestCategory = testVM.TestCategory,
                    AppUserId = curUserId
                };
                _testRepository.Update(test);

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest(testVM);
            }
        }

        [HttpGet("delete/{testId}")]
        [ProducesResponseType(200, Type = typeof(Test))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Delete(int testId)
        {
            if (!_testRepository.TestExist(testId))
                return NotFound();

            var testDetails = await _testRepository.GetTestAsync(testId);
            var testDto = _mapper.Map<TestDto>(testDetails);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(testDto);
        }
        [HttpPost("delete/{testId}"), ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteTest(int testId)
        {
            if (!_testRepository.TestExist(testId))
                return NotFound();

            var testDetails = await _testRepository.GetTestAsync(testId);

            _testRepository.Delete(testDetails);
            return RedirectToAction("Index");
        }
    }
}
