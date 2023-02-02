using AutoMapper;
using CloudinaryDotNet.Actions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestYourself_API.Controllers;
using TestYourself_API.Dto;
using TestYourself_API.Helper;
using TestYourself_API.Models;
using TestYourself_API.Repository;

namespace TestYourself_API.Tests.Controller
{

    public class TestControllerTests
    {
        private readonly ITestRepository _testRepository;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly TestController _testController;

        public TestControllerTests()
        {
            _testRepository = A.Fake<ITestRepository>();
            _photoService = A.Fake<IPhotoService>();
            _mapper = A.Fake<IMapper>();

            _testController = new TestController(_testRepository, _photoService, _mapper);
        }

        [Fact]
        public async void TestController_Index_ReturnsOK()
        {
            var tests = A.Fake<IEnumerable<Test>>();
            A.CallTo(() => _mapper.Map<IEnumerable<TestDto>>(tests));

            var result = await _testController.Index();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void TestController_CreateTest_ReturnsOK()
        {
            var testDto = A.Fake<createTestDto>();
            var userId = Guid.NewGuid().ToString();
            var imageUploadResult = A.Fake<ImageUploadResult>();
            imageUploadResult.Url = new Uri("https://"+ Guid.NewGuid().ToString());

            A.CallTo(() => _photoService.AddPhotoAsync(testDto.Image)).Returns(imageUploadResult);
            
            var result = await _testController.Create(userId, testDto);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void TestController_EditGet_ReturnsOK()
        {
            var testId = 1;
            var test = A.Fake<Test>();

            A.CallTo(() => _testRepository.GetTestAsync(testId)).Returns(test);
            A.CallTo(() => _mapper.Map<TestDto>(test));

            var result = await _testController.Edit(testId);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void TestController_EditPost_ReturnsOK()
        {
            var testId = 1;
            var testVM = A.Fake<EditTestDto>();
            var userId = Guid.NewGuid().ToString();
            var userTest = A.Fake<Test>();
            var test = A.Fake<Test>();
            var imageUploadResult = A.Fake<ImageUploadResult>();
            imageUploadResult.Url = new Uri("https://" + Guid.NewGuid().ToString());

            A.CallTo(() => _testRepository.GetTestAsNoTrackingAsync(testId)).Returns(userTest);
            A.CallTo(() => _photoService.DeletePhotoAsync(userTest.Image));
            A.CallTo(() => _photoService.AddPhotoAsync(testVM.Image)).Returns(imageUploadResult);
            A.CallTo(() => _testRepository.Update(test));

            var result = await _testController.Edit(testId, userId, testVM);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
